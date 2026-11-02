using System;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using SGN.Negocio.CRUD;
using SGN.Negocio.ORM;
using SGN.Web.Controles.Servidor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SGN.Web.Catalogos
{
    public partial class CatAnexos : PageBase
    {
        private readonly DatosCrud datosCrud = new DatosCrud();

        private List<Cat_Anexos> Anexos
        {
            get { return Session["sseCatAnexos"] as List<Cat_Anexos> ?? new List<Cat_Anexos>(); }
            set { Session["sseCatAnexos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) CargarAnexos();
        }

        private void CargarAnexos()
        {
            Anexos = datosCrud.ConsultaCatAnexos();
            gvAnexos.DataBind();
        }

        protected void gvAnexos_DataBinding(object sender, EventArgs e) { gvAnexos.DataSource = Anexos; }

        protected void gvAnexos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "CargarRegistros") CargarAnexos();
        }

        protected void gvAnexos_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ValidarRequerido(e, "IdAnexo", "Clave");
            ValidarRequerido(e, "NombreAnexo", "Nombre");
            ValidarRequerido(e, "Descripcion", "Descripción");
            string id = Convert.ToString(e.NewValues["IdAnexo"]).Trim();
            bool duplicado = Anexos.Any(x => string.Equals(x.IdAnexo, id, StringComparison.OrdinalIgnoreCase))
                && (e.IsNewRow || !string.Equals(Convert.ToString(e.Keys[0]), id, StringComparison.OrdinalIgnoreCase));
            if (duplicado) e.RowError += "Ya existe un anexo con esa clave.\n";
        }

        private static void ValidarRequerido(DevExpress.Web.Data.ASPxDataValidationEventArgs e, string campo, string etiqueta)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(e.NewValues[campo])))
                e.RowError += "El campo " + etiqueta + " es obligatorio.\n";
        }

        protected void gvAnexos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            datosCrud.AltaCatAnexos(CrearAnexo(e.NewValues));
            e.Cancel = true;
            gvAnexos.CancelEdit();
            CargarAnexos();
        }

        protected void gvAnexos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (HayCambios(e.OldValues, e.NewValues))
            {
                var anexo = CrearAnexo(e.NewValues);
                anexo.IdAnexo = Convert.ToString(e.Keys[0]);
                datosCrud.ActualizarCatAnexos(anexo);
            }
            e.Cancel = true;
            gvAnexos.CancelEdit();
            CargarAnexos();
        }

        protected void gvAnexos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            datosCrud.EliminarCatAnexos(new Cat_Anexos { IdAnexo = Convert.ToString(e.Keys[0]) });
            e.Cancel = true;
            CargarAnexos();
        }

        protected void gvAnexos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!gvAnexos.IsNewRowEditing && e.Column.FieldName == "IdAnexo") e.Editor.ReadOnly = true;
        }

        private static Cat_Anexos CrearAnexo(System.Collections.Specialized.OrderedDictionary values)
        {
            return new Cat_Anexos
            {
                IdAnexo = Convert.ToString(values["IdAnexo"]).Trim(),
                NombreAnexo = Convert.ToString(values["NombreAnexo"]).Trim(),
                Descripcion = Convert.ToString(values["Descripcion"]).Trim(),
                Activo = values["Activo"] != null && Convert.ToBoolean(values["Activo"])
            };
        }

        private static bool HayCambios(System.Collections.Specialized.OrderedDictionary anteriores, System.Collections.Specialized.OrderedDictionary nuevos)
        {
            foreach (DictionaryEntry item in anteriores)
                if (nuevos.Contains(item.Key) && !Equals(nuevos[item.Key], item.Value)) return true;
            return false;
        }

        protected void gvAnexos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            if (e.Item.Name == "CustomExportToXLS")
                ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            else if (e.Item.Name == "CustomExportToXLSX")
                ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }
    }
}
