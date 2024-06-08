using Dapper;
using System;
using SGN.Negocio.ORM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.CRUD
{
    public class DatosCrud
    {

        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];


        #region DatosDocumentos
        public Boolean AltaDatosDocumentos(DatosDocumentos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_DatosDocumentos_Insert", param: new
                    {
                        values.IdHojaDatos,
                        values.IdVariente,
                        values.TextoVariante,
                        values.TextoFigura,
                        values.IdDoc,
                        values.Observaciones

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_DatosDocumentos_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region RecibosDePago
        public Boolean AltaRecibosDePago(RecibosDePago values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_RecibosDePago_Insert", param: new
                    {
                        values.NumRecibo,
                        values.IdHojaDatos,
                        values.CantidadTotal,
                        values.CantidadAbonada,
                        values.Concepto,
                        values.UsuarioRecibe,
                        values.NotaComentario

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_RecibosDePago_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region DatosParticipantes
        public Boolean AltaDatosParticipantes(DatosParticipantes values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_DatosParticipantes_Insert", param: new
                    {
                        values.IdHojaDatos,
                        values.FiguraOperacion,
                        values.RolOperacion,
                        values.Nombres,
                        values.ApellidoPaterno,
                        values.ApellidoMaterno,
                        values.FechaNacimiento,
                        values.Sexo,
                        values.Ocupacion,
                        values.EstadoCivil,
                        values.RegimenConyugal,
                        values.SabeLeerEscribir,
                        values.Notas

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_DatosParticipantes_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region DatosVariantes

        public Boolean AltaDatosVariantes(DatosVariantes values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_DatosVariantes_Insert", param: new
                    {
                        values.IdHojaDatos,
                        values.IdActo,
                        values.IdVariante,
                        values.TextoActo,
                        values.TextoVariante,
                        values.NotasEspeciales,
                        values.Dispocisiones,
                        values.NotasClausulasEspeciales,
                        values.CoApActaNacNum,
                        values.CoApActaNacFecha,
                        values.OtrosNombres,
                        values.NominacionPermisoSE,
                        values.TipoSociedad,
                        values.DatosFaltantes

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_DatosVariantes_Insert, detalle: \n" + ex.Message, ex);
            }
        }



        public Boolean ActualizarDatosVariantes(DatosVariantes values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_DatosVariantes_Update", param: new
                    {
                        values.IdRegistro,
                        values.IdHojaDatos,
                        values.IdActo,
                        values.IdVariante,
                        values.TextoActo,
                        values.TextoVariante,
                        values.NotasEspeciales,
                        values.Dispocisiones,
                        values.NotasClausulasEspeciales,
                        values.CoApActaNacNum,
                        values.CoApActaNacFecha,
                        values.OtrosNombres,
                        values.NominacionPermisoSE,
                        values.TipoSociedad,
                        values.DatosFaltantes

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_DatosVariantes_Update, detalle: \n" + ex.Message, ex);
            }
        }


        public DatosVariantes ConsultaDatosVariantes(int IdRegistro)
        {
            try
            {
                DatosVariantes resultado = new DatosVariantes();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<DatosVariantes>
                        (
                        sql: "sp_CRUD_DatosVariantes_Select", param: new
                        {
                            IdRegistro
                        }, commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_DatosVariantes_Select, detalle: \n" + ex.Message, ex);
            }
        }




        #endregion

        #region HojaDatos
        public Boolean AltaHojaDatos(ref HojaDatos values)
        {
            try
            {
                HojaDatos nuevahoja = new HojaDatos();
                using (var db = new SqlConnection(cnn))
                {
                    nuevahoja = db.QuerySingle<HojaDatos>(sql: "sp_CRUD_HojaDatos_Insert", param: new
                    {

                        values.numExpediente,
                        values.NombreAsesor,
                        values.FechaIngreso,
                        values.FechaCompleto,
                        values.IdUsuarioResponsable,
                        values.IdEquipoResponsable,
                        values.NumbreUsuarioTramita,
                        values.NumTelCelular1,
                        values.NumTelCelular2,
                        values.CorreoElectronico


                    }, commandType: CommandType.StoredProcedure);
                }
                values = nuevahoja;
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_HojaDatos_Insert, detalle: \n" + ex.Message, ex);
            }
        }

        public Boolean ActualizarHojaDatos(HojaDatos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_HojaDatos_Update", param: new
                    {
                        values.IdHojaDatos,
                        values.numExpediente,
                        values.NombreAsesor,
                        values.FechaIngreso,
                        values.FechaCompleto,
                        values.IdUsuarioResponsable,
                        values.IdEquipoResponsable,
                        values.NumbreUsuarioTramita,
                        values.NumTelCelular1,
                        values.NumTelCelular2,
                        values.CorreoElectronico

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_HojaDatos_Update, detalle: \n" + ex.Message, ex);
            }
        }


        public HojaDatos ConsultaHojaDatos(int IdHojaDatos)
        {
            try
            {
                HojaDatos resultado = new HojaDatos();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.QuerySingle<HojaDatos>
                        (
                        sql: "sp_CRUD_HojaDatos_Select", param: new
                        {
                            IdHojaDatos
                        }, commandType: CommandType.StoredProcedure
                        );
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_HojaDatos_Select, detalle: \n" + ex.Message, ex);
            }
        }


        #endregion

        #region Cat_DocumentosPorActo
        public List<Cat_DocumentosPorActo> ConsultaCatDocumentosPorActo()
        {
            try
            {
                List<Cat_DocumentosPorActo> resultado = new List<Cat_DocumentosPorActo>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_DocumentosPorActo>(sql: "[sp_CRUD_Cat_DocumentosPorActo_Select]").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar [sp_CRUD_Cat_DocumentosPorActo_Select], detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region Cat_RolParticipantes
        public List<Cat_RolParticipantes> ConsultaCatRolParticipantes()
        {
            try
            {
                List<Cat_RolParticipantes> resultado = new List<Cat_RolParticipantes>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_RolParticipantes>(sql: "[sp_CRUD_Cat_RolParticipantes_Select]").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar [sp_CRUD_Cat_RolParticipantes_Select], detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region Cat_VariantesPorActo
        public List<Cat_VariantesPorActo> ConsultaCatVariantesPorActo()
        {
            try
            {
                List<Cat_VariantesPorActo> resultado = new List<Cat_VariantesPorActo>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_VariantesPorActo>(sql: "sp_CRUD_Cat_VariantesPorActo_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_VariantesPorActo_Select, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region Cat_Actos
        public List<Cat_Actos> ConsultaCatActos()
        {
            try
            {
                List<Cat_Actos> resultado = new List<Cat_Actos>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_Actos>(sql: "sp_CRUD_Cat_Actos_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Insert", param: new
                    {
                        values.TextoActo,
                        values.Descripcion,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Update", param: new
                    {
                        values.TextoActo,
                        values.Descripcion,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Delete", param: new
                    {
                        values.IdActo

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Delete, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region Cat_Estatus

        public List<Cat_Estatus> ConsultaCatEstatus()
        {
            try
            {
                List<Cat_Estatus> resultado = new List<Cat_Estatus>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_Estatus>(sql: "sp_CRUD_Cat_Estatus_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Estatus_Select, detalle: \n" + ex.Message, ex);
            }
        }



        #endregion

        #region PerfilesXestatus

        public List<PerfilesXestatus> ConsultaPerfilesXestatus()
        {
            try
            {
                List<PerfilesXestatus> resultado = new List<PerfilesXestatus>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<PerfilesXestatus>(sql: "sp_CRUD_PerfilesXestatus_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_PerfilesXestatus_Select, detalle: \n" + ex.Message, ex);
            }
        }

        #endregion

        #region Cat_Proyectistas

        public List<Cat_Proyectistas> ConsultaCatProyectista()
        {
            try
            {
                List<Cat_Proyectistas> resultado = new List<Cat_Proyectistas>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_Proyectistas>(sql: "sp_CRUD_Cat_Proyectistas_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Proyectistas_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaCatProyectista(Cat_Proyectistas values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Proyectistas_Insert", param: new
                    {
                        values.NombreProyectista,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Proyectistas_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarCatEstatus(Cat_Proyectistas values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Proyectistas_Update", param: new
                    {
                        values.idProyectista,
                        values.NombreProyectista,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Proyectistas_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarCatEstatus(Cat_Proyectistas values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Proyectistas_Delete", param: new
                    {
                        values.idProyectista

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Proyectistas_Delete, detalle: \n" + ex.Message, ex);
            }
        }


        #endregion
        
        #region Expedientes

        public Expedientes ConsultaExpediente(string numExp)
        {
            try
            {
                Expedientes resultado = new Expedientes();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Expedientes>
                        (
                        sql: "sp_CRUD_Expedientes_Select", param: new
                        {
                            IdExpediente = numExp
                        }, commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Expedientes_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaExpediente(Expedientes values)
        {
            try
            {

                using (var db = new SqlConnection(cnn))
                {


                    db.Execute(sql: "sp_CRUD_Expedientes_Insert", param: new
                    {
                        values.IdExpediente,
                        values.IdHojaDatos,
                        values.IdEstatus,
                        values.Otorga,
                        values.AfavorDe,
                        values.UbicacionPredio,
                        values.FechaElaboracion,
                        values.FechaEnvioRPP,
                        values.EsTramitePorSistema,
                        values.FechaPagoBoleta,
                        values.FechaRecibidoRPP,
                        values.NombreProyectista,
                        values.FechaAsignacionProyectista,
                        values.FechaPrevistaTerminoProyectista,
                        values.FechaAvisoPreventivo,
                        values.ISR,
                        values.NotasFirma,
                        values.Escritura,
                        values.Volumen,
                        values.AplicaTraslado,
                        values.FechaRecepcionTerminoEscritura,
                        values.FechaElaboracionDefinitivo,
                        values.FechaEnvioRPPDefinitivo,
                        values.EsTramitePorSistemaDefinitivo,
                        values.FechaPagoBoletaDefinitivo,
                        values.FechaRecibidoRPPDefinitivo,
                        values.FechaRecibioTraslado,
                        values.FechaAsignacionMesa,
                        values.FechaTerminoMesa,
                        values.ObservacionesEngrega,
                        values.RegistroEntrega,
                        values.FechaRegistroEntrega,
                        values.FechaBoletaPagoRegistroEntrega,
                        values.FechaRegresoRegistro,
                        values.FechaSalida,
                        values.ObservacionesTramiteTerminado

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Expedientes_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarExpediente(Expedientes values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Expedientes_Update", param: new
                    {
                        values.IdExpediente,                        
                        values.IdHojaDatos,
                        values.IdEstatus,
                        values.Otorga,
                        values.AfavorDe,
                        values.UbicacionPredio,
                        values.FechaElaboracion,
                        values.FechaEnvioRPP,
                        values.EsTramitePorSistema,
                        values.FechaPagoBoleta,
                        values.FechaRecibidoRPP,
                        values.NombreProyectista,
                        values.FechaAsignacionProyectista,
                        values.FechaPrevistaTerminoProyectista,
                        values.FechaAvisoPreventivo,
                        values.ISR,
                        values.NotasFirma,
                        values.Escritura,
                        values.Volumen,
                        values.AplicaTraslado,
                        values.FechaRecepcionTerminoEscritura,
                        values.FechaElaboracionDefinitivo,
                        values.FechaEnvioRPPDefinitivo,
                        values.EsTramitePorSistemaDefinitivo,
                        values.FechaPagoBoletaDefinitivo,
                        values.FechaRecibidoRPPDefinitivo,
                        values.FechaRecibioTraslado,
                        values.FechaAsignacionMesa,
                        values.FechaTerminoMesa,
                        values.ObservacionesEngrega,
                        values.RegistroEntrega,
                        values.FechaRegistroEntrega,
                        values.FechaBoletaPagoRegistroEntrega,
                        values.FechaRegresoRegistro,
                        values.FechaSalida,
                        values.ObservacionesTramiteTerminado

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Expedientes_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarExpediente(Expedientes values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Expedientes_Delete", param: new
                    {
                        values.IdExpediente

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Expedientes_Delete, detalle: \n" + ex.Message, ex);
            }
        }

        #endregion


        #region Alertas

        public Alertas ConsultaAlertas (string IdAlerta)
        {
            try
            {
                Alertas resultado = new Alertas();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Alertas>
                        (
                        sql: "sp_CRUD_Alertas_Select", param: new
                        {
                            IdAlerta
                        }, commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Alertas_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaAlertas(Alertas values)
        {
            try
            {

                using (var db = new SqlConnection(cnn))
                {


                    db.Execute(sql: "sp_CRUD_Alertas_Insert", param: new
                    {
                        values.NumExpediente,
                        values.NomUsuarioInformante,
                        values.MensajeAlerta,
                        values.Prioridad           
                  

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Alertas_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarExpediente(Alertas values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Alertas_Update", param: new
                    {
                        values.IdAlerta,
                        values.NumExpediente,
                        values.NomUsuarioInformante,
                        values.MensajeAlerta,
                        values.Prioridad,
                        values.AlertaActiva,
                        values.NomUsuarioCierra                


                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Alertas_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarExpediente(Alertas values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Alertas_Delete", param: new
                    {
                        values.IdAlerta

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Alertas_Delete, detalle: \n" + ex.Message, ex);
            }
        }



        #endregion
    }
}
