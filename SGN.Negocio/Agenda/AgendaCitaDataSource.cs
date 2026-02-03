using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SGN.Negocio.Agenda
{
    public class AgendaCitaDataSource
    {
        private string Cnn => ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        // ===== SELECT (ObjectDataSource) =====

        public IEnumerable SelectMethodHandler()
        {
            // ✅ Rango visible real (lo setea Agenda.aspx.cs antes de DataBind)
            DateTime desde, hasta;

            //var ctx = HttpContext.Current;
            //if (ctx != null && ctx.Items["SGN_AGENDA_DESDE"] is DateTime d && ctx.Items["SGN_AGENDA_HASTA"] is DateTime h)
            //{
            //    desde = d;
            //    hasta = h;
            //}
            //else
            //{
                // fallback (por si alguien llama el datasource fuera del scheduler)
                desde = DateTime.Today.AddDays(-60);
                hasta = DateTime.Today.AddDays(60);
            //}

            return SelectRange(desde, hasta);
        }

        private IList SelectRange(DateTime desde, DateTime hasta)
        {
            var list = new List<AgendaCitas>();

            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_AgendaCitas_SelectRange", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaInicio", desde);
                cmd.Parameters.AddWithValue("@FechaFin", hasta);

                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var c = new AgendaCitas
                        {
                            IdCita = Convert.ToInt32(rd["IdCita"]),
                            FechaInicio = Convert.ToDateTime(rd["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(rd["FechaFin"]),
                            TodoDia = Convert.ToBoolean(rd["TodoDia"]),
                            Asunto = rd["Asunto"] as string,
                            Descripcion = rd["Descripcion"] as string,
                            Ubicacion = rd["Ubicacion"] as string,

                            Etiqueta = rd["Etiqueta"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Etiqueta"]),
                            Estatus = rd["Estatus"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Estatus"]),
                            Tipo = rd["Tipo"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Tipo"]),

                            RecurrenceInfo = rd["RecurrenceInfo"] as string,
                            ReminderInfo = rd["ReminderInfo"] as string,

                            IdRecurso = rd["IdRecurso"] == DBNull.Value ? 0 : Convert.ToInt32(rd["IdRecurso"])
                        };

                        list.Add(c);
                    }
                }
            }

            return list;
        }

        private int SafeGetOrdinal(SqlDataReader rd, string col)
        {
            try { return rd.GetOrdinal(col); }
            catch { return -1; }
        }

        // ===== INSERT =====
        public int InsertMethodHandler(AgendaCitas cita)
        {
            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_CRUD_AgendaCitas_Insert", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FechaInicio", cita.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", cita.FechaFin);
                cmd.Parameters.AddWithValue("@TodoDia", cita.TodoDia);

                cmd.Parameters.AddWithValue("@Asunto", (cita.Asunto ?? "").Trim());
                cmd.Parameters.AddWithValue("@Descripcion", (object)cita.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ubicacion", (object)cita.Ubicacion ?? DBNull.Value);

                // ✅ FIX: 0 => NULL (si no seleccionó nada)
                cmd.Parameters.AddWithValue("@Etiqueta", cita.Etiqueta > 0 ? (object)cita.Etiqueta : DBNull.Value);
                cmd.Parameters.AddWithValue("@Estatus", cita.Estatus > 0 ? (object)cita.Estatus : DBNull.Value);

                cmd.Parameters.AddWithValue("@Tipo", cita.Tipo);

                cmd.Parameters.AddWithValue("@RecurrenceInfo", (object)cita.RecurrenceInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReminderInfo", (object)cita.ReminderInfo ?? DBNull.Value);

                // ✅ NUEVO: recurso
                cmd.Parameters.AddWithValue("@IdRecurso", cita.IdRecurso > 0 ? (object)cita.IdRecurso : DBNull.Value);

                // (Opcionales dominio si los manejas en SP)
                // cmd.Parameters.AddWithValue("@IdExpediente", (object)cita.IdExpediente ?? DBNull.Value);
                // cmd.Parameters.AddWithValue("@IdTipoCita", cita.IdTipoCita > 0 ? (object)cita.IdTipoCita : DBNull.Value);

                var pOut = new SqlParameter("@IdCita", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pOut);

                cn.Open();
                cmd.ExecuteNonQuery();

                int newId = Convert.ToInt32(pOut.Value);

                // ✅ recomendado (como demo): asignar el id al objeto
                cita.IdCita = newId;

                return newId;
            }
        }

        // ===== UPDATE =====
        public void UpdateMethodHandler(AgendaCitas cita)
        {
            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_CRUD_AgendaCitas_Update", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCita", cita.IdCita);

                cmd.Parameters.AddWithValue("@FechaInicio", cita.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", cita.FechaFin);
                cmd.Parameters.AddWithValue("@TodoDia", cita.TodoDia);

                cmd.Parameters.AddWithValue("@Asunto", (cita.Asunto ?? "").Trim());
                cmd.Parameters.AddWithValue("@Descripcion", (object)cita.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ubicacion", (object)cita.Ubicacion ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@Etiqueta", cita.Etiqueta > 0 ? (object)cita.Etiqueta : DBNull.Value);
                cmd.Parameters.AddWithValue("@Estatus", cita.Estatus > 0 ? (object)cita.Estatus : DBNull.Value);

                cmd.Parameters.AddWithValue("@Tipo", cita.Tipo);

                cmd.Parameters.AddWithValue("@RecurrenceInfo", (object)cita.RecurrenceInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReminderInfo", (object)cita.ReminderInfo ?? DBNull.Value);

                // ✅ NUEVO: recurso
                cmd.Parameters.AddWithValue("@IdRecurso", cita.IdRecurso > 0 ? (object)cita.IdRecurso : DBNull.Value);

                // (Opcionales dominio)
                // cmd.Parameters.AddWithValue("@IdExpediente", (object)cita.IdExpediente ?? DBNull.Value);
                // cmd.Parameters.AddWithValue("@IdTipoCita", cita.IdTipoCita > 0 ? (object)cita.IdTipoCita : DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===== DELETE =====
        public void DeleteMethodHandler(AgendaCitas cita)
        {
            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_CRUD_AgendaCitas_Delete", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCita", cita.IdCita);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
