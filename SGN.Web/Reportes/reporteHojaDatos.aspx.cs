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


                // se rellenan los abjetos a el dataSet


                Negocio.Reportes.dsHojaDatos.HojaDatosDataTable dtHojaDatos =
                    new Negocio.Reportes.dsHojaDatos.HojaDatosDataTable();

                Negocio.Reportes.dsHojaDatos origen = new Negocio.Reportes.dsHojaDatos();

                origen.HojaDatos.AddHojaDatosRow(datosReporte.IdEstatus, datosReporte.TextoEstatus, datosReporte.IdHojaDatos, datosReporte.numExpediente,
                    datosReporte.NombreAsesor, datosReporte.FechaIngreso, datosReporte.FechaCompleto, datosReporte.IdUsuarioResponsable, datosReporte.IdEquipoResponsable,
                    datosReporte.NumbreUsuarioTramita, datosReporte.NumTelCelular1, datosReporte.NumTelCelular2, datosReporte.CorreoElectronico, datosReporte.TextoActo,
                    datosReporte.TextoVariante, datosReporte.Otorga, datosReporte.AfavorDe);



                foreach (var item in datosReporte.DetalleParticipantes.Where(x=> x.FiguraOperacion== "Otorga o Solicita").ToList())
                {
                    origen.DatosParticipantesOtorga.AddDatosParticipantesOtorgaRow(item.IdRegistro, item.IdHojaDatos, item.FiguraOperacion, item.RolOperacion,
                        item.Nombres, item.ApellidoPaterno, item.ApellidoMaterno, item.FechaNacimiento, item.Sexo, item.Ocupacion, item.EstadoCivil,
                        item.RegimenConyugal, item.SabeLeerEscribir, item.Notas);
                }

                foreach (var item in datosReporte.DetalleParticipantes.Where(x => x.FiguraOperacion == "A favor de").ToList())
                {
                    origen.DatosParticipantesAfavorDe.AddDatosParticipantesAfavorDeRow(item.IdRegistro, item.IdHojaDatos, item.FiguraOperacion, item.RolOperacion,
                     item.Nombres, item.ApellidoPaterno, item.ApellidoMaterno, item.FechaNacimiento, item.Sexo, item.Ocupacion, item.EstadoCivil,
                     item.RegimenConyugal, item.SabeLeerEscribir, item.Notas);
                }



                XtraReport report = new XtraReport();
                report.CreateDocument();
                XtraHojaDatos hojaDatos = new XtraHojaDatos();

               // DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
                //objectDataSource.DataSource = origen; 

                //objectDataSource.Fill();
  
                hojaDatos.DataSource = origen;

                hojaDatos.RequestParameters = false;
                hojaDatos.CreateDocument();
                report.Pages.Add(hojaDatos.Pages[0]);


                reportePrinsipalView.OpenReport(report);
            }
        }
    }
}