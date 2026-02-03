using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

using SGN.Negocio.Agenda;
using SGN.Web.Controles.Servidor;

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

        /// <summary>
        /// ✅ Oculta el campo Ubicación en el Appointment Form estándar.
        /// Usamos AppointmentFormShowing para acceder al contenedor del form. :contentReference[oaicite:7]{index=7}
        /// </summary>
        protected void scAgenda_AppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            // Container es el contenedor del form estándar. :contentReference[oaicite:8]{index=8}
            if (e.Container == null) return;

            // En el template default los IDs suelen incluir "Location".
            // Lo hacemos robusto: ocultamos cualquier control cuyo ID contenga "Location".
            HideControlsByIdContains(e.Container, "Location");
            HideControlsByIdContains(e.Container, "Ubicacion"); // por si tu localización cambia el ID

            // También puedes ocultar por texto del label si lo llegas a localizar.
        }

        private void HideControlsByIdContains(System.Web.UI.Control root, string token)
        {
            if (root == null) return;

            foreach (System.Web.UI.Control c in GetAllControls(root))
            {
                if (!string.IsNullOrEmpty(c.ID) && c.ID.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    c.Visible = false;

                    // A veces el editor está dentro de una celda/fila; ocultamos el contenedor si existe
                    if (c.Parent != null) c.Parent.Visible = false;
                }
            }
        }

        private IEnumerable<System.Web.UI.Control> GetAllControls(System.Web.UI.Control root)
        {
            foreach (System.Web.UI.Control c in root.Controls)
            {
                yield return c;
                foreach (var child in GetAllControls(c))
                    yield return child;
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
