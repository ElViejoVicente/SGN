using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public class ExpedienteUnicoControl
    {
        public string IdExpedienteUnico { get; set; } = string.Empty;
        public int NumeroVersion { get; set; } = 0;
        public string NumExpediente { get; set; }
        public int Estatus { get; set; } = 1;
        public string UsuarioAutor { get; set; }
        public DateTime FechaCreacionVersion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int NumVersionAnexo { get; set; }
        public string NombreDelAnexo { get; set; }
        public string Instrumento { get; set; }
        public string Volumen { get; set; }
        public string ActividadVulnerable { get; set; }
        public string AnexoSnapshot { get; set; }
    }
}
