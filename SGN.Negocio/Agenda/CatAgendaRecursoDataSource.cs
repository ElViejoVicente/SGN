using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SGN.Negocio.Agenda
{
    public class CatAgendaRecursoDataSource
    {
        private string Cnn => ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        // ObjectDataSource -> SelectMethodHandler()
        public IEnumerable<CatAgendaRecurso> SelectMethodHandler()
        {
            var list = new List<CatAgendaRecurso>();

            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_Cat_AgendaRecurso_List", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new CatAgendaRecurso
                        {
                            IdRecurso = rd["IdRecurso"] == DBNull.Value ? 0 : Convert.ToInt32(rd["IdRecurso"]),
                            Nombre = rd["Nombre"] as string,
                            Activo = rd["Activo"] != DBNull.Value && Convert.ToBoolean(rd["Activo"]),
                            Orden = rd["Orden"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Orden"])
                        });
                    }
                }
            }

            return list;
        }
    }
}
