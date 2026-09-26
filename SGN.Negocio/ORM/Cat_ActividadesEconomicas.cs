using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class Cat_ActividadesEconomicas
    {
        public int Clave { get; set; }
        public string Grupo { get; set; } = "";
        public string SubGrupo { get; set; } = "";
        public string DescripcionActividad { get; set; } = "";
        public string ActividadesQueIncluye { get; set; } = "";
    }
}
