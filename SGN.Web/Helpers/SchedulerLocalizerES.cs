using DevExpress.Web.ASPxScheduler.Localization;

namespace SGN.Web.Helpers
{
    public class SchedulerLocalizerES : ASPxSchedulerLocalizer
    {
        public override string GetLocalizedString(ASPxSchedulerStringId id)
        {
            switch (id)
            {
                case ASPxSchedulerStringId.FloatingActionButton_NewAppointment:
                    return "Nueva cita";

                case ASPxSchedulerStringId.FloatingActionButton_EditAppointment:
                    return "Editar cita";
            }

            return base.GetLocalizedString(id);
        }
    }
}
