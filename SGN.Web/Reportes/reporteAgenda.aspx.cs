using DevExpress.XtraReports.UI;
using SGN.Negocio.Agenda;
using SGN.Negocio.Expediente;
using SGN.Negocio.ExpedienteUnico;
using SGN.Negocio.ORM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.Reportes
{
    public partial class ReporteAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            DatosAgenda datosAgenda = new DatosAgenda();

            if (!IsPostBack)
            {


                // datos 

                {
                    string fechaAgendaStrin = Server.UrlEncode(Request.QueryString["fechaAgenda"]);
                    DateTime fechaAgenda = DateTime.Parse(fechaAgendaStrin);


                    List<AgendaReporte> ListaAgendaReporte = new List<AgendaReporte>();
                    ListaAgendaReporte = datosAgenda.DameAlertasPorExpediente(fechaConsulta: fechaAgenda.ToString("yyyy-MM-dd"));

                    Negocio.Reportes.dsAgenda ListaAgenda = new Negocio.Reportes.dsAgenda();

                    if (ListaAgendaReporte.Count > 0)
                    {
                        Int32 contador = 0;
                        foreach (var item in ListaAgendaReporte)
                        {
                            contador++;
                            int.TryParse(item.NumeroEscritura?.ToString(), out int numero);

                            ListaAgenda.AgendaNotaria.AddAgendaNotariaRow
                                (agHorario: item.Horario,
                                agMesa: item.Mesa,
                                agExpediente: item.IdExpediente,
                                agVendedor: item.Otorga,
                                agComprador: item.AfavorDe,
                                agActo: item.Acto+ "-" +item.Variante,
                                agValorOperacion: item.ValorOperacion.ToString("C", new CultureInfo("es-MX")),
                                agNumeroEscritura: numero,
                                agISR: item.ISR,
                                agActividadVulnerable: item.ActividadVulnerable,
                                agLugar: item.TextoRecurso, agIDRegistro: contador);



                        }
                    }


                    string fechaTexto = fechaAgenda.ToString("dddd dd 'de' MMMM yyyy", new CultureInfo("es-MX") );


                    XtraReport reporte = new XtraReport();
                    reporte.CreateDocument();
                    XtraAgenda exAgenda = new XtraAgenda();



                    if (ListaAgendaReporte.Count > 0)
                    {
                        exAgenda.DataSource = ListaAgenda;
                        exAgenda.RequestParameters = false;
                        exAgenda.Parameters["paramFecha"].Value = fechaTexto;
                        exAgenda.CreateDocument();
                        reporte.Pages.Add(exAgenda.Pages[0]);
                    }



                    reportePrinsipalView.OpenReport(reporte);

                }

            }
        }
    }
}