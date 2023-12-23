using SGN.Negocio.Operativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Expediente
{
    public class ListaExpedientes
    {
		public string IdExpediente { get; set; } = "";
        public string numReciboPago { get; set; } = "";
        public string TextoEstatus { get; set; } = "";
		public string TextoActo { get; set; } = "";
		public DateTime FechaIngreso { get; set; } = Constantes.FechaGlobal;
		public string Otorga { get; set; } = "";
		public string AfavorDe { get; set; } = "";
		public string OperacionProyectada { get; set; } = "";
		public string UbicacionPredio { get; set; } = "";
		public string Faltantes { get; set; } = "";
		public DateTime FechaElaboracion { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaEnvioRPP { get; set; } = Constantes.FechaGlobal;
		public bool EsTramitePorSistema { get; set; }

		public DateTime FechaPagoBoleta { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRecibidoRPP { get; set; } = Constantes.FechaGlobal;

		public string NombreProyectista { get; set; } = "";
		public DateTime FechaAsignacionProyectista { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaPrevistaTerminoProyectista { get; set; } = Constantes.FechaGlobal;
		public string AvisoPreventivo { get; set; } = "";
		public decimal ISR { get; set; } = 0;
		public string NotasFirma { get; set; } = "";
		public int Escritura { get; set; } = 0;
		public int Volumen { get; set; } = 0;
		public DateTime FechaTrasladoEntregado { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaElaboracionDefinitivo { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaEnvioRPPDefinitivo { get; set; } = Constantes.FechaGlobal;
		public bool EsTramitePorSistemaDefinitivo { get; set; } = false;
		public DateTime FechaPagoBoletaDefinitivo { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRecibidoRPPDefinitivo { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRecepcionTerminoEscrituta { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaAsignacionMesa { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaTerminoMesa { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRegistroEntrega { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaBoletaPagoRegistroEntrega { get; set; } = Constantes.FechaGlobal;		
		public DateTime FechaSalida { get; set; } = Constantes.FechaGlobal;
		public string ObservacionesTramiteTerminado { get; set; } = "";

	}
}
