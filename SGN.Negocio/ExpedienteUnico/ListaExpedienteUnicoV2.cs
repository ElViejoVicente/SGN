using SGN.Negocio.Operativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ExpedienteUnico
{
    public class ListaExpedienteUnicoV2
    {


        public string ImageEstado { get; set; } = "";
        public string IdExpediente { get; set; } = "";
        public string IdEstatus { get; set; } = "";
        public string TextoEstatus { get; set; } = "";
        public string TextoActo { get; set; } = "";
        public DateTime FechaIngreso { get; set; } = Constantes.FechaGlobal;
        public string TextoVariante { get; set; } = "";
        public bool RequiereExUnico { get; set; } = false;
        public int IdRegistro { get; set; } = 0;
        public int IdHojaDatos { get; set; } = 0;
        public string FiguraOperacion { get; set; } = "";
        public string RolOperacion { get; set; } = "";

        public string Nombres { get; set; } = "";
        public string ApellidoPaterno { get; set; } = "";
        public string ApellidoMaterno { get; set; } = "";
        public DateTime FechaNacimiento { get; set; } = Constantes.FechaGlobal;
        public string Sexo { get; set; } = "";
        public string Ocupacion { get; set; } = "";
        public string EstadoCivil { get; set; } = "";
        public string RegimenConyugal { get; set; } = "";
        public string SabeLeerEscribir { get; set; } = "";
        public string Notas { get; set; } = "";

        public string IdExpedienteUnico { get; set; } = "";

        public string ActividadVulnerable { get; set; } = "";

        public DateTime FechaCreacionVersion { get; set; } = Constantes.FechaGlobal;
        public DateTime FechaUltimaModificacion { get; set; } = Constantes.FechaGlobal;

        public string NombreDelAnexo { get; set; } = "";

        public string UsuarioAutor { get; set; } = "";


        public string Resumen
        {
            get
            {
                return IdExpediente.Trim() + " - " + TextoActo.Trim() + " - " + TextoVariante.Trim() + " - " + FechaIngreso.ToString() + " - " + TextoEstatus;
            }

        }



    }
}
