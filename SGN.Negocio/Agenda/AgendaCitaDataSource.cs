using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SGN.Negocio.Agenda
{
    public class AgendaCitaDataSource
    {
        private string Cnn => ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        // ===== SELECT (ObjectDataSource) =====
        public IEnumerable SelectMethodHandler()
        {
            // Ventana amplia (estable) mientras trabajas.
            // Si luego quieres “rango visible real” lo afinamos.
            DateTime desde = DateTime.Today.AddDays(-60);
            DateTime hasta = DateTime.Today.AddDays(60);

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
                    int ordIdCita = rd.GetOrdinal("IdCita");
                    int ordIni = rd.GetOrdinal("FechaInicio");
                    int ordFin = rd.GetOrdinal("FechaFin");
                    int ordTodoDia = rd.GetOrdinal("TodoDia");
                    int ordAsunto = rd.GetOrdinal("Asunto");
                    int ordDesc = rd.GetOrdinal("Descripcion");
                    int ordUbic = rd.GetOrdinal("Ubicacion");
                    int ordEtiqueta = SafeGetOrdinal(rd, "Etiqueta");
                    int ordEstatus = SafeGetOrdinal(rd, "Estatus");
                    int ordTipo = SafeGetOrdinal(rd, "Tipo");
                    int ordRec = SafeGetOrdinal(rd, "RecurrenceInfo");
                    int ordRem = SafeGetOrdinal(rd, "ReminderInfo");
                    int ordRecurso = SafeGetOrdinal(rd, "IdRecurso");

                    while (rd.Read())
                    {
                        var c = new AgendaCitas
                        {
                            IdCita = rd.GetInt32(ordIdCita),
                            FechaInicio = rd.GetDateTime(ordIni),
                            FechaFin = rd.GetDateTime(ordFin),
                            TodoDia = rd.GetBoolean(ordTodoDia),

                            Asunto = rd.IsDBNull(ordAsunto) ? null : rd.GetString(ordAsunto),
                            Descripcion = (ordDesc >= 0 && !rd.IsDBNull(ordDesc)) ? rd.GetString(ordDesc) : null,
                            Ubicacion = (ordUbic >= 0 && !rd.IsDBNull(ordUbic)) ? rd.GetString(ordUbic) : null,

                            // ✅ FIX: si viene NULL => 0 (DevExpress feliz)
                            Etiqueta = (ordEtiqueta >= 0 && !rd.IsDBNull(ordEtiqueta)) ? Convert.ToInt32(rd.GetValue(ordEtiqueta)) : 0,
                            Estatus = (ordEstatus >= 0 && !rd.IsDBNull(ordEstatus)) ? Convert.ToInt32(rd.GetValue(ordEstatus)) : 0,

                            Tipo = (ordTipo >= 0 && !rd.IsDBNull(ordTipo)) ? Convert.ToInt32(rd.GetValue(ordTipo)) : 0,

                            RecurrenceInfo = (ordRec >= 0 && !rd.IsDBNull(ordRec)) ? rd.GetString(ordRec) : null,
                            ReminderInfo = (ordRem >= 0 && !rd.IsDBNull(ordRem)) ? rd.GetString(ordRem) : null,

                            // ✅ NUEVO: recurso/sala
                            IdRecurso = (ordRecurso >= 0 && !rd.IsDBNull(ordRecurso)) ? Convert.ToInt32(rd.GetValue(ordRecurso)) : 0
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
