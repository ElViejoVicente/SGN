using Dapper;
using Microsoft.Exchange.WebServices.Data;
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


        public List<Alertas> DameAlertasPorExpediente(string NumExpediente)
        {
            try
            {
                List<Alertas> resultado = new List<Alertas>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Alertas>
                        (
                        sql: "sp_DameAlertaPorExpediente", param: new
                        {
                            NumExpediente

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameAlertaPorExpediente , detalle: \n" + ex.Message, ex);
            }
        }
        public List<ListaHojaDatos> DameListaHojaDatos(
    DateTime fechaInicial,
    DateTime fechaFinal,
    bool AnioActual,
    bool AnioAnterior,
    bool IncluirArchivados)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Open();

                    using (var multi = db.QueryMultiple(
                        sql: "sp_DameHojaDatosCompletaPorFechaV2",
                        param: new
                        {
                            fechaInicial,
                            fechaFinal,
                            AnioActual,
                            AnioAnterior,
                            IncluirArchivados
                        },
                        commandType: CommandType.StoredProcedure,
                        commandTimeout: 120))
                    {
                        // =====================================================
                        // 1. LISTA PRINCIPAL
                        // =====================================================

                        var resultado = multi
                            .Read<ListaHojaDatos>()
                            .ToList();


                        // =====================================================
                        // 2. DETALLE HOJA DATOS
                        // =====================================================

                        var hojasDatos = multi
                            .Read<HojaDatos>()
                            .ToList();


                        // =====================================================
                        // 3. VARIANTES
                        // =====================================================

                        var variantes = multi
                            .Read<DatosVariantes>()
                            .ToList();


                        // =====================================================
                        // 4. EXPEDIENTES
                        // =====================================================

                        var expedientes = multi
                            .Read<Expedientes>()
                            .ToList();


                        // =====================================================
                        // 5. PARTICIPANTES
                        // =====================================================

                        var participantes = multi
                            .Read<DatosParticipantes>()
                            .ToList();


                        // =====================================================
                        // 6. DOCUMENTOS
                        // =====================================================

                        var documentos = multi
                            .Read<DatosDocumentos>()
                            .ToList();


                        // =====================================================
                        // 7. RECIBOS
                        // =====================================================

                        var recibos = multi
                            .Read<RecibosDePago>()
                            .ToList();


                        if (resultado.Count == 0)
                        {
                            return resultado;
                        }


                        // =====================================================
                        // CREAR DICCIONARIOS
                        // =====================================================

                        var hojasDatosPorId = hojasDatos
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.FirstOrDefault()
                            );


                        var variantesPorHoja = variantes
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.FirstOrDefault()
                            );


                        var expedientesPorHoja = expedientes
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.FirstOrDefault()
                            );


                        var participantesPorHoja = participantes
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.ToList()
                            );


                        var documentosPorHoja = documentos
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.ToList()
                            );


                        var recibosPorHoja = recibos
                            .GroupBy(x => x.IdHojaDatos)
                            .ToDictionary(
                                x => x.Key,
                                x => x.ToList()
                            );


                        // =====================================================
                        // ASIGNAR DETALLES
                        // =====================================================

                        foreach (var item in resultado)
                        {
                            // HojaDatos

                            if (hojasDatosPorId.TryGetValue(
                                item.IdHojaDatos,
                                out var hojaDatos))
                            {
                                item.DetalleHojaDatos = hojaDatos;
                            }


                            // Variante

                            if (variantesPorHoja.TryGetValue(
                                item.IdHojaDatos,
                                out var variante))
                            {
                                item.DetalleVariante = variante;
                            }


                            // Expediente

                            if (expedientesPorHoja.TryGetValue(
                                item.IdHojaDatos,
                                out var expediente))
                            {
                                item.DetalleExpediente = expediente;
                            }


                            // Participantes

                            if (participantesPorHoja.TryGetValue(
                                item.IdHojaDatos,
                                out var listaParticipantes))
                            {
                                item.DetalleParticipantes =
                                    listaParticipantes;
                            }
                            else
                            {
                                item.DetalleParticipantes =
                                    new List<DatosParticipantes>();
                            }


                            // Documentos

                            if (documentosPorHoja.TryGetValue(
                                item.IdHojaDatos,
                                out var listaDocumentos))
                            {
                                item.DetalleDocumentos =
                                    listaDocumentos;


                                item.DetalleDocumentosOtorgSolicita =
                                    listaDocumentos
                                        .Where(x =>
                                            x.TextoFigura ==
                                            "Otorga o Solicita")
                                        .ToList();


                                item.DetalleDocumentosAfavorDe =
                                    listaDocumentos
                                        .Where(x =>
                                            x.TextoFigura ==
                                            "A favor de")
                                        .ToList();
                            }
                            else
                            {
                                item.DetalleDocumentos =
                                    new List<DatosDocumentos>();

                                item.DetalleDocumentosOtorgSolicita =
                                    new List<DatosDocumentos>();

                                item.DetalleDocumentosAfavorDe =
                                    new List<DatosDocumentos>();
                            }


                            // Recibos

                            if (recibosPorHoja.TryGetValue(
                                item.IdHojaDatos,
                                out var listaRecibos))
                            {
                                item.DetalleRecibosPago =
                                    listaRecibos;
                            }
                            else
                            {
                                item.DetalleRecibosPago =
                                    new List<RecibosDePago>();
                            }
                        }


                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al ejecutar sp_DameHojaDatosCompletaPorFechaV2, detalle: \n"
                    + ex.Message,
                    ex);
            }
        }

        //public List<ListaHojaDatos> DameListaHojaDatos(DateTime fechaInicial, DateTime fechaFinal, Boolean AnioActual, Boolean AnioAnterior, Boolean IncluirArchivados)
        //{
        //    try
        //    {

        //        List<ListaHojaDatos> resultado = new List<ListaHojaDatos>();

        //        using (var db = new SqlConnection(cnn))
        //        {
        //            resultado = db.Query<ListaHojaDatos>
        //                (

        //                sql: "sp_DameHojaDatosPorFecha", param: new
        //                {
        //                    fechaInicial,
        //                    fechaFinal,
        //                    AnioActual,
        //                    AnioAnterior,
        //                    IncluirArchivados

        //                }, commandType: CommandType.StoredProcedure


        //                ).ToList();
        //        }

        //        if (resultado.Count > 0)
        //        {



        //            foreach (var item in resultado)
        //            {
        //                // consultamos los datos DatosVariantes

        //                item.DetalleHojaDatos = datosCrud.ConsultaHojaDatos(item.IdHojaDatos);

        //                item.DetalleVariante = ConsultaDatosVariantesXHojaDatos(item.IdHojaDatos);

        //                item.DetalleExpediente = ConsultaExpedienteXHojaDatos(item.IdHojaDatos);

        //                item.DetalleParticipantes = DameListaParticipantes(item.IdHojaDatos);

        //                item.DetalleDocumentos = DameListaDocumentos(item.IdHojaDatos);

        //                item.DetalleDocumentosOtorgSolicita = DameListaDocumentos(item.IdHojaDatos, "Otorga o Solicita");

        //                item.DetalleDocumentosAfavorDe = DameListaDocumentos(item.IdHojaDatos, "A favor de");

        //                item.DetalleRecibosPago = DameRecibosDePago(item.IdHojaDatos);


        //            }
        //        }

        //        return resultado;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Error al ejecutar sp_DameHojaDatosPorFecha , detalle: \n" + ex.Message, ex);
        //    }
        //}



        public DatosVariantes ConsultaDatosVariantesXHojaDatos(int IdRegistro)
        {
            try
            {
                DatosVariantes resultado = new DatosVariantes();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<DatosVariantes>
                        (
                        sql: "sp_DameDatosVariantesXHojaDatos", param: new
                        {
                            IdRegistro
                        }, commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameDatosVariantesXHojaDatos, detalle: \n" + ex.Message, ex);
            }
        }

        public Expedientes ConsultaExpedienteXHojaDatos(int IdRegistro)
        {
            try
            {
                Expedientes resultado = new Expedientes();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Expedientes>
                        (
                        sql: "sp_DameExpedienteXHojaDatos ", param: new
                        {
                            IdRegistro
                        }, commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameExpedienteXHojaDatos , detalle: \n" + ex.Message, ex);
            }
        }

        public ListaHojaDatos DameHojaDatosDetalle(int idHojaDatosdate)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Open();

                    using (var multi = db.QueryMultiple(
                        sql: "sp_DameHojaDatosPorIDV2",
                        param: new
                        {
                            idHojaDatosdate
                        },
                        commandType: CommandType.StoredProcedure))
                    {
                        // =====================================================
                        // RESULTSET 1
                        // Equivale a:
                        // sp_DameHojaDatosPorID
                        // =====================================================

                        ListaHojaDatos resultado =
                            multi.Read<ListaHojaDatos>()
                                 .SingleOrDefault();


                        if (resultado == null)
                        {
                            return null;
                        }


                        // =====================================================
                        // RESULTSET 2
                        // Equivale a:
                        // datosCrud.ConsultaHojaDatos()
                        // =====================================================

                        resultado.DetalleHojaDatos =
                            multi.Read<HojaDatos>()
                                 .SingleOrDefault();


                        // =====================================================
                        // RESULTSET 3
                        // Equivale a:
                        // ConsultaDatosVariantesXHojaDatos()
                        // =====================================================

                        resultado.DetalleVariante =
                            multi.Read<DatosVariantes>()
                                 .FirstOrDefault();


                        // =====================================================
                        // RESULTSET 4
                        // Equivale a:
                        // ConsultaExpedienteXHojaDatos()
                        // =====================================================

                        resultado.DetalleExpediente =
                            multi.Read<Expedientes>()
                                 .FirstOrDefault();


                        // =====================================================
                        // RESULTSET 5
                        // Equivale a:
                        // DameListaParticipantes()
                        // =====================================================

                        resultado.DetalleParticipantes =
                            multi.Read<DatosParticipantes>()
                                 .ToList();


                        // =====================================================
                        // RESULTSET 6
                        // Cargamos TODOS los documentos una sola vez
                        // =====================================================

                        var documentos =
                            multi.Read<DatosDocumentos>()
                                 .ToList();


                        // Todos

                        resultado.DetalleDocumentos =
                            documentos;


                        // Otorga o Solicita

                        resultado.DetalleDocumentosOtorgSolicita =
                            documentos
                                .Where(x =>
                                    x.TextoFigura == "Otorga o Solicita")
                                .ToList();


                        // A favor de

                        resultado.DetalleDocumentosAfavorDe =
                            documentos
                                .Where(x =>
                                    x.TextoFigura == "A favor de")
                                .ToList();


                        // =====================================================
                        // RESULTSET 7
                        // Equivale a:
                        // DameRecibosDePago()
                        // =====================================================

                        resultado.DetalleRecibosPago =
                            multi.Read<RecibosDePago>()
                                 .ToList();


                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al ejecutar sp_DameHojaDatosPorIDV2, detalle: \n"
                    + ex.Message,
                    ex);
            }
        }

        //public ListaHojaDatos DameHojaDatosDetalle(int idHojaDatosdate)
        //{
        //    try
        //    {

        //        ListaHojaDatos resultado = new ListaHojaDatos();

        //        using (var db = new SqlConnection(cnn))
        //        {
        //            resultado = db.QuerySingle<ListaHojaDatos>
        //                (
        //                 sql: "sp_DameHojaDatosPorID", param: new
        //                 {
        //                     idHojaDatosdate

        //                 }, commandType: CommandType.StoredProcedure
        //                );
        //        }

        //        if (resultado != null)
        //        {


        //            // consultamos los datos DatosVariantes

        //            resultado.DetalleHojaDatos = datosCrud.ConsultaHojaDatos(resultado.IdHojaDatos);

        //            resultado.DetalleVariante = ConsultaDatosVariantesXHojaDatos(resultado.IdHojaDatos);

        //            resultado.DetalleExpediente = ConsultaExpedienteXHojaDatos(resultado.IdHojaDatos);

        //            resultado.DetalleParticipantes = DameListaParticipantes(resultado.IdHojaDatos);

        //            resultado.DetalleDocumentos = DameListaDocumentos(resultado.IdHojaDatos);

        //            resultado.DetalleDocumentosOtorgSolicita = DameListaDocumentos(resultado.IdHojaDatos, "Otorga o Solicita");

        //            resultado.DetalleDocumentosAfavorDe = DameListaDocumentos(resultado.IdHojaDatos, "A favor de");

        //            resultado.DetalleRecibosPago = DameRecibosDePago(resultado.IdHojaDatos);

        //        }

        //        return resultado;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Error al ejecutar sp_DameHojaDatosPorID , detalle: \n" + ex.Message, ex);
        //    }
        //}



        public List<DatosVariantes> DameDatosVariantes(int idHojaDatos)
        {
            try
            {
                List<DatosVariantes> resultado = new List<DatosVariantes>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<DatosVariantes>
                        (
                        sql: "sp_DameDatosVariantesporHojaDatos", param: new
                        {
                            idHojaDatos

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameDatosVariantesporHojaDatos , detalle: \n" + ex.Message, ex);
            }
        }


        public Boolean BorraDatosParticipantesDocumentos(int idHojaDatos)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_BorraDatosParticipantesPorIdHojaDatos", param: new
                    {
                        idHojaDatos

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_BorraDatosParticipantesPorIdHojaDatos, detalle: \n" + ex.Message, ex);
            }
        }




        public List<RecibosDePago> DameRecibosDePago(int idHojaDatos)
        {
            try
            {
                List<RecibosDePago> resultado = new List<RecibosDePago>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<RecibosDePago>
                        (
                        sql: "sp_DameRecibosDePagoPorHojaDatos", param: new
                        {
                            idHojaDatos

                        }, commandType: CommandType.StoredProcedure
                        ).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameRecibosDePagoPorHojaDatos , detalle: \n" + ex.Message, ex);
            }
        }

        public List<DatosDocumentos> DameListaDocumentos(int idHojaDatos, string textoFigura)
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

                if (resultado.Count > 0)
                {
                    resultado = resultado.Where(x => x.TextoFigura == textoFigura).ToList();
                }

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameDatosDocumentosPorHojaDatos , detalle: \n" + ex.Message, ex);
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

        public List<ListaExpedientes> DameListaExpediente(DateTime fechaInicial, DateTime fechaFinal, int idUsuario, Boolean AnioActual, Boolean AnioAnterior, Boolean IncluirArchivados)
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
                            fechaFinal,
                            idUsuario,
                            AnioActual,
                            AnioAnterior,
                            IncluirArchivados


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
