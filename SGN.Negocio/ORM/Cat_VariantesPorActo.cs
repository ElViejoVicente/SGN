using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class Cat_VariantesPorActo
    {
        public int IdVariante { get; set; } = 0;
        public int IdActo { get; set; } = 0;    
        public string TextoVariante { get; set; } = "";
        public string Descripcion { get; set; } = "";       
        public bool Activo { get; set; } = false;
    }
}
