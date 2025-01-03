using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
    public  class ListaNegraSAT
    {

        public long No { get; set; } = 0;
        public string RFC { get; set; } = string.Empty;
        public string NombreContribuyente { get; set; }= string.Empty;
        public string SitucacionContribuyente { get; set; } = string.Empty;
        public string NumFechaOficioGlobalPresun { get; set; } = string.Empty;
        public string PubliPaginaPresuntos { get; set; } = string.Empty;
        public string NumFechaOficioGlobalDOF { get; set; } = string.Empty;
        public string PubDofPresuntos { get; set; } = string.Empty;
        public string NumFechaOficioGlobalDesvirtuaron { get; set; } = string.Empty;
        public string Pubdesvirtuados { get; set; } = string.Empty;
        public string NumFechaOficioGlobalDesvirtuaronDof { get; set; } = string.Empty;
        public string PubDofDesvirtuados { get; set; } = string.Empty;
        public string NumFechaOficioGlobalDefinitivos { get; set; } = string.Empty;
        public string PubDefinitivos { get; set; } = string.Empty;
        public string NumFechaOficioGlobalDefinitivosDof { get; set; } = string.Empty;
        public string PubDofDefinitivos { get; set; } = string.Empty;
        public string NumFechaOficioGlobalSentencioFavorable { get; set; } = string.Empty;
        public string PubSentencioFavarable { get; set; } = string.Empty;
        public string NumFechaOficioGlobalSentenciaFavorableDof { get; set; } = string.Empty;
        public string PubDofSentenciaFavorable { get; set; } = string.Empty;
    }
}
