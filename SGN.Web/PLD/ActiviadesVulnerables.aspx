<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiviadesVulnerables.aspx.cs" Inherits="SGN.Web.PLD.ActiviadesVulnerables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../Scripts/mensajes.js"></script>

    <title>SGN</title>

    <script type="text/javascript">

        /* Script de funcionalidad de la pagina OJO solo colocar en este bloque */
        window.onresize = function (event) {
            AdjustSize();
        };

        function OnInit(s, e) {
            s.GetWindowElement(-1).className += " popupStyle";
        }
        function AdjustSize() {

            var height = document.getElementById('maindiv').clientHeight - 130;  // I have some buttons below the grid so needed -50
            var width = document.getElementById('maindiv').clientWidth;
            gvAVDetectadas.SetHeight(height);

        }


        function gridView_EndCallback(s, e) {

            if (s.cp_swMsg != null) {
                mostrarMensajeSweet(s.cp_swType, s.cp_swMsg);
                s.cp_swType = null;
                s.cp_swMsg = null;
            }

            //validar con un parametro si es necesario el refreco de los datos

            if (s.cp_Update != null) {

                gvAVDetectadas.UnselectRows();
                gvAVDetectadas.PerformCallback('CargarRegistros');
                s.cp_Update = null;
            }
        }

        function CerrarModalyVerAlertas(s, e) {

            // cbListaMatFleje.PerformCallback();

            if (s.cp_swType != null && s.cp_swAlert == null) {

                ppNuevaHojaDatos.Hide();


                mostrarMensajeSweet(s.cp_swType, s.cp_swMsg);
                gvAVDetectadas.PerformCallback('CargarRegistros');
                s.cp_swType = null;
                s.cp_swMsg = null;
                s.cp_swAlert = null;
            }
            else {

                mostrarMensajeSweet(s.cp_swType, s.cp_swMsg);
                s.cp_swType = null;
                s.cp_swMsg = null;
                s.cp_swAlert = null;
            }
        }
        function AdjustStylePopUp(s, e) {

            s.GetWindowElement(-1).className += " popupStyle";
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

            }
        }


    </script>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">
        <dx:ASPxPanel ID="TopPanel" runat="server" FixedPosition="WindowTop" FixedPositionOverlap="true" CssClass="topPanel">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowCollapseButton="true" Width="170px" HeaderText="Folio de Operacion/Expediente:" View="GroupBox">
                                    <PanelCollection>
                                        <dx:PanelContent>


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxDateEdit Caption="Inicio" runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" AutoPostBack="false">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxDateEdit Caption="Fin" runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaFin" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" AutoPostBack="false">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxCheckBox runat="server" ID="chkBusquedaCompleta" Width="150px" ClientInstanceName="chkBusquedaCompleta" Text="Todas las fechas" ToggleSwitchDisplayMode="Always">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {  
                                                   if (chkBusquedaCompleta.GetChecked()) 
                                                   {
                                                   dtFechaInicio.SetEnabled(false);
                                                   dtFechaFin.SetEnabled(false);
                                                   }
                                                   else
                                                   {
                                                   dtFechaInicio.SetEnabled(true);
                                                   dtFechaFin.SetEnabled(true);
                                                   }                }" />
                                                        </dx:ASPxCheckBox>
                                                    </td>


                                                    <td>&nbsp;</td>

                                                    <td>
                                                        <dx:ASPxCheckBox runat="server" ID="chkSoloAvActivas" Width="150px" ClientInstanceName="chkSoloAvActivas" Text="Solo Ac Activas" ToggleSwitchDisplayMode="Always">
                                                        </dx:ASPxCheckBox>
                                                    </td>

                                                    <td>&nbsp;</td>

                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar" AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) {  gvAVDetectadas.PerformCallback('CargarRegistros'); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>



        <section class="CLPageContent" id="maindiv">
            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvAVDetectadas"></dx:ASPxGridViewExporter>

            <dx:ASPxHiddenField runat="server" ID="HidDocumentoSelect" ClientInstanceName="HidDocumentoSelect"></dx:ASPxHiddenField>

            <dx:ASPxGridView runat="server" ID="gvAVDetectadas" ClientInstanceName="gvAVDetectadas" AutoGenerateColumns="False" Width="100%" KeyFieldName="IdAV"
                OnDataBinding="gvAVDetectadas_DataBinding"
                OnCustomCallback="gvAVDetectadas_CustomCallback"
                OnToolbarItemClick="gvAVDetectadas_ToolbarItemClick"
                OnRowUpdating="gvAVDetectadas_RowUpdating"
                OnRowValidating="gvAVDetectadas_RowValidating">

                <ClientSideEvents Init="AdjustSize" EndCallback="gridView_EndCallback" />

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />


                <SettingsPager Mode="ShowPager" PageSize="100" />

                <Settings ShowFooter="True" ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                <%--                <SettingsCookies Enabled="true" />--%>

                <SettingsResizing ColumnResizeMode="Control" />


                <SettingsDetail ExportMode="Expanded" ShowDetailRow="false" />

                <SettingsBehavior
                    AllowGroup="true"
                    AllowDragDrop="true"
                    AllowFixedGroups="false"
                    AllowSelectByRowClick="true"
                    AllowSelectSingleRowOnly="true"
                    AutoExpandAllGroups="true"
                    AllowFocusedRow="True"
                    ProcessFocusedRowChangedOnServer="False"
                    AllowSort="true"
                    ConfirmDelete="true"
                    EnableCustomizationWindow="true"></SettingsBehavior>

                <SettingsCommandButton>
                    <EditButton Text="" ButtonType="Image">
                        <Image ToolTip="Editar" IconID="edit_edit_16x16"></Image>
                    </EditButton>

                    <DeleteButton Text="" ButtonType="Image">
                        <Image ToolTip="Eliminar" IconID="edit_delete_16x16"></Image>
                    </DeleteButton>

                    <NewButton Text="" ButtonType="Image">
                        <Image ToolTip="Nuevo" IconID="actions_add_16x16"></Image>
                    </NewButton>

                    <UpdateButton Text="" ButtonType="Image">


                        <Image ToolTip="Aceptar" IconID="actions_apply_16x16"></Image>
                    </UpdateButton>

                    <CancelButton Text="" ButtonType="Image">
                        <Image ToolTip="Cancelar" IconID="actions_cancel_16x16"></Image>
                    </CancelButton>

                </SettingsCommandButton>


                <Styles>
                    <AlternatingRow Enabled="true" />
                    <SelectedRow BackColor="#0066ff"></SelectedRow>
                </Styles>

                <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="true"  />
                <SettingsSearchPanel Visible="true" />
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />

                <Columns>

                    <dx:GridViewCommandColumn Visible="true" VisibleIndex="1" ShowNewButton="false" ShowEditButton="true" ShowDeleteButton="false" ShowNewButtonInHeader="false" ButtonRenderMode="Button" Width="50px"></dx:GridViewCommandColumn>


                    <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Num" ToolTip="Numero de Registro" FieldName="IdAV" Width="40px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Tipo de AV" FieldName="TipoAVDetectada" Width="300px" Visible="true" GroupIndex="1">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Expediente" FieldName="IdExpediente" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataDateColumn VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm tt" Caption="Fecha Deteccion" FieldName="FechaIngreso" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Id Estatus" FieldName="IdEstatus" Width="350px" Visible="false">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Estatus" FieldName="TextoEstatus" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Acto" FieldName="TextoActo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Variante" FieldName="TextoVariante" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Escritura" FieldName="Escritura" Width="80px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Volumen" FieldName="Volumen" Width="80px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn Caption="Valor Operacion" FieldName="ValorOperacion" ReadOnly="True" Width="100px" VisibleIndex="11">
                        <PropertiesTextEdit DisplayFormatString="{0:C2}">

                            <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn Caption="Umbral Operacion" FieldName="UmbralAcVulnerable" ReadOnly="True" Width="110px" VisibleIndex="12">
                        <PropertiesTextEdit DisplayFormatString="{0:C2}">
                            <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataCheckColumn VisibleIndex="13" Caption="Av Activa" FieldName="AvActiva" Width="100px" Visible="true">
                        <EditFormSettings Visible="True"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="14" Caption="Usuario Gestiona Aviso" FieldName="UsuarioGestionaAviso" Width="200px" Visible="true">
                        <EditFormSettings Visible="True"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="15" Caption="Folio de Aviso" FieldName="FolioDeAviso" Width="200px" Visible="true">
                        <EditFormSettings Visible="True"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="16" Caption="Observaciones" FieldName="Observaciones" Width="200px" Visible="true">
                        <EditFormSettings Visible="True"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <%--  columnas datos generales de la hoja de datos--%>
                </Columns>


                <Toolbars>
                    <dx:GridViewToolbar>
                        <Items>


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
