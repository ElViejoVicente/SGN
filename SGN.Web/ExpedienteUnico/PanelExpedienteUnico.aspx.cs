using DevExpress.Web;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ExpedienteUnico;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;
using SGN.Web.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.ExpedienteUnico
{
    public partial class PanelExpedienteUnico : System.Web.UI.Page
    {
        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
        DatosExpedienteUnico datosExpUnico = new DatosExpedienteUnico();

        public List<ListaExpedienteUnico> lsExpedienteUnico
        {
            get

            {
                List<ListaExpedienteUnico> sseListaExpediente = new List<ListaExpedienteUnico>();
                if (this.Session["sseListaExpedienteUnico"] != null)
                {
                    sseListaExpediente = (List<ListaExpedienteUnico>)this.Session["sseListaExpedienteUnico"];
                }

                return sseListaExpediente;
            }
            set
            {
                this.Session["sseListaExpedienteUnico"] = value;
            }

        }

        public List<Cat_Paises> catPaises
        {
            get

            {
                List<Cat_Paises> sseCatPaises = new List<Cat_Paises>();
                if (this.Session["sseCatPaises"] != null)
                {
                    sseCatPaises = (List<Cat_Paises>)this.Session["sseCatPaises"];
                }

                return sseCatPaises;
            }
            set
            {
                this.Session["sseCatPaises"] = value;
            }

        }


        #endregion

        #region Funciones
        private void DameCatalogos()

        {
            try
            {

                catPaises = datosCrud.ConsultaCatPaises();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dtFechaInicio.Date = DateTime.Now.Date.AddDays(-15);
                dtFechaFin.Date = DateTime.Now.Date;
                DameCatalogos();
            }
        }

        protected void gvExpedienteUnico_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsExpedienteUnico;

        }

        protected void gvExpedienteUnico_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

            if (e.Parameters == "CargarRegistros")
            {
               
                    lsExpedienteUnico = datosExpUnico.DameListaExpedienteUnico (fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date,  
                        todasLasFechas: chkBusquedaCompleta.Checked).OrderByDescending(x => x.FechaIngreso).ToList();// cargamos todos los registros


                gvExpedienteUnico.DataBind();
                return;
            }

        }

        protected void gvExpedienteUnico_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {

        }

        protected void gvExpedienteUnico_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {

        }




        #endregion

        protected void cbSexoOtorgaSolicita_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbPaisNacimiento_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;

        }

        protected void cbPaisNacimiento_DataBinding(object sender, EventArgs e)
        {

        }

        protected void cbPaisNacionalidad_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbPaisNacionalidad_DataBinding(object sender, EventArgs e)
        {

        }

        protected void cbPaisDomicilio_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;

        }

        protected void cbPaisDomicilio_DataBinding(object sender, EventArgs e)
        {

        }

        protected void cbPaisRazonSocial_Init(object sender, EventArgs e)
        {

        }

        protected void cbPaisRazonSocial_DataBinding(object sender, EventArgs e)
        {

        }

        protected void cbTipoRegimen_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;

        }
    }
}