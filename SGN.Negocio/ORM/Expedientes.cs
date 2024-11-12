using SGN.Negocio.Operativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
	public class Expedientes
	{
        // Datos ya no necesario ya que ahora estan en la tabla hoja de datos


        //      public string numReciboPago { get; set; } = "";
        //      public string numReciboPago2 { get; set; }
        public string IdExpediente { get; set; } = "";
        public int IdHojaDatos { get; set; } = 0;
        public string IdEstatus { get; set; } = "";
        //      public int IdActo { get; set; } = 0;
        //      public DateTime FechaIngreso { get; set; } = Constantes.FechaGlobal;
        //      public string Faltantes { get; set; } = "";

        //Expedientes
        public string Otorga { get; set; } = "";
		public string AfavorDe { get; set; } = "";
		//public string OperacionProyectada { get; set; } = "";
		public string UbicacionPredio { get; set; } = "";
		

        //Aviso preventivo
        public DateTime FechaElaboracion { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaEnvioRPP { get; set; } = Constantes.FechaGlobal;
		public bool EsTramitePorSistema { get; set; } = false;
		public DateTime FechaPagoBoleta { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRecibidoRPP { get; set; } = Constantes.FechaGlobal;

        //Proyecto
        public string NombreProyectista { get; set; } = "";
		public DateTime FechaAsignacionProyectista { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaPrevistaTerminoProyectista { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaAvisoPreventivo { get; set; } = Constantes.FechaGlobal;
        public decimal ISR { get; set; } = 0;
        public decimal ValorOperacion { get; set; } = 0;

        //Firmas
        public string NotasFirma { get; set; } = "";
		public int Escritura { get; set; } = 0;
		public int Volumen { get; set; } = 0;
        public bool AplicaTraslado { get; set; } = false;
		public DateTime FechaRecepcionTerminoEscritura { get; set; } = Constantes.FechaGlobal;

        //Aviso definitivo
        public DateTime FechaElaboracionDefinitivo { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaEnvioRPPDefinitivo { get; set; } = Constantes.FechaGlobal;
		public bool EsTramitePorSistemaDefinitivo { get; set; } = false;
		public DateTime FechaPagoBoletaDefinitivo { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaRecibidoRPPDefinitivo { get; set; } = Constantes.FechaGlobal;

        //Escrituracion
        public DateTime FechaRecibioTraslado { get; set; } = Constantes.FechaGlobal;
        public DateTime FechaAsignacionMesa { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaTerminoMesa { get; set; } = Constantes.FechaGlobal;

        //Entrega
        public string ObservacionesEngrega { get; set; } = "";
        public bool RegistroEntrega { get; set; } = false;       
        public DateTime FechaRegistroEntrega { get; set; } = Constantes.FechaGlobal;
		public DateTime FechaBoletaPagoRegistroEntrega { get; set; } = Constantes.FechaGlobal;
        public DateTime FechaRegresoRegistro { get; set; } = Constantes.FechaGlobal;      
		public DateTime FechaSalida { get; set; } = Constantes.FechaGlobal;
		public string ObservacionesTramiteTerminado { get; set; } = "";
	}
}
