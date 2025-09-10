using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using SGN.Negocio.CRUD;
using SGN.Negocio.Inventarios;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;
using SGN.Web.Controles.Servidor;

namespace SGN.Web.Catalogos
{

    public partial class CatClientes : System.Web.UI.Page
    {


        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
    public List<Cat_Clientes> catClientes
    {
        get

        {
            List<Cat_Clientes> sseCliente = new List<Cat_Clientes>();
            if (this.Session["sseCatCliente"] != null)
            {
                sseCliente = (List<Cat_Clientes>)this.Session["sseCliente"];
            }

            return sseCliente;
        }
        set
        {
            this.Session["sseCliente"] = value;
        }

    }

    #endregion
   
        private void DameCatalogos()
        {
            catClientes = datosCrud.ConsultaCatClientes();
            gvClientes.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DameCatalogos();
            }
        }

        protected void gvClientes_DataBinding(object sender, EventArgs e)
        {
            gvClientes.DataSource = catClientes;
        }

        protected void gvClientes_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            Cat_Clientes nuevoObjeto = new Cat_Clientes();
            nuevoObjeto.Gestor = e.NewValues["Texto Gestor"].ToString();
            nuevoObjeto.NumTelefonico = e.NewValues["Texto Numero Telefonico"].ToString();
            nuevoObjeto.CorreoElectronico = e.NewValues["CorreoElectronico"].ToString();
            nuevoObjeto.Observaciones = e.NewValues["Texto Observaciones"].ToString();

            datosCrud.AltaCatClientes(nuevoObjeto);

            e.Cancel = true;
            gvClientes.CancelEdit();

            DameCatalogos();

         }

        protected void gvClientes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            Boolean exitenCambios = false;

             foreach(DictionaryEntry item in e.OldValues)
            {
                if (e.NewValues.Contains(item.Key))
                {
                    if (e.NewValues[item.Key] != null && !e.NewValues[item.Key].Equals(item.Value))
                    {

                        exitenCambios = true;
                        break;
                    }
                }
            }

            if (exitenCambios == false)
            {
                gvClientes.CancelEdit();
                e.Cancel = true;
                return;
            }

            var miRegistro = catClientes.Where(x => x.idCliente == Convert.ToInt64(e.Keys[0])).First();

            if (miRegistro != null)
            {
                miRegistro.Gestor = e.NewValues["Texto gestor"].ToString();
                miRegistro.NumTelefonico = e.NewValues["Texto Numero Telefonico"].ToString();
                miRegistro.CorreoElectronico = e.NewValues["CorreoElectronico"].ToString();
                miRegistro.Observaciones = e.NewValues["Texto Observaciones"].ToString();
            }

            datosCrud.ActualizarCatClientes(miRegistro);

            e.Cancel = true;
            gvClientes.CancelEdit();
            DameCatalogos();

        }

        protected void gvClientes_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            if (e.NewValues["Texto Gestor"] == null)
            {
                e.RowError += "El campo Texto Gestor es obligatorio.\n ";
            }

            if (e.NewValues["Texto Numero Telefonico"] == null)
            {
                e.RowError += "El campo Texto Numero Telefonico es obligatorio.\n ";
            }

            if (e.NewValues["CorreoElectronico"] == null)
            {
                e.RowError += "El campo CorreoElectronico es obligatorio.\n ";
            }

            if (e.NewValues["Texto Observaciones"] == null)
            {
                e.RowError += "El campo Texto Observaciones es obligatorio.\n ";
            }
        }

        protected void gvClientes_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {

                case "CustomExportToXLS":
                    gridExporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;
                case "CustomExportToXLSX":
                    gridExporter.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;

                default:
                    break;
            }
        }

        protected void gvClientes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("CargarLista"))
            {
                DameCatalogos();
                return;
            }
        }
    }
}