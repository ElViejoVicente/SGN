using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.Reportes
{
    public partial class reporteHojaDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string idHojaDatos = Server.UrlEncode(Request.QueryString["idHojaDatos"]);


                XtraReport report = new XtraReport();
                report.CreateDocument();
                XtraHojaDatos hojaDatos = new XtraHojaDatos();

                hojaDatos.RequestParameters = false;
                hojaDatos.CreateDocument();
                report.Pages.Add(hojaDatos.Pages[0]);


                reportePrinsipalView.OpenReport(report);
            }
        }
    }
}