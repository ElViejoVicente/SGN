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

namespace SGN.Negocio.Expediente
{
    public class DatosExpedientes
    {
        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];
        DatosCrud datosCrud = new DatosCrud();
        public List<ListaHojaDatos> DameListaHojaDatos(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {

                List<ListaHojaDatos> resultado = new List<ListaHojaDatos>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<ListaHojaDatos>
                        (
                        sql: "sp_DameHojaDatosPorFecha", param: new
                        {
                            fechaInicial,
                            fechaFinal

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }

                if (resultado.Count>0)
                {

                    

                    foreach (var item in resultado)
                    {
                        // consultamos los datos DatosVariantes

                        item.DetalleVariantes = datosCrud.ConsultaDatosVariantes(item.IdHojaDatos);

                        item.DetalleParticipantes=DameListaParticipantes(item.IdHojaDatos);

                        item.DetalleDocumentos= DameListaDocumentos(item.IdHojaDatos);

                        item.DetalleRecibosPago = null;

                    }
                }

                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameHojaDatosPorFecha , detalle: \n" + ex.Message, ex);
            }
        }


        public List<DatosDocumentos> DameListaDocumentos(int idHojaDatos)
        {
            try
            {
                List<DatosDocumentos> resultado = new List<DatosDocumentos>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<DatosDocumentos>
                        (
                        sql: "sp_DameDatosDocumentosPorHojaDatos", param: new
                        {
                            idHojaDatos

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameDatosDocumentosPorHojaDatos , detalle: \n" + ex.Message, ex);
            }
        }


        public List<DatosParticipantes> DameListaParticipantes(int idHojaDatos)
        {
            try
            {
                List<DatosParticipantes> resultado = new List<DatosParticipantes>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<DatosParticipantes>
                        (
                        sql: "sp_DameDatosParticipantesporHojaDatos", param: new
                        {
                            idHojaDatos

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameDatosParticipantesporHojaDatos , detalle: \n" + ex.Message, ex);
            }
        }

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
