using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Agenda
{
    public class AgendaReporte
    {
        public int IdCita { get; set; }=0;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Horario { get; set; } = "";
        public string Mesa { get; set; } = "";
        public string IdExpediente { get; set; } = "";
        public int Estatus { get; set; }=0;
        public decimal ValorOperacion { get; set; }=0;
        public decimal ISR { get; set; }=0;
        public string NumeroEscritura { get; set; } = "";
        public string ActividadVulnerable { get; set; } = "";
        public string Otorga { get; set; } = "";
        public string AfavorDe { get; set; } = "";
        public string Acto { get; set; } = "";
        public string Variante { get; set; } = "";
        public bool EsActoVulnerable { get; set; } = false;
        public string TextoRecurso { get; set; } = "";  
        public string TextoEtiqueta { get; set; } = "";
    }
}
