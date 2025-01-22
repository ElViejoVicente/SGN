using DevExpress.CodeParser;
using DevExpress.Export;
using DevExpress.Utils.Extensions;
using DevExpress.Web;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;

using SGN.Web.Controles.Servidor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SGN.Web.ExpedientesTramites
{
    public partial class PanelExpedientes : PageBase
    {

        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
        //DatosUsuario datosUsuario = new DatosUsuario();
        DatosExpedientes datosExpediente = new DatosExpedientes();
        public List<Cat_Actos> catActos
        {
            get

            {
                List<Cat_Actos> sseCatActos = new List<Cat_Actos>();
                if (this.Session["sseCatActos"] != null)
                {
                    sseCatActos = (List<Cat_Actos>)this.Session["sseCatActos"];
                }

                return sseCatActos;
            }
            set
            {
                this.Session["sseCatActos"] = value;
            }

        }

        public List<PerfilesXestatus> lsPerfilesXestatus
        {
            get

            {
                List<PerfilesXestatus> ssPerfilesXestatus = new List<PerfilesXestatus>();
                if (this.Session["ssPerfilesXestatus"] != null)
                {
                    ssPerfilesXestatus = (List<PerfilesXestatus>)this.Session["ssPerfilesXestatus"];
                }

                return ssPerfilesXestatus;
            }
            set
            {
                this.Session["ssPerfilesXestatus"] = value;
            }

        }

        public List<Cat_Estatus> catEstatus
        {
            get

            {
                List<Cat_Estatus> sseCatEstatus = new List<Cat_Estatus>();
                if (this.Session["sseCatEstatus"] != null)
                {
                    sseCatEstatus = (List<Cat_Estatus>)this.Session["sseCatEstatus"];
                }

                return sseCatEstatus;
            }
            set
            {
                this.Session["sseCatEstatus"] = value;
            }

        }

        public List<Cat_Estatus> catEstatusFull
        {
            get

            {
                List<Cat_Estatus> sseCatEstatusFull = new List<Cat_Estatus>();
                if (this.Session["sseCatEstatusFull"] != null)
                {
                    sseCatEstatusFull = (List<Cat_Estatus>)this.Session["sseCatEstatusFull"];
                }

                return sseCatEstatusFull;
            }
            set
            {
                this.Session["sseCatEstatusFull"] = value;
            }

        }


        public List<Usuario> catProyectistas
        {
            get

            {
                List<Usuario> sseCatProyectistas = new List<Usuario>();
                if (this.Session["sseCatProyectistas"] != null)
                {
                    sseCatProyectistas = (List<Usuario>)this.Session["sseCatProyectistas"];
                }

                return sseCatProyectistas;
            }
            set
            {
                this.Session["sseCatProyectistas"] = value;
            }

        }


        public List<ListaExpedientes> lsExpediente
        {
            get

            {
                List<ListaExpedientes> sseListaExpediente = new List<ListaExpedientes>();
                if (this.Session["sseListaExpediente"] != null)
                {
                    sseListaExpediente = (List<ListaExpedientes>)this.Session["sseListaExpediente"];
                }

                return sseListaExpediente;
            }
            set
            {
                this.Session["sseListaExpediente"] = value;
            }

        }

        public List<Alertas> lsAlertas
        {
            get

            {
                List<Alertas> sseListaAlertas = new List<Alertas>();
                if (this.Session["sseListaAlertas"] != null)
                {
                    sseListaAlertas = (List<Alertas>)this.Session["sseListaAlertas"];
                }

                return sseListaAlertas;
            }
            set
            {
                this.Session["sseListaAlertas"] = value;
            }

        }


        public Expedientes RegistroExistente
        {
            get

            {
                Expedientes ssRegistroExistente = new Expedientes();
                if (this.Session["ssRegistroExistente"] != null)
                {
                    ssRegistroExistente = (Expedientes)this.Session["ssRegistroExistente"];
                }

                return ssRegistroExistente;
            }
            set
            {
                this.Session["ssRegistroExistente"] = value;
            }

        }


        public string rutaArchivosRoot
        {
            get

            {
                string ssRutaArchivosRoot = "";
                if (this.Session["ssRutaArchivosRoot"] != null)
                {
                    ssRutaArchivosRoot = this.Session["ssRutaArchivosRoot"].ToString();
                }

                return ssRutaArchivosRoot;
            }
            set
            {
                this.Session["ssRutaArchivosRoot"] = value;
            }

        }

        public string NumExpSelccionadoAlerta
        {
            get

            {
                string ssNumExpSelccionadoAlerta = "";
                if (this.Session["ssNumExpSelccionadoAlerta"] != null)
                {
                    ssNumExpSelccionadoAlerta = this.Session["ssNumExpSelccionadoAlerta"].ToString();
                }

                return ssNumExpSelccionadoAlerta;
            }
            set
            {
                this.Session["ssNumExpSelccionadoAlerta"] = value;
            }

        }
        public string NumExpSelccionadoCambioEstatus
        {
            get

            {
                string ssNumExpSelccionadoCambioEstatus = "";
                if (this.Session["ssNumExpSelccionadoCambioEstatus"] != null)
                {
                    ssNumExpSelccionadoCambioEstatus = this.Session["ssNumExpSelccionadoCambioEstatus"].ToString();
                }

                return ssNumExpSelccionadoCambioEstatus;
            }
            set
            {
                this.Session["ssNumExpSelccionadoCambioEstatus"] = value;
            }

        }




        #endregion

        #region Funciones
        private void DameCatalogos()

        {
            try
            {

                catEstatus = datosCrud.ConsultaCatEstatus();
                catEstatusFull = datosCrud.ConsultaCatEstatus();

                catActos = datosCrud.ConsultaCatActos();
                catProyectistas = datosUsuario.DameDatosUsuario(-1).Where(x => x.EsProyectista == true).ToList();
                lsPerfilesXestatus = datosCrud.ConsultaPerfilesXestatus();

                if (lsPerfilesXestatus.Count > 0)
                {

                    var estatusPermitidos = (from dato in catEstatus where lsPerfilesXestatus.Exists(p => p.esIdEstatus == dato.IdEstatus && p.esIdPerfil == UsuarioPagina.Perfil) select dato).ToList();
                    catEstatus = estatusPermitidos;
                }


                //cbActosNuevo.DataBind();
                cbPRfnProyectista.DataBind();
                trlEstatusExpedientes.DataBind();

                MostrarCampoPorPerfil(UsuarioPagina.NombrePerfil);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void MostrarCampoPorPerfil(string nombrePerfil)
        {

            frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = false;


            frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = false;

            frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = false;


            frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = false;

            frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = false;

            frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = false;

            frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = false;


            frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = false;
            frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = false;

            frmExpedienteExistente.FindItemByFieldName("PldActVulnerable").ClientVisible = false;



            switch (nombrePerfil)
            {
                case "Consultoria-IT":

                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
       

                    frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;


                    frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PldActVulnerable").ClientVisible = true;



                    break;
                case "Dirección":


                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
                 

                    frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;


                    frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PldActVulnerable").ClientVisible = true;


                    break;
                case "Datos":
                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
   

                    //frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;


                    frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PldActVulnerable").ClientVisible = true;


                    break;

                case "Mesas":

                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnISR").ClientVisible = true;

                    //Firmas
                    frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    //Aviso definitivo
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    //Escrituracion
                    //frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;
                    break;

                case "Capturista":

                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnISR").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;
                    break;

                case "Contabilidad":


                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;



                    frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = true;


                    break;


                case "Entregas":


                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("PRfnISR").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    //frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;

                    break;
                case "Coordinacion":


                    frmExpedienteExistente.FindItemByFieldName("ExfnNumeroExpediente").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnOtorga").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EXfnAfavorde").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnOperacionProyectada").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ExfnUbicacionPredio").ClientVisible = true;
                    // frmExpedienteExistente.FindItemByFieldName("ExfnDocumentosFaltantes").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("APfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaEnvioAlRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("APfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PRfnProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAsignacionProyectista").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaPrevistaTermino").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("PRfnFechaAvisoPreventivo").ClientVisible = true;
     

                    frmExpedienteExistente.FindItemByFieldName("FIfnNotasFirmas").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumEscritura").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnNumVolumen").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnAplicaTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("FIfnFechaRecepcionTerminoEscritura").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaElaboracion").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaEnvioRPP").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnEsTramitePorSistema").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaPagoBoleta").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaRecibido").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EsfnRecibioTraslado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaAsignacionMesa").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("AdfnFechaTerminoTramite").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesEntrega").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnRegistroSolicitado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaBoletaPago").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaRegresoRegistro").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnFechaSalida").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("EnfnObservacionesSobreTramiteTerminado").ClientVisible = true;


                    frmExpedienteExistente.FindItemByFieldName("ContISR").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContISRCalculado").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoCatastral").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoFiscal").ClientVisible = true;
                    frmExpedienteExistente.FindItemByFieldName("ContAvaluoComercial").ClientVisible = true;

                    frmExpedienteExistente.FindItemByFieldName("PldActVulnerable").ClientVisible = true;

                    break;
            }
        }
        #endregion


        #region Eventos

    


        protected void Page_Init(object sender, EventArgs e)
        {
            //try
            //{
            //    if (rutaArchivosRoot != "")
            //    {
            //        ASPxFileManager1.Settings.RootFolder = rutaArchivosRoot;

            //        ASPxFileManager1.Refresh();

            //        var rootFolder = GetRootFolder(ASPxFileManager1.SelectedFolder);

            //        ApplyRules(rootFolder);

            //    }

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}


        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                dtFechaInicio.Date = DateTime.Now.Date.AddDays(-15);
                dtFechaFin.Date = DateTime.Now.Date;
                DameCatalogos();
            }
        }

        protected void gvExpedientes_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsExpediente;
        }

        protected void gvExpedientes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //ASPxGridView control = (ASPxGridView)sender;
            if (e.Parameters == "CargarRegistros")
            {
                if (UsuarioPagina.NombrePerfil == "Mesas") //2024-03-17 si el perfil es de mesas entonces solo mostramos lo registros del usuario 
                {
                    lsExpediente = datosExpediente.DameListaExpediente(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date, idUsuario: UsuarioPagina.Id, todasLasFechas: chkBusquedaCompleta.Checked).OrderByDescending(x => x.FechaIngreso).ToList();// cargamos registros
                }
                else
                {
                    lsExpediente = datosExpediente.DameListaExpediente(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date, idUsuario: 0, todasLasFechas: chkBusquedaCompleta.Checked).OrderByDescending(x => x.FechaIngreso).ToList();// cargamos todos los registros
                }


                // 2024-06-22 filtramos por registros alertados

                if (chkVerExpAlertaActiva.Checked)
                {
                    lsExpediente = lsExpediente.Where(x => x.AlertaActiva == true).ToList();
                }


                if (chkVerExpAlertaNoActiva.Checked)
                {
                    lsExpediente = lsExpediente.Where(x => x.ExistenAlertas == true).ToList();
                }


                gvExpedientes.DataBind();
                return;
            }

            if (e.Parameters == "AsignarRutaExpediente")
            {

                ASPxGridView control = (ASPxGridView)sender;
                if (control.Selection.Count > 0)
                {
                    var datosExpediente = control.GetSelectedFieldValues("IdExpediente");
                    string numeroExpediente = datosExpediente[0].ToString().Replace("/", "-");
                    rutaArchivosRoot = "~/GNArchivosRoot/" + numeroExpediente;
                    //  ASPxFileManager1.Settings.RootFolder = rutaArchivosRoot;
                    //fmArchivosControl.Refresh();

                }
                return;
            }

        }

        protected void gvExpedientes_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {

                case "CustomExportToXLS":
                    ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;
                case "CustomExportToXLSX":
                    ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;

                default:
                    break;
            }
        }

        protected void gvExpedientes_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "IdExpediente")
            {

                if (e.CellValue != null)
                {

                    var miExpediente = lsExpediente.Where(x => x.IdExpediente == e.KeyValue.ToString()).FirstOrDefault();

                    if (miExpediente != null)
                    {

                        ASPxImage Campo = (ASPxImage)gvExpedientes.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "imgExpedienteAlerta");
                        Campo.Caption = Convert.ToString(e.CellValue);

                        if (miExpediente.AlertaActiva)
                        {
                            Campo.EmptyImage.IconID = "status_warning_16x16";

                        }
                        else if (miExpediente.ExistenAlertas)
                        {
                            Campo.EmptyImage.IconID = "status_warning_svg_gray_16x16";
                        }



                    }
                }

            }
        }

        //protected void ppOrdenNuevoExpediente_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        //{
        //    if (e.Parameter == "NuevoExpediente")
        //    {
        //        dtFechaIngresoNuevo.Date = DateTime.Now;
        //        return;

        //    }

        //    if (e.Parameter == "guardar")
        //    {
        //        Expedientes nuevoRegistro = new Expedientes();
        //        nuevoRegistro.IdExpediente = "";
        //        nuevoRegistro.numReciboPago = txtNumReciboNuevo.Text;
        //        nuevoRegistro.numReciboPago2 = "";

        //        nuevoRegistro.FechaIngreso = dtFechaIngresoNuevo.Date;
        //        nuevoRegistro.IdActo = Convert.ToInt32(cbActosNuevo.Value.ToString());
        //        nuevoRegistro.Otorga = txtOtorgaNuevo.Text;
        //        nuevoRegistro.AfavorDe = txtAfavorDeNuevo.Text;
        //        nuevoRegistro.OperacionProyectada = txtOperacionProyectadaNuevo.Text;
        //        nuevoRegistro.UbicacionPredio = txtUbicacionPredioNuevo.Text;
        //        nuevoRegistro.Faltantes = txtDocumentoFaltantesNuevo.Text;

        //        if (string.IsNullOrEmpty(txtDocumentoFaltantesNuevo.Text))
        //        {
        //            nuevoRegistro.IdEstatus = "EX1";
        //        }
        //        else
        //        {
        //            nuevoRegistro.IdEstatus = "EX2";
        //        }



        //        if (datosCrud.AltaExpediente(nuevoRegistro))
        //        {

        //            // 2024-01-22 creamos la carpetas necesarias para la gestion del expediente.



        //            string directorioVirtual = "~/GNArchivosRoot";
        //            string directorioFisico = MapPath(directorioVirtual);




        //            string rutaFisicaCalculada = Path.Combine(directorioFisico, nuevoRegistro.IdExpediente);

        //            if (!Directory.Exists(rutaFisicaCalculada))
        //            {
        //                Directory.CreateDirectory(rutaFisicaCalculada);

        //                if (Directory.Exists(rutaFisicaCalculada))
        //                {
        //                    string carpetaAvisos = Path.Combine(rutaFisicaCalculada, "Avisos");
        //                    string carpetaFirmados = Path.Combine(rutaFisicaCalculada, "Firmados");
        //                    string carpetaPendientesFirma = Path.Combine(rutaFisicaCalculada, "PedientesFirma");
        //                    string carpetaProyecto = Path.Combine(rutaFisicaCalculada, "Proyecto");
        //                    string carpetaDocumentos = Path.Combine(rutaFisicaCalculada, "Documentos");

        //                    Directory.CreateDirectory(carpetaAvisos);

        //                    Directory.CreateDirectory(carpetaFirmados);

        //                    Directory.CreateDirectory(carpetaPendientesFirma);

        //                    Directory.CreateDirectory(carpetaProyecto);

        //                    Directory.CreateDirectory(carpetaDocumentos);




        //                }

        //            }


        //            ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Nuevo expediente: " + nuevoRegistro.IdExpediente + " listo.!";
        //            ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;
        //            return;
        //        }
        //        else
        //        {

        //            ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar guardar el registro.";
        //            ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;
        //            return;
        //        }



        //    }

        //}

        //protected void cbActosNuevo_DataBinding(object sender, EventArgs e)
        //{
        //    ASPxComboBox control = (ASPxComboBox)sender;

        //    control.ValueField = "IdActo";
        //    control.TextField = "TextoActo";
        //    control.DataSource = catActos;
        //}

        protected void gvAvisoPreventivo_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;

        }


        protected void gvProyecto_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvFirmas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvAvisoDefinitivo_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvEscrituracion_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvEntregas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }
        protected void gvContabilidad_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }

        protected void gvPld_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }

        protected void ppCambiarEstatus_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter.Contains("CargarEstados"))
            {

                string numExpedinte = e.Parameter.Split('~')[1].ToString();

                NumExpSelccionadoCambioEstatus = e.Parameter.Split('~')[1].ToString();

                txtProyecSelecEstatus.Text = numExpedinte;

                //trlEstatusExpedientes.DataSource = catEstatus;

                trlEstatusExpedientes.DataBind();
                trlEstatusExpedientes.ExpandAll();
                // rbEstados.DataBind();
                return;

            }
            if (e.Parameter.Contains("guardar"))
            {
                var EstadoSelec = e.Parameter.Split('~')[1].ToString();

                RegistroExistente = new Expedientes();

                string numExpediente = NumExpSelccionadoCambioEstatus; //txtProyecSelecEstatus.Text;//gvExpedientes.GetSelectedFieldValues("IdExpediente")[0].ToString();

                RegistroExistente = datosCrud.ConsultaExpediente(numExp: numExpediente);

                RegistroExistente.IdEstatus = EstadoSelec;


                if (datosCrud.ActualizarExpediente(RegistroExistente))
                {
                    gvExpedientes.SearchPanelFilter = "";
                    ppCambiarEstatus.JSProperties["cp_swMsg"] = "Estatus Modificado!";
                    ppCambiarEstatus.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;

                }
                else
                {

                    ppCambiarEstatus.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar Modificar el Estatus.";
                    ppCambiarEstatus.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;

                }

                return;
            }

        }
        protected void ppAlertasExpediente_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter.Contains("CargarRegistros"))
            {
                RegistroExistente = new Expedientes();
                string numExpediente = e.Parameter.Split('~')[1].ToString();
                NumExpSelccionadoAlerta = numExpediente;
                RegistroExistente = datosCrud.ConsultaExpediente(numExp: numExpediente);



                // consultamos las alertas por expediente 

                lsAlertas = datosExpediente.DameAlertasPorExpediente(NumExpSelccionadoAlerta).OrderByDescending(x => x.IdAlerta).ToList();

                if (RegistroExistente != null)
                {
                    //Expediente 
                    txtNumExpedienteAlert.Text = RegistroExistente.IdExpediente;
                    txtExfnOtorgaAlert.Text = RegistroExistente.Otorga;
                    txtEXfnAfavordeAlert.Text = RegistroExistente.AfavorDe;

                    //cargar registro de alertas de BD
                }

                gvAlertas.DataBind();
            }
        }

        protected void gvAlertas_DataBinding(object sender, EventArgs e)
        {
            gvAlertas.DataSource = lsAlertas;
        }


        protected void ppEditarExpediente_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter.Contains("CargarRegistros"))
            {
                RegistroExistente = new Expedientes();


                string numExpediente = e.Parameter.Split('~')[1].ToString();

                RegistroExistente = datosCrud.ConsultaExpediente(numExp: numExpediente);

                if (RegistroExistente != null)
                {

                    // cargamos los campos en el form layaout

                    //Expediente 
                    txtNumExpediente.Text = RegistroExistente.IdExpediente;

                    txtExfnOtorga.Text = RegistroExistente.Otorga;
                    txtEXfnAfavorde.Text = RegistroExistente.AfavorDe;
                    txtExfnUbicacionPredio.Text = RegistroExistente.UbicacionPredio;

                    //Aviso preventivo
                    dtAPfnFechaElaboracion.Date = RegistroExistente.FechaElaboracion;
                    dtAPfnFechaEnvioAlRPP.Date = RegistroExistente.FechaEnvioRPP;
                    chkAPfnEsTramitePorSistema.Checked = RegistroExistente.EsTramitePorSistema;
                    dtAPfnFechaPagoBoleta.Date = RegistroExistente.FechaPagoBoleta;
                    dtAPfnFechaRecibido.Date = RegistroExistente.FechaRecibidoRPP;

                    //Proyecto
                    cbPRfnProyectista.Value = RegistroExistente.NombreProyectista;
                    cbPRfnProyectista.SelectedIndex = catProyectistas.FindIndex(w => w.Nombre == RegistroExistente.NombreProyectista);

                    dtPRfnFechaAsignacionProyectista.Date = RegistroExistente.FechaAsignacionProyectista;
                    dtPRfnFechaPrevistaTermino.Date = RegistroExistente.FechaPrevistaTerminoProyectista;
                    dtPRfnFechaAvisoPreventivo.Date = RegistroExistente.FechaAvisoPreventivo;
                
                    txtPRfnValorOperacion.Value = RegistroExistente.ValorOperacion;

                    //Firmas
                    txtFIfnNotasFirmas.Text = RegistroExistente.NotasFirma;
                    txtFIfnNumEscritura.Value = RegistroExistente.Escritura;
                    txtFIfnNumVolumen.Value = RegistroExistente.Volumen;
                    chkFIfnAplicaTraslado.Checked = RegistroExistente.AplicaTraslado;
                    dtFIfnFechaRecepcionTerminoEscritura.Date = RegistroExistente.FechaRecepcionTerminoEscritura;

                    //Aviso definitivo
                    dtAdfnFechaElaboracion.Date = RegistroExistente.FechaElaboracionDefinitivo;
                    dtAdfnFechaEnvioRPP.Date = RegistroExistente.FechaEnvioRPPDefinitivo;
                    chkAdfnEsTramitePorSistema.Checked = RegistroExistente.EsTramitePorSistemaDefinitivo;
                    dtAdfnFechaPagoBoleta.Date = RegistroExistente.FechaPagoBoletaDefinitivo;
                    dtAdfnFechaRecibido.Date = RegistroExistente.FechaRecibidoRPPDefinitivo;

                    //Escrituracion 
                    dtEsfnRecibioTraslado.Date = RegistroExistente.FechaRecibioTraslado;
                    dtAdfnFechaAsignacionMesa.Date = RegistroExistente.FechaAsignacionMesa;
                    dtAdfnFechaTerminoTramite.Date = RegistroExistente.FechaTerminoMesa;

                    //Entregas
                    txtEnfnObservacionesEntrega.Text = RegistroExistente.ObservacionesEngrega;
                    chkEnfnRegistroSolicitado.Checked = RegistroExistente.RegistroEntrega;
                    dtEnfnFechaRegistro.Date = RegistroExistente.FechaRegistroEntrega;
                    dtEnfnFechaBoletaPago.Date = RegistroExistente.FechaBoletaPagoRegistroEntrega;
                    dtEnfnFechaRegresoRegistro.Date = RegistroExistente.FechaRegresoRegistro;
                    dtEnfnFechaSalida.Date = RegistroExistente.FechaSalida;
                    txtEnfnObservacionesSobreTramiteTerminado.Text = RegistroExistente.ObservacionesTramiteTerminado;

                    //Contabilidad

                    txtContISR.Value = RegistroExistente.ISR;
                    txtContISRCalculado.Value = RegistroExistente.ISRcalculado;
                    txtContAvaluoCatastral.Value = RegistroExistente.AvaluoCatastral;
                    txtContAvaluoComercial.Value = RegistroExistente.AvaluoComercial;
                    txtContAvaluoFiscal.Value = RegistroExistente.AvaluoFiscal;





                    //PLD

                    txtPldActividadVulnerable.Value = RegistroExistente.ActividadVulnerable;


                }


                return;
            }

            if (e.Parameter == "guardarCambios")
            {
                if (RegistroExistente != null)
                {


                    //2024-10-01 Vh Listado de reglas a considerar antes del guardado

                    // 1.0 si el expediente es >= Estado EN1 no se permite el guardado por ningun perfil que no sea direccion o coordinacion.


                    if (UsuarioPagina.NombrePerfil.Trim() == "Mesas")
                    {
                        // determinamos el valos del estado EN1
                        var ValorEnI = catEstatusFull.Where(x => x.IdEstatus == "EN1").First();

                        //determinamos el valor del estatus del proyecto

                        var ValorExpediente = catEstatusFull.Where(x => x.IdEstatus == RegistroExistente.IdEstatus).First();

                        if ( ValorExpediente.Orden >= ValorEnI.Orden )
                        {
                            // no se permite ser moficado por el perfil
                            ppEditarExpediente.JSProperties["cp_swMsg"] = "El expediente se encuentra en un estatus: " + ValorExpediente.TextoEstatus.Trim() +
                                                                          " No es permitido realizar modificaciones con tu perfil";
                            ppEditarExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.warning;
                            return;

                        }


                    }

                    //2025-01-20 bitacora de cambios
                    // registro de cambios OJo tratar de mantener el orden de campos y el mismo numero de registros

                    #region Validacion de Cambios LOG

                    BitacoraExpediente logCambios = new BitacoraExpediente();

                    logCambios.UsuarioImplicado = UsuarioPagina.Nombre;
                    logCambios.IdExpediente = RegistroExistente.IdExpediente;
                    logCambios.NombreModulo = "Expediente";


                    if (RegistroExistente.Otorga != txtExfnOtorga.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Ortorga" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.Otorga + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtExfnOtorga.Text + " | ";
                    }

                    if (RegistroExistente.UbicacionPredio != txtExfnUbicacionPredio.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Ubicacion de predio" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.UbicacionPredio + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtExfnUbicacionPredio.Text + " | ";
                    }


                    //Aviso preventivo

                    if (RegistroExistente.FechaElaboracion != dtAPfnFechaElaboracion.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso preventivo-Elaboracion" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaElaboracion + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAPfnFechaElaboracion.Date + " | ";
                    }
                    if (RegistroExistente.FechaEnvioRPP != dtAPfnFechaEnvioAlRPP.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso preventivo-Envio al R.P.P." + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaEnvioRPP + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAPfnFechaEnvioAlRPP.Date + " | ";
                    }
                    if (RegistroExistente.EsTramitePorSistema != chkAPfnEsTramitePorSistema.Checked)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso preventivo-Es tramite por sistema" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.EsTramitePorSistema + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + chkAPfnEsTramitePorSistema.Checked + " | ";
                    }
                    if (RegistroExistente.FechaPagoBoleta != dtAPfnFechaPagoBoleta.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso preventivo-Pago de la boleta" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaPagoBoleta + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAPfnFechaPagoBoleta.Date + " | ";
                    }
                    if (RegistroExistente.FechaRecibidoRPP != dtAPfnFechaRecibido.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso preventivo-Recibido" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRecibidoRPP + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAPfnFechaRecibido.Date + " | ";
                    }



                    //Proyecto

                    if (RegistroExistente.NombreProyectista != (cbPRfnProyectista.Value == null ? "" : cbPRfnProyectista.Value.ToString()))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Proyecto-Proyectista" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.NombreProyectista + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (cbPRfnProyectista.Value == null ? "" : cbPRfnProyectista.Value.ToString()) + " | ";
                    }
                    if (RegistroExistente.FechaAsignacionProyectista != dtPRfnFechaAsignacionProyectista.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Proyecto-Asignacion" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaAsignacionProyectista + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtPRfnFechaAsignacionProyectista.Date + " | ";

                    }
                    if (RegistroExistente.FechaPrevistaTerminoProyectista != dtPRfnFechaPrevistaTermino.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Proyecto-Prevision de termino" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaPrevistaTerminoProyectista + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtPRfnFechaPrevistaTermino.Date + " | ";
                    }
                    if (RegistroExistente.FechaAvisoPreventivo != dtPRfnFechaAvisoPreventivo.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Proyecto-Aviso Preventivo" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaAvisoPreventivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtPRfnFechaAvisoPreventivo.Date + " | ";
                    }
                    if (RegistroExistente.ValorOperacion != (txtPRfnValorOperacion.Value == null ? 0 : Convert.ToDecimal(txtPRfnValorOperacion.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Proyecto-Valor Operacion" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ValorOperacion + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtPRfnValorOperacion.Value == null ? 0 : Convert.ToDecimal(txtPRfnValorOperacion.Value.ToString())) + " | ";
                    }



                    //Firmas

                    if (RegistroExistente.NotasFirma != txtFIfnNotasFirmas.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Firmas-Notas" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.NotasFirma + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtFIfnNotasFirmas.Text + " | ";
                    }
                    if (RegistroExistente.Escritura != (txtFIfnNumEscritura.Value == null ? 0 : Convert.ToInt32(txtFIfnNumEscritura.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Firmas-Num Escritura" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.Escritura + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtFIfnNumEscritura.Value == null ? 0 : Convert.ToInt32(txtFIfnNumEscritura.Value.ToString())) + " | ";
                    }
                    if (RegistroExistente.Volumen != (txtFIfnNumVolumen.Value == null ? 0 : Convert.ToInt32(txtFIfnNumVolumen.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Firmas-Num Volumen" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.Volumen + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtFIfnNumVolumen.Value == null ? 0 : Convert.ToInt32(txtFIfnNumVolumen.Value.ToString())) + " | ";
                    }
                    if (RegistroExistente.AplicaTraslado != chkFIfnAplicaTraslado.Checked)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Firmas-Aplica traslado" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.AplicaTraslado + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + chkFIfnAplicaTraslado.Checked + " | ";
                    }
                    if (RegistroExistente.FechaRecepcionTerminoEscritura != dtFIfnFechaRecepcionTerminoEscritura.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Firmas-Recepcion para termino escritura" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRecepcionTerminoEscritura + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtFIfnFechaRecepcionTerminoEscritura.Date + " | ";
                    }




                    //Aviso definitivo

                    if (RegistroExistente.FechaElaboracionDefinitivo != dtAdfnFechaElaboracion.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso definitivo-Elaboracion" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaElaboracionDefinitivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaElaboracion.Date + " | ";
                    }
                    if (RegistroExistente.FechaEnvioRPPDefinitivo != dtAdfnFechaEnvioRPP.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso definitivo-Envio R.P.P." + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaEnvioRPPDefinitivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaEnvioRPP.Date + " | ";
                    }
                    if (RegistroExistente.EsTramitePorSistemaDefinitivo != chkAdfnEsTramitePorSistema.Checked)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso definitivo-Tramite por sistema" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.EsTramitePorSistemaDefinitivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + chkAdfnEsTramitePorSistema.Checked + " | ";
                    }
                    if (RegistroExistente.FechaPagoBoletaDefinitivo != dtAdfnFechaPagoBoleta.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso definitivo-Pago boleta" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaPagoBoletaDefinitivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaPagoBoleta.Date + " | ";
                    }
                    if (RegistroExistente.FechaRecibidoRPPDefinitivo != dtAdfnFechaRecibido.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Aviso definitivo-Recibido" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRecibidoRPPDefinitivo + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaRecibido.Date + " | ";
                    }



                    //Escrituracion 

                    if (RegistroExistente.FechaRecibioTraslado != dtEsfnRecibioTraslado.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Escrituracion-Recibio traslado" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRecibioTraslado + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtEsfnRecibioTraslado.Date + " | ";
                    }
                    if (RegistroExistente.FechaAsignacionMesa != dtAdfnFechaAsignacionMesa.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Escrituracion-Asignacion a mesa" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaAsignacionMesa + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaAsignacionMesa.Date + " | ";
                    }
                    if (RegistroExistente.FechaTerminoMesa != dtAdfnFechaTerminoTramite.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Escrituracion-Termino del tramite" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaTerminoMesa + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtAdfnFechaTerminoTramite.Date + " | ";
                    }


                    //Entregas


                    if (RegistroExistente.ObservacionesEngrega != txtEnfnObservacionesEntrega.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Observaciones" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ObservacionesEngrega + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtEnfnObservacionesEntrega.Text  + " | ";
                    }
                    if (RegistroExistente.RegistroEntrega != chkEnfnRegistroSolicitado.Checked)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-¿Requiere Registro?" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.RegistroEntrega + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + chkEnfnRegistroSolicitado.Checked + " | ";
                    }
                    if (RegistroExistente.FechaRegistroEntrega != dtEnfnFechaRegistro.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Registro" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRegistroEntrega + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtEnfnFechaRegistro.Date + " | ";
                    }
                    if (RegistroExistente.FechaBoletaPagoRegistroEntrega != dtEnfnFechaBoletaPago.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Boleta Pago" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaBoletaPagoRegistroEntrega + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtEnfnFechaBoletaPago.Date + " | ";
                    }
                    if (RegistroExistente.FechaRegresoRegistro != dtEnfnFechaRegresoRegistro.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Regreso registro" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaRegresoRegistro + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtEnfnFechaRegresoRegistro.Date + " | ";
                    }
                    if (RegistroExistente.FechaSalida != dtEnfnFechaSalida.Date)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Salida" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.FechaSalida + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + dtEnfnFechaSalida.Date + " | ";
                    }
                    if (RegistroExistente.ObservacionesTramiteTerminado != txtEnfnObservacionesSobreTramiteTerminado.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Entregas-Observaciones del tramite terminado" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ObservacionesTramiteTerminado + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtEnfnObservacionesSobreTramiteTerminado.Text  + " | ";
                    }

                    //Contabilidad 

                    if (RegistroExistente.ISR != (txtContISR.Value == null ? 0 : Convert.ToDecimal(txtContISR.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Contabilidad-I.S.R." + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ISR + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtContISR.Value == null ? 0 : Convert.ToDecimal(txtContISR.Value.ToString())).ToString() + " | ";
                    }
                    if (RegistroExistente.ISRcalculado != (txtContISRCalculado.Value == null ? 0 : Convert.ToDecimal(txtContISRCalculado.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Contabilidad-I.S.R. Calculado" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ISRcalculado + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtContISRCalculado.Value == null ? 0 : Convert.ToDecimal(txtContISRCalculado.Value.ToString())).ToString() + " | ";
                    }
                    if (RegistroExistente.AvaluoCatastral != (txtContAvaluoCatastral.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoCatastral.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Contabilidad-Avaluo Catastral" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.AvaluoCatastral + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtContAvaluoCatastral.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoCatastral.Value.ToString())).ToString() + " | ";
                    }
                    if (RegistroExistente.AvaluoFiscal != (txtContAvaluoFiscal.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoFiscal.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Contabilidad-Avaluo Fiscal" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.AvaluoFiscal + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtContAvaluoFiscal.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoFiscal.Value.ToString())).ToString() + " | ";
                    }
                    if (RegistroExistente.AvaluoComercial != (txtContAvaluoComercial.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoComercial.Value.ToString())))
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "Contabilidad-Avaluo Comercial" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.AvaluoComercial + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + (txtContAvaluoComercial.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoComercial.Value.ToString())).ToString() + " | ";
                    }




                    //PLD
                 

                    if (RegistroExistente.ActividadVulnerable != txtPldActividadVulnerable.Text)
                    {
                        logCambios.NombreCampo = logCambios.NombreCampo + "PLD-Actividad Vulnerable" + " | ";
                        logCambios.ValorOriginal = logCambios.ValorOriginal + RegistroExistente.ActividadVulnerable + " | ";
                        logCambios.ValorImputado = logCambios.ValorImputado + txtPldActividadVulnerable.Text  + " | ";
                    }

                    // guardar  bitacora de cambios


                    datosCrud.AltaBitacoraExpediente(logCambios);



                    #endregion



                    RegistroExistente.Otorga = txtExfnOtorga.Text;
                    RegistroExistente.AfavorDe = txtEXfnAfavorde.Text;

                    RegistroExistente.UbicacionPredio = txtExfnUbicacionPredio.Text;


                    //Aviso preventivo
                    RegistroExistente.FechaElaboracion = dtAPfnFechaElaboracion.Date;
                    RegistroExistente.FechaEnvioRPP = dtAPfnFechaEnvioAlRPP.Date;
                    RegistroExistente.EsTramitePorSistema = chkAPfnEsTramitePorSistema.Checked;
                    RegistroExistente.FechaPagoBoleta = dtAPfnFechaPagoBoleta.Date;
                    RegistroExistente.FechaRecibidoRPP = dtAPfnFechaRecibido.Date;

                    //Proyecto
                    RegistroExistente.NombreProyectista = cbPRfnProyectista.Value == null ? "" : cbPRfnProyectista.Value.ToString();
                    RegistroExistente.FechaAsignacionProyectista = dtPRfnFechaAsignacionProyectista.Date;
                    RegistroExistente.FechaPrevistaTerminoProyectista = dtPRfnFechaPrevistaTermino.Date;
                    RegistroExistente.FechaAvisoPreventivo = dtPRfnFechaAvisoPreventivo.Date;                 
                    RegistroExistente.ValorOperacion= txtPRfnValorOperacion.Value == null ? 0 : Convert.ToDecimal(txtPRfnValorOperacion.Value.ToString());

                    //Firmas
                    RegistroExistente.NotasFirma = txtFIfnNotasFirmas.Text;
                    RegistroExistente.Escritura = txtFIfnNumEscritura.Value == null ? 0 : Convert.ToInt32(txtFIfnNumEscritura.Value.ToString()); // validar nulos
                    RegistroExistente.Volumen = txtFIfnNumVolumen.Value == null ? 0 : Convert.ToInt32(txtFIfnNumVolumen.Value.ToString());
                    RegistroExistente.AplicaTraslado = chkFIfnAplicaTraslado.Checked;
                    RegistroExistente.FechaRecepcionTerminoEscritura = dtFIfnFechaRecepcionTerminoEscritura.Date;

                    //Aviso definitivo
                    RegistroExistente.FechaElaboracionDefinitivo = dtAdfnFechaElaboracion.Date;
                    RegistroExistente.FechaEnvioRPPDefinitivo = dtAdfnFechaEnvioRPP.Date;
                    RegistroExistente.EsTramitePorSistemaDefinitivo = chkAdfnEsTramitePorSistema.Checked;
                    RegistroExistente.FechaPagoBoletaDefinitivo = dtAdfnFechaPagoBoleta.Date;
                    RegistroExistente.FechaRecibidoRPPDefinitivo = dtAdfnFechaRecibido.Date;

                    //Escrituracion 
                    RegistroExistente.FechaRecibioTraslado = dtEsfnRecibioTraslado.Date;
                    RegistroExistente.FechaAsignacionMesa = dtAdfnFechaAsignacionMesa.Date;
                    RegistroExistente.FechaTerminoMesa = dtAdfnFechaTerminoTramite.Date;

                    //Entregas
                    RegistroExistente.ObservacionesEngrega = txtEnfnObservacionesEntrega.Text;
                    RegistroExistente.RegistroEntrega = chkEnfnRegistroSolicitado.Checked;
                    RegistroExistente.FechaRegistroEntrega = dtEnfnFechaRegistro.Date;
                    RegistroExistente.FechaBoletaPagoRegistroEntrega = dtEnfnFechaBoletaPago.Date;
                    RegistroExistente.FechaRegresoRegistro = dtEnfnFechaRegresoRegistro.Date;
                    RegistroExistente.FechaSalida = dtEnfnFechaSalida.Date;
                    RegistroExistente.ObservacionesTramiteTerminado = txtEnfnObservacionesSobreTramiteTerminado.Text;

                    //Contabilidad 
                    RegistroExistente.ISR = txtContISR .Value == null ? 0 : Convert.ToDecimal(txtContISR.Value.ToString());
                    RegistroExistente.ISRcalculado = txtContISRCalculado.Value == null ? 0 : Convert.ToDecimal(txtContISRCalculado.Value.ToString());
                    RegistroExistente.AvaluoCatastral = txtContAvaluoCatastral.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoCatastral.Value.ToString());
                    RegistroExistente.AvaluoFiscal = txtContAvaluoFiscal.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoFiscal.Value.ToString());
                    RegistroExistente.AvaluoComercial = txtContAvaluoComercial.Value == null ? 0 : Convert.ToDecimal(txtContAvaluoComercial.Value.ToString());


                    //PLD

                    RegistroExistente.ActividadVulnerable = txtPldActividadVulnerable.Text;

                    if (datosCrud.ActualizarExpediente(RegistroExistente))
                    {
                        ppEditarExpediente.JSProperties["cp_swMsg"] = "Registro Modificado!";
                        ppEditarExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;

                    }
                    else
                    {

                        ppEditarExpediente.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar Modificar el registro.";
                        ppEditarExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;

                    }

                }
                return;
            }
        }

        protected void trlEstatusExpedientes_DataBinding(object sender, EventArgs e)
        {
            trlEstatusExpedientes.DataSource = catEstatus.OrderBy(x => x.Orden);
            trlEstatusExpedientes.ExpandAll();
        }



        protected void cbExfnActo_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "IdActo";
            control.TextField = "TextoActo";
            control.DataSource = catActos;
        }

        protected void cbPRfnProyectista_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "Nombre";
            control.TextField = "Nombre";
            control.DataSource = catProyectistas;

        }
        void ApplyRules(FileManagerFolder folder)
        {
            FileManagerFolder[] folders = folder.GetFolders();



            for (int i = 0; i < folders.Length; i++)
            {
                FileManagerFolderAccessRule folderEditingRule = new FileManagerFolderAccessRule(folders[i].RelativeName);
                folderEditingRule.Edit = Rights.Deny;
                FileManagerFolderAccessRule folderContentEditingRule = new FileManagerFolderAccessRule(folders[i].RelativeName);
                folderContentEditingRule.EditContents = Rights.Allow;

                //ASPxFileManager1.SettingsPermissions.AccessRules.Add(folderEditingRule);
                //ASPxFileManager1.SettingsPermissions.AccessRules.Add(folderContentEditingRule);

                ApplyRules(folders[i]);
            }
        }

        FileManagerFolder root = null;
        FileManagerFolder GetRootFolder(FileManagerFolder folder)
        {


            if (folder.Parent == null)
            {
                root = folder;
            }
            else
            {
                GetRootFolder(folder.Parent);
            }

            return root;
        }


        protected void ppArchivos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {

        }

        protected void fmArchivosControl_CustomThumbnail(object source, FileManagerThumbnailCreateEventArgs e)
        {
            if (e.File == null)
            {
                return;
            }

            switch (((FileManagerFile)e.Item).Extension)
            {
                case ".pdf":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/application-pdf-2.ico";
                    break;
                case ".doc":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-document.ico";
                    break;
                case ".docx":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-document.ico";
                    break;
                case ".xlsx":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-spreadsheet.ico";
                    break;
                case ".xls":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-spreadsheet.ico";
                    break;
                case ".png":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".txt":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/text-x-generic.ico";
                    break;
                case ".rtf":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".gif":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".jpeg":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".jpg":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
            }
        }

        protected void gvAlertas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            Alertas nuevaAlerta = new Alertas();
            nuevaAlerta.IdAlerta = 0;
            nuevaAlerta.NumExpediente = NumExpSelccionadoAlerta;
            nuevaAlerta.FechaAlta = DateTime.Now;
            nuevaAlerta.AlertaActiva = true;
            nuevaAlerta.NomUsuarioInformante = UsuarioPagina.Nombre;
            nuevaAlerta.MensajeAlerta = e.NewValues["MensajeAlerta"].ToString();
            nuevaAlerta.Prioridad = "";



            // guardamos en BD

            datosCrud.AltaAlertas(nuevaAlerta);

            lsAlertas = datosExpediente.DameAlertasPorExpediente(NumExpSelccionadoAlerta).OrderByDescending(x => x.IdAlerta).ToList();


            // actualiozamos el control 

            e.Cancel = true;
            gvAlertas.CancelEdit();

            gvAlertas.DataBind();
        }


        protected void gvAlertas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //Validar si existen cambios de lo contrario no es necesario actulizar nada
            Boolean existenCambios = false;

            foreach (DictionaryEntry item in e.OldValues)
            {
                if (e.NewValues.Contains(item.Key))
                {
                    if (e.NewValues[item.Key] != null && !e.NewValues[item.Key].Equals(item.Value))
                    {
                        existenCambios = true;
                    }
                }
            }

            if (existenCambios == false)
            {
                e.Cancel = true;
                return;
            }


            var miAlerta = lsAlertas.Where(x => x.IdAlerta == int.Parse(e.Keys[0].ToString())).First();

            if (miAlerta != null)
            {
                miAlerta.MensajeAlerta = e.NewValues["MensajeAlerta"].ToString();
                miAlerta.AlertaActiva = Convert.ToBoolean(e.NewValues["AlertaActiva"].ToString());

                if (miAlerta.AlertaActiva == false)
                {
                    miAlerta.NomUsuarioCierra = UsuarioPagina.Nombre;
                }

            }

            datosCrud.ActualizarAlerta(miAlerta);


            lsAlertas = datosExpediente.DameAlertasPorExpediente(NumExpSelccionadoAlerta).OrderByDescending(x => x.IdAlerta).ToList();


            // actualiozamos el control 

            e.Cancel = true;
            gvAlertas.CancelEdit();

            gvAlertas.DataBind();


        }

        #endregion

     
    }
}