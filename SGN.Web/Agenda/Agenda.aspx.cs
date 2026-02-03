using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using DevExpress.Web.ASPxScheduler;

using SGN.Negocio.Agenda;          // AgendaCita, AgendaCitaDataSource, CatAgendaEtiquetaDataSource, CatAgendaRecursoDataSource
using DevExpress.XtraScheduler;    // AppointmentLabel, etc.

namespace SGN.Web.Agenda
{
    public partial class Agenda : System.Web.UI.Page
    {
        // Atajo al Storage del scheduler
        private ASPxSchedulerStorage Storage => scAgenda.Storage;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1) Mappings (como el demo)
            SetupMappings();

            // 2) Recursos desde BD (ObjectDataSource)
            //    (En el ASPX debes tener: resourceDataSource apuntando a CatAgendaRecursoDataSource)
            scAgenda.ResourceDataSource = resourceDataSource;

            // 3) Etiquetas desde BD (llenar Storage.Appointments.Labels)
            CargarEtiquetasDesdeBD();

            // 4) DataSource de citas (como el demo)
            scAgenda.AppointmentDataSource = appointmentDataSource;

            // 5) DataBind SIEMPRE (DevExpress usa callbacks)
            scAgenda.DataBind();
        }

        private void SetupMappings()
        {
            ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;

            Storage.BeginUpdate();
            try
            {
                // Citas
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

                // Recursos (por cita)
                mappings.ResourceId = "IdRecurso";

                // Recursos (catálogo)
                // Cuando usas ResourceDataSource, DevExpress requiere mappings en Resources
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
        /// Carga etiquetas desde BD (Cat_AgendaEtiqueta) y las registra como Appointment Labels
        /// para que aparezcan en el formulario estándar del Scheduler y pinten con colores.
        /// </summary>
        private void CargarEtiquetasDesdeBD()
        {
            // Recomendación: cachear por sesión para no pegarle a BD en cada callback
            // (igual funciona sin cache, pero esto mejora rendimiento).
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

            // Si BD viene vacía, deja al menos una etiqueta base (opcional)
            if (etiquetas.Count == 0)
            {
                labels.Add(labels.CreateNewLabel(1, "Cita", "Cita", Color.SteelBlue));
                return;
            }

            foreach (var e in etiquetas)
            {
                // ColorArgb si existe, si no, asignamos un color “default” determinístico por orden
                Color color = e.ColorArgb.HasValue
                    ? Color.FromArgb(e.ColorArgb.Value)
                    : ColorPorOrden(e.Orden);

                // CreateNewLabel: (id, displayName, menuCaption, color)
                // id debe coincidir con el valor que guardas en AgendaCitas.Etiqueta (FK a Cat_AgendaEtiqueta.IdEtiqueta)
                labels.Add(labels.CreateNewLabel(e.IdEtiqueta, e.Nombre, e.Nombre, color));
            }
        }

        /// <summary>
        /// Colores por defecto (si BD no define ColorArgb).
        /// No importa el color exacto; lo importante es consistencia.
        /// </summary>
        private Color ColorPorOrden(int orden)
        {
            // Paleta fija, se repite si hay más etiquetas
            Color[] palette = new[]
            {
                Color.FromArgb(0, 122, 204),  // azul
                Color.FromArgb(0, 153, 136),  // teal
                Color.FromArgb(96, 125, 139), // gris-azulado
                Color.FromArgb(63, 81, 181),  // indigo
                Color.FromArgb(255, 152, 0),  // naranja
                Color.FromArgb(244, 67, 54),  // rojo
                Color.FromArgb(121, 85, 72),  // café
                Color.FromArgb(156, 39, 176), // morado
                Color.FromArgb(76, 175, 80),  // verde
            };

            if (orden <= 0) orden = 1;
            return palette[(orden - 1) % palette.Length];
        }
    }
}
