using System;

namespace SGN.Negocio.Agenda
{
    [Serializable]
    public class AgendaCitas
    {
        public int IdCita { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool TodoDia { get; set; }

        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }

        public int Etiqueta { get; set; }  // LabelKey -> Cat_AgendaEtiqueta.IdEtiqueta (0 si ninguno)
        public int Estatus { get; set; }   // StatusKey (0 si ninguno)
        public int Tipo { get; set; }      // AppointmentType

        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        public int IdRecurso { get; set; } // ResourceId -> Cat_AgendaRecurso.IdRecurso (0 si ninguno)

        // opcionales dominio
        public string IdExpediente { get; set; }
        public int IdTipoCita { get; set; }
    }
}
