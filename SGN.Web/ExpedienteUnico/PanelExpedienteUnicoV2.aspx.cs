using SGN.Web.Controles.Servidor;
using DevExpress.Web;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.XtraEditors.Filtering.Templates;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ExpedienteUnico;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;
using SGN.Web.Catalogos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.ExpedienteUnico
{
    public partial class PanelExpedienteUnicoV2 : PageBase
    {
        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
        DatosExpedienteUnico datosExpUnico = new DatosExpedienteUnico();

        public List<ListaExpedienteUnicoV2> lsExpedienteUnico
        {
            get

            {
                List<ListaExpedienteUnicoV2> sseListaExpediente = new List<ListaExpedienteUnicoV2>();
                if (this.Session["ListaExpedienteUnicoV2"] != null)
                {
                    sseListaExpediente = (List<ListaExpedienteUnicoV2>)this.Session["ListaExpedienteUnicoV2"];
                }

                return sseListaExpediente;
            }
            set
            {
                this.Session["ListaExpedienteUnicoV2"] = value;
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

                //  cbPaisNacimiento.



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void CargarDatos()
        {
          

                lsExpedienteUnico = datosExpUnico.DameListaExpedienteUnicoV2(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date,
                    todasLasFechas: false).OrderByDescending(x => x.FechaIngreso).ToList();// cargamos todos los registros


                gvExpedienteUnico.DataBind();
                return; 
            
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["UsuarioConsultaLN"] = UsuarioPagina.Nombre;
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

                CargarDatos();
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
            ASPxComboBox cb = (ASPxComboBox)sender;

            //cb.DataSource = catPaises;
            cb.TextField = "TextoPais";
            cb.ValueField = "TextoPais";
            cb.ValueType = typeof(string);

            cb.DataSource = catPaises;
        }

        protected void cbPaisNacionalidad_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbPaisNacionalidad_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            //cb.DataSource = catPaises;
            cb.TextField = "TextoPais";
            cb.ValueField = "TextoPais";
            cb.ValueType = typeof(string);

            cb.DataSource = catPaises;
        }

        protected void cbPaisDomicilio_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;

        }

        protected void cbPaisDomicilio_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            //cb.DataSource = catPaises;
            cb.TextField = "TextoPais";
            cb.ValueField = "TextoPais";
            cb.ValueType = typeof(string);

            cb.DataSource = catPaises;
        }

        protected void cbPaisRazonSocial_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.DataSource = catPaises;
        }

        protected void cbPaisRazonSocial_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            //cb.DataSource = catPaises;
            cb.TextField = "TextoPais";
            cb.ValueField = "TextoPais";
            cb.ValueType = typeof(string);

            cb.DataSource = catPaises;
        }

        protected void cbTipoRegimen_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;

        }


    }
}