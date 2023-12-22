using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.ORM
{
	public class Cat_Actos
	{
		public int IdActo { get; set; } = 0;
		public string TextoActo { get; set; } = "";
		public string Descripcion { get; set; } = "";
		public bool Activo { get; set; } = false;
	}
}
