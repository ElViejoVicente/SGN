using System;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SGN.Negocio.ExpedienteUnico;
using SGN.Negocio.ExpedienteUnico.ANEXOS;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SGN.Negocio.Expediente;
using SGN.Negocio.ORM;
using SGN.Negocio.CRUD;
using SGN.Web.Reportes;

namespace SGN.Web.ReportesAnexos
{
    public partial class reporteExUnicoV2 : System.Web.UI.Page
    {
        private readonly DatosCrud datosCrud = new DatosCrud();
        protected void Page_Load(object sender, EventArgs e)
        {

            ExpedienteUnicoControl datosExpedienteUnico = new ExpedienteUnicoControl();

            // instancias 



            if (!IsPostBack)
            {

                try
                {

                    string idExpedienteUnico = (Request.QueryString["IdExpedienteUnico"]
                        ?? Request.QueryString["idRegistro"]
                        ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(idExpedienteUnico))
                        throw new InvalidOperationException("No se recibió el IdExpedienteUnico para generar el reporte.");
                    List<Cat_Anexos> catalogoAnexos = new List<Cat_Anexos>();


                    datosExpedienteUnico = datosCrud.ConsultaExpedienteUnicoControl(IdExpedienteUnico: idExpedienteUnico, NumeroVersion: 0);  //NumeroVersion =0 para tener siempre la ultima version
                    catalogoAnexos = datosCrud.ConsultaCatAnexos().ToList();


                    if (datosExpedienteUnico != null && !string.IsNullOrWhiteSpace(datosExpedienteUnico.AnexoSnapshot))
                    {

                        XtraReport reporte = new XtraReport();
                        reporte.CreateDocument();



                        Cat_Anexos catalogo = catalogoAnexos.FirstOrDefault(x =>
                            string.Equals(x.NombreAnexo, datosExpedienteUnico.NombreDelAnexo, StringComparison.OrdinalIgnoreCase));
                        if (catalogo == null)
                            throw new InvalidOperationException("No se encontró en el catálogo el anexo: " + datosExpedienteUnico.NombreDelAnexo + ".");

                        Type tipoAnexo = ResolverTipo(catalogo.ClassInterna, typeof(AnexoBase).Assembly, typeof(AnexoBase));
                        if (tipoAnexo == null)
                            throw new InvalidOperationException("ClassInterna no corresponde a una clase de anexo válida: " + catalogo.ClassInterna + ".");

                        Type tipoReporte = ResolverTipo(catalogo.ReportInterna, typeof(reporteExUnicoV2).Assembly, typeof(XtraReport));
                        if (tipoReporte == null)
                            throw new InvalidOperationException("ReportInterna no corresponde a un XtraReport válido: " + catalogo.ReportInterna + ".");

                        object anexo = DeserializarAnexo(datosExpedienteUnico.AnexoSnapshot, tipoAnexo);
                        XtraReport reporteFormato = (XtraReport)Activator.CreateInstance(tipoReporte);


                        // deserizalizar   datosExpedienteUnico.AnexoSnapshot; en base el formato guardado el nombre de la clase esta en        datosExpedienteUnico.NombreDelAnexo;


                        // XtraReports no admite un POCO individual como DataSource.
                        // El arreglo conserva el tipo concreto del anexo y genera un solo registro.
                        Array datosReporte = Array.CreateInstance(tipoAnexo, 1);
                        datosReporte.SetValue(anexo, 0);
                        reporteFormato.DataSource = datosReporte;
                        VincularBandasDetalle(reporteFormato.Bands, datosReporte, anexo as AnexoBase);

                        DateTime fecha = DateTime.Now;
                        CultureInfo cultura = CultureInfo.GetCultureInfo("es-MX");

              
                        reporteFormato.RequestParameters = false;
                        AsignarParametro(reporteFormato, "ActividadVulnerable", datosExpedienteUnico.ActividadVulnerable);
                        AsignarParametro(reporteFormato, "Anio", fecha.Year);
                        AsignarParametro(reporteFormato, "Dia", fecha.Day);
                        AsignarParametro(reporteFormato, "Asesor", string.Empty);
                        AsignarParametro(reporteFormato, "FehaCompleta", $"HUAMANTLA, TLAXCALA, A {fecha.ToString("dd 'DE' MMMM 'DEL' yyyy", cultura).ToUpper(cultura)}.");
                        AsignarParametro(reporteFormato, "Instrumento", datosExpedienteUnico.Instrumento);
                        AsignarParametro(reporteFormato, "Mes", fecha.ToString("MMMM", cultura).ToUpper(cultura));
                        AsignarParametro(reporteFormato, "Volumen", datosExpedienteUnico.Volumen);
                        AsignarParametro(reporteFormato, "NumeroExpediente", datosExpedienteUnico.NumExpediente);

                        reporteFormato.CreateDocument();
                        reporte.Pages.AddRange(reporteFormato.Pages);


                        reportePrinsipalView.OpenReport(reporte);


                    }
                    else
                    {
                        throw new Exception("No no encontro el formato correcto o esta vacio, revise el anexo guardado...");
                    }


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        private static Type ResolverTipo(string nombreConfigurado, Assembly assembly, Type tipoBase)
        {
            if (string.IsNullOrWhiteSpace(nombreConfigurado)) return null;

            string nombre = HttpUtility.HtmlDecode(nombreConfigurado).Trim().Replace("global::", string.Empty);
            Type tipo = assembly.GetType(nombre, false, true);
            if (tipo != null && !tipo.IsAbstract && tipoBase.IsAssignableFrom(tipo)) return tipo;

            string nombreSimple = nombre.Split(',')[0].Replace('\\', '/');
            nombreSimple = nombreSimple.Substring(nombreSimple.LastIndexOf('/') + 1);
            if (nombreSimple.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                nombreSimple = nombreSimple.Substring(0, nombreSimple.Length - 3);
            if (nombreSimple.Contains("."))
                nombreSimple = nombreSimple.Substring(nombreSimple.LastIndexOf('.') + 1);

            string nombreNormalizado = Regex.Replace(nombreSimple, "[^a-zA-Z0-9]", string.Empty);
            return assembly.GetTypes().FirstOrDefault(x =>
                x.IsClass && !x.IsAbstract && tipoBase.IsAssignableFrom(x) &&
                string.Equals(Regex.Replace(x.Name, "[^a-zA-Z0-9]", string.Empty), nombreNormalizado, StringComparison.OrdinalIgnoreCase));
        }

        private static object DeserializarAnexo(string snapshot, Type tipoAnexo)
        {
            try
            {
                return JsonConvert.DeserializeObject(snapshot, tipoAnexo);
            }
            catch (JsonSerializationException)
            {
                // Compatibilidad con snapshots anteriores donde DocumentosSeleccionados
                // se almacenaba como una lista en vez de un diccionario.
                JObject json = JObject.Parse(snapshot);
                json.Remove("DocumentosSeleccionados");
                return json.ToObject(tipoAnexo);
            }
        }

        private static void AsignarParametro(XtraReport reporte, string nombre, object valor)
        {
            DevExpress.XtraReports.Parameters.Parameter parametro = reporte.Parameters
                .Cast<DevExpress.XtraReports.Parameters.Parameter>()
                .FirstOrDefault(x => string.Equals(x.Name, nombre, StringComparison.OrdinalIgnoreCase));
            if (parametro != null) parametro.Value = valor ?? string.Empty;
        }

        private static void VincularBandasDetalle(BandCollection bandas, object origenDatos, AnexoBase anexo)
        {
            foreach (Band banda in bandas)
            {
                DetailReportBand detalle = banda as DetailReportBand;
                if (detalle == null) continue;

                if (string.Equals(detalle.DataMember, "DocumentosSeleccionados", StringComparison.OrdinalIgnoreCase) && anexo != null)
                {
                    // XtraReports enumera Dictionary<string,string>, pero no publica
                    // KeyValuePair.Value como campo para las expresiones del reporte.
                    // Se usa un modelo explícito para conservar la expresión [Value].
                    detalle.DataSource = anexo.DocumentosSeleccionados
                        .Select((x, indice) => new DocumentoSeleccionadoReporte
                        {
                            Key = x.Key,
                            Value = ConvertirARomano(indice + 1) + ".- " + x.Value
                        })
                        .ToList();
                    detalle.DataMember = string.Empty;
                }
                else
                {
                    detalle.DataSource = origenDatos;
                }

                VincularBandasDetalle(detalle.Bands, origenDatos, anexo);
            }
        }

        private static string ConvertirARomano(int numero)
        {
            if (numero <= 0) return string.Empty;

            int[] valores = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] simbolos = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            System.Text.StringBuilder resultado = new System.Text.StringBuilder();

            for (int i = 0; i < valores.Length; i++)
            {
                while (numero >= valores[i])
                {
                    resultado.Append(simbolos[i]);
                    numero -= valores[i];
                }
            }

            return resultado.ToString();
        }
    }

    public sealed class DocumentoSeleccionadoReporte
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
