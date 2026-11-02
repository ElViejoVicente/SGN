using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class Cat_DocumentosPorAnexo
    {
        public string IDAnexo { get; set; }
        public string TextoDocuemento { get; set; }
        public bool CopiaRequerida { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
