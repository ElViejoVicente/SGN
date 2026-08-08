using System;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;

namespace SGN.Web.Agenda.CustomForms
{
    public static class AgendaCustomFieldNames
    {
        public const string IdExpediente = "IdExpediente";
    }

    public class AgendaAppointmentFormTemplateContainer : AppointmentFormTemplateContainer
    {
        public AgendaAppointmentFormTemplateContainer(ASPxScheduler control) : base(control) { }

        public string IdExpediente
        {
            get { return Convert.ToString(Appointment.CustomFields[AgendaCustomFieldNames.IdExpediente]); }
        }
    }

    public class AgendaAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand
    {
        public AgendaAppointmentSaveCallbackCommand(ASPxScheduler control) : base(control) { }

        protected internal new AgendaAppointmentFormController Controller
        {
            get { return (AgendaAppointmentFormController)base.Controller; }
        }

        protected override void AssignControllerValues()
        {
            base.AssignControllerValues();
            var expediente = FindControlByID("tbExpediente") as ASPxTextBox;
            Controller.IdExpediente = expediente == null ? string.Empty : expediente.Text.Trim().ToUpperInvariant();
        }

        protected override AppointmentFormController CreateAppointmentFormController(Appointment appointment)
        {
            return new AgendaAppointmentFormController(Control, appointment);
        }

        protected override Control FindControlByID(string id)
        {
            return FindTemplateControl(TemplateContainer, id);
        }

        private static Control FindTemplateControl(Control root, string id)
        {
            Control found = root.FindControl(id);
            if (found != null)
                return found;

            foreach (Control child in root.Controls)
            {
                found = FindTemplateControl(child, id);
                if (found != null)
                    return found;
            }
            return null;
        }
    }

    public class AgendaAppointmentFormController : AppointmentFormController
    {
        public AgendaAppointmentFormController(ASPxScheduler control, Appointment appointment) : base(control, appointment) { }

        public string IdExpediente
        {
            get { return Convert.ToString(EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.IdExpediente]); }
            set { EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.IdExpediente] = value; }
        }

        private string SourceIdExpediente
        {
            get { return Convert.ToString(SourceAppointment.CustomFields[AgendaCustomFieldNames.IdExpediente]); }
            set { SourceAppointment.CustomFields[AgendaCustomFieldNames.IdExpediente] = value; }
        }

        public override bool IsAppointmentChanged()
        {
            return base.IsAppointmentChanged() || !string.Equals(SourceIdExpediente, IdExpediente, StringComparison.OrdinalIgnoreCase);
        }

        protected override void ApplyCustomFieldsValues()
        {
            SourceIdExpediente = IdExpediente;
        }
    }
}
