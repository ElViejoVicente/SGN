using System;
using System.Globalization;
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
        public const string ValorOperacion = "ValorOperacion";
        public const string ISR = "ISR";
        public const string NumeroEscritura = "NumeroEscritura";
        public const string ActividadVulnerable = "ActividadVulnerable";
    }

    public class AgendaAppointmentFormTemplateContainer : AppointmentFormTemplateContainer
    {
        public AgendaAppointmentFormTemplateContainer(ASPxScheduler control) : base(control) { }

        public string IdExpediente
        {
            get { return Convert.ToString(Appointment.CustomFields[AgendaCustomFieldNames.IdExpediente]); }
        }

        public decimal ValorOperacion { get { return ObtenerDecimal(Appointment.CustomFields[AgendaCustomFieldNames.ValorOperacion]); } }
        public decimal ISR { get { return ObtenerDecimal(Appointment.CustomFields[AgendaCustomFieldNames.ISR]); } }
        public string NumeroEscritura { get { return Convert.ToString(Appointment.CustomFields[AgendaCustomFieldNames.NumeroEscritura]); } }
        public string ActividadVulnerable { get { return Convert.ToString(Appointment.CustomFields[AgendaCustomFieldNames.ActividadVulnerable]); } }

        private static decimal ObtenerDecimal(object value)
        {
            return value == null || value == DBNull.Value
                ? 0m
                : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
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
            Controller.ValorOperacion = ObtenerDecimal("tbValorOperacion");
            Controller.ISR = ObtenerDecimal("tbISR");
            Controller.NumeroEscritura = ObtenerTexto("tbNumeroEscritura");
            Controller.ActividadVulnerable = ObtenerTexto("tbActividadVulnerable");
        }

        private string ObtenerTexto(string id)
        {
            var control = FindControlByID(id) as ASPxTextBox;
            return control == null ? string.Empty : control.Text.Trim();
        }

        private decimal ObtenerDecimal(string id)
        {
            var control = FindControlByID(id) as ASPxSpinEdit;
            return control == null || control.Value == null
                ? 0m
                : Convert.ToDecimal(control.Value, CultureInfo.InvariantCulture);
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

        public decimal ValorOperacion
        {
            get { return Convert.ToDecimal(EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ValorOperacion] ?? 0, CultureInfo.InvariantCulture); }
            set { EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ValorOperacion] = value; }
        }

        public decimal ISR
        {
            get { return Convert.ToDecimal(EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ISR] ?? 0, CultureInfo.InvariantCulture); }
            set { EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ISR] = value; }
        }

        public string NumeroEscritura
        {
            get { return Convert.ToString(EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.NumeroEscritura]); }
            set { EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.NumeroEscritura] = value; }
        }

        public string ActividadVulnerable
        {
            get { return Convert.ToString(EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ActividadVulnerable]); }
            set { EditedAppointmentCopy.CustomFields[AgendaCustomFieldNames.ActividadVulnerable] = value; }
        }

        private string SourceIdExpediente
        {
            get { return Convert.ToString(SourceAppointment.CustomFields[AgendaCustomFieldNames.IdExpediente]); }
            set { SourceAppointment.CustomFields[AgendaCustomFieldNames.IdExpediente] = value; }
        }

        private decimal SourceValorOperacion { get { return Convert.ToDecimal(SourceAppointment.CustomFields[AgendaCustomFieldNames.ValorOperacion] ?? 0, CultureInfo.InvariantCulture); } set { SourceAppointment.CustomFields[AgendaCustomFieldNames.ValorOperacion] = value; } }
        private decimal SourceISR { get { return Convert.ToDecimal(SourceAppointment.CustomFields[AgendaCustomFieldNames.ISR] ?? 0, CultureInfo.InvariantCulture); } set { SourceAppointment.CustomFields[AgendaCustomFieldNames.ISR] = value; } }
        private string SourceNumeroEscritura { get { return Convert.ToString(SourceAppointment.CustomFields[AgendaCustomFieldNames.NumeroEscritura]); } set { SourceAppointment.CustomFields[AgendaCustomFieldNames.NumeroEscritura] = value; } }
        private string SourceActividadVulnerable { get { return Convert.ToString(SourceAppointment.CustomFields[AgendaCustomFieldNames.ActividadVulnerable]); } set { SourceAppointment.CustomFields[AgendaCustomFieldNames.ActividadVulnerable] = value; } }

        public override bool IsAppointmentChanged()
        {
            return base.IsAppointmentChanged()
                || !string.Equals(SourceIdExpediente, IdExpediente, StringComparison.OrdinalIgnoreCase)
                || SourceValorOperacion != ValorOperacion
                || SourceISR != ISR
                || !string.Equals(SourceNumeroEscritura, NumeroEscritura, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(SourceActividadVulnerable, ActividadVulnerable, StringComparison.OrdinalIgnoreCase);
        }

        protected override void ApplyCustomFieldsValues()
        {
            SourceIdExpediente = IdExpediente;
            SourceValorOperacion = ValorOperacion;
            SourceISR = ISR;
            SourceNumeroEscritura = NumeroEscritura;
            SourceActividadVulnerable = ActividadVulnerable;
        }
    }
}
