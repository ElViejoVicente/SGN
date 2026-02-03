using System;

namespace SGN.Negocio.Agenda
{
    [Serializable]
    public class CatAgendaRecurso
    {
        public int IdRecurso { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int Orden { get; set; }
    }
}
