using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Controls;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;

namespace SGN.Web.Agenda.CustomForms
{
    public partial class AgendaAppointmentForm : SchedulerFormControl
    {
        protected HtmlGenericControl ValidationContainer;
        protected ASPxLabel lblExpediente;
        protected ASPxTextBox tbExpediente;
        protected ASPxButton btnBuscarExpediente;
        protected ASPxLabel lblSubject;
        protected ASPxTextBox tbSubject;
        protected ASPxLabel lblLocation;
        protected ASPxTextBox tbLocation;
        protected ASPxLabel lblLabel;
        protected ASPxComboBox edtLabel;
        protected ASPxLabel lblStartDate;
        protected ASPxDateEdit edtStartDate;
        protected ASPxLabel lblEndDate;
        protected ASPxDateEdit edtEndDate;
        protected ASPxLabel lblStatus;
        protected ASPxComboBox edtStatus;
        protected ASPxCheckBox chkAllDay;
        protected ASPxLabel lblResource;
        protected ASPxComboBox edtResource;
        protected ASPxLabel lblDescription;
        protected ASPxMemo tbDescription;
        protected ASPxLabel lblValorOperacion;
        protected ASPxSpinEdit tbValorOperacion;
        protected ASPxLabel lblISR;
        protected ASPxSpinEdit tbISR;
        protected ASPxLabel lblNumeroEscritura;
        protected ASPxTextBox tbNumeroEscritura;
        protected ASPxLabel lblActividadVulnerable;
        protected ASPxTextBox tbActividadVulnerable;
        protected AppointmentRecurrenceForm AppointmentRecurrenceForm1;
        protected ASPxButton btnOk;
        protected ASPxButton btnCancel;
        protected ASPxButton btnDelete;
        protected ASPxSchedulerStatusInfo schedulerStatusInfo;

        public IEnumerable ResourceDataSource
        {
            get { return ((AppointmentFormTemplateContainer)Parent).ResourceDataSource; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tbSubject.Focus();
        }

        public override void DataBind()
        {
            base.DataBind();

            var container = (AgendaAppointmentFormTemplateContainer)Parent;
            Appointment appointment = container.Appointment;
            IAppointmentStorageBase storage = container.Control.Storage.Appointments;

            IAppointmentLabel label = storage.Labels.GetById(appointment.LabelKey);
            IAppointmentStatus status = storage.Statuses.GetById(appointment.StatusKey);
            edtLabel.ValueType = appointment.LabelKey.GetType();
            edtLabel.SelectedIndex = storage.Labels.IndexOf(label);
            edtStatus.ValueType = appointment.StatusKey.GetType();
            edtStatus.SelectedIndex = storage.Statuses.IndexOf(status);

            BindResource(appointment);
            AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence;

            btnOk.ClientSideEvents.Click = container.SaveHandler;
            btnCancel.ClientSideEvents.Click = container.CancelHandler;
            btnDelete.ClientSideEvents.Click = container.DeleteHandler;
        }

        private void BindResource(Appointment appointment)
        {
            ListEditItem empty = null;
            foreach (ListEditItem item in edtResource.Items)
            {
                string value = Convert.ToString(item.Value);
                if (string.IsNullOrWhiteSpace(value) || value == SchedulerIdHelper.EmptyResourceId ||
                    string.Equals(item.Text, "Cualquiera", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(item.Text, "(Cualquiera)", StringComparison.OrdinalIgnoreCase))
                {
                    empty = item;
                    break;
                }
            }
            if (empty != null)
                edtResource.Items.Remove(empty);

            if (!Equals(appointment.ResourceId, EmptyResourceId.Id))
                edtResource.Value = Convert.ToString(appointment.ResourceId);
            else
                edtResource.SelectedIndex = -1;
        }

        protected override void PrepareChildControls()
        {
            var container = (AppointmentFormTemplateContainer)Parent;
            ASPxScheduler control = container.Control;
            AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons);
            base.PrepareChildControls();
        }

        protected override ASPxEditBase[] GetChildEditors()
        {
            return new ASPxEditBase[]
            {
                lblExpediente, tbExpediente, lblSubject, tbSubject, lblLocation, tbLocation,
                lblLabel, edtLabel, lblStartDate, edtStartDate, lblEndDate, edtEndDate,
                lblStatus, edtStatus, chkAllDay, lblResource, edtResource, lblDescription, tbDescription,
                lblValorOperacion, tbValorOperacion, lblISR, tbISR,
                lblNumeroEscritura, tbNumeroEscritura, lblActividadVulnerable, tbActividadVulnerable
            };
        }

        protected override ASPxButton[] GetChildButtons()
        {
            return new[] { btnBuscarExpediente, btnOk, btnCancel, btnDelete };
        }

        protected override Control[] GetChildControls()
        {
            return new Control[] { ValidationContainer, AppointmentRecurrenceForm1 };
        }

        protected override WebControl GetDefaultButton()
        {
            return btnOk;
        }
    }
}
