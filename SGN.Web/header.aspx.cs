using SGN.Negocio.Operativa;
using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web
{
    public partial class header : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {      
           

            if (!Page.IsPostBack)
            {
                Response.Expires = 0;

               
                obtieneDatosUsuario();

            }

        }

        private void obtieneDatosUsuario()
        {
            try
            {
                string strHora= DateTime.Now.ToString("HH:mm:ss");
                if(UsuarioPagina != null)
                {
                    HidUsuario.Value = UsuarioPagina.Id.ToString();
                }


                //lblHora.Text = System.DateTime.Today.ToLongDateString();
                //lblNomUsuario.Text = ((Usuario)Session["usuario"]).Nombre;
                try
                {
                    if (Session["urlMenu"] != null)
                    {
                        String url = Session["urlMenu"].ToString();
                        string[] paramsURL = url.Split('=');

                        //foreach (var parameter in paramsURL)
                        //{
                        //    System.Console.WriteLine($"<{word}>");
                        //}
                        CargarConfigruacionHead(int.Parse(paramsURL[1].ToString()));

                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex )
            {
                throw;
            }
        }



        protected void CargarConfigruacionHead(int codPAgina)
        {
            try
            {
                DataTable confiPAgina = datosUsuario.DameConfiguracionPagina(codPAgina);
                if (confiPAgina.Rows.Count > 0)
                {
                    lblNombrePagina.Text = "/" + confiPAgina.Rows[0]["fcDesModuloLargo"].ToString() + "  -  " + confiPAgina.Rows[0]["fiVersion"].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                //cuSweetMsgbox1.mostrarMensaje(ex.Message, Controles.Usuario.SweetMsgBox.tipoMsg.error);
            }

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)

            {
                SGN.Negocio.Operativa.Usuario usu = (SGN.Negocio.Operativa.Usuario)Session["usuario"];
                //datosUsuario.borrarlogusuario(usu.UserName);
                //datosUsuario.QuitarExpbloqueadas(usu.UserName);
            }

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "window.parent.location.href='/login.aspx'; ", true);


        }
    }
}