using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SGN.Negocio.Agenda
{
    public class CatAgendaEtiquetaDataSource
    {
        private string Cnn => ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        // ObjectDataSource -> SelectMethodHandler()
        public IEnumerable<CatAgendaEtiqueta> SelectMethodHandler()
        {
            var list = new List<CatAgendaEtiqueta>();

            using (var cn = new SqlConnection(Cnn))
            using (var cmd = new SqlCommand("sp_Cat_AgendaEtiqueta_List", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new CatAgendaEtiqueta
                        {
                            IdEtiqueta = rd["IdEtiqueta"] == DBNull.Value ? 0 : Convert.ToInt32(rd["IdEtiqueta"]),
                            Nombre = rd["Nombre"] as string,
                            ColorArgb = rd["ColorArgb"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["ColorArgb"]),
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
