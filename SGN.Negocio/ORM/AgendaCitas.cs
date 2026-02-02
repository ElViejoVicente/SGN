using System;

namespace SGN.Web.Agenda
{
    [Serializable]
    public class AgendaCita
    {
        // Debe coincidir con mappings.AppointmentId
        public int IdCita { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Asunto { get; set; }
        public bool TodoDia { get; set; }

        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }

        public int? Estatus { get; set; }
        public int? Etiqueta { get; set; }

        // DevExpress appointment type (0=Normal, etc)
        public int Tipo { get; set; }

        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
    }
}
