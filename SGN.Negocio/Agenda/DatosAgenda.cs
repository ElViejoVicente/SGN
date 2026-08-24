using Dapper;
using SGN.Negocio.CRUD;
using SGN.Negocio.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Agenda
{
    public  class DatosAgenda
    {
        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];
  


        public List<AgendaReporte> DameAlertasPorExpediente(string fechaConsulta)
        {
            try
            {
                List<AgendaReporte> resultado = new List<AgendaReporte>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<AgendaReporte>
                        (
                        sql: "sp_DameREporteAgendaXdia", param: new
                        {
                            fechaConsulta

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameREporteAgendaXdia , detalle: \n" + ex.Message, ex);
            }
        }

        public HashSet<DateTime> DameFechasConAgenda()
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    return new HashSet<DateTime>(db.Query<DateTime>(
                        @"SELECT DISTINCT CONVERT(date, FechaInicio)
                          FROM dbo.AgendaCitas
                          ORDER BY CONVERT(date, FechaInicio);"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar las fechas disponibles de la agenda, detalle: \n" + ex.Message, ex);
            }
        }


    }
}
