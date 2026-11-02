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
    public partial class CatDocumentosXAnexo : PageBase
    {
        private readonly DatosCrud datosCrud = new DatosCrud();

        private List<Cat_Anexos> Anexos
        {
            get { return Session["sseAnexosDocumentos"] as List<Cat_Anexos> ?? new List<Cat_Anexos>(); }
            set { Session["sseAnexosDocumentos"] = value; }
        }

        private List<Cat_DocumentosPorAnexo> Documentos
        {
            get { return Session["sseDocumentosPorAnexo"] as List<Cat_DocumentosPorAnexo> ?? new List<Cat_DocumentosPorAnexo>(); }
            set { Session["sseDocumentosPorAnexo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Anexos = datosCrud.ConsultaCatAnexos().Where(x => x.Activo).ToList();
                cbAnexos.DataBind();
                Documentos = new List<Cat_DocumentosPorAnexo>();
                gvDocumentos.DataBind();
            }
        }

        protected void cbAnexos_DataBinding(object sender, EventArgs e)
        {
            cbAnexos.TextField = "NombreAnexo";
            cbAnexos.ValueField = "IdAnexo";
            cbAnexos.DataSource = Anexos;
        }

        protected void cbAnexos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDocumentos(Convert.ToString(cbAnexos.Value));
        }

        private void CargarDocumentos(string idAnexo)
        {
            Documentos = string.IsNullOrWhiteSpace(idAnexo) ? new List<Cat_DocumentosPorAnexo>()
                : datosCrud.ConsultaCatDocumentosPorAnexo(idAnexo);
            gvDocumentos.DataBind();
        }

        protected void gvDocumentos_DataBinding(object sender, EventArgs e) { gvDocumentos.DataSource = Documentos; }
        protected void gvDocumentos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) { CargarDocumentos(e.Parameters); }

        protected void gvDocumentos_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(e.NewValues["TextoDocuemento"])))
                e.RowError += "El campo Documento es obligatorio.\n";
            if (string.IsNullOrWhiteSpace(Convert.ToString(e.NewValues["Descripcion"])))
                e.RowError += "El campo Descripción es obligatorio.\n";
            if (cbAnexos.Value == null) e.RowError += "Debe seleccionar un anexo.\n";

            string texto = Convert.ToString(e.NewValues["TextoDocuemento"]).Trim();
            bool duplicado = Documentos.Any(x => string.Equals(x.TextoDocuemento, texto, StringComparison.OrdinalIgnoreCase))
                && (e.IsNewRow || !string.Equals(Convert.ToString(e.Keys["TextoDocuemento"]), texto, StringComparison.OrdinalIgnoreCase));
            if (duplicado) e.RowError += "El documento ya existe para el anexo seleccionado.\n";
        }

        protected void gvDocumentos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string idAnexo = Convert.ToString(cbAnexos.Value);
            datosCrud.AltaCatDocumentosPorAnexo(CrearDocumento(e.NewValues, idAnexo));
            e.Cancel = true;
            gvDocumentos.CancelEdit();
            CargarDocumentos(idAnexo);
        }

        protected void gvDocumentos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string idAnexo = Convert.ToString(e.Keys["IDAnexo"]);
            if (HayCambios(e.OldValues, e.NewValues))
            {
                var documento = CrearDocumento(e.NewValues, idAnexo);
                documento.TextoDocuemento = Convert.ToString(e.Keys["TextoDocuemento"]);
                datosCrud.ActualizarCatDocumentosPorAnexo(documento);
            }
            e.Cancel = true;
            gvDocumentos.CancelEdit();
            CargarDocumentos(idAnexo);
        }

        protected void gvDocumentos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var documento = new Cat_DocumentosPorAnexo
            {
                IDAnexo = Convert.ToString(e.Keys["IDAnexo"]),
                TextoDocuemento = Convert.ToString(e.Keys["TextoDocuemento"])
            };
            datosCrud.EliminarCatDocumentosPorAnexo(documento);
            e.Cancel = true;
            CargarDocumentos(documento.IDAnexo);
        }

        protected void gvDocumentos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!gvDocumentos.IsNewRowEditing && e.Column.FieldName == "TextoDocuemento") e.Editor.ReadOnly = true;
        }

        private static Cat_DocumentosPorAnexo CrearDocumento(System.Collections.Specialized.OrderedDictionary values, string idAnexo)
        {
            return new Cat_DocumentosPorAnexo
            {
                IDAnexo = idAnexo,
                TextoDocuemento = Convert.ToString(values["TextoDocuemento"]).Trim(),
                Descripcion = Convert.ToString(values["Descripcion"]).Trim(),
                CopiaRequerida = values["CopiaRequerida"] != null && Convert.ToBoolean(values["CopiaRequerida"]),
                Activo = values["Activo"] != null && Convert.ToBoolean(values["Activo"])
            };
        }

        private static bool HayCambios(System.Collections.Specialized.OrderedDictionary anteriores, System.Collections.Specialized.OrderedDictionary nuevos)
        {
            foreach (DictionaryEntry item in anteriores)
                if (nuevos.Contains(item.Key) && !Equals(nuevos[item.Key], item.Value)) return true;
            return false;
        }

        protected void gvDocumentos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            if (e.Item.Name == "CustomExportToXLS")
                ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            else if (e.Item.Name == "CustomExportToXLSX")
                ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }
    }
}
