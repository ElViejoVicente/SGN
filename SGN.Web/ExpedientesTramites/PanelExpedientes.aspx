<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedientes.aspx.cs" Inherits="SGN.Web.ExpedientesTramites.PanelExpedientes" %>

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

            var height = document.getElementById('maindiv').clientHeight - 60;  // I have some buttons below the grid so needed -50
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

        function CerrarModalyVerAlertas(s, e) {

            // cbListaMatFleje.PerformCallback();

            if (s.cp_swType != null && s.cp_swAlert == null) {

                ppOrdenNuevoExpediente.Hide();
                ppEditarExpediente.Hide();
                ppCambiarEstatus.Hide();

                mostrarMensajeSweet(s.cp_swType, s.cp_swMsg);
                gvExpedientes.PerformCallback('CargarRegistros');
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


                case "cmdNuevoExpediente":

                    ppOrdenNuevoExpediente.Show();
                    ppOrdenNuevoExpediente.PerformCallback("NuevoExpediente");

                    break;


                case "cmdEditarExpediente":

                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {
                        ppEditarExpediente.Show();
                        ppEditarExpediente.PerformCallback("CargarRegistros");
                    }


                    break;


                case "cmdEstatusExpediente":
                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {
                        ppCambiarEstatus.Show();
                        ppCambiarEstatus.PerformCallback("CargarEstados");
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
                                                        <dx:ASPxDateEdit Caption="Inicio: " runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" AutoPostBack="false">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxDateEdit Caption="Fin: " runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaInicio" AutoPostBack="false">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar" AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) {  gvExpedientes.PerformCallback('CargarRegistros'); }" />
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


            <dx:ASPxGridView runat="server" ID="gvExpedientes" ClientInstanceName="gvExpedientes" AutoGenerateColumns="False" Width="100%" KeyFieldName="IdExpediente"
                OnDataBinding="gvExpedientes_DataBinding"
                OnCustomCallback="gvExpedientes_CustomCallback"
                OnToolbarItemClick="gvExpedientes_ToolbarItemClick"
                OnHtmlDataCellPrepared="gvExpedientes_HtmlDataCellPrepared">

                <ClientSideEvents Init="AdjustSize" EndCallback="gridView_EndCallback" />

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                <SettingsPager Mode="ShowAllRecords" />

                <Settings ShowFooter="True" ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                <%--                <SettingsCookies Enabled="true" />--%>

                <SettingsResizing ColumnResizeMode="Control" />


                <SettingsDetail ExportMode="All" ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />

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

                <SettingsCommandButton>
                    <EditButton Text="" ButtonType="Image">
                        <Image ToolTip="Editar" IconID="edit_edit_16x16"></Image>
                    </EditButton>

                    <DeleteButton Text="" ButtonType="Image">
                        <Image ToolTip="Eliminar Fabricacion" IconID="edit_delete_16x16"></Image>
                    </DeleteButton>
                </SettingsCommandButton>



                <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="false" />
                <SettingsSearchPanel Visible="true" />
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />

                <Columns>


                    <%--  columnas expedientes--%>

                    <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Num Expediente" FieldName="IdExpediente" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Num recibo pago" FieldName="numReciboPago" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Estatus" FieldName="TextoEstatus" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Acto" FieldName="TextoActo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="5" Caption="Fecha ingreso" FieldName="FechaIngreso" Width="120px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Otorga" FieldName="Otorga" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="7" Caption="A favor De" FieldName="AfavorDe" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Operacion Proyectada" FieldName="OperacionProyectada" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Ubicacion del Predio" FieldName="UbicacionPredio" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Faltantes" FieldName="Faltantes" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


















                </Columns>


                <Toolbars>
                    <dx:GridViewToolbar>
                        <Items>





                            <dx:GridViewToolbarItem Text="Nuevo" Image-IconID="dashboards_new_svg_16x16" Name="cmdNuevoExpediente" />

                            <dx:GridViewToolbarItem Text="Editar" Image-IconID="dashboards_edit_svg_16x16" Name="cmdEditarExpediente" />

                            <dx:GridViewToolbarItem Text="Cambiar Estatus" Image-IconID="dashboards_scatterchartlabeloptions_svg_16x16" Name="cmdEstatusExpediente" />



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


                <Templates>
                    <DetailRow>
                        <div style="padding: 3px 3px 2px 3px">
                            <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                                <TabPages>
                                    <dx:TabPage Text="Aviso preventivo" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>

                                                <dx:ASPxGridView runat="server" ID="gvAvisoPreventivo" ClientInstanceName="gvAvisoPreventivo" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvAvisoPreventivo_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>

                                                        <%--  columnas aviso preventivo --%>

                                                        <dx:GridViewDataDateColumn VisibleIndex="1" Caption="Elaboracion" FieldName="FechaElaboracion" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="2" Caption="Envio al RPP" FieldName="FechaEnvioRPP" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataCheckColumn VisibleIndex="3" Caption="Es tramite por sistema" FieldName="EsTramitePorSistema" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="4" Caption="Pago boleta" FieldName="FechaPagoBoleta" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="5" Caption="Recibo RPP" FieldName="FechaRecibidoRPP" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>


                                                    </Columns>
                                                </dx:ASPxGridView>

                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Proyecto" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvProyecto" ClientInstanceName="gvProyecto" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvProyecto_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  columnas  Proyecto --%>

                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Proyectista" FieldName="NombreProyectista" Width="50px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="2" Caption="Fecha asignacion" FieldName="FechaAsignacionProyectista" Width="100px" Visible="true" ToolTip="Fecha de Asignacion al Proyectista">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="3" Caption="Fecha prevista Termino" FieldName="FechaPrevistaTerminoProyectista" Width="100px" Visible="true" ToolTip=" Fecha prevista de Termino  por parte del proyectista">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="4" Caption="Fecha aviso preventivo" FieldName="FechaAvisoPreventivo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataSpinEditColumn VisibleIndex="5" Caption="I.S.R." FieldName="ISR" ReadOnly="true" Visible="true" Width="100px" PropertiesSpinEdit-DisplayFormatString="C">
                                                            <PropertiesSpinEdit>
                                                                <SpinButtons ClientVisible="false" />
                                                            </PropertiesSpinEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Firmas" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvFirmas" ClientInstanceName="gvFirmas" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvFirmas_BeforePerformDataSelect">

                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  columnas firmas --%>

                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Anotaciones firma" FieldName="NotasFirma" Width="150px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataSpinEditColumn VisibleIndex="2" Caption="Escritura" FieldName="Escritura" ReadOnly="true" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataSpinEditColumn>

                                                        <dx:GridViewDataSpinEditColumn VisibleIndex="3" Caption="Volumen" FieldName="Volumen" ReadOnly="true" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataSpinEditColumn>

                                                        <dx:GridViewDataCheckColumn VisibleIndex="4" Caption="Aplica Traslado" FieldName="AplicaTraslado" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="5" Caption="Fecha recepcion termino escrituta" FieldName="FechaRecepcionTerminoEscritura" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Aviso Definitivo" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvAvisoDefinitivo" ClientInstanceName="gvAvisoDefinitivo" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvAvisoDefinitivo_BeforePerformDataSelect">

                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>

                                                        <%--  Aviso definitivo --%>

                                                        <dx:GridViewDataDateColumn VisibleIndex="1" Caption="Fecha elaboracion definitivo" FieldName="FechaElaboracionDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="2" Caption="Fecha Envio RPP Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataCheckColumn VisibleIndex="3" Caption="Es tramite por sistema Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="4" Caption="Fecha de traslado entregado" FieldName="FechaPagoBoletaDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="5" Caption="Fecha recibido RPP definitivo" FieldName="FechaRecibidoRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Escrituracion" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvEscrituracion" ClientInstanceName="gvEscrituracion" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvEscrituracion_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  Escrituracion --%>
                                                        <dx:GridViewDataDateColumn VisibleIndex="1" Caption="Fecha recibo traslado" FieldName="FechaRecibioTraslado" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn VisibleIndex="2" Caption="Fecha asignacion mesa" FieldName="FechaAsignacionMesa" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="3" Caption="Fecha termino mesa" FieldName="FechaTerminoMesa" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Entregas" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvEntregas" ClientInstanceName="gvEntregas" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvEntregas_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  Entregas --%>

                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Observaciones Entrega" FieldName="ObservacionesEngrega" Width="150px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataCheckColumn VisibleIndex="2" Caption="Registro Entrega" FieldName="RegistroEntrega" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="3" Caption="Fecha registro entrega" FieldName="FechaRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="4" Caption="Fecha boleta pago registro entrega" FieldName="FechaBoletaPagoRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn VisibleIndex="5" Caption="Fecha regreso registro" FieldName="FechaRegresoRegistro" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn VisibleIndex="6" Caption="Fecha salida" FieldName="FechaSalida" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Observaciones de tramite terminado" FieldName="ObservacionesTramiteTerminado" Width="200px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </div>
                    </DetailRow>
                </Templates>


            </dx:ASPxGridView>

            <dx:ASPxPopupControl runat="server" ID="ppCambiarEstatus" ClientInstanceName="ppCambiarEstatus" Height="300px" Width="700px" EnableClientSideAPI="true" ShowFooter="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="false" AllowDragging="true" CloseAction="CloseButton" HeaderText="Cambiar estatus del Expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppCambiarEstatus_WindowCallback">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">

                        <dx:ASPxFormLayout runat="server" ID="frmEstatus" ClientInstanceName="frmEstatus" ColCount="3" ColumnCount="3" Width="100%">

                            <Items>
                                <dx:LayoutItem Caption="Expediente seleccionado" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxTextBox runat="server" ID="txtProyecSelecEstatus" ReadOnly="true"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutGroup Caption="Catalogo de estatus" ColSpan="3" ColumnSpan="3">
                                    <Items>
                                        <dx:LayoutItem ColSpan="1" Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbEstados" AutoPostBack="false" OnDataBinding="rbEstados_DataBinding" RepeatColumns="2" RepeatLayout="Table">
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>




                    </dx:PopupControlContentControl>
                </ContentCollection>

                <FooterContentTemplate>
                    <div>
                        <dx:ASPxButton Style="float: right" Image-IconID="richedit_trackingchanges_accept_svg_16x16" HorizontalAlign="Right" runat="server" ID="btnAceptarstatus" Text="Aceptar" AutoPostBack="false" ClientInstanceName="btnAceptar">
                            <ClientSideEvents Click="function(s, e) 
                                                        {                                             
                                                        ppCambiarEstatus.PerformCallback('guardar');                                                      
                                                        }" />

                        </dx:ASPxButton>
                    </div>
                </FooterContentTemplate>

            </dx:ASPxPopupControl>


            <dx:ASPxPopupControl runat="server" ID="ppEditarExpediente" ClientInstanceName="ppEditarExpediente" Height="600px" Width="950px" EnableClientSideAPI="true" ShowFooter="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="true" AllowDragging="true" CloseAction="CloseButton" HeaderText="Editar Expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppEditarExpediente_WindowCallback" ScrollBars="Auto">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxFormLayout runat="server" ID="frmExpedienteExistente" ClientInstanceName="frmExpedienteExistente" ColCount="3" ColumnCount="3" Width="100%">

                            <Items>
                                <dx:LayoutGroup Caption="Expediente" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" Caption="Numero" ColumnSpan="2" FieldName="ExfnNumeroExpediente">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxLabel runat="server" ID="txtNumExpediente" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Numero de recibo" FieldName="ExfnNumeroRecibo" ColSpan="2" ColumnSpan="2" Width="100%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtExfnNumeroRecibo"></dx:ASPxTextBox>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha de ingreso" FieldName="ExfnFechaIngreso" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" Width="100%" ID="dtExfnFechaIngreso"></dx:ASPxDateEdit>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Acto" FieldName="ExfnActo" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxComboBox runat="server" Width="100%" ID="cbExfnActo" OnDataBinding="cbExfnActo_DataBinding"></dx:ASPxComboBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Otorga" FieldName="ExfnOtorga" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtExfnOtorga" AutoPostBack="false" Width="100%">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="A favor de" FieldName="EXfnAfavorde" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtEXfnAfavorde" AutoPostBack="false" Width="100%">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Operacion proyectada" FieldName="ExfnOperacionProyectada" ColSpan="2" ColumnSpan="2" RowSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" Width="100%" ID="txtExfnOperacionProyectada"></dx:ASPxMemo>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ubicacion de predio" FieldName="ExfnUbicacionPredio" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtExfnUbicacionPredio"></dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Documentos faltantes" FieldName="ExfnDocumentosFaltantes" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" Width="100%" ID="txtExfnDocumentosFaltantes"></dx:ASPxMemo>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Aviso preventivo" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Elaboracion" FieldName="APfnFechaElaboracion" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaElaboracion" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Envio al R.P.P." FieldName="APfnFechaEnvioAlRPP" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaEnvioAlRPP" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Es tramite por sistema" FieldName="APfnEsTramitePorSistema" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxCheckBox runat="server" CheckState="Unchecked" ID="chkAPfnEsTramitePorSistema" AutoPostBack="false"></dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Pago de la boleta" FieldName="APfnFechaPagoBoleta" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaPagoBoleta" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Recibido" FieldName="APfnFechaRecibido" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaRecibido" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Proyecto" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Proyectista" FieldName="PRfnProyectista" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxComboBox runat="server" ID="cbPRfnProyectista" OnDataBinding="cbPRfnProyectista_DataBinding" AutoPostBack="false" Width="100%"></dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Asignacion" FieldName="PRfnFechaAsignacionProyectista" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaAsignacionProyectista" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Prevision de termino" FieldName="PRfnFechaPrevistaTermino" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaPrevistaTermino" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Aviso Preventivo" FieldName="PRfnFechaAvisoPreventivo" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaAvisoPreventivo" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="I.S.R." FieldName="PRfnISR" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxSpinEdit runat="server" ID="txtPRfnISR" AutoPostBack="false" Width="100%" SpinButtons-ClientVisible="false" MinValue="0" NumberType="Float" DisplayFormatString="C">
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Firmas" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Notas" FieldName="FIfnNotasFirmas" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtFIfnNotasFirmas" AutoPostBack="false" Width="100%"></dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Num Escritura" FieldName="FIfnNumEscritura" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxSpinEdit runat="server" ID="txtFIfnNumEscritura" AutoPostBack="false" Width="100%" SpinButtons-ClientVisible="false" MinValue="0"></dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Num Volumen" FieldName="FIfnNumVolumen" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxSpinEdit runat="server" ID="txtFIfnNumVolumen" AutoPostBack="false" Width="100%" SpinButtons-ClientVisible="false" MinValue="0"></dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Aplica traslado" FieldName="FIfnAplicaTraslado" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxCheckBox runat="server" CheckState="Unchecked" ID="chkFIfnAplicaTraslado" AutoPostBack="false"></dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Recepcion para termino escritura" FieldName="FIfnFechaRecepcionTerminoEscritura" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtFIfnFechaRecepcionTerminoEscritura" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Aviso definitivo" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="1" Caption="Elaboracion" FieldName="AdfnFechaElaboracion">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaElaboracion" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Envio R.P.P." FieldName="AdfnFechaEnvioRPP">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaEnvioRPP" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" ColumnSpan="2" Caption="Tramite por sistema" FieldName="AdfnEsTramitePorSistema">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxCheckBox runat="server" CheckState="Unchecked" ID="chkAdfnEsTramitePorSistema" AutoPostBack="false"></dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Pago boleta" FieldName="AdfnFechaPagoBoleta">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaPagoBoleta" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Recibido" FieldName="AdfnFechaRecibido">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaRecibido" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Escrituracion" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="1" Caption="Recibio traslado" FieldName="EsfnRecibioTraslado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEsfnRecibioTraslado" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Asignacion a mesa (sube)" FieldName="AdfnFechaAsignacionMesa">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaAsignacionMesa" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Termino del tramite" FieldName="AdfnFechaTerminoTramite">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaTerminoTramite" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Entregas" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" ColumnSpan="2" Caption="Observaciones" FieldName="EnfnObservacionesEntrega ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtEnfnObservacionesEntrega" AutoPostBack="false" Width="100%"></dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" ColumnSpan="2" Caption="¿Requiere Registro?" FieldName="EnfnRegistroSolicitado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxCheckBox runat="server" CheckState="Unchecked" ID="chkEnfnRegistroSolicitado" AutoPostBack="false"></dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Registro" FieldName="EnfnFechaRegistro">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaRegistro" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Boleta Pago" FieldName="EnfnFechaBoletaPago">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaBoletaPago" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Regreso registro" FieldName="EnfnFechaRegresoRegistro">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaRegresoRegistro" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Salida" FieldName="EnfnFechaSalida">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaSalida" AutoPostBack="false" Width="100%"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" ColumnSpan="2" Caption="Observaciones del tramite terminado" FieldName="EnfnObservacionesSobreTramiteTerminado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtEnfnObservacionesSobreTramiteTerminado" AutoPostBack="false" Width="100%"></dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>

                <FooterContentTemplate>
                    <div>
                        <dx:ASPxButton Style="float: right" Image-IconID="richedit_trackingchanges_accept_svg_16x16" HorizontalAlign="Right" runat="server" ID="btnAceptar" Text="AceptarCambios" AutoPostBack="false" ClientInstanceName="btnAceptar">
                            <ClientSideEvents Click="function(s, e) 
                                                        {                                             
                                                        ppEditarExpediente.PerformCallback('guardarCambios');                                                      
                                                        }" />

                        </dx:ASPxButton>
                    </div>
                </FooterContentTemplate>

            </dx:ASPxPopupControl>


            <dx:ASPxPopupControl runat="server" ID="ppOrdenNuevoExpediente" ClientInstanceName="ppOrdenNuevoExpediente" Height="300px" Width="700px" EnableClientSideAPI="true" ShowFooter="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="false" AllowDragging="true" CloseAction="CloseButton" HeaderText="Nuevo Expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppOrdenNuevoExpediente_WindowCallback">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">

                        <dx:ASPxFormLayout runat="server" ID="frmNuevoExpediente" ClientInstanceName="frmNuevoExpediente" ColCount="3" ColumnCount="3" Width="100%">

                            <Items>
                                <dx:LayoutItem ColSpan="3" Caption="Numero de recibo" ColumnSpan="3" FieldName="fnNumeroRecibo" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxTextBox runat="server" ID="txtNumReciboNuevo" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Fecha de ingreso" FieldName="fnFechaIngreso" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxDateEdit runat="server" ID="dtFechaIngresoNuevo" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Acto" FieldName="fnActo" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxComboBox runat="server" ID="cbActosNuevo" OnDataBinding="cbActosNuevo_DataBinding" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Otorga" FieldName="fnOtorga" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxMemo runat="server" ID="txtOtorgaNuevo" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="A favor de" FieldName="fnAfavorde" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxMemo runat="server" ID="txtAfavorDeNuevo" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Operacion proyectada" FieldName="fnOperacionProyectada" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxMemo runat="server" ID="txtOperacionProyectadaNuevo" AutoPostBack="false" Width="100%">
                                                <ValidationSettings ValidationGroup="requerido">
                                                    <RequiredField IsRequired="true" ErrorText="Campo obligatorio" />
                                                </ValidationSettings>
                                            </dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ubicacion de predio" FieldName="fnUbicacionPredio" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxTextBox runat="server" ID="txtUbicacionPredioNuevo" AutoPostBack="false" Width="100%"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Documentos faltantes" FieldName="fnDocumentosFaltantes" ColSpan="3" ColumnSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxMemo runat="server" ID="txtDocumentoFaltantesNuevo" AutoPostBack="false" Width="100%"></dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>

                    </dx:PopupControlContentControl>
                </ContentCollection>
                <FooterContentTemplate>
                    <div>
                        <dx:ASPxButton Style="float: right" Image-IconID="richedit_trackingchanges_accept_svg_16x16" HorizontalAlign="Right" runat="server" ID="btnAceptar" Text="Aceptar" AutoPostBack="false" ClientInstanceName="btnAceptar">
                            <ClientSideEvents Click="function(s, e) {
                                                      if (ASPxClientEdit.ValidateGroup('requerido'))
                                                          {
                                                          ppOrdenNuevoExpediente.PerformCallback('guardar');   
                                                          }
                                                      }" />

                        </dx:ASPxButton>

                        <%-- <dx:ASPxButton Style="float: right" Image-IconID="actions_cancel_16x16office2013" HorizontalAlign="Right" runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" ClientInstanceName="btnCancelar">
                            <ClientSideEvents Click="function(s, e) 
                                                    {                                     
                                                        ppOrdenNuevoExpediente.Hide() 
                                                    }" />
                        </dx:ASPxButton>--%>
                    </div>
                </FooterContentTemplate>
            </dx:ASPxPopupControl>

        </section>
    </form>
</body>
</html>
