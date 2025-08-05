using System;
using DevExpress.XtraReports.UI;
using SGN.Negocio.ExpedienteUnico;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SGN.Negocio.Expediente;
using SGN.Negocio.ORM;

namespace SGN.Web.Reportes
{
    public partial class reporteFormConociendoCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatosExpedienteUnico datosExpedienteUnico = new DatosExpedienteUnico();
            DatosExpedientes datosExpedientes = new DatosExpedientes();

            if (!IsPostBack)
            {
                string idRegistroCliente = Server.UrlEncode(Request.QueryString["idRegistro"]);
                ListaExpedienteUnico datosReporte = new ListaExpedienteUnico();

                datosReporte = datosExpedienteUnico.DameExpedienteUnico(int.Parse(idRegistroCliente));

                Negocio.Reportes.dsExpedienteUnico.ExpedienteUnicoDataTable dtExUnico = new Negocio.Reportes.dsExpedienteUnico.ExpedienteUnicoDataTable();

                Negocio.Reportes.dsExpedienteUnico origen = new Negocio.Reportes.dsExpedienteUnico();


                XtraReport reporte = new XtraReport();
                reporte.CreateDocument();

                XtraFormConocCliente exFormConocimientoCliente = new XtraFormConocCliente();

                exFormConocimientoCliente.RequestParameters = false;
                exFormConocimientoCliente.Parameters["IdRegistro"].Value = datosReporte.IdRegistro;











                exFormConocimientoCliente.CreateDocument();
                reporte.Pages.AddRange(exFormConocimientoCliente.Pages);

                reportePrinsipalView.OpenReport(reporte);

            }

        }
    }
}