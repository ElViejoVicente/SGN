using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SGN.Web.Agenda
{
    public class AgendaCitaDataSource
    {
        private string Cnn => ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        // ===== SELECT (ObjectDataSource) =====
        public IEnumerable SelectMethodHandler()
        {
            // Rango “seguro” si no podemos leer visible range del scheduler
            DateTime desde = DateTime.Today.AddDays(-60);
            DateTime hasta = DateTime.Today.AddDays(60);

            // Si quieres, aquí puedes afinar: leer parámetros del callback del scheduler
            // pero con ventana amplia ya se pinta todo mientras validamos.
            // Luego lo afinamos a rango visible cuando ya esté estable.

            return SelectRange(desde, hasta);
        }

        private IList SelectRange(DateTime desde, DateTime hasta)
        {
            var list = new System.Collections.Generic.List<AgendaCita>();

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
                        var c = new AgendaCita
                        {
                            IdCita = rd.GetInt32(rd.GetOrdinal("IdCita")),
                            FechaInicio = rd.GetDateTime(rd.GetOrdinal("FechaInicio")),
                            FechaFin = rd.GetDateTime(rd.GetOrdinal("FechaFin")),
                            TodoDia = rd.GetBoolean(rd.GetOrdinal("TodoDia")),
                            Asunto = rd["Asunto"] as string,
                            Descripcion = rd["Descripcion"] as string,
                            Ubicacion = rd["Ubicacion"] as string,
                            Etiqueta = rd["Etiqueta"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Etiqueta"]),
                            Estatus = rd["Estatus"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Estatus"]),
                            Tipo = rd["Tipo"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Tipo"]),
                            RecurrenceInfo = rd["RecurrenceInfo"] as string,
                            ReminderInfo = rd["ReminderInfo"] as string
                        };
                        list.Add(c);
                    }
                }
            }

            return list;
        }

        // ===== INSERT =====
        public int InsertMethodHandler(AgendaCita cita)
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
                cmd.Parameters.AddWithValue("@Etiqueta", (object)cita.Etiqueta ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estatus", (object)cita.Estatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", cita.Tipo);
                cmd.Parameters.AddWithValue("@RecurrenceInfo", (object)cita.RecurrenceInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReminderInfo", (object)cita.ReminderInfo ?? DBNull.Value);

                var pOut = new SqlParameter("@IdCita", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pOut);

                cn.Open();
                cmd.ExecuteNonQuery();

                // ObjectDataSource espera que el método regrese el ID para AutoRetrieveId=true
                return Convert.ToInt32(pOut.Value);
            }
        }

        // ===== UPDATE =====
        public void UpdateMethodHandler(AgendaCita cita)
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
                cmd.Parameters.AddWithValue("@Etiqueta", (object)cita.Etiqueta ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estatus", (object)cita.Estatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", cita.Tipo);
                cmd.Parameters.AddWithValue("@RecurrenceInfo", (object)cita.RecurrenceInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReminderInfo", (object)cita.ReminderInfo ?? DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===== DELETE =====
        public void DeleteMethodHandler(AgendaCita cita)
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
