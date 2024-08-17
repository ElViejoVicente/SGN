using SGN.Negocio.Operativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class DatosParticipantes
    {
        public int IdRegistro { get; set; } = 0;
        public int IdHojaDatos { get; set; } = 0;
        public string FiguraOperacion { get; set; } = ""; 
        public string RolOperacion { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string ApellidoPaterno { get; set; } = "";
        public string ApellidoMaterno { get; set; } = "";
        public DateTime FechaNacimiento { get; set; } =  Constantes.FechaGlobal;
        public string Sexo { get; set; } = "";
        public string Ocupacion { get; set; } = "";
        public string EstadoCivil { get; set; } = "";
        public string RegimenConyugal { get; set; } = "";
        public string SabeLeerEscribir { get; set; } = "";
        public string Notas { get; set; } = "";  
    }
}
