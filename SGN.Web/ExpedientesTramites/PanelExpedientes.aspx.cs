using DevExpress.CodeParser;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ORM;

using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.ExpedientesTramites
{
    public partial class PanelExpedientes : PageBase
    {

        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
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


            catActos = datosCrud.ConsultaCatActos();
            cbActosNuevo.DataBind();


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

        }

        protected void ppEditarExpediente_WindowCallback(object source, PopupWindowCallbackArgs e)
        {

        }
    }
}