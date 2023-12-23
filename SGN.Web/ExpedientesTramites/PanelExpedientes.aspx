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


                case "cmdNuevoAxpediente":

                    ppOrdenNuevoExpediente.Show();
                    ppOrdenNuevoExpediente.PerformCallback("NuevoExpedeinte");

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

                    <dx:GridViewDataDateColumn VisibleIndex="11" Caption="Elaboracion" FieldName="FechaElaboracion" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="12" Caption="Envio al RPP" FieldName="FechaEnvioRPP" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataCheckColumn VisibleIndex="13" Caption="Es tramite por sistema" FieldName="EsTramitePorSistema" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="14" Caption="Pago boleta" FieldName="FechaPagoBoleta" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="15" Caption="Recibo RPP" FieldName="FechaRecibidoRPP" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="16" Caption="Proyectista" FieldName="NombreProyectista" Width="50px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="17" Caption="Fecha asignacion" FieldName="FechaAsignacionProyectista" Width="100px" Visible="true" ToolTip="Fecha de Asignacion al Proyectista">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="18" Caption="Fecha prevista Termino" FieldName="FechaPrevistaTerminoProyectista" Width="100px" Visible="true" ToolTip=" Fecha prevista de Termino  por parte del proyectista">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="19" Caption="Aviso preventivo" FieldName="AvisoPreventivo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="20" Caption="I.S.R." FieldName="ISR" ReadOnly="true" Visible="true" Width="100px">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="21" Caption="Anotaciones firma" FieldName="NotasFirma" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="22" Caption="Escritura" FieldName="Escritura" ReadOnly="true"  Width="100px" Visible="true" >
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="23" Caption="Volumen" FieldName="Volumen" ReadOnly="true"   Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="24" Caption="Fecha traslado entregado" FieldName="FechaTrasladoEntregado" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="25" Caption="Fecha elaboracion definitivo" FieldName="FechaElaboracionDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="26" Caption="Fecha Envio RPP Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataCheckColumn VisibleIndex="27" Caption="Es tramite por sistema Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="28" Caption="Fecha de traslado entregado" FieldName="FechaPagoBoletaDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="29" Caption="Fecha recibido RPP definitivo" FieldName="FechaRecibidoRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="30" Caption="Fecha recepcion termino escrituta" FieldName="FechaRecepcionTerminoEscrituta" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="31" Caption="Fecha asignacion mesa" FieldName="FechaAsignacionMesa" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="32" Caption="Fecha termino mesa" FieldName="FechaTerminoMesa" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="33" Caption="Fecha registro entrega" FieldName="FechaRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="34" Caption="Fecha boleta pago registro entrega" FieldName="FechaBoletaPagoRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn VisibleIndex="35" Caption="Fecha salida" FieldName="FechaSalida" Width="100px" Visible="true" ToolTip="">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="36" Caption="Observaciones de tramite terminado" FieldName="ObservacionesTramiteTerminado" Width="200px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                </Columns>


                <Toolbars>
                    <dx:GridViewToolbar>
                        <Items>





                            <dx:GridViewToolbarItem Text="Nuevo Expediente" Image-IconID="tasks_newtask_16x16" Name="cmdNuevoAxpediente" />

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
                        <dx:ASPxButton Style="float: right" Image-IconID="richedit_trackingchanges_accept_svg_16x16" HorizontalAlign="Right" runat="server" ID="btnOrdenar" Text="Aceptar" AutoPostBack="false" ClientInstanceName="btnOrdenar">
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
