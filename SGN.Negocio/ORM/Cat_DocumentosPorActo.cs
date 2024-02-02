using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class Cat_DocumentosPorActo
    {
        public int IdDoc { get; set; } = 0;
        public int IdActo { get; set; } = 0;
        public string TextoDocumento { get; set; } = "";
        public bool CopiaRequerida { get; set; } = false;
        public string Descripcion { get; set; } = "";
        public bool Activo { get; set; }=false;
    }
}
