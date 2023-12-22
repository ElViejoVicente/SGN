using Dapper;
using System;
using SGN.Negocio.ORM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.CRUD
{
    public class DatosCrud
    {

        protected String cnn = ConfigurationManager.AppSettings["sqlConn.ConnectionString"];

        #region Cat_Actos
        public List<Cat_Actos> ConsultaCatActos()
        {
            try
            {
                List<Cat_Actos> resultado = new List<Cat_Actos>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_Actos>(sql: "sp_CRUD_Cat_Actos_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Insert", param: new
                    {
                        values.TextoActo,
                        values.Descripcion,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Update", param: new
                    {
                        values.TextoActo,
                        values.Descripcion,
                        values.Activo

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarCatActos(Cat_Actos values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Actos_Delete", param: new
                    {
                       values.IdActo 

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Actos_Delete, detalle: \n" + ex.Message, ex);
            }
        }
        #endregion

        #region Cat_Estatus

        public List<Cat_Estatus> ConsultaCatEstatus()
        {
            try
            {
                List<Cat_Estatus> resultado = new List<Cat_Estatus>();

                using (var db = new SqlConnection(cnn))
                {
                    resultado = db.Query<Cat_Estatus>(sql: "sp_CRUD_Cat_Estatus_Select").ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Estatus_Select, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean AltaCatEstatus(Cat_Estatus values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Estatus_Insert", param: new
                    {
                        values.TextoEstatus,
                        values.Descripcion

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar sp_CRUD_Cat_Estatus_Insert, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean ActualizarCatEstatus(Cat_Estatus values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Estatus_Update ", param: new
                    {
                        values.IdEstatus,
                        values.TextoEstatus,
                        values.Descripcion

                    }, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Estatus_Update, detalle: \n" + ex.Message, ex);
            }
        }
        public Boolean EliminarCatEstatus(Cat_Estatus values)
        {
            try
            {
                using (var db = new SqlConnection(cnn))
                {
                    db.Execute(sql: "sp_CRUD_Cat_Estatus_Delete", param: new
                    {
                       values.IdEstatus

                    }, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar sp_CRUD_Cat_Estatus_Delete, detalle: \n" + ex.Message, ex);
            }
        }


        #endregion
        #region Cat_Proyectistas

        #endregion
        #region Expedientes

        #endregion
    }
}
