using Dapper;
using Microsoft.Exchange.WebServices.Data;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ExpedienteUnico
{
    public class DatosExpedienteUnico
    {
        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];
        DatosCrud datosCrud = new DatosCrud();

        public List<ListaExpedienteUnico> DameListaExpedienteUnico(DateTime fechaInicial, DateTime fechaFinal, Boolean todasLasFechas)
        {
            try
            {
                List<ListaExpedienteUnico> resultado = new List<ListaExpedienteUnico>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<ListaExpedienteUnico>
                        (
                        sql: "sp_DameExUnicoPorFecha", param: new
                        {
                            fechaInicial,
                            fechaFinal,                            
                            todasLasFechas

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameExUnicoPorFecha , detalle: \n" + ex.Message, ex);
            }
        }
    }
}
