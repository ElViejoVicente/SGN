using System;

namespace SGN.Negocio.Agenda
{
    [Serializable]
    public class CatAgendaEtiqueta
    {
        public int IdEtiqueta { get; set; }
        public string Nombre { get; set; }
        public int? ColorArgb { get; set; }
        public bool Activo { get; set; }
        public int Orden { get; set; }
    }
}
