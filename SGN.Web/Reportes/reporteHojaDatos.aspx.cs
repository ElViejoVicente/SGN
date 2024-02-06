using DevExpress.XtraReports.UI;
using SGN.Negocio.Expediente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using DevExpress.DataAccess.ObjectBinding;

namespace SGN.Web.Reportes
{
    public partial class reporteHojaDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatosExpedientes datosExpediente = new DatosExpedientes();


            if (!Page.IsPostBack)
            {
                string idHojaDatos = Server.UrlEncode(Request.QueryString["idHojaDatos"]);

                //consutamos los datos de la hoja de datos para tenerlos en la memoria del reporte.
                ListaHojaDatos datosReporte = new ListaHojaDatos();


                datosReporte = datosExpediente.DameHojaDatosDetalle(Convert.ToInt32(idHojaDatos));

                XtraReport report = new XtraReport();
                report.CreateDocument();
                XtraHojaDatos hojaDatos = new XtraHojaDatos();

                DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
                objectDataSource.DataSource = datosReporte; 

                objectDataSource.Fill();
  
                hojaDatos.DataSource = objectDataSource;

                hojaDatos.RequestParameters = false;
                hojaDatos.CreateDocument();
                report.Pages.Add(hojaDatos.Pages[0]);


                reportePrinsipalView.OpenReport(report);
            }
        }
    }
}