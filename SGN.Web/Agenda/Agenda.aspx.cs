using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

using SGN.Negocio.Agenda;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.ORM;
using SGN.Web.Controles.Servidor;
using SGN.Web.Agenda.CustomForms;

namespace SGN.Web.Agenda
{
    public partial class Agenda : PageBase
    {
        private ASPxSchedulerStorage Storage => scAgenda.Storage;





        protected void Page_Load(object sender, EventArgs e)
        {
            SetupMappings();

            ConfigurarHorarioLaboral();

            // Recursos desde BD (ObjectDataSource)
            scAgenda.ResourceDataSource = resourceDataSource;

            // Etiquetas desde BD -> Storage.Appointments.Labels
            CargarEtiquetasDesdeBD();

            // ✅ Rango visible real (para que el DataSource filtre bien)
            SetVisibleRangeContext();

            scAgenda.AppointmentDataSource = appointmentDataSource;

            // ✅ DataBind SIEMPRE (callbacks)
            scAgenda.DataBind();



            var perfilesConEdicion = new[] { "Datos", "Consultoria-IT", "Dirección" };

            if (!perfilesConEdicion.Contains(UsuarioPagina.NombrePerfil.Trim()))
            {
                ConfigurarAgendaSoloLectura();
            }

        }

        private void SetupMappings()
        {
            var mappings = Storage.Appointments.Mappings;

            Storage.BeginUpdate();
            try
            {
                mappings.AppointmentId = "IdCita";
                mappings.Start = "FechaInicio";
                mappings.End = "FechaFin";
                mappings.Subject = "Asunto";
                mappings.AllDay = "TodoDia";
                mappings.Description = "Descripcion";
                mappings.Location = "Ubicacion";
                mappings.Label = "Etiqueta";
                mappings.Status = "Estatus";
                mappings.Type = "Tipo";
                mappings.RecurrenceInfo = "RecurrenceInfo";
                mappings.ReminderInfo = "ReminderInfo";

                // ✅ Recursos por cita
                mappings.ResourceId = "IdRecurso";

                Storage.Appointments.CustomFieldMappings.Clear();
                Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping(AgendaCustomFieldNames.IdExpediente, "IdExpediente"));
                Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping(AgendaCustomFieldNames.ValorOperacion, "ValorOperacion"));
                Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping(AgendaCustomFieldNames.ISR, "ISR"));
                Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping(AgendaCustomFieldNames.NumeroEscritura, "NumeroEscritura"));
                Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping(AgendaCustomFieldNames.ActividadVulnerable, "ActividadVulnerable"));

                // ✅ Catálogo de recursos
                var rm = Storage.Resources.Mappings;
                rm.ResourceId = "IdRecurso";
                rm.Caption = "Nombre";
            }
            finally
            {
                Storage.EndUpdate();
            }
        }

        /// <summary>
        /// ✅ Horario laboral: Lun–Sáb 08:00–17:00.
        /// Usa WorkDays y WorkTime/ShowWorkTimeOnly. :contentReference[oaicite:3]{index=3}
        /// </summary>
        private void ConfigurarHorarioLaboral()
        {
            // WorkDays pertenece al Scheduler, no al view. :contentReference[oaicite:4]{index=4}
            scAgenda.WorkDays.Clear();
            scAgenda.WorkDays.Add(WeekDays.Monday);
            scAgenda.WorkDays.Add(WeekDays.Tuesday);
            scAgenda.WorkDays.Add(WeekDays.Wednesday);
            scAgenda.WorkDays.Add(WeekDays.Thursday);
            scAgenda.WorkDays.Add(WeekDays.Friday);
            scAgenda.WorkDays.Add(WeekDays.Saturday);

            var workTime = new TimeOfDayInterval(TimeSpan.FromHours(7), TimeSpan.FromHours(18));

            // Aplica a vistas usadas
            scAgenda.WorkWeekView.WorkTime = workTime;
            scAgenda.FullWeekView.WorkTime = workTime;
            scAgenda.DayView.WorkTime = workTime;

            // Mostrar solo horas laborales
            scAgenda.WorkWeekView.ShowWorkTimeOnly = true;
            scAgenda.FullWeekView.ShowWorkTimeOnly = true;
            scAgenda.DayView.ShowWorkTimeOnly = true;
        }

        /// <summary>
        /// ✅ Visible range real con ActiveView.GetVisibleIntervals(). :contentReference[oaicite:5]{index=5}
        /// Lo dejamos en HttpContext.Items para que el DataSource lo use.
        /// </summary>
        private void SetVisibleRangeContext()
        {
            // Nota: GetVisibleIntervals puede devolver el rango correcto del view actual. :contentReference[oaicite:6]{index=6}
            var vis = scAgenda.ActiveView.GetVisibleIntervals();
            DateTime desde = vis.Start;
            DateTime hasta = vis.End;

            HttpContext.Current.Items["SGN_AGENDA_DESDE"] = desde;
            HttpContext.Current.Items["SGN_AGENDA_HASTA"] = hasta;
        }

        /// <summary>
        /// Carga etiquetas desde BD (Cat_AgendaEtiqueta) y las registra como Labels del Scheduler.
        /// </summary>
        private void CargarEtiquetasDesdeBD()
        {
            const string key = "SGN_Agenda_EtiquetasCache";

            List<CatAgendaEtiqueta> etiquetas = Session[key] as List<CatAgendaEtiqueta>;
            if (etiquetas == null)
            {
                var ds = new CatAgendaEtiquetaDataSource();
                etiquetas = ds.SelectMethodHandler()
                              .Where(x => x.Activo)
                              .OrderBy(x => x.Orden)
                              .ToList();

                Session[key] = etiquetas;
            }

            var labels = Storage.Appointments.Labels;
            labels.Clear();

            if (etiquetas.Count == 0)
            {
                labels.Add(labels.CreateNewLabel(1, "Cita", "Cita", Color.SteelBlue));
                return;
            }

            foreach (var e in etiquetas)
            {
                Color color = e.ColorArgb.HasValue
                    ? Color.FromArgb(e.ColorArgb.Value)
                    : ColorPorOrden(e.Orden);

                labels.Add(labels.CreateNewLabel(e.IdEtiqueta, e.Nombre, e.Nombre, color));
            }
        }

        private Color ColorPorOrden(int orden)
        {
            Color[] palette = new[]
            {
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(0, 153, 136),
                Color.FromArgb(96, 125, 139),
                Color.FromArgb(63, 81, 181),
                Color.FromArgb(255, 152, 0),
                Color.FromArgb(244, 67, 54),
                Color.FromArgb(121, 85, 72),
            };

            if (orden <= 0) orden = 1;
            return palette[(orden - 1) % palette.Length];
        }

        protected void scAgenda_AppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            e.Container = new AgendaAppointmentFormTemplateContainer((ASPxScheduler)sender);
        }

        protected void scAgenda_PrepareAppointmentFormPopupContainer(
            object sender,
            ASPxSchedulerPrepareFormPopupContainerEventArgs e)
        {
            e.Popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
            e.Popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter;
            e.Popup.PopupHorizontalOffset = 0;
            e.Popup.PopupVerticalOffset = 0;
            e.Popup.AutoUpdatePosition = true;
        }

        protected void scAgenda_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
        {
            if (e.CommandId == SchedulerCallbackCommandId.AppointmentSave)
                e.Command = new AgendaAppointmentSaveCallbackCommand((ASPxScheduler)sender);
        }

        [WebMethod(EnableSession = true)]
        public static ResultadoBusquedaExpediente BuscarExpediente(string numeroExpediente)
        {
            string expediente = (numeroExpediente ?? string.Empty).Trim().ToUpperInvariant();
            if (!Regex.IsMatch(expediente, @"^\d{4}-(0[1-9]|1[0-2])-\d+[A-Z]?$"))
                return ResultadoBusquedaExpediente.Error("El formato del expediente no es válido.");

            try
            {
                Expedientes registro = new DatosCrud().ConsultaExpediente(expediente);
                if (registro == null || registro.IdHojaDatos <= 0)
                    return ResultadoBusquedaExpediente.Error("No se encontró el expediente indicado.");

                ListaExpedientes otroDetalle = new DatosExpedientes().DameExpedientePorFolio(expediente);


                ListaHojaDatos detalle = new DatosExpedientes().DameHojaDatosDetalle(registro.IdHojaDatos);
                if (detalle == null)
                    return ResultadoBusquedaExpediente.Error("El expediente no tiene hoja de datos disponible.");

                return new ResultadoBusquedaExpediente
                {
                    Exito = true,
                    Expediente = expediente,
                    Acto = UnirExpedienteYActo(expediente, UnirActo(detalle.TextoActo, detalle.TextoVariante)),
                    Proyectista = registro.NombreProyectista ?? string.Empty,
                    Descripcion = CrearDescripcion(detalle, registro, otroDetalle),
                    ValorOperacion = registro.ValorOperacion,
                    ISR = registro.ISR,
                    NumeroEscritura = registro.Escritura > 0 ? registro.Escritura.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    ActividadVulnerable = otroDetalle.EsActoVulnerable ? "Sí" : "No"
                };
            }
            catch
            {
                return ResultadoBusquedaExpediente.Error("Ocurrió un error al consultar el expediente.");
            }
        }

        protected void cbBuscarExpediente_Callback(object source, CallbackEventArgs e)
        {
            ResultadoBusquedaExpediente resultado = BuscarExpediente(e.Parameter);
            e.Result = new JavaScriptSerializer().Serialize(resultado);
        }

        private static string UnirActo(string acto, string variante)
        {
            return string.Join(" - ", new[] { acto, variante }.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()));
        }

        private static string UnirExpedienteYActo(string expediente, string acto)
        {
            return string.Join(" - ", new[] { expediente, acto }.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()));
        }

        private static string CrearDescripcion(ListaHojaDatos detalle, Expedientes expediente, ListaExpedientes otroDetalle )
        {
            var lineas = new List<string>();
            AgregarLinea(lineas, "Estatus", detalle.TextoEstatus);
            AgregarLinea(lineas, "Otorga", detalle.Otorga);
            AgregarLinea(lineas, "A favor de", detalle.AfavorDe);
            AgregarLinea(lineas, "Asesor", detalle.NombreAsesor);
            AgregarLinea(lineas, "Tramita", detalle.NumbreUsuarioTramita);
            AgregarLinea(lineas, "Valor operación", expediente.ValorOperacion.ToString("N2", CultureInfo.GetCultureInfo("es-MX")));
            AgregarLinea(lineas, "ISR", expediente.ISR.ToString("N2", CultureInfo.GetCultureInfo("es-MX")));
            AgregarLinea(lineas, "Actividad Vulnerable", otroDetalle.EsActoVulnerable ? "Si" : "No" );
            return string.Join(Environment.NewLine, lineas);
        }

        private static void AgregarLinea(ICollection<string> lineas, string etiqueta, string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
                lineas.Add(etiqueta + ": " + valor.Trim());
        }

        public sealed class ResultadoBusquedaExpediente
        {
            public bool Exito { get; set; }
            public string Mensaje { get; set; }
            public string Expediente { get; set; }
            public string Acto { get; set; }
            public string Proyectista { get; set; }
            public string Descripcion { get; set; }
            public decimal ValorOperacion { get; set; }
            public decimal ISR { get; set; }
            public string NumeroEscritura { get; set; }
            public string ActividadVulnerable { get; set; }

            public static ResultadoBusquedaExpediente Error(string mensaje)
            {
                return new ResultadoBusquedaExpediente { Exito = false, Mensaje = mensaje };
            }
        }

        private void ConfigurarAgendaSoloLectura()
        {
            // 1️⃣ No permitir crear citas
            scAgenda.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;

            // 2️⃣ No permitir editar
            scAgenda.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;

            // 3️⃣ No permitir eliminar
            scAgenda.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;

            // 4️⃣ No permitir mover ni redimensionar
            scAgenda.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.None;
            scAgenda.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.None;

            // 5️⃣ Ocultar menú contextual completo (clic derecho)
            // scAgenda.EnableContextMenu = false; // ❌ Línea incorrecta, propiedad no existe
            // scAgenda.OptionsCustomization.allowdisAllowDisplayContextMenu = false; // ✅ Usar la propiedad correcta

            // 6️⃣ Evitar doble clic para editar
            //scAgenda.ClientSideEvents.AppointmentDoubleClick =
            //    "function(s,e){ e.handled = true; }";

            // 7️⃣ Evitar selección de rango para crear cita
            //scAgenda.ClientSideEvents.SelectionChanged =
            //    "function(s,e){ s.Unselect(); }";

            // 8️⃣ Visual feedback: cursor normal
            //scAgenda.ClientSideEvents.Init =
            //    "function(s,e){ s.GetMainElement().style.cursor = 'default'; }";
        }



    }
}
