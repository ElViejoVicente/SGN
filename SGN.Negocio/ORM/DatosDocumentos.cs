using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class DatosDocumentos
    {
        public int IdRegistro { get; set; } = 0;
        public int IdHojaDatos { get; set; }= 0;
        public int IdVariente { get; set; }= 0;
        public string TextoFigura { get; set; } = "";
        public int IdDoc { get; set; } = 0;
        public string Observaciones { get; set; } = "";
    }
}
