<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatDocumentosXAnexo.aspx.cs" Inherits="SGN.Web.Catalogos.CatDocumentosXAnexo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <title>SGN - Documentos por Anexo</title>
    <script type="text/javascript">
        window.onresize = AdjustSize;
        function AdjustSize() {
            var panel = document.querySelector('.topPanel'), area = document.getElementById('maindiv');
            if (area && typeof gvDocumentos !== 'undefined') gvDocumentos.SetHeight(area.clientHeight - (panel ? panel.offsetHeight : 0));
        }
        function cargarDocumentos() {
            var valor = cbAnexos.GetValue();
            gvDocumentos.PerformCallback(valor == null ? '' : valor.toString());
        }
        function OnToolbarItemClick(s, e) {
            if (e.item.name === 'CustomExportToXLS' || e.item.name === 'CustomExportToXLSX') { e.processOnServer = true; e.usePostBack = true; }
        }
    </script>
</head>
<body>
<form id="form1" runat="server" class="Principal">
    <dx:ASPxPanel ID="TopPanel" ClientInstanceName="TopPanel" runat="server" FixedPosition="WindowTop" FixedPositionOverlap="true" CssClass="topPanel">
        <PanelCollection><dx:PanelContent runat="server">
            <dx:ASPxRoundPanel ID="pnlOpciones" runat="server" ShowCollapseButton="true" Width="470px" HeaderText="Opciones de consulta:" View="GroupBox">
                <PanelCollection><dx:PanelContent runat="server">
                    <div style="display:flex;gap:10px;align-items:flex-end;">
                        <dx:ASPxComboBox ID="cbAnexos" ClientInstanceName="cbAnexos" runat="server" Width="280px" Caption="Anexo:" DropDownStyle="DropDownList"
                            AutoPostBack="true" OnDataBinding="cbAnexos_DataBinding" OnSelectedIndexChanged="cbAnexos_SelectedIndexChanged" />
                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ cargarDocumentos(); }" />
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent></PanelCollection>
            </dx:ASPxRoundPanel>
        </dx:PanelContent></PanelCollection>
    </dx:ASPxPanel>
    <section class="CLPageContent" id="maindiv">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDocumentos" />
        <dx:ASPxGridView ID="gvDocumentos" ClientInstanceName="gvDocumentos" runat="server" AutoGenerateColumns="false" Width="100%" KeyFieldName="IDAnexo;TextoDocuemento"
            OnDataBinding="gvDocumentos_DataBinding" OnCustomCallback="gvDocumentos_CustomCallback" OnRowValidating="gvDocumentos_RowValidating"
            OnRowInserting="gvDocumentos_RowInserting" OnRowUpdating="gvDocumentos_RowUpdating" OnRowDeleting="gvDocumentos_RowDeleting"
            OnCellEditorInitialize="gvDocumentos_CellEditorInitialize" OnToolbarItemClick="gvDocumentos_ToolbarItemClick">
            <ClientSideEvents Init="AdjustSize" ToolbarItemClick="OnToolbarItemClick" />
            <SettingsEditing Mode="PopupEditForm" />
            <SettingsPopup><EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Modal="true" /></SettingsPopup>
            <SettingsPager Mode="ShowPager" PageSize="100" />
            <Settings ShowFooter="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
            <SettingsBehavior AllowGroup="true" AllowDragDrop="false" AllowSelectByRowClick="true" AllowSort="true" ConfirmDelete="true" EnableCustomizationWindow="true" />
            <SettingsDataSecurity AllowInsert="true" AllowDelete="true" AllowEdit="true" />
            <SettingsSearchPanel Visible="true" />
            <SettingsCommandButton>
                <EditButton Text="" ButtonType="Image"><Image ToolTip="Editar" IconID="edit_edit_16x16" /></EditButton>
                <DeleteButton Text="" ButtonType="Image"><Image ToolTip="Eliminar" IconID="edit_delete_16x16" /></DeleteButton>
                <NewButton Text="" ButtonType="Image"><Image ToolTip="Nuevo" IconID="actions_add_16x16" /></NewButton>
                <UpdateButton Text="" ButtonType="Image"><Image ToolTip="Aceptar" IconID="actions_apply_16x16" /></UpdateButton>
                <CancelButton Text="" ButtonType="Image"><Image ToolTip="Cancelar" IconID="actions_cancel_16x16" /></CancelButton>
            </SettingsCommandButton>
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" ButtonRenderMode="Button" Width="70px" />
                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Documento" FieldName="TextoDocuemento" Width="720px" />
                <dx:GridViewDataMemoColumn VisibleIndex="2" Caption="Descripción" FieldName="Descripcion" Width="250px" />
                <dx:GridViewDataCheckColumn VisibleIndex="3" Caption="Requiere copia" FieldName="CopiaRequerida" Width="130px" />
                <dx:GridViewDataCheckColumn VisibleIndex="4" Caption="Activo" FieldName="Activo" Width="100px" />
            </Columns>
            <Toolbars><dx:GridViewToolbar><Items>
                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" Alignment="Right" />
                <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" Alignment="Right"><Items>
                    <dx:GridViewToolbarItem Command="ExportToPdf" /><dx:GridViewToolbarItem Command="ExportToCsv" />
                    <dx:GridViewToolbarItem Name="CustomExportToXLS" Text="Exportar a XLS" /><dx:GridViewToolbarItem Name="CustomExportToXLSX" Text="Exportar a XLSX" />
                </Items></dx:GridViewToolbarItem>
            </Items></dx:GridViewToolbar></Toolbars>
        </dx:ASPxGridView>
    </section>
</form>
</body>
</html>
