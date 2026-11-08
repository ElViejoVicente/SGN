using DevExpress.Web;
using DevExpress.Web.Internal.XmlProcessor;
using Newtonsoft.Json;
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
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
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


        private int IdRegistroSeleccionado
        {
            get { return Session["IdRegistroExpedienteUnicoV2"] == null ? 0 : Convert.ToInt32(Session["IdRegistroExpedienteUnicoV2"]); }
            set { Session["IdRegistroExpedienteUnicoV2"] = value; }
        }

        #endregion

        #region Funciones
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (AnexoSeleccionado != null) CrearFormularioAnexo(AnexoSeleccionado);
        }
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
            ExpedienteControlSeleccionado = null;
            AnexoSeleccionado = null;
            IdRegistroSeleccionado = 0;


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

        private ListaExpedienteUnicoV2 RegistroSeleccionado
        {
            get { return lsExpedienteUnico.FirstOrDefault(x => x.IdRegistro == IdRegistroSeleccionado); }
        }

        private Type ObtenerTipoAnexo(Cat_Anexos catalogo)
        {
            if (catalogo == null || string.IsNullOrWhiteSpace(catalogo.ClassInterna)) return null;

            Assembly assemblyAnexos = typeof(AnexoBase).Assembly;
            string nombreConfigurado = HttpUtility.HtmlDecode(catalogo.ClassInterna).Trim();
            nombreConfigurado = nombreConfigurado.Replace("global::", string.Empty);

            // Primero intenta resolver ClassInterna como nombre completo.
            Type tipo = assemblyAnexos.GetType(nombreConfigurado, false, true);
            if (tipo != null && typeof(AnexoBase).IsAssignableFrom(tipo)) return tipo;

            // El catálogo también puede contener una ruta, extensión .cs o solamente el nombre de clase.
            string nombreSimple = nombreConfigurado.Split(',')[0].Replace('\\', '/');
            nombreSimple = nombreSimple.Substring(nombreSimple.LastIndexOf('/') + 1);
            if (nombreSimple.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                nombreSimple = nombreSimple.Substring(0, nombreSimple.Length - 3);
            if (nombreSimple.Contains("."))
                nombreSimple = nombreSimple.Substring(nombreSimple.LastIndexOf('.') + 1);

            string nombreNormalizado = Regex.Replace(nombreSimple, "[^a-zA-Z0-9]", string.Empty);
            tipo = assemblyAnexos.GetTypes().FirstOrDefault(x =>
                x.IsClass && !x.IsAbstract && typeof(AnexoBase).IsAssignableFrom(x) &&
                string.Equals(Regex.Replace(x.Name, "[^a-zA-Z0-9]", string.Empty), nombreNormalizado, StringComparison.OrdinalIgnoreCase));

            return tipo;
        }

        private void AbrirModal(int idRegistro)
        {
            IdRegistroSeleccionado = idRegistro;
            ListaExpedienteUnicoV2 registro = RegistroSeleccionado;
            if (registro == null) throw new InvalidOperationException("No fue posible localizar el participante seleccionado.");

            CargarDatosPersona(registro);
            ExpedienteControlSeleccionado = null;
            AnexoSeleccionado = null;
            phCamposAnexo.Controls.Clear();
            cbAnexo.DataBind();
            cbAnexo.SelectedIndex = -1;
            txtInstrumento.Text = string.Empty;
            txtVolumen.Text = string.Empty;
            txtActividadVulnerable.Text = string.Empty;
            pcExpediente.ActiveTabIndex = 0;

            if (!string.IsNullOrWhiteSpace(registro.IdExpedienteUnico))
            {
                ExpedienteControlSeleccionado = datosCrud.ConsultaExpedienteUnicoControl(registro.IdExpedienteUnico, 0);
                if (ExpedienteControlSeleccionado == null) throw new InvalidOperationException("No se encontró la versión vigente del expediente.");

                Cat_Anexos catalogo = CatAnexos.FirstOrDefault(x => string.Equals(x.NombreAnexo, ExpedienteControlSeleccionado.NombreDelAnexo, StringComparison.OrdinalIgnoreCase));
                Type tipo = ObtenerTipoAnexo(catalogo);
                if (catalogo == null || tipo == null)
                    throw new InvalidOperationException("El anexo guardado no tiene una clase interna válida. ClassInterna: " + (catalogo == null ? "sin catálogo" : catalogo.ClassInterna));

                AnexoSeleccionado = string.IsNullOrWhiteSpace(ExpedienteControlSeleccionado.AnexoSnapshot)
                    ? (AnexoBase)Activator.CreateInstance(tipo)
                    : (AnexoBase)JsonConvert.DeserializeObject(ExpedienteControlSeleccionado.AnexoSnapshot, tipo);
                cbAnexo.Value = catalogo.IdAnexo;
                txtInstrumento.Text = ExpedienteControlSeleccionado.Instrumento;
                txtVolumen.Text = ExpedienteControlSeleccionado.Volumen;
                txtActividadVulnerable.Text = ExpedienteControlSeleccionado.ActividadVulnerable;
                CrearFormularioAnexo(AnexoSeleccionado);
            }
        }

        private void CargarDatosPersona(ListaExpedienteUnicoV2 registro)
        {
            lblPersonaNombre.Text = GetNombreCompleto(registro.Nombres, registro.ApellidoPaterno, registro.ApellidoMaterno);
            txtPersonaRol.Text = registro.RolOperacion;
            txtPersonaSexo.Text = registro.Sexo == "F" ? "Femenino" : registro.Sexo == "M" ? "Masculino" : registro.Sexo;
            txtPersonaFechaNacimiento.Text = registro.FechaNacimiento.Year > 1900 ? registro.FechaNacimiento.ToString("dd/MM/yyyy") : string.Empty;
            txtPersonaEdad.Text = registro.FechaNacimiento.Year > 1900 ? CalcularEdad(registro.FechaNacimiento) + " años" : string.Empty;
            txtPersonaOcupacion.Text = registro.Ocupacion;
            txtOperacion.Text = string.Join(" - ", new[] { registro.TextoActo, registro.TextoVariante }.Where(x => !string.IsNullOrWhiteSpace(x)));
            txtNumeroExpediente.Text = registro.IdExpediente;
            txtEstado.Text = string.IsNullOrWhiteSpace(registro.IdExpedienteUnico) ? "Pendiente de generación" : registro.TextoEstatus;
            txtUsuarioResponsable.Text = string.IsNullOrWhiteSpace(registro.UsuarioAutor) ? UsuarioPagina.Nombre : registro.UsuarioAutor;
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad;
        }

        private void SeleccionarAnexo(string idAnexo)
        {
            Cat_Anexos catalogo = CatAnexos.FirstOrDefault(x => x.IdAnexo == idAnexo);
            Type tipo = ObtenerTipoAnexo(catalogo);
            if (tipo == null)
                throw new InvalidOperationException("No se encontró la clase del anexo. ClassInterna: " + (catalogo == null ? "sin catálogo" : catalogo.ClassInterna));

            AnexoSeleccionado = (AnexoBase)Activator.CreateInstance(tipo);
            phCamposAnexo.Controls.Clear();
            cbAnexo.DataBind();
            cbAnexo.Value = idAnexo;
            CrearFormularioAnexo(AnexoSeleccionado);
            pcExpediente.ActiveTabIndex = 1;
        }

        private void CrearFormularioAnexo(AnexoBase anexo)
        {
            phCamposAnexo.Controls.Clear();
            if (anexo == null) return;

            AnexoMapeo metadata = ObtenerMapeoAnexo(anexo.GetType().Name);
            Panel contenedor = new Panel { CssClass = "dynamic-form-grid" };
            Panel contenedorGrupo = new Panel { CssClass = "dynamic-form-grid" };
            foreach (PropertyInfo propiedad in anexo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead && x.CanWrite && x.DeclaringType != typeof(AnexoBase)))
            {
                string etiquetaCampo = ObtenerEtiquetaCampo(propiedad.Name, metadata);
                bool perteneceGrupo = !string.IsNullOrWhiteSpace(metadata.PrefijoGrupo) && etiquetaCampo.StartsWith(metadata.PrefijoGrupo + ":", StringComparison.OrdinalIgnoreCase);
                if (perteneceGrupo) etiquetaCampo = etiquetaCampo.Substring(etiquetaCampo.IndexOf(':') + 1).Trim();

                Panel campo = new Panel { CssClass = "modal-field dynamic-field" };
                ASPxLabel etiqueta = new ASPxLabel { Text = etiquetaCampo, CssClass = "dynamic-label" };
                ASPxEdit editor = CrearEditor(propiedad, propiedad.GetValue(anexo, null));
                campo.Controls.Add(etiqueta);
                campo.Controls.Add(editor);
                (perteneceGrupo ? contenedorGrupo : contenedor).Controls.Add(campo);
            }
            phCamposAnexo.Controls.Add(contenedor);
            if (contenedorGrupo.Controls.Count > 0)
            {
                Panel seccion = new Panel { CssClass = "dynamic-group-section" };
                seccion.Controls.Add(new ASPxLabel { Text = metadata.TituloGrupo, CssClass = "dynamic-group-title" });
                seccion.Controls.Add(contenedorGrupo);
                phCamposAnexo.Controls.Add(seccion);
            }
        }

        private ASPxEdit CrearEditor(PropertyInfo propiedad, object valor)
        {
            ASPxEdit editor;
            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
            if (EsCampoFecha(propiedad))
            {
                ASPxDateEdit fecha = new ASPxDateEdit { DisplayFormatString = "dd/MM/yyyy", EditFormatString = "dd/MM/yyyy" };
                DateTime fechaValor;
                if (valor != null && DateTime.TryParse(Convert.ToString(valor), out fechaValor)) fecha.Date = fechaValor;
                editor = fecha;
            }
            else if (EsEntero(tipo))
            {
                ASPxSpinEdit entero = new ASPxSpinEdit { NumberType = SpinEditNumberType.Integer, MinValue = decimal.MinValue, MaxValue = decimal.MaxValue };
                if (valor != null && !string.IsNullOrWhiteSpace(Convert.ToString(valor))) entero.Value = valor;
                editor = entero;
            }
            else if (EsDecimal(tipo))
            {
                ASPxSpinEdit numero = new ASPxSpinEdit { NumberType = SpinEditNumberType.Float, DecimalPlaces = 2, MinValue = decimal.MinValue, MaxValue = decimal.MaxValue };
                if (valor != null && !string.IsNullOrWhiteSpace(Convert.ToString(valor))) numero.Value = valor;
                editor = numero;
            }
            else
            {
                ASPxTextBox texto = new ASPxTextBox { Text = Convert.ToString(valor) };
                if (propiedad.Name.IndexOf("correo", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    texto.ValidationSettings.RegularExpression.ValidationExpression = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
                    texto.ValidationSettings.RegularExpression.ErrorText = "Capture un correo electrónico válido.";
                }
                editor = texto;
            }

            editor.ID = "fld_" + propiedad.Name;
            editor.Width = Unit.Percentage(100);
            editor.ValidationSettings.ValidationGroup = "Anexo";
            editor.ValidationSettings.Display = Display.Dynamic;
            editor.ValidationSettings.RequiredField.IsRequired = true;
            editor.ValidationSettings.RequiredField.ErrorText = "Campo obligatorio.";
            return editor;
        }

        private bool EsCampoFecha(PropertyInfo propiedad)
        {
            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
            return tipo == typeof(DateTime) || (tipo == typeof(string) && propiedad.Name.IndexOf("Fecha", StringComparison.OrdinalIgnoreCase) >= 0 && propiedad.Name.IndexOf("ORfc", StringComparison.OrdinalIgnoreCase) < 0);
        }

        private bool EsEntero(Type tipo)
        {
            return tipo == typeof(byte) || tipo == typeof(short) || tipo == typeof(int) || tipo == typeof(long);
        }

        private bool EsDecimal(Type tipo)
        {
            return tipo == typeof(decimal) || tipo == typeof(double) || tipo == typeof(float);
        }

        private string ObtenerEtiquetaCampo(string propiedad, AnexoMapeo metadata)
        {
            string etiqueta;
            return metadata.Etiquetas.TryGetValue(propiedad, out etiqueta) ? etiqueta : Regex.Replace(propiedad, "([a-z0-9])([A-Z])", "$1 $2");
        }

        private AnexoMapeo ObtenerMapeoAnexo(string nombreClase)
        {
            Dictionary<string, AnexoMapeo> mapeos = CargarMapeosAnexos();
            AnexoMapeo metadata;
            return mapeos.TryGetValue(nombreClase, out metadata) ? metadata : new AnexoMapeo();
        }

        private Dictionary<string, AnexoMapeo> CargarMapeosAnexos()
        {
            const string cacheKey = "MapeosCamposAnexosV2";
            Dictionary<string, AnexoMapeo> cache = Application[cacheKey] as Dictionary<string, AnexoMapeo>;
            if (cache != null) return cache;
            cache = new Dictionary<string, AnexoMapeo>(StringComparer.OrdinalIgnoreCase);
            Assembly assembly = typeof(AnexoBase).Assembly;
            string recurso = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith("MAPEO_CAMPOS_ANEXOS.md", StringComparison.OrdinalIgnoreCase));
            if (recurso != null)
            {
                using (Stream stream = assembly.GetManifestResourceStream(recurso))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string linea;
                    List<string> clasesActuales = new List<string>();
                    while ((linea = reader.ReadLine()) != null)
                    {
                        Match encabezado = Regex.Match(linea, @"^##\s+Anexo(?:s)?\s+(?<numero>.+)$", RegexOptions.IgnoreCase);
                        if (encabezado.Success)
                        {
                            string sufijo = Regex.Replace(encabezado.Groups["numero"].Value, @"\s+", string.Empty);
                            clasesActuales = sufijo == "3"
                                ? new List<string> { "Anexo3", "Anexo3ConApoderado", "Anexo3ConTestigoARuegoYEncargo", "Anexo3GestorDeNegocios" }
                                : new List<string> { "Anexo" + sufijo };
                            foreach (string clase in clasesActuales) if (!cache.ContainsKey(clase)) cache[clase] = new AnexoMapeo();
                            continue;
                        }

                        Match campo = Regex.Match(linea, @"^\|\s*`(?<prop>[^`]+)`\s*\|\s*(?<label>[^|]+)\s*\|");
                        if (campo.Success && clasesActuales.Count > 0)
                        {
                            foreach (string clase in clasesActuales) cache[clase].Etiquetas[campo.Groups["prop"].Value] = campo.Groups["label"].Value.Trim();
                            continue;
                        }

                        Match grupo = Regex.Match(linea, "Agrupar.*?:\\s*\"(?<prefijo>[^\"]+)\".*?titul(?:o|ar).*?:\\s*\"(?<titulo>[^\"]+)\"", RegexOptions.IgnoreCase);
                        if (grupo.Success)
                        {
                            foreach (string clase in clasesActuales)
                            {
                                cache[clase].PrefijoGrupo = grupo.Groups["prefijo"].Value.Trim();
                                cache[clase].TituloGrupo = grupo.Groups["titulo"].Value.Trim();
                            }
                        }
                    }
                }
            }
            Application[cacheKey] = cache;
            return cache;
        }

        private class AnexoMapeo
        {
            public Dictionary<string, string> Etiquetas { get; private set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            public string PrefijoGrupo { get; set; }
            public string TituloGrupo { get; set; }
        }

        private void GuardarExpediente()
        {
            ListaExpedienteUnicoV2 registro = RegistroSeleccionado;
            if (registro == null || AnexoSeleccionado == null || cbAnexo.Value == null) throw new InvalidOperationException("Seleccione un anexo y capture sus campos obligatorios.");

            foreach (PropertyInfo propiedad in AnexoSeleccionado.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanWrite && x.DeclaringType != typeof(AnexoBase)))
            {
                ASPxEdit editor = BuscarControl(phCamposAnexo, "fld_" + propiedad.Name) as ASPxEdit;
                object valor = ObtenerValorEditor(editor, propiedad);
                if (valor == null || string.IsNullOrWhiteSpace(Convert.ToString(valor)))
                    throw new InvalidOperationException("Todos los campos del anexo son obligatorios. Verifique: " + ObtenerEtiquetaCampo(propiedad.Name, ObtenerMapeoAnexo(AnexoSeleccionado.GetType().Name)));
                propiedad.SetValue(AnexoSeleccionado, valor, null);
            }

            Cat_Anexos catalogo = CatAnexos.First(x => x.IdAnexo == Convert.ToString(cbAnexo.Value));
            bool esNuevo = ExpedienteControlSeleccionado == null || string.IsNullOrWhiteSpace(ExpedienteControlSeleccionado.IdExpedienteUnico);
            if (esNuevo) ExpedienteControlSeleccionado = new ExpedienteUnicoControl { FechaCreacionVersion = DateTime.Now };

            ExpedienteControlSeleccionado.NumExpediente = registro.IdExpediente;
            ExpedienteControlSeleccionado.UsuarioAutor = UsuarioPagina.Nombre;
            ExpedienteControlSeleccionado.FechaUltimaModificacion = DateTime.Now;
            ExpedienteControlSeleccionado.NombreDelAnexo = catalogo.NombreAnexo;
            ExpedienteControlSeleccionado.Instrumento = txtInstrumento.Text;
            ExpedienteControlSeleccionado.Volumen = txtVolumen.Text;
            ExpedienteControlSeleccionado.ActividadVulnerable = txtActividadVulnerable.Text;
            ExpedienteControlSeleccionado.AnexoSnapshot = JsonConvert.SerializeObject(AnexoSeleccionado);

            ExpedienteUnicoControl expedienteGuardado = ExpedienteControlSeleccionado;
            datosCrud.AltaExpedienteUnicoControl(ref expedienteGuardado);
            ExpedienteControlSeleccionado = expedienteGuardado;

            if (string.IsNullOrWhiteSpace(ExpedienteControlSeleccionado.IdExpedienteUnico))
                throw new InvalidOperationException("El expediente fue insertado, pero el procedimiento no devolvió el IdExpedienteUnico generado.");

            DatosParticipantes datosCliente = datosCrud.ConsultaDatosParticipantes(registro.IdRegistro);
            if (datosCliente == null)
                throw new InvalidOperationException("El expediente fue generado, pero no se encontró el participante con IdRegistro " + registro.IdRegistro + ".");

            datosCliente.IdExpedienteUnico = ExpedienteControlSeleccionado.IdExpedienteUnico;
            datosCrud.ActualizaDatosParticipantes(datosCliente);

            cpExpediente.JSProperties["cp_Guardado"] = string.Format("Expediente {0} correctamente. Expediente: {1}. Anexo: {2}.", esNuevo ? "generado" : "actualizado", registro.IdExpediente, catalogo.NombreAnexo);
        }

        private object ObtenerValorEditor(ASPxEdit editor, PropertyInfo propiedad)
        {
            if (editor == null) return null;
            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;

            ASPxDateEdit fecha = editor as ASPxDateEdit;
            if (fecha != null)
            {
                if (fecha.Value == null) return null;
                DateTime valorFecha = Convert.ToDateTime(fecha.Value);
                return tipo == typeof(string) ? (object)valorFecha.ToString("dd/MM/yyyy") : valorFecha;
            }

            ASPxSpinEdit numero = editor as ASPxSpinEdit;
            if (numero != null)
            {
                if (numero.Value == null) return null;
                return Convert.ChangeType(numero.Value, tipo);
            }

            ASPxTextBox texto = editor as ASPxTextBox;
            string valor = texto == null ? Convert.ToString(editor.Value).Trim() : texto.Text.Trim();
            if (propiedad.Name.IndexOf("correo", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                try { new MailAddress(valor); }
                catch { throw new InvalidOperationException("Capture un correo electrónico válido en " + ObtenerEtiquetaCampo(propiedad.Name, ObtenerMapeoAnexo(AnexoSeleccionado.GetType().Name))); }
            }
            return tipo == typeof(string) ? (object)valor : Convert.ChangeType(valor, tipo);
        }

        private Control BuscarControl(Control raiz, string id)
        {
            foreach (Control control in raiz.Controls)
            {
                if (control.ID == id) return control;
                Control encontrado = BuscarControl(control, id);
                if (encontrado != null) return encontrado;
            }
            return null;
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

        protected void cbAnexo_DataBinding(object sender, EventArgs e)
        {
            cbAnexo.DataSource = CatAnexos;
        }

        protected void cpExpediente_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                string[] partes = (e.Parameter ?? string.Empty).Split(new[] { '|' }, 2);
                switch (partes[0])
                {
                    case "Abrir":
                        AbrirModal(Convert.ToInt32(partes[1]));
                        break;
                    case "Anexo":
                        SeleccionarAnexo(partes[1]);
                        break;
                    case "Guardar":
                        GuardarExpediente();
                        break;
                }
            }
            catch (Exception ex)
            {
                cpExpediente.JSProperties["cp_Error"] = ex.Message;
            }
            finally
            {
                // El callback vuelve a renderizar ambas pestañas. Se hidrata nuevamente
                // la información de Persona aunque la pestaña activa sea Anexo.
                ListaExpedienteUnicoV2 participante = RegistroSeleccionado;
                if (participante != null)
                    CargarDatosPersona(participante);
            }
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
