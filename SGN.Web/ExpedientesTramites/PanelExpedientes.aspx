﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedientes.aspx.cs" Inherits="SGN.Web.ExpedientesTramites.PanelExpedientes" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v24.2, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v24.2, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../../SwitcherResources/Content/Cosmo/bootstrap.min.css" crossorigin="anonymous" />
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

            var height = document.getElementById('maindiv').clientHeight - 9;   // I have some buttons below the grid so needed -50
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


                gvExpedientes.PerformCallback('CargarRegistros');

                s.cp_Update = null;
            }
        }

        function CerrarModalyVerAlertas(s, e) {

            // cbListaMatFleje.PerformCallback();

            if (s.cp_swType != null && s.cp_swAlert == null) {


                ppEditarExpediente.Hide();
                ppCambiarEstatus.Hide();
                ppArchivos.Hide();
                ppAlertasExpediente.Hide();
                ppEditarAvisoNotarial.Hide();

                //2024-06-26 borramos seleccion

                //gvExpedientes.UnselectRows();
                //gvExpedientes.ClearFilter();
                //gvExpedientes.ApplySearchPanelFilter('');





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


                //case "cmdNuevoExpediente": // Esta opracion ya noes valida en este modulo ya que la informacion inicial (alta) partira de la hoja de datos

                //    ppOrdenNuevoExpediente.Show();
                //    ppOrdenNuevoExpediente.PerformCallback("NuevoExpediente");

                //    break;


                case "cmdEditarExpediente":

                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {

                        gvExpedientes.GetRowValues(gvExpedientes.GetFocusedRowIndex(), 'IdExpediente', onCallbackEditarRegistroExp);

                    }


                    break;


                case "cmdEstatusExpediente":
                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {

                        gvExpedientes.GetRowValues(gvExpedientes.GetFocusedRowIndex(), 'IdExpediente', onCallbackEditarEstatusExp);
                    }


                    break;

                case "cmdAlertasExpediente":
                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {

                        gvExpedientes.GetRowValues(gvExpedientes.GetFocusedRowIndex(), 'IdExpediente', onCallbackAlertaPorExp);
                    }

                    break;

                case "cmdArchivos":
                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {


                        ppArchivos.Show();
                        ppArchivos.PerformCallback("CargarArchivos");

                        // gvExpedientes.GetRowValues(gvExpedientes.GetFocusedRowIndex(), 'IdExpediente', onCallbackOneValue);


                    }

                case "cmdAvisoNotarial":
                    if (gvExpedientes.GetFocusedRowIndex() >= 0) {

                        gvExpedientes.GetRowValues(gvExpedientes.GetFocusedRowIndex(), 'IdExpediente', onCallbackEditarAvisoNotarial);

                    }



            }
        }


        function onCallbackEditarAvisoNotarial(value) {
            console.log(value);
            ppEditarAvisoNotarial.Show();
            ppEditarAvisoNotarial.PerformCallback("CargarRegistros~" + value);

        }


        function onCallbackEditarRegistroExp(value) {
            console.log(value);
            ppEditarExpediente.Show();
            ppEditarExpediente.PerformCallback("CargarRegistros~" + value);

        }

        function onCallbackAlertaPorExp(value) {
            console.log(value);
            ppAlertasExpediente.Show();
            ppAlertasExpediente.PerformCallback("CargarRegistros~" + value);

        }


        function onCallbackEditarEstatusExp(value) {
            console.log(value);
            ppCambiarEstatus.Show();
            ppCambiarEstatus.PerformCallback("CargarEstados~" + value);

        }

    </script>



    <title>SGN</title>
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
                                                        <dx:ASPxDateEdit Caption="Inicio" runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxDateEdit Caption="Fin" runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaFin" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
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
                                                                }                                            }" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>&nbsp;</td>

                                                    <td>

                                                        <dx:ASPxCheckBox runat="server" ID="chkVerExpAlertaActiva" Width="150px" ClientInstanceName="chkVerExpAlertaActiva" Text="Ver expedientes con alertas" ToggleSwitchDisplayMode="Always">
                                                        </dx:ASPxCheckBox>

                                                    </td>
                                                    <td>&nbsp;</td>

                                                    <td>

                                                        <dx:ASPxCheckBox runat="server" ID="chkVerExpAlertaNoActiva" Width="150px" ClientInstanceName="chkVerExpAlertaNoActiva" Text="Ver expedientes que tuvieron alertas" ToggleSwitchDisplayMode="Always">
                                                        </dx:ASPxCheckBox>

                                                    </td>
                                                    <td>&nbsp;</td>


                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar" AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) {  gvExpedientes.PerformCallback('CargarRegistros'); }" />

                                                            <Image IconID="xaf_action_reload_svg_16x16"></Image>
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

                <%--    <ClientSideEvents Init="AdjustSize" EndCallback="gridView_EndCallback"
                    SelectionChanged="function(s, e) 
                                                        {                                             
                                                        gvExpedientes.PerformCallback('AsignarRutaExpediente');                                                     
                                                        }" />--%>


                <ClientSideEvents Init="AdjustSize" EndCallback="gridView_EndCallback" />

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                <SettingsPager Mode="ShowPager" PageSize="100" />

                <Settings ShowFooter="True" ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                <%--                <SettingsCookies Enabled="true" />--%>

                <SettingsResizing ColumnResizeMode="Control" />


                <SettingsDetail ExportMode="Expanded" ShowDetailRow="true" />

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
                        <Image ToolTip="Eliminar Fabricacion" IconID="edit_delete_16x16"></Image>
                    </DeleteButton>
                </SettingsCommandButton>



                <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="false" />
                <SettingsSearchPanel Visible="true" ShowApplyButton="true" ShowClearButton="true" />
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />

                <Columns>


                    <%--  columnas expedientes--%>

                    <%--
                    <dx:GridViewDataColumn Caption="" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <dx:ASPxButton ID="btnEditar" runat="server" AutoPostBack="false" Height="20px" ImagePosition="Top">
                                <ClientSideEvents />
                                <Image IconID="cmdEditarExpediente" ></Image>
                              
                            </dx:ASPxButton>
                    
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>--%>


                    <dx:GridViewDataTextColumn VisibleIndex="0" Caption="Num Expediente" FieldName="IdExpediente" Width="110px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>

                        <DataItemTemplate>

                            <dx:ASPxImage ID="imgExpedienteAlerta" runat="server" ImageAlign="Right" ClientInstanceName="imgExpedienteAlerta">
                                <CaptionSettings ShowColon="false" Position="Left" />

                            </dx:ASPxImage>

                        </DataItemTemplate>

                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataImageColumn VisibleIndex="1" Caption="Semaforo" FieldName="Semaforo" Width="80px">
                        <PropertiesImage ImageUrlFormatString="~/imagenes/Produccion/{0}"></PropertiesImage>
                        <EditFormSettings Visible="False"></EditFormSettings>

                    </dx:GridViewDataImageColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Estatus" FieldName="TextoEstatus" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Acto" FieldName="TextoActo" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Variante" FieldName="TextoVariante" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6" Caption="Fecha ingreso" FieldName="FechaIngreso" Width="120px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Otorga" FieldName="Otorga" Width="250px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="7" Caption="A favor De" FieldName="AfavorDe" Width="250px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Ubicacion del Predio" FieldName="UbicacionPredio" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataSpinEditColumn VisibleIndex="9" Caption="Escritura" FieldName="Escritura" Width="100px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Proyectista" FieldName="NombreProyectista" Width="150px" Visible="true">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="11" Caption="Fecha prevista Termino" FieldName="FechaPrevistaTerminoProyectista" Width="100px" Visible="true" ToolTip=" Fecha prevista de Termino  por parte del proyectista">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>



                    <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="12" Caption="Fecha termino mesa" FieldName="FechaTerminoMesa" Width="100px" Visible="true" ToolTip="Fecha Termino por parte de la mesa">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataDateColumn>




                </Columns>


                <Toolbars>
                    <dx:GridViewToolbar>
                        <Items>





                            <%--                   <dx:GridViewToolbarItem Text="Nuevo" Image-IconID="dashboards_new_svg_16x16" Name="cmdNuevoExpediente" />--%>

                            <dx:GridViewToolbarItem Text="Editar" Image-IconID="dashboards_edit_svg_16x16" Name="cmdEditarExpediente" />

                            <dx:GridViewToolbarItem Text="Cambiar Estatus" Image-IconID="dashboards_scatterchartlabeloptions_svg_16x16" Name="cmdEstatusExpediente" />

                            <dx:GridViewToolbarItem Text="Alertas" Image-IconID="status_warning_16x16" Name="cmdAlertasExpediente" />

                            <dx:GridViewToolbarItem Text="Aviso Notarial" Image-IconID="functionlibrary_information_16x16" Name="cmdAvisoNotarial" />

                            <dx:GridViewToolbarItem Text="Impresion Aviso Notarial" Image-IconID="actions_print_16x16devav" Name="cmdImpresionAvisoNotarial" />


                            <%--        <dx:GridViewToolbarItem Text="Archivos" Image-IconID="businessobjects_bofolder_16x16" Name="cmdArchivos" />--%>


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
                            <dx:ASPxPageControl runat="server" ID="pageControl" ClientInstanceName="pageControl" Width="100%" EnableCallBacks="true">
                                <TabPages>
                                    <dx:TabPage Text="Aviso preventivo" Name="TapAP" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>

                                                <dx:ASPxGridView runat="server" ID="gvAvisoPreventivo" ClientInstanceName="gvAvisoPreventivo" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvAvisoPreventivo_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>

                                                        <%--  columnas aviso preventivo --%>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="1" Caption="Elaboracion" FieldName="FechaElaboracion" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="2" Caption="Envio al RPP" FieldName="FechaEnvioRPP" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataCheckColumn VisibleIndex="3" Caption="Es tramite por sistema" FieldName="EsTramitePorSistema" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="4" Caption="Pago boleta" FieldName="FechaPagoBoleta" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="5" Caption="Recibo RPP" FieldName="FechaRecibidoRPP" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>


                                                    </Columns>
                                                </dx:ASPxGridView>

                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Proyecto" Name="TapProyecto" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvProyecto" ClientInstanceName="gvProyecto" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvProyecto_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  columnas  Proyecto --%>

                                                        <dx:GridViewDataTextColumn VisibleIndex="0" Caption="Proyectista" FieldName="NombreProyectista" Width="50px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="1" Caption="Fecha asignacion" FieldName="FechaAsignacionProyectista" Width="100px" Visible="true" ToolTip="Fecha de Asignacion al Proyectista">

                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="2" Caption="Fecha prevista Termino" FieldName="FechaPrevistaTerminoProyectista" Width="100px" Visible="true" ToolTip=" Fecha prevista de Termino  por parte del proyectista">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="3" Caption="Fecha aviso preventivo" FieldName="FechaAvisoPreventivo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                        <%--             <dx:GridViewDataTextColumn FieldName="ISR" ReadOnly="True" Width="100px" Caption="I.S.R." VisibleIndex="4">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>--%>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Firmas" Name="TapFirmas" Visible="true">
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

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="5" Caption="Fecha recepcion termino escrituta" FieldName="FechaRecepcionTerminoEscritura" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6" Caption="Firma De Traslado" FieldName="FirmaDeTraslado" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="7" Caption="Fecha De Otorgamiento" FieldName="FechaDeOtorgamiento" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Aviso Definitivo" Name="TapAD" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvAvisoDefinitivo" ClientInstanceName="gvAvisoDefinitivo" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvAvisoDefinitivo_BeforePerformDataSelect">

                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>

                                                        <%--  Aviso definitivo --%>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="1" Caption="Fecha elaboracion definitivo" FieldName="FechaElaboracionDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="2" Caption="Fecha Envio RPP Definitivo" FieldName="FechaEnvioRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataCheckColumn VisibleIndex="3" Caption="Es tramite por sistema Definitivo" FieldName="EsTramitePorSistemaDefinitivo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataCheckColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="4" Caption="Fecha de traslado entregado" FieldName="FechaPagoBoletaDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="5" Caption="Fecha recibido RPP definitivo" FieldName="FechaRecibidoRPPDefinitivo" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Escrituracion" Name="TapEscritura" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvEscrituracion" ClientInstanceName="gvEscrituracion" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="true" OnBeforePerformDataSelect="gvEscrituracion_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  Escrituracion --%>
                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="1" Caption="Fecha recibo traslado" FieldName="FechaRecibioTraslado" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="2" Caption="Fecha asignacion mesa" FieldName="FechaAsignacionMesa" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="3" Caption="Fecha termino mesa" FieldName="FechaTerminoMesa" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="4" Caption="Fecha Autorizacion" FieldName="FechaAutorizacion" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>


                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Entregas" Name="TapEntrega" Visible="true">
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

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="3" Caption="Fecha registro entrega" FieldName="FechaRegistroEntrega" Width="100px" Visible="true" ToolTip="">

                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="4" Caption="Fecha boleta pago registro entrega" FieldName="FechaBoletaPagoRegistroEntrega" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="5" Caption="Fecha regreso registro" FieldName="FechaRegresoRegistro" Width="100px" Visible="true" ToolTip="">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6" Caption="Fecha salida" FieldName="FechaSalida" Width="100px" Visible="true" ToolTip="">
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
                                    <dx:TabPage Text="Contabilidad" Name="TapContabilidad" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvContabilidad" ClientInstanceName="gvContabilidad" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvContabilidad_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  columnas  Contabilidad --%>

                                                        <dx:GridViewDataTextColumn FieldName="ISR" ReadOnly="True" Width="100px" Caption="I.S.R." VisibleIndex="1">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn FieldName="ISRcalculado" ReadOnly="True" Width="100px" Caption="I.S.R. Calculado" VisibleIndex="2">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn FieldName="AvaluoCatastral" ReadOnly="True" Width="150px" Caption="Avaluo Catastral" VisibleIndex="3">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn FieldName="AvaluoFiscal" ReadOnly="True" Width="150px" Caption="Avaluo Fiscal" VisibleIndex="4">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn FieldName="AvaluoComercial" ReadOnly="True" Width="150px" Caption="Avaluo Comercial" VisibleIndex="5">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6" Caption="Fecha De Avaluo" FieldName="FechaDeAvaluo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6" Caption="Fecha pago Avaluo" FieldName="FechaPagoAvaluo" Width="100px" Visible="true">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataTextColumn FieldName="ValorOperacion" ReadOnly="True" Width="100px" Caption="Valor Operacion" VisibleIndex="7">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="$<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dx:GridViewDataTextColumn>


                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>

                                    <dx:TabPage Text="PLD" Name="TapPLD" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxGridView runat="server" ID="gvPld" ClientInstanceName="gvPld" KeyFieldName="IdExpediente"
                                                    EnablePagingGestures="False" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvPld_BeforePerformDataSelect">
                                                    <SettingsPager PageSize="100" NumericButtonCount="100"></SettingsPager>
                                                    <Columns>
                                                        <%--  columnas  PLD --%>


                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Actividad Vulnerable" FieldName="ActividadVulnerable" Width="150px" Visible="true">
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

            <dx:ASPxPopupControl runat="server" ID="ppCambiarEstatus" ClientInstanceName="ppCambiarEstatus" Height="500px" Width="800px" EnableClientSideAPI="true" ShowFooter="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="false" AllowDragging="true" CloseAction="CloseButton" HeaderText="Cambiar estatus del Expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppCambiarEstatus_WindowCallback" ScrollBars="Auto">
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


                                                    <dx:ASPxTreeList ID="trlEstatusExpedientes" ClientInstanceName="trlEstatusExpedientes" runat="server" KeyFieldName="IdEstatus"
                                                        ParentFieldName="IdEstadoPadre" AutoGenerateColumns="False" OnDataBinding="trlEstatusExpedientes_DataBinding">

                                                        <Columns>
                                                            <dx:TreeListDataColumn FieldName="Orden" Caption="Orden" VisibleIndex="1" Visible="false" SortOrder="Ascending" />
                                                            <dx:TreeListDataColumn FieldName="IdEstatus" Caption="Id Estatus" VisibleIndex="2" SortOrder="None" />
                                                            <dx:TreeListDataColumn FieldName="TextoEstatus" Caption="Estatus" VisibleIndex="3" SortOrder="None" />
                                                            <dx:TreeListDataColumn FieldName="Descripcion" VisibleIndex="4" SortOrder="None" />
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedNode="true" AllowSort="false" AutoExpandAllNodes="True"></SettingsBehavior>

                                                        <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>

                                                        <SettingsPopup>
                                                            <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                        </SettingsPopup>
                                                        <Border BorderStyle="Solid" />
                                                    </dx:ASPxTreeList>

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
                                                        ppCambiarEstatus.PerformCallback('guardar~'+trlEstatusExpedientes.GetFocusedNodeKey());                                                   

                                                        }" />

                        </dx:ASPxButton>
                    </div>
                </FooterContentTemplate>

            </dx:ASPxPopupControl>

            <dx:ASPxPopupControl runat="server" ID="ppAlertasExpediente" ClientInstanceName="ppAlertasExpediente" Height="600px" Width="1100px" EnableClientSideAPI="true" ShowFooter="false"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="false" AllowDragging="true" CloseAction="CloseButton" HeaderText="Alertas por Expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppAlertasExpediente_WindowCallback" ScrollBars="Auto">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">

                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" ClientInstanceName="frmExpedienteExistente" ColCount="3" ColumnCount="3" Width="100%">

                            <Items>
                                <dx:LayoutGroup Caption="Expediente" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" Caption="Numero" ColumnSpan="2" FieldName="ExfnNumeroExpediente">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxLabel runat="server" ID="txtNumExpedienteAlert" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Otorga" FieldName="ExfnOtorga" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtExfnOtorgaAlert" AutoPostBack="false" Width="100%" ClientEnabled="false">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="A favor de" FieldName="EXfnAfavorde" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtEXfnAfavordeAlert" AutoPostBack="false" Width="100%" ClientEnabled="false">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Alertas" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" Caption="" ColumnSpan="2" FieldName="ExfnAlertas">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">

                                                    <dx:ASPxGridView ID="gvAlertas" runat="server" AutoGenerateColumns="false" Width="100%" KeyFieldName="IdAlerta"
                                                        OnDataBinding="gvAlertas_DataBinding"
                                                        OnRowInserting="gvAlertas_RowInserting"
                                                        OnRowUpdating="gvAlertas_RowUpdating">


                                                        <SettingsPager Mode="ShowAllRecords" />

                                                        <Settings ShowFooter="True" ShowFilterRow="false"
                                                            ShowFilterBar="Auto" ShowFilterRowMenu="false"
                                                            ShowHeaderFilterButton="True" ShowGroupPanel="false"
                                                            VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                                                        <SettingsResizing ColumnResizeMode="Control" />

                                                        <SettingsEditing Mode="PopupEditForm" />

                                                        <SettingsDetail ExportMode="All" ShowDetailRow="false" />

                                                        <SettingsBehavior
                                                            AllowGroup="true"
                                                            AllowDragDrop="false"
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

                                                        <SettingsDataSecurity AllowInsert="true" AllowDelete="true" AllowEdit="true" />
                                                        <SettingsSearchPanel Visible="false" />

                                                        <Columns>

                                                            <dx:GridViewCommandColumn Visible="true" VisibleIndex="0" ShowNewButton="false" ShowEditButton="true" ShowDeleteButton="false" ShowNewButtonInHeader="true" ButtonRenderMode="Button" Width="30px"></dx:GridViewCommandColumn>

                                                            <dx:GridViewDataTextColumn FieldName="IdAlerta" Caption="n°" VisibleIndex="1" ReadOnly="true" Width="40px">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn FieldName="FechaAlta" Caption="Fecha Reporte" VisibleIndex="2" ReadOnly="true" Width="100px">
                                                                <EditFormSettings Visible="False" />
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm">
                                                                    <TimeSectionProperties Visible="true">
                                                                        <TimeEditProperties EditFormatString="HH:mm" DisplayFormatString="HH:mm"></TimeEditProperties>
                                                                    </TimeSectionProperties>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn FieldName="NomUsuarioInformante" Caption="Usuario Reporta" VisibleIndex="3" ReadOnly="true" Width="150px">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataMemoColumn FieldName="MensajeAlerta" Caption="Mensaje" VisibleIndex="4" ReadOnly="false" Width="500px"></dx:GridViewDataMemoColumn>
                                                            <dx:GridViewDataCheckColumn FieldName="AlertaActiva" Caption="Activo" VisibleIndex="5" Width="60px"></dx:GridViewDataCheckColumn>
                                                            <dx:GridViewDataDateColumn FieldName="FechaCierre" Caption="Fecha Cierre" VisibleIndex="6" ReadOnly="true" Width="90px">
                                                                <EditFormSettings Visible="False" />
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm">
                                                                    <TimeSectionProperties Visible="true">
                                                                        <TimeEditProperties EditFormatString="HH:mm" DisplayFormatString="HH:mm"></TimeEditProperties>
                                                                    </TimeSectionProperties>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn FieldName="NomUsuarioCierra" Caption="Usuario Cierre" VisibleIndex="7" ReadOnly="true" Width="150px">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                        </Columns>

                                                    </dx:ASPxGridView>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>


                    </dx:PopupControlContentControl>
                </ContentCollection>

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

                                        <dx:LayoutItem Caption="Otorga" FieldName="ExfnOtorga" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtExfnOtorga" AutoPostBack="false" Width="100%" ClientEnabled="false">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="A favor de" FieldName="EXfnAfavorde" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxMemo runat="server" ID="txtEXfnAfavorde" AutoPostBack="false" Width="100%" ClientEnabled="false">
                                                    </dx:ASPxMemo>
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

                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Aviso preventivo" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapAP">
                                    <Items>
                                        <dx:LayoutItem Caption="Elaboracion" FieldName="APfnFechaElaboracion" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaElaboracion" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Envio al R.P.P." FieldName="APfnFechaEnvioAlRPP" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaEnvioAlRPP" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
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
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaPagoBoleta" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Recibido" FieldName="APfnFechaRecibido" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAPfnFechaRecibido" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Proyecto" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapProyecto">
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
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaAsignacionProyectista" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Prevision de termino" FieldName="PRfnFechaPrevistaTermino" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaPrevistaTermino" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Aviso Preventivo" FieldName="PRfnFechaAvisoPreventivo" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtPRfnFechaAvisoPreventivo" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Firmas" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapFirmas">
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
                                                    <dx:ASPxDateEdit runat="server" ID="dtFIfnFechaRecepcionTerminoEscritura" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Firma Traslado" ColSpan="1" FieldName="FIfnFirmaTraslado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtFirmaTraslado" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha Otorgamiento" ColSpan="1" FieldName="FIfnFEchaOtorgamiento">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="fnFechaOtorgamiento" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Aviso definitivo" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapAD">
                                    <Items>
                                        <dx:LayoutItem ColSpan="1" Caption="Elaboracion" FieldName="AdfnFechaElaboracion">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaElaboracion" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Envio R.P.P." FieldName="AdfnFechaEnvioRPP">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaEnvioRPP" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
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
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaPagoBoleta" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Recibido" FieldName="AdfnFechaRecibido">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaRecibido" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Escrituracion" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapEscritura">
                                    <Items>
                                        <dx:LayoutItem ColSpan="1" Caption="Recibio traslado" FieldName="EsfnRecibioTraslado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEsfnRecibioTraslado" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Asignacion a mesa" FieldName="AdfnFechaAsignacionMesa">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaAsignacionMesa" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Termino del tramite" FieldName="AdfnFechaTerminoTramite">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtAdfnFechaTerminoTramite" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha de Autorizacion" ColSpan="1" FieldName="EnfnFechaAutorizacion">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtFechaAutorizacion" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Entregas" ColSpan="3" ColumnSpan="3" ColCount="2" ColumnCount="2" Name="TapEntrega">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" ColumnSpan="2" Caption="Observaciones" FieldName="EnfnObservacionesEntrega">
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
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaRegistro" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Boleta Pago" FieldName="EnfnFechaBoletaPago">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaBoletaPago" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Regreso registro" FieldName="EnfnFechaRegresoRegistro">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaRegresoRegistro" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="1" Caption="Salida" FieldName="EnfnFechaSalida">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="dtEnfnFechaSalida" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
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
                                <dx:LayoutGroup Caption="Contabilidad" ColCount="2" ColumnCount="2" ColSpan="3" ColumnSpan="3" Name="TapContabilidad">
                                    <Items>
                                        <dx:LayoutItem Caption="Valor Operacion" FieldName="PRfnValorOperacion" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtPRfnValorOperacion">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="I.S.R." FieldName="ContISR" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtContISR">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="I.S.R. Calculado" FieldName="ContISRCalculado" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtContISRCalculado">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Avaluo Catastral" FieldName="ContAvaluoCatastral" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtContAvaluoCatastral">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Avaluo Fiscal" FieldName="ContAvaluoFiscal" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtContAvaluoFiscal">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Avaluo Comercial" FieldName="ContAvaluoComercial" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtContAvaluoComercial">
                                                        <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    </dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha de Avaluo" ColSpan="1" FieldName="ConFechaDeAvaluo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="fnFechaAvaluo" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Fecha pago Avaluo" ColSpan="1" FieldName="ConFechaPagoAvaluo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit runat="server" ID="fnFechaPagoAvaluo" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="PLD" ColCount="3" ColumnCount="3" ColSpan="2" ColumnSpan="2" Name="TapPLD">
                                    <Items>
                                        <dx:LayoutItem Caption="Actividad Vulnerable" FieldName="PldActVulnerable" ColSpan="2" ColumnSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="txtPldActividadVulnerable"></dx:ASPxTextBox>
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


            <dx:ASPxPopupControl runat="server" ID="ppArchivos" ClientInstanceName="ppArchivos" Height="300px" Width="900px" EnableClientSideAPI="true" ShowFooter="false" Modal="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="false" AllowDragging="true" CloseAction="CloseButton" HeaderText="Archivos del expediente"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppArchivos_WindowCallback">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">



                        <%--     <dx:BootstrapFileManager runat="server" ID="ASPxFileManager1" Height="480px" Settings-EnableMultiSelect="true" ClientInstanceName="ASPxFileManager1" ClientIDMode="Static" >
                            <SettingsFileList View="Details" ShowFolders="true" ShowParentFolder="true">
                                <DetailsViewSettings AllowColumnDragDrop="true" ShowSelectAllCheckbox="true" ShowHeaderFilterButton="true" />
                            </SettingsFileList>
                            <Settings RootFolder="~/GNArchivosRoot" />
                            <SettingsEditing AllowCreate="true" AllowDelete="true" AllowMove="true" AllowCopy="true" AllowRename="true"
                                AllowDownload="true" TemporaryFolder="~/GNArchivosRoot" />
                            <SettingsToolbar ShowPath="false" />
                            <SettingsAdaptivity Enabled="true" CollapseFolderContainerAtWindowInnerWidth="991" />
                        </dx:BootstrapFileManager>--%>
                    </dx:PopupControlContentControl>
                </ContentCollection>

            </dx:ASPxPopupControl>



            <dx:ASPxPopupControl runat="server" ID="ppEditarAvisoNotarial" ClientInstanceName="ppEditarAvisoNotarial" Height="700px" Width="1100px" EnableClientSideAPI="true" ShowFooter="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="true" AllowDragging="true" CloseAction="CloseButton" HeaderText="Editar Datos Avisono Notarial"
                PopupAnimationType="Auto" AutoUpdatePosition="true" CloseOnEscape="true" OnWindowCallback="ppEditarAvisoNotarial_WindowCallback" ScrollBars="Auto">
                <ClientSideEvents EndCallback="CerrarModalyVerAlertas" Init="AdjustStylePopUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxFormLayout runat="server" ID="frmAvisoNotarial" ClientInstanceName="frmAvisoNotarial" ColCount="3" ColumnCount="3" Width="100%">

                            <Items>
                                <dx:LayoutGroup Caption="Aviso Notarial" ColSpan="3" ColumnSpan="3">
                                    <Items>
                                        <dx:LayoutGroup Caption="Generales" ColCount="4" ColumnCount="4" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Oficio Numero" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel runat="server" Font-Bold="True" ID="txtAnFolioSistena" Width="100%" ClientEnabled="false"></dx:ASPxLabel>


                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Fecha Otorgamiento" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">

                                                            <dx:ASPxDateEdit runat="server" ID="dtAnFechaOtorgamiento" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" ClientEnabled="false"></dx:ASPxDateEdit>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="N&#176; Escritura" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnEscritura" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>


                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Volumen" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnEscrituraVolumen" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Ubicacion" ColSpan="2" ColumnSpan="2">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbicacion" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Clave catastral" ColSpan="2" ColumnSpan="2">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnClaveCatrastal" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Datos de la operacion" ColCount="7" ColumnCount="7" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Valor Operacion/Fiscal" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnValorOperacionFiscal" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>
                                                            <masksettings mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" includeliterals="DecimalSymbol"></masksettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Valor del Avaluo" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnValorAvaluo" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>
                                                                <MaskSettings Mask="$&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Fecha del Avaluo" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                  
                                                             <dx:ASPxDateEdit runat="server" ID="dtAnFechaAvaluo" AutoPostBack="false" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" ClientEnabled="false"></dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Institucion que practico el avaluo(Anexo)" ColSpan="2" ColumnSpan="2">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnInstitucionPracticoAvaluo" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Naturaleza del acto o concepto de la adquisici&#243;n" ColSpan="2" ColumnSpan="2">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnNaturalezaActoAdquisicion" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Datos catastrales" ColCount="5" ColumnCount="5" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Superficie" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnSuperficie" Width="100%"></dx:ASPxTextBox>
                                                                <MaskSettings Mask="&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Vendida" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnVendida" Width="100%"></dx:ASPxTextBox>
                                                                <MaskSettings Mask="&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Restante" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnRestante" Width="100%"></dx:ASPxTextBox>
                                                                       <MaskSettings Mask="&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Construida" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnConstruida" Width="100%"></dx:ASPxTextBox>
                                                                       <MaskSettings Mask="&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Plantas" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnPlantas" Width="100%"></dx:ASPxTextBox>
                                                                       <MaskSettings Mask="&lt;0..9999999999g&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Contratantes" ColCount="2" ColumnCount="2" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Vendedor (es)" ColSpan="1" RowSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnContraVendedores" Width="100%" Height="100px" Native="true" ClientEnabled="false"  ></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Comprador (es)" ColSpan="1" RowSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnContraCompradores" Width="100%" Height="100px" Native="true" ClientEnabled="false"  ></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Domicilio de los contratantes" ColCount="2" ColumnCount="2" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Vendedor (es)" ColSpan="1" RowSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnDomiciVededores" Width="100%" Height="100px" Native="true"  ClientEnabled="false" ></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Comprador (es)" ColSpan="1" RowSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnDomiciCompradores" Width="100%" Height="100px" Native="true" ClientEnabled="false"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Datos de la direccion de notarias y registro publico del estado" ColCount="6" ColumnCount="6" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Partida" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnPartida" Width="100%" ></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Fojas" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnFojas" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Seccion" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnSeccion" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Volumen" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnVolumen" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Distrito" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnDistrito" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Folio real electronico" ColSpan="3" ColumnSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnFolioRealElectronico" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Sello registral" ColSpan="3" ColumnSpan="3">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnSelloRegistral" Width="100%"></dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Ubicacion / Descripcion de los bienes" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnUbicacionDescripcionBienes" Width="100%"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Medidas y colindancias" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnMedidasColindancias" Width="100%"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Observaciones y/o aclaraciones" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo runat="server" ID="txtAnObservacionesAclaraciones" Width="100%"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>

                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="Solicitud - Informe de propiedad territorial" ColSpan="3" ColumnSpan="3">
                                    <Items>
                                        <dx:LayoutGroup Caption="Generales" ColCount="2" ColumnCount="2" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Recibo pago impuesto predial (Folio)" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnReciboPagoImpuestaPre" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Fecha de ultimo pago" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnFechaUltimoPago" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Ubicacion del predio" ColCount="3" ColumnCount="3" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem Caption="Calle" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiCalle" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Numero" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiNumero" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Colonia" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiColonia" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Estado" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiEstado" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Municipio" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiMunicipio" Width="100%"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Localidad" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox runat="server" ID="txtAnUbiLocalidad"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>

                                                    <CaptionSettings Location="Top"></CaptionSettings>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Observaciones" ColSpan="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMemo Width="100%" runat="server" ID="txtAnUbiObservaciones"></dx:ASPxMemo>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>

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
                                                        ppEditarAvisoNotarial.PerformCallback('guardarCambios');                                                      
                                                        }" />

                        </dx:ASPxButton>
                    </div>
                </FooterContentTemplate>

            </dx:ASPxPopupControl>



        </section>








    </form>
</body>
</html>
