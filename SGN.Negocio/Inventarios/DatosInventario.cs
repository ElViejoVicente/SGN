using Dapper;
using SGN.Negocio.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Inventarios
{
    public  class DatosInventario
    {

        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];



        public List<Inventario> DameListaInventario()
        {
            try
            {
                List<Inventario> resultado = new List<Inventario>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Inventario>(sql: "sp_DameListaInventario").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_DameListaInventario, detalle: \n" + ex.Message, ex);
            }
        }



    }
}
