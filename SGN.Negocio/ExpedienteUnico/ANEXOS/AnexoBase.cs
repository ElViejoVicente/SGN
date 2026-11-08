using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ExpedienteUnico.ANEXOS
{
    public  class AnexoBase
    {


        public Dictionary<string, string> DocumentosSeleccionados { get; set; } = new Dictionary<string, string>();
    }
}
