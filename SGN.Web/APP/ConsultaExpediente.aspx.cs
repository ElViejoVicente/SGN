using System;
using System.Web.UI;
using DevExpress.Web;

namespace SGN.Web.APP
{
    public partial class ConsultaExpediente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            // Obtener el folio ingresado por el usuario
            string folio = txtFolioIterno.Text.Trim();

            // Simular procesamiento del folio (aquí va tu lógica real)
            string estado = "";
            if (string.IsNullOrEmpty(folio))
            {
                estado = "El folio no puede estar vacío.";
            }
            else if (folio == "125-9-2025S")
            {
                estado = "Folio encontrado: ESTATUS = FINALIZADO";
            }
            else if (folio == "12-10-2024C")
            {
                estado = "Folio encontrado: ESTATUS = EN PROCESO";
            }
            else if (folio == "1-1-2026S")
            {
                estado = "Folio encontrado: ESTATUS = PENDIENTE";
            }
            else
            {
                estado = "Folio no encontrado o formato incorrecto.";
            }

            // Mostrar el estado en el memo
            txtEstatusFolio.Text = estado;
        }

        protected void cpConsultaFolio_Callback(object sender, CallbackEventArgsBase e)
        {
            string folio = e.Parameter?.Trim() ?? "";
            string estado = "";
            if (string.IsNullOrEmpty(folio))
            {
                estado = "El folio no puede estar vacío.";
            }
            else if (folio == "125-9-2025S")
            {
                estado = "Folio encontrado: ESTATUS = FINALIZADO";
            }
            else if (folio == "12-10-2024C")
            {
                estado = "Folio encontrado: ESTATUS = EN PROCESO";
            }
            else if (folio == "1-1-2026S")
            {
                estado = "Folio encontrado: ESTATUS = PENDIENTE";
            }
            else
            {
                estado = "Folio no encontrado o formato incorrecto.";
            }
            txtEstatusFolio.Text = estado;
        }
    }
}