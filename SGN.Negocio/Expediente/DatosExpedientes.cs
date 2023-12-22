using Dapper;
using SGN.Negocio.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Expediente
{
    public class DatosExpedientes
    {
        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        public List<ListaExpedientes> DameListaExpediente(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                List<ListaExpedientes> resultado = new List<ListaExpedientes>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<ListaExpedientes>
                        (
                        sql: "sp_DameExpedientePorFecha", param: new
                        {
                            fechaInicial,
                            fechaFinal

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameExpedientePorFecha , detalle: \n" + ex.Message, ex);
            }
        }


    }
}
