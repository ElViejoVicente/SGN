using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPS.Web.Controles.Usuario
{
    public partial class InfoMsgBoxMovil : System.Web.UI.UserControl
    {
        public enum tipoMsg
        {
            info = 1,
            success = 2,
            warning = 3,
            error = 4
        }

        public Boolean mostrarMensaje(string mensaje, tipoMsg tipoMenssaje)
        {
            try
            {
                if (mensaje.Trim() == "" && tipoMenssaje == 0)
                {
                    return false;
                }

                lblMensaje.Text = mensaje;

                if (tipoMenssaje == tipoMsg.info)
                {

                    ppControl.HeaderText = "Información";
                    ppControl.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#3ec2ee");
                    imgIcon.ImageUrl = "../../imagenes/mensajes/infoSinfondo.png";
                }
                else if (tipoMenssaje == tipoMsg.success)
                {
                    ppControl.HeaderText = "Éxito";
                    ppControl.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#69ca62");
                    imgIcon.ImageUrl = "../../imagenes/mensajes/successSinFondo.png";
                }
                else if (tipoMenssaje == tipoMsg.warning)
                {
                    ppControl.HeaderText = "Precaución";
                    ppControl.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb600");
                    imgIcon.ImageUrl = "../../imagenes/mensajes/warningSinFondo.png";
                }
                else if (tipoMenssaje == tipoMsg.error)
                {
                    ppControl.HeaderText = "Error";
                    ppControl.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#9e311c");
                    imgIcon.ImageUrl = "../../imagenes/mensajes/errorsinFondo.png";
                }
                ppControl.ShowOnPageLoad = true;
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}