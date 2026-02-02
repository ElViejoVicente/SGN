using System;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

namespace SGN.Web.Agenda
{
    public partial class Agenda : System.Web.UI.Page
    {
        ASPxSchedulerStorage Storage => scAgenda.Storage;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupMappings();


            scAgenda.AppointmentDataSource = appointmentDataSource;
            scAgenda.DataBind();

            //scAgenda.GroupType = SchedulerGroupType.Resource;
        }

        private void SetupMappings()
        {
            ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;

            Storage.BeginUpdate();
            try
            {
                mappings.AppointmentId = "IdCita";
                mappings.Start = "FechaInicio";
                mappings.End = "FechaFin";
                mappings.Subject = "Asunto";
                mappings.AllDay = "TodoDia";
                mappings.Description = "Descripcion";
                mappings.Label = "Etiqueta";
                mappings.Location = "Ubicacion";
                mappings.RecurrenceInfo = "RecurrenceInfo";
                mappings.ReminderInfo = "ReminderInfo";   // si no lo usas, igual puede ir null
                mappings.Status = "Estatus";
                mappings.Type = "Tipo";
            }
            finally
            {
                Storage.EndUpdate();
            }
        }

       

    }
}
