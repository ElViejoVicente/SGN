using DevExpress.Web;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.XtraEditors.Filtering.Templates;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ExpedienteUnico;
using SGN.Negocio.ExpedienteUnico.ANEXOS;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;
using SGN.Web.Catalogos;
using SGN.Web.Controles.Servidor;
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

        public List<Cat_Anexos> CatAnexos
        {
            get

            {
                List<Cat_Anexos> sseCatAnexos = new List<Cat_Anexos>();
                if (this.Session["sseCatAnexos"] != null)
                {
                    sseCatAnexos = (List<Cat_Anexos>)this.Session["sseCatAnexos"];
                }

                return sseCatAnexos;
            }
            set
            {
                this.Session["sseCatAnexos"] = value;
            }

        }


        public ExpedienteUnicoControl ExpedienteControlSeleccionado
        {
            get

            {
                ExpedienteUnicoControl ssExpedienteUnicoControl = null;
                if (this.Session["ssExpedienteUnicoControl"] != null)
                {
                    ssExpedienteUnicoControl = (ExpedienteUnicoControl)Session["ssExpedienteUnicoControl"];
                }

                return ssExpedienteUnicoControl;
            }
            set
            {
                this.Session["ssExpedienteUnicoControl"] = value;
            }

        }


        public AnexoBase AnexoSeleccionado
        {
            get

            {
                AnexoBase ssAnexoSeleccionado = null;
                if (this.Session["ssAnexoSeleccionado"] != null)
                {
                    ssAnexoSeleccionado = (AnexoBase)Session["ssAnexoSeleccionado"];
                }

                return ssAnexoSeleccionado;
            }
            set
            {
                this.Session["ssAnexoSeleccionado"] = value;
            }

        }


        #endregion

        #region Funciones
        private void DameCatalogos()

        {
            try
            {
             
                catPaises = datosCrud.ConsultaCatPaises();

                CatAnexos = datosCrud.ConsultaCatAnexos().Where(x=> x.Activo).ToList();

                //  cbPaisNacimiento.



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void CargarDatos()
        {


            //limpieza de la variable que se puede quedar en sessionstate 
            ExpedienteControlSeleccionado = new ExpedienteUnicoControl();


             lsExpedienteUnico = datosExpUnico.DameListaExpedienteUnicoV2(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date,
                    todasLasFechas: false).OrderByDescending(x => x.FechaIngreso).ToList();// cargamos todos los registros

                ActualizarKpi();
                gvExpedienteUnico.DataBind();
                return; 
            
        }

        protected string GetNombreCompleto(object nombres, object apellidoPaterno, object apellidoMaterno)
        {
            return string.Join(" ", new[] { Convert.ToString(nombres), Convert.ToString(apellidoPaterno), Convert.ToString(apellidoMaterno) }.Where(x => !string.IsNullOrWhiteSpace(x))).Trim();
        }

        protected string GetDescripcionParticipante(object rol, object sexo, object ocupacion)
        {
            string textoSexo = Convert.ToString(sexo).Equals("F", StringComparison.OrdinalIgnoreCase) ? "F" : "M";
            return string.Format("{0} · {1} · {2}", Convert.ToString(rol), textoSexo, Convert.ToString(ocupacion));
        }

        protected bool TieneExpediente(object idExpedienteUnico) { return !string.IsNullOrWhiteSpace(Convert.ToString(idExpedienteUnico)); }

        protected string GetAnexo(object nombreAnexo, object idExpedienteUnico)
        {
            string anexo = Convert.ToString(nombreAnexo);
            return string.IsNullOrWhiteSpace(anexo) ? Convert.ToString(idExpedienteUnico) : anexo;
        }

        protected string FormatFecha(object valor)
        {
            DateTime fecha;
            return DateTime.TryParse(Convert.ToString(valor), out fecha) && fecha.Year > 1900 ? fecha.ToString("dd/MM/yyyy HH:mm") : "Sin fecha";
        }

        protected string GetTextoEncabezado(string groupText)
        {
            int separador = groupText.IndexOf(':');
            string encabezado = separador >= 0 ? groupText.Substring(separador + 1).Trim() : groupText;
            return HttpUtility.HtmlDecode(encabezado);
        }

        private void ActualizarKpi()
        {
            int expedientes = lsExpedienteUnico
                .Select(x => x.IdExpediente)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count();
            int conAnexo = lsExpedienteUnico.Count(x => !string.IsNullOrWhiteSpace(x.IdExpedienteUnico));
            int sinAnexo = lsExpedienteUnico.Count - conAnexo;
            decimal cumplimiento = lsExpedienteUnico.Count == 0 ? 0 : (decimal)conAnexo / lsExpedienteUnico.Count * 100;

            lblKpiExpedientes.Text = expedientes.ToString("N0");
            lblKpiSinAnexo.Text = sinAnexo.ToString("N0");
            lblKpiConAnexo.Text = conAnexo.ToString("N0");
            lblKpiCumplimiento.Text = cumplimiento.ToString("0.#") + "%";

            gvExpedienteUnico.JSProperties["cp_KpiExpedientes"] = lblKpiExpedientes.Text;
            gvExpedienteUnico.JSProperties["cp_KpiSinAnexo"] = lblKpiSinAnexo.Text;
            gvExpedienteUnico.JSProperties["cp_KpiConAnexo"] = lblKpiConAnexo.Text;
            gvExpedienteUnico.JSProperties["cp_KpiCumplimiento"] = lblKpiCumplimiento.Text;
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
