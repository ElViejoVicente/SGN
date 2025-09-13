using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.APP
{
    public partial class ConsultaExpediente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (captcha.IsValid)
            {
                txtEstatusFolio.Text = "Estatus de prueba esto se tiene que consultar desde BBDDA";
            }
            else
            {
                txtEstatusFolio.Text = "CapÇha no valido";
            }



        }
    }
}