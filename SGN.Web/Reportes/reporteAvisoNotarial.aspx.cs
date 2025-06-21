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
    public partial class reporteAvisoNotarial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {


                string idRegistroCliente = Server.UrlEncode(Request.QueryString["idRegistro"]);


                // falta consulta de datos para llenar parametros del reporte







                XtraReport reporte = new XtraReport();
                reporte.CreateDocument();

                XtraAvisoNotarialHoja1 exAvisoHoja1 = new XtraAvisoNotarialHoja1();
                XtraAvisoNotarialHoja2 exAvisoHoja2 = new XtraAvisoNotarialHoja2();



                exAvisoHoja1.RequestParameters= false;
                exAvisoHoja2.RequestParameters = false;

                exAvisoHoja1.Parameters["IdRegistroCliente"].Value = idRegistroCliente;
                exAvisoHoja1.CreateDocument();


                exAvisoHoja2.Parameters["IdRegistroCliente"].Value = idRegistroCliente;
                exAvisoHoja2.CreateDocument();




                reporte.Pages.AddRange(exAvisoHoja1.Pages);

                reporte.Pages.AddRange(exAvisoHoja2.Pages);





                reportePrinsipalView.OpenReport(reporte);

            }

        }
    }
}