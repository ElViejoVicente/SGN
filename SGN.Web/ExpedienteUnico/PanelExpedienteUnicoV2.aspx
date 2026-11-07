<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedienteUnicoV2.aspx.cs" Inherits="SGN.Web.ExpedienteUnico.PanelExpedienteUnicoV2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="../Scripts/sweetalert2.min.css" />
    <link rel="stylesheet" href="PanelExpedienteUnicoV2.aspx.css" />
    <script src="../Scripts/mensajes.js"></script>
    <title>Expediente único</title>
    <script type="text/javascript">
window.onresize = function() { adjustGridHeight(); };
function adjustGridHeight() { var c = document.getElementById('expContent'); if (c && window.gvExpedienteUnico) { gvExpedienteUnico.SetHeight(Math.max(240, c.clientHeight - 2)); } }
        function gridView_EndCallback(s,e){
            adjustGridHeight();
            if(s.cp_KpiExpedientes!==undefined){
                lblKpiExpedientes.SetText(s.cp_KpiExpedientes);
                lblKpiSinAnexo.SetText(s.cp_KpiSinAnexo);
                lblKpiConAnexo.SetText(s.cp_KpiConAnexo);
                lblKpiCumplimiento.SetText(s.cp_KpiCumplimiento);
            }
            if(s.cp_swMsg){mostrarMensajeSweet(s.cp_swType,s.cp_swMsg);s.cp_swType=null;s.cp_swMsg=null;}
        }
    </script>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">
        <div class="exp-page">
            <div class="exp-filter">
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Opciones de consulta:" View="GroupBox" CssClass="exp-filter-options">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="exp-filter-row">
                                <dx:ASPxDateEdit Caption="Inicio" runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="125px" />
                                <dx:ASPxDateEdit Caption="Fin" runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaFin" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="125px" />
                                <dx:ASPxButton ID="btnActualizar" runat="server" Text="Actualizar" AutoPostBack="false">
                                    <Image IconID="xaf_action_reload_svg_16x16" />
                                    <ClientSideEvents Click="function(s,e){gvExpedienteUnico.PerformCallback('CargarRegistros');}" />
                                </dx:ASPxButton>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="KPI:" View="GroupBox" CssClass="exp-filter-kpi">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="exp-kpi-content">
                                <div class="kpi-item"><span class="kpi-title">Expedientes</span><dx:ASPxLabel ID="lblKpiExpedientes" ClientInstanceName="lblKpiExpedientes" runat="server" Text="0" CssClass="kpi-value" /></div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-warning"><span class="kpi-title">Sin anexo</span><dx:ASPxLabel ID="lblKpiSinAnexo" ClientInstanceName="lblKpiSinAnexo" runat="server" Text="0" CssClass="kpi-value" /></div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-success"><span class="kpi-title">Con anexo</span><dx:ASPxLabel ID="lblKpiConAnexo" ClientInstanceName="lblKpiConAnexo" runat="server" Text="0" CssClass="kpi-value" /></div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-compliance"><span class="kpi-title">Cumplimiento</span><dx:ASPxLabel ID="lblKpiCumplimiento" ClientInstanceName="lblKpiCumplimiento" runat="server" Text="0%" CssClass="kpi-value" /></div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </div>
            <div id="expContent" class="exp-content">
                <dx:ASPxGridView runat="server" ID="gvExpedienteUnico" ClientInstanceName="gvExpedienteUnico" AutoGenerateColumns="False" Width="100%" KeyFieldName="IdRegistro" CssClass="exp-grid" OnDataBinding="gvExpedienteUnico_DataBinding" OnCustomCallback="gvExpedienteUnico_CustomCallback" OnToolbarItemClick="gvExpedienteUnico_ToolbarItemClick" OnHtmlDataCellPrepared="gvExpedienteUnico_HtmlDataCellPrepared">
                    <ClientSideEvents Init="function(s,e){adjustGridHeight();}" EndCallback="gridView_EndCallback" />
                    <Settings ShowColumnHeaders="False" ShowGroupPanel="False" ShowFilterRow="False" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Hidden" />
                    <SettingsPager Mode="ShowPager" PageSize="50" Position="Bottom" />
                    <SettingsBehavior AutoExpandAllGroups="true" AllowGroup="false" AllowSort="false" AllowFocusedRow="false" EnableRowHotTrack="false" />
                    <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="false" />
                    <SettingsText EmptyDataRow="No se encontraron expedientes para el periodo seleccionado." />
                    <Templates>
                        <GroupRowContent>
                            <div class="exp-group-header">
                                <i class="fas fa-chevron-down exp-group-icon"></i>
                                <dx:ASPxLabel runat="server" CssClass="exp-group-title" Text='<%# GetTextoEncabezado(Container.GroupText) %>' />
                            </div>
                        </GroupRowContent>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Resumen" GroupIndex="0" VisibleIndex="0" />
                        <dx:GridViewDataTextColumn FieldName="Nombres" VisibleIndex="1">
                            <DataItemTemplate>
                                <div class="person-card">
                                    <div>
                                        <dx:ASPxLabel runat="server" CssClass="person-name" Text='<%# GetNombreCompleto(Eval("Nombres"),Eval("ApellidoPaterno"),Eval("ApellidoMaterno")) %>' />
                                        <dx:ASPxLabel runat="server" CssClass="person-subtitle" Text='<%# GetDescripcionParticipante(Eval("RolOperacion"),Eval("Sexo"),Eval("Ocupacion")) %>' />
                                        <asp:Panel runat="server" CssClass="person-warning" Visible='<%# !TieneExpediente(Eval("IdExpedienteUnico")) %>'><i class="fas fa-exclamation-triangle"></i>&nbsp;Sin expediente generado</asp:Panel>
                                    </div>
                                    <div class="person-meta" runat="server" visible='<%# TieneExpediente(Eval("IdExpedienteUnico")) %>'>
                                        <span class="meta-item"><i class="far fa-file-alt meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# GetAnexo(Eval("NombreDelAnexo"),Eval("IdExpedienteUnico")) %>' />
                                        </span>
                                        <span class="meta-item"><i class="far fa-calendar-alt meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# FormatFecha(Eval("FechaCreacionVersion")) %>' />
                                        </span>
                                        <span class="meta-item"><i class="far fa-clock meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# FormatFecha(Eval("FechaUltimaModificacion")) %>' />
                                        </span>
                                        <span class="meta-item"><i class="fas fa-user meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# Eval("UsuarioAutor") %>' />
                                        </span>
                                    </div>
                                    <div class="person-actions">
                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text='<%# TieneExpediente(Eval("IdExpedienteUnico")) ? "Abrir expediente" : "Generar expediente" %>' />
                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Generar PDF" />
                                    </div>
                                </div>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </form>
</body>
</html>
