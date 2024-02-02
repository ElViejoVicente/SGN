using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class RecibosDePago
    {
        public int IdRegistro { get; set; } = 0;
        public int NumRecibo { get; set; } = 0;
        public int IdHojaDatos { get; set; } = 0;   
        public decimal CantidadTotal { get; set; }= 0;
        public decimal CantidadAbonada { get; set; } = 0;
        public string Concepto { get; set; } = "";
        public string UsuarioRecibe { get; set; } = "";
        public string NotaComentario { get; set; } = "";
    }
}
