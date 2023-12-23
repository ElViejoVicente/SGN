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



    <title>SGN </title>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">




        <dx:ASPxPanel ID="TopPanel" runat="server" FixedPosition="WindowTop" FixedPositionOverlap="true" CssClass="topPanel">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowCollapseButton="true" Width="170px" HeaderText="Opciones de consulta:" View="GroupBox">
                                    <PanelCollection>
                                        <dx:PanelContent>


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCalendar runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" AutoPostBack="false">
                                                        </dx:ASPxCalendar>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxCalendar runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaInicio" AutoPostBack="false">
                                                        </dx:ASPxCalendar>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar" AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) {   gridView.PerformCallback('CargarFabricaciones'); }" />
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

                    <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Estatus" FieldName="TextoEstatus" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Acto" FieldName="TextoActo" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="4" Caption="Fecha ingreso" FieldName="FechaIngreso" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Otorga" FieldName="Otorga" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="6" Caption="A favor De" FieldName="AfavorDe" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Operacion Proyectada" FieldName="OperacionProyectada" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Ubicacion del Predio" FieldName="UbicacionPredio" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Faltantes" FieldName="Faltantes" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="10" Caption="Fecha de elaboracion" FieldName="FechaElaboracion" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="11" Caption="Fecha de Envio al RPP" FieldName="FechaEnvioRPP" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataCheckColumn VisibleIndex="12" Caption="Es tramite por sistema" FieldName="EsTramitePorSistema" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="13" Caption="Fecha pago boleta" FieldName="FechaPagoBoleta" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="14" Caption="Fecha recibo RPP" FieldName="FechaRecibidoRPP" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="15" Caption="Proyectista" FieldName="NombreProyectista" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="16" Caption="Fecha asignacion" FieldName="FechaAsignacionProyectista" Width="100px" Visible="true" ToolTip="Fecha de Asignacion al Proyectista">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="17" Caption="Fecha prevista Termino" FieldName="FechaPrevistaTerminoProyectista" Width="100px" Visible="true" ToolTip=" Fecha prevista de Termino  por parte del proyectista">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="18" Caption="Aviso preventivo" FieldName="AvisoPreventivo" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="19" Caption="I.S.R." FieldName="ISR" ReadOnly="true" Visible="true">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="20" Caption="Anotaciones firma" FieldName="NotasFirma" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="21" Caption="Escritura" FieldName="Escritura" ReadOnly="true" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="22" Caption="Volumen" FieldName="Volumen" ReadOnly="true" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="23" Caption="Fecha traslado entregado" FieldName="FechaTrasladoEntregado" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="24" Caption="Fecha elaboracion definitivo" FieldName="FechaElaboracionDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="25" Caption="Fecha Envio RPP Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataCheckColumn VisibleIndex="26" Caption="Es tramite por sistema Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="27" Caption="Fecha de traslado entregado" FieldName="FechaPagoBoletaDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="28" Caption="Fecha recibido RPP definitivo" FieldName="FechaRecibidoRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="29" Caption="Fecha recepcion termino escrituta" FieldName="FechaRecepcionTerminoEscrituta" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="30" Caption="Fecha asignacion mesa" FieldName="FechaAsignacionMesa" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="31" Caption="Fecha termino mesa" FieldName="FechaTerminoMesa" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="32" Caption="Fecha registro entrega" FieldName="FechaRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="33" Caption="Fecha boleta pago registro entrega" FieldName="FechaBoletaPagoRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="34" Caption="Fecha salida" FieldName="FechaSalida" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="35" Caption="Observaciones de tramite terminado" FieldName="ObservacionesTramiteTerminado" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


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
