<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedientes.aspx.cs" Inherits="SGN.Web.Expedientes.PanelExpedientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../Scripts/mensajes.js"></script>

    <script type="text/javascript">

        /* Script de funcionalidad de la pagina OJO solo colocar en este bloque */
        window.onresize = function (event) {
            AdjustSize();
        };

        function OnInit(s, e) {
            s.GetWindowElement(-1).className += " popupStyle";
        }
        function AdjustSize() {

            var height = document.getElementById('maindiv').clientHeight;  // I have some buttons below the grid so needed -50
            var width = document.getElementById('maindiv').clientWidth;
            gvExpedientes.SetHeight(height);

        }


        function gridView_EndCallback(s, e) {

            if (s.cp_swMsg != null) {
                mostrarMensajeSweet(s.cp_swType, s.cp_swMsg);
                s.cp_swType = null;
                s.cp_swMsg = null;
            }

            //validar con un parametro si es necesario el refreco de los datos

            if (s.cp_Update != null) {

                gvExpedientes.UnselectRows();
                gvExpedientes.PerformCallback('CargarRegistros');
                s.cp_Update = null;
            }
        }

        function OnToolbarItemClick(s, e) {

            switch (e.item.name) {

                case "CustomExportToXLS":
                    e.processOnServer = true;
                    e.usePostBack = true;
                    break;
                case "CustomExportToXLSX":
                    e.processOnServer = true;
                    e.usePostBack = true;
                    break;
                case "cmdVerRegistro":


                    if (gvRiesgos.GetFocusedRowIndex() >= 0) {
                        gvRiesgos.GetRowValues(gvRiesgos.GetFocusedRowIndex(), 'Id_Registro', onCallbackOneValueRead);
                    }
                    break;

                case "cmdEditarRegistro":
                    if (gvRiesgos.GetFocusedRowIndex() >= 0) {
                        gvRiesgos.GetRowValues(gvRiesgos.GetFocusedRowIndex(), 'Id_Registro', onCallbackOneValueEdit);
                    }
                    break;

                case "cmdEditarEstado":
                    if (gvRiesgos.GetFocusedRowIndex() >= 0) {
                        ppEditarEstado.Show(); ppEditarEstado.PerformCallback("cargarListaEstados");
                    }
                    break;

                case "cmdNuevaAccion":
                    if (gvRiesgos.GetFocusedRowIndex() >= 0) {
                        ppNuevaAccion.Show(); ppNuevaAccion.PerformCallback();
                    }
                    break;

            }
        }



    </script>



    <title> SGN </title>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">
        <section class="CLPageContent" id="maindiv">
            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExpedientes"></dx:ASPxGridViewExporter>


            <dx:ASPxGridView runat="server" ID="gvExpedientes" ClientInstanceName="gvExpedientes" AutoGenerateColumns="False" Width="100%" KeyFieldName="Id_Registro"
                OnDataBinding="gvExpedientes_DataBinding"
                OnCustomCallback="gvExpedientes_CustomCallback"
                OnToolbarItemClick="gvExpedientes_ToolbarItemClick"
                OnHtmlDataCellPrepared="gvExpedientes_HtmlDataCellPrepared">

                <ClientSideEvents Init="AdjustSize" EndCallback="gridView_EndCallback" />

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                <SettingsPager Mode="ShowAllRecords" />

                <Settings ShowFooter="True" ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                <SettingsCookies Enabled="true" />

                <SettingsResizing ColumnResizeMode="Control" />
                <%--    <SettingsDetail ExportMode="All" ShowDetailRow="true" />--%>

                <SettingsBehavior
                    AllowGroup="true"
                    AllowDragDrop="true"
                    AllowFixedGroups="false"
                    AllowSelectByRowClick="true"
                    AllowSelectSingleRowOnly="false"
                    AutoExpandAllGroups="true"
                    AllowFocusedRow="True"
                    ProcessFocusedRowChangedOnServer="False"
                    AllowSort="true"
                    ConfirmDelete="true"
                    EnableCustomizationWindow="true"></SettingsBehavior>

                <SettingsDataSecurity AllowInsert="true" AllowDelete="true" AllowEdit="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />

                <Columns>


                    
                    <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Num Expediente" FieldName="IdExpediente" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataSpinEditColumn VisibleIndex="2" Caption="Id Estatus" FieldName="IdEstatus" ReadOnly="true" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                               <dx:GridViewDataSpinEditColumn VisibleIndex="3" Caption="Id Acto" FieldName="IdActo" ReadOnly="true" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>


                   
                    <dx:GridViewDataTextColumn VisibleIndex="2" Caption="IdEstado" FieldName="Id_Estatus" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="4" Caption="IdUsuarioDeclarante" FieldName="Id_Usuario_Declarante" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Id centro trabajo" FieldName="Id_Centro_Trabajo" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Id_Seccion" FieldName="Id_Seccion" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Id_Usuario_Responsable_Area_Seccion" FieldName="Id_Usuario_Responsable_Area_Seccion" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="14" Caption="Id_Unidad_Negocio" FieldName="Id_Unidad_Negocio" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="16" Caption="Id_Sub_Unidad_Negocio" FieldName="Id_Sub_Unidad_Negocio" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="19" Caption="Id_Ambito" FieldName="Id_Ambito" Width="100px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Estado" FieldName="Texto_Estado_Riesgo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Usuario Declarante" FieldName="Nombre_Usuario_Declarante" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="6" Caption="Fecha registro" FieldName="Fecha_Entrada" Width="100px" Visible="true">
                        <PropertiesDateEdit DisplayFormatString="g"></PropertiesDateEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Centro trabajo" FieldName="Texto_Centro_trabajo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Tipo Comunicacion / riesgo" FieldName="Id_Tipo_Comunicacion" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="11" Caption="Seccion" FieldName="Texto_Seccion" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="13" Caption=" Responsable Area/Seccion" FieldName="Nombre_Responsable_Area_Seccion" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="15" Caption="Unidad de negocio" FieldName="Unidad_de_negocio" Width="120px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="17" Caption="Sub unidad de negocio" FieldName="Subunidad_de_negocio" Width="120px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="18" Caption="Usuario proyecto asignado" FieldName="Nombre_Usuario_Proyecto_Asignado" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="20" Caption="Ambito" FieldName="Ambito" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="21" Caption="Descripcion" FieldName="Descripcion" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="22" Caption="Accion Inmediata" FieldName="Accion_Inmediata" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="23" Caption="Dialogo" FieldName="Dialogo" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn VisibleIndex="24" Caption="Validado Responsable Area" FieldName="Validado_Responsable_Area" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="25" Caption="Motivo Rechazo" FieldName="Motivo_Rechazo_Validacion" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataCheckColumn VisibleIndex="26" Caption="Plan_Accion" FieldName="Requiere_Plan_Accion" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>


                </Columns>


                <Toolbars>
                    <dx:GridViewToolbar>
                        <Items>

                            <dx:GridViewToolbarItem Text="Mostrar Registro" Image-IconID="miscellaneous_viewonweb_16x16" Name="cmdVerRegistro" />

                            <dx:GridViewToolbarItem Text="Modificar Registro" Image-IconID="actions_edit_16x16devav" Name="cmdEditarRegistro" />

                            <dx:GridViewToolbarItem Text="Editar Estado" Image-IconID="pdfviewer_menu_svg_16x16" Name="cmdEditarEstado" />

                            <dx:GridViewToolbarItem Text="Nueva Accion" Image-IconID="tasks_newtask_16x16" Name="cmdNuevaAccion" />

                            <dx:GridViewToolbarItem Command="ShowCustomizationWindow" Alignment="Right" />
                            <dx:GridViewToolbarItem Text="Export to" Image-IconID="actions_download_16x16office2013" BeginGroup="true" AdaptivePriority="1" Alignment="Right">
                                <Items>
                                    <dx:GridViewToolbarItem Command="ExportToPdf" />
                                    <dx:GridViewToolbarItem Command="ExportToDocx" />
                                    <dx:GridViewToolbarItem Command="ExportToRtf" />
                                    <dx:GridViewToolbarItem Command="ExportToCsv" />
                                    <dx:GridViewToolbarItem Command="ExportToXls" Text="Export to XLS(DataAware)" />
                                    <dx:GridViewToolbarItem Name="CustomExportToXLS" Text="Export to XLS(WYSIWYG)" Image-IconID="export_exporttoxls_16x16office2013">
                                        <Image IconID="export_exporttoxls_16x16office2013"></Image>
                                    </dx:GridViewToolbarItem>
                                    <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to XLSX(DataAware)" />
                                    <dx:GridViewToolbarItem Name="CustomExportToXLSX" Text="Export to XLSX(WYSIWYG)" Image-IconID="export_exporttoxlsx_16x16office2013">
                                        <Image IconID="export_exporttoxlsx_16x16office2013"></Image>
                                    </dx:GridViewToolbarItem>
                                </Items>

                                <Image IconID="actions_download_16x16office2013"></Image>
                            </dx:GridViewToolbarItem>

                        </Items>

                        <SettingsAdaptivity Enabled="True" EnableCollapseRootItemsToIcons="True"></SettingsAdaptivity>
                    </dx:GridViewToolbar>
                </Toolbars>


            </dx:ASPxGridView>

        </section>
    </form>
</body>
</html>
