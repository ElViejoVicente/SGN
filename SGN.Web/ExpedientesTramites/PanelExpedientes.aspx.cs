using DevExpress.CodeParser;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;

using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
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
        DatosUsuario datosUsuario = new DatosUsuario();
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


        #region Funciones
        private void DameCatalogos()
        {

            catEstatus = datosCrud.ConsultaCatEstatus();
            catActos = datosCrud.ConsultaCatActos();
            catProyectistas = datosUsuario.DameDatosUsuario(-1).Where(x=> x.EsProyectista==true).ToList();

            cbActosNuevo.DataBind();
            cbExfnActo.DataBind();
            cbPRfnProyectista.DataBind();

        }
        #endregion

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dtFechaInicio.Date = DateTime.Now.Date;
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
                lsExpediente = datosExpediente.DameListaExpediente(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date);// cargamos registros
                gvExpedientes.DataBind();
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

        }

        protected void ppOrdenNuevoExpediente_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "NuevoExpediente")
            {
                dtFechaIngresoNuevo.Date = DateTime.Now;
                return;

            }

            if (e.Parameter == "guardar")
            {
                Expedientes nuevoRegistro = new Expedientes();
                nuevoRegistro.IdExpediente = "123456";
                nuevoRegistro.numReciboPago = txtNumReciboNuevo.Text;

                nuevoRegistro.FechaIngreso = dtFechaIngresoNuevo.Date;
                nuevoRegistro.IdActo = Convert.ToInt32(cbActosNuevo.Value.ToString());
                nuevoRegistro.Otorga = txtOtorgaNuevo.Text;
                nuevoRegistro.AfavorDe = txtAfavorDeNuevo.Text;
                nuevoRegistro.OperacionProyectada = txtOperacionProyectadaNuevo.Text;
                nuevoRegistro.UbicacionPredio = txtUbicacionPredioNuevo.Text;
                nuevoRegistro.Faltantes = txtDocumentoFaltantesNuevo.Text;

                if (string.IsNullOrEmpty(txtDocumentoFaltantesNuevo.Text))
                {
                    nuevoRegistro.IdEstatus = "EX1";
                }
                else
                {
                    nuevoRegistro.IdEstatus = "EX2";
                }


                if (datosCrud.AltaExpediente(nuevoRegistro))
                {
                    ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Registro creado!";
                    ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;
                    return;
                }
                else
                {

                    ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar guardar la nueva accion.";
                    ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;
                    return;
                }



            }

        }

        protected void cbActosNuevo_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "IdActo";
            control.TextField = "TextoActo";
            control.DataSource = catActos;
        }

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

        protected void ppCambiarEstatus_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "CargarEstados")
            {

                string numExpedinte = gvExpedientes.GetSelectedFieldValues("IdExpediente")[0].ToString();


                txtProyecSelecEstatus.Text = numExpedinte;

                rbEstados.DataBind();               
                return;

            }
        }

        protected void ppEditarExpediente_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "CargarRegistros")
            {
                Expedientes registroExistente = new Expedientes();

                string numExpediente = gvExpedientes.GetSelectedFieldValues("IdExpediente")[0].ToString();

                registroExistente = datosCrud.ConsultaExpediente(numExp: numExpediente);

                if (registroExistente != null)
                {

                    // cargamos los campos en el form layaout

                    //Expediente
                    txtExfnNumeroRecibo.Text = registroExistente.numReciboPago;
                    dtExfnFechaIngreso.Date = registroExistente.FechaIngreso;

                    cbExfnActo.Value = registroExistente.IdActo;
                    cbExfnActo.SelectedIndex = catActos.FindIndex(w => w.IdActo == registroExistente.IdActo);

                    txtExfnOtorga.Text = registroExistente.Otorga;
                    txtEXfnAfavorde.Text = registroExistente.AfavorDe;
                    txtExfnOperacionProyectada.Text = registroExistente.OperacionProyectada;
                    txtExfnUbicacionPredio.Text = registroExistente.UbicacionPredio;
                    txtExfnDocumentosFaltantes.Text = registroExistente.Faltantes;

                    //Aviso preventivo
                    dtAPfnFechaElaboracion.Date = registroExistente.FechaElaboracion;
                    dtAPfnFechaEnvioAlRPP.Date = registroExistente.FechaEnvioRPP;
                    chkAPfnEsTramitePorSistema.Checked = registroExistente.EsTramitePorSistema;
                    dtAPfnFechaPagoBoleta.Date = registroExistente.FechaPagoBoleta;
                    dtAPfnFechaRecibido.Date = registroExistente.FechaRecibidoRPP;

                    //Proyecto
                    cbPRfnProyectista.Value = registroExistente.NombreProyectista;
                    cbPRfnProyectista.SelectedIndex = catProyectistas.FindIndex(w => w.Nombre == registroExistente.NombreProyectista);


                    //dtPRfnFechaAsignacionProyectista.Date =
                    //dtPRfnFechaPrevistaTermino.Date =
                    //dtPRfnFechaAvisoPreventivo.Date =
                    //txtPRfnISR.Text =

                    ////Firmas
                    //txtFIfnNotasFirmas.Text =
                    //txtFIfnNumEscritura.Text =
                    //txtFIfnNumVolumen.Text =
                    //chkFIfnAplicaTraslado.Checked =
                    //dtFIfnFechaRecepcionTerminoEscritura.Date =

                    ////Aviso definitivo
                    //dtAdfnFechaElaboracion.Date =
                    //dtAdfnFechaEnvioRPP.Date =
                    //chkAdfnEsTramitePorSistema.Checked =
                    //dtAdfnFechaPagoBoleta.Date =
                    //dtAdfnFechaRecibido.Date =

                    ////Escrituracion 
                    //dtEsfnRecibioTraslado.Date =
                    //dtAdfnFechaAsignacionMesa.Date =
                    //dtAdfnFechaTerminoTramite.Date =

                    ////Entregas
                    //txtEnfnObservacionesEntrega.Text =
                    //chkEnfnRegistroSolicitado.Checked =
                    //dtEnfnFechaRegistro.Date =
                    //dtEnfnFechaBoletaPago.Date =
                    //dtEnfnFechaRegresoRegistro.Date =
                    //dtEnfnFechaSalida.Date =
                    //txtEnfnObservacionesSobreTramiteTerminado.Text =




                }


                return;
            }

            if (e.Parameter== "guardarCambios")
            {
                return;
            }
        }

        protected void rbEstados_DataBinding(object sender, EventArgs e)
        {
            rbEstados.ValueField = "IdEstatus";
            rbEstados.TextField = "Descripcion";            
            rbEstados.DataSource = catEstatus;
     
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
    }
}