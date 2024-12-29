using System;
using DevExpress.XtraReports.UI;
using SGN.Negocio.ExpedienteUnico;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.Reportes
{
    public partial class reporteExUnico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatosExpedienteUnico datosExpedienteUnico = new DatosExpedienteUnico();

            if (!IsPostBack)
            {

                string idRegistroCliente = Server.UrlEncode(Request.QueryString["idRegistro"]);
                ListaExpedienteUnico datosReporte = new ListaExpedienteUnico();

                datosReporte = datosExpedienteUnico.DameExpedienteUnico(int.Parse(idRegistroCliente));

                Negocio.Reportes.dsExpedienteUnico.ExpedienteUnicoDataTable dtExUnico = new Negocio.Reportes.dsExpedienteUnico.ExpedienteUnicoDataTable();

                Negocio.Reportes.dsExpedienteUnico origen = new Negocio.Reportes.dsExpedienteUnico();



                origen.ExpedienteUnico.AddExpedienteUnicoRow(
                        IdExpediente: datosReporte.IdExpediente, IdEstatus: datosReporte.IdEstatus, TextoEstatus: datosReporte.TextoEstatus,
                    TextoActo: datosReporte.TextoEstatus, FechaIngreso: datosReporte.FechaIngreso, TextoVariante: datosReporte.TextoVariante,
                    RequiereExUnico: datosReporte.RequiereExUnico, IdRegistro: datosReporte.IdRegistro, IdHojaDatos: datosReporte.IdHojaDatos,
                    FiguraOperacion: datosReporte.FiguraOperacion, RolOperacion: datosReporte.RolOperacion, Nombres: datosReporte.Nombres,
                    ApellidoPaterno: datosReporte.ApellidoPaterno, ApellidoMaterno: datosReporte.ApellidoMaterno, FechaNacimiento: datosReporte.FechaNacimiento,
                    Sexo: datosReporte.Sexo, Ocupacion: datosReporte.Ocupacion, EstadoCivil: datosReporte.EstadoCivil, RegimenConyugal: datosReporte.RegimenConyugal,
                    SabeLeerEscribir: datosReporte.SabeLeerEscribir, Notas: datosReporte.Notas, TipoRegimen: datosReporte.TipoRegimen,
                    PaisNacimiento: datosReporte.PaisNacimiento, PaisNacionalidad: datosReporte.PaisNacionalidad, Domicilio: datosReporte.Domicilio,
                    NumeroExterior: datosReporte.NumeroExterior, NumeroInterior: datosReporte.NumeroInterior, Colonia: datosReporte.Colonia,
                    Municipio: datosReporte.Municipio, Ciudad: datosReporte.Ciudad, Estado: datosReporte.Estado, PaisDomicilio: datosReporte.PaisDomicilio,
                    CP: datosReporte.CP, NumeroTefonico: datosReporte.NumeroTefonico, CorreoElectronico: datosReporte.CorreoElectronico, Curp: datosReporte.Curp,
                    Rfc: datosReporte.Rfc, NombreIdentificacionID: datosReporte.NombreIdentificacionID, AutoridadEmiteID: datosReporte.AutoridadEmiteID,
                    NumeroSerieID: datosReporte.NumeroSerieID, RazonSocial: datosReporte.RazonSocial, FechaConstitucion: datosReporte.FechaConstitucion,
                    PaisRazonSocial: datosReporte.PaisRazonSocial, ActividadRazonSocial: datosReporte.ActividadRazonSocial, SeValidoEnListaNegra: datosReporte.SeValidoEnListaNegra,
                    FechaPrimeraValidacion: datosReporte.FechaPrimeraValidacion, ObsePrimeraValidacion: datosReporte.ObsePrimeraValidacion,
                    FechaSegundaValicacion: datosReporte.FechaSegundaValicacion, ObseSegundaValidacion: datosReporte.ObseSegundaValidacion, Resumen: datosReporte.Resumen,
                    ResumenIdentificacion: datosReporte.NombreIdentificacionID+","+ datosReporte.AutoridadEmiteID+","+datosReporte.NumeroSerieID
                    );

                XtraReport reporte = new XtraReport();
                reporte.CreateDocument();
                XtraExUnicoPersonaFisica exUnicoPerFisica = new XtraExUnicoPersonaFisica();




                if (datosReporte.TipoRegimen== "Fisica")
                {
                    exUnicoPerFisica.DataSource = origen;
                    exUnicoPerFisica.RequestParameters = false;

                    exUnicoPerFisica.Parameters["ActividadVulnerable"].Value= "Venta de casas robadas";
                    exUnicoPerFisica.Parameters["Anio"].Value = "aa";
                    exUnicoPerFisica.Parameters["Asesor"].Value = "aa";
                    exUnicoPerFisica.Parameters["Dia"].Value = 10;
                    exUnicoPerFisica.Parameters["Mes"].Value = "Dic";
                    exUnicoPerFisica.Parameters["NumEscritura"].Value = "150";
                    exUnicoPerFisica.Parameters["NumExpediente"].Value = "128-12-2024S";
                    exUnicoPerFisica.Parameters["Volumen"].Value = "zzz4";


                    exUnicoPerFisica.CreateDocument();
                    reporte.Pages.AddRange(exUnicoPerFisica.Pages);



                }



                if (datosReporte.TipoRegimen == "Moral")
                {

                }


                if (datosReporte.TipoRegimen == "Apoderado")
                {

                }

                reportePrinsipalView.OpenReport(reporte);

            }

        }
    }
}