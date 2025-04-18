<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelInventario.aspx.cs" Inherits="SGN.Web.Inventarios.PanelInventario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <title>Activos</title>
    <link rel="stylesheet" href="https://cdn.devexpress.com/ASPxGridView.css" />
    <script src="https://cdn.devexpress.com/ASPxGridView.js"></script>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">

        <dx:ASPxPanel ID="TopPanel" runat="server" FixedPosition="WindowTop" FixedPositionOverlap="true" CssClass="topPanel">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowCollapseButton="true" Width="170px" HeaderText="Opciones de Consulta" View="GroupBox">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxDateEdit Caption="Fecha De Alta" runat="server" ID="Fecha" ClientInstanceName="dtFecha"
                                                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" AutoPostBack="false" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxCheckBox runat="server" ID="chkBusquedaCompleta" Width="150px" ClientInstanceName="chkBusquedaCompleta"
                                                            Text="Todas las fechas" ToggleSwitchDisplayMode="Always">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
                                                                if (chkBusquedaCompleta.GetChecked()) {
                                                                    dtFecha.SetEnabled(false);
                                                                } else {
                                                                    dtFecha.SetEnabled(true);
                                                                }
                                                            }" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox runat="server" ID="Activo" Width="150px" ClientInstanceName="Activo"
                                                            Text="Activo" ToggleSwitchDisplayMode="Always" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar"
                                                            AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) { gridActivos.PerformCallback('CargarLista'); }" />
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

        <div style="padding:20px;">
            <dx:ASPxGridView ID="gridActivos" runat="server" AutoGenerateColumns="False" Width="100%" 
                EnableRowsCache="false" ShowFilterRow="True" ShowHeaderFilterButton="True" 
                Settings-ShowFilterRowMenu="True" Settings-VerticalScrollBarMode="Visible" 
                Settings-VerticalScrollableHeight="400" SettingsBehavior-AllowFocusedRow="True" 
                SettingsPager-PageSize="15" ClientInstanceName="gridActivos">

                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Num" Caption="#" ReadOnly="True" Width="50px" />
                    <dx:GridViewDataTextColumn FieldName="Tipo" Caption="Tipo" />
                    <dx:GridViewDataTextColumn FieldName="Modelo" Caption="Modelo" />
                    <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Nombre" />
                    <dx:GridViewDataTextColumn FieldName="Marca" Caption="Marca" />
                    <dx:GridViewDataDateColumn FieldName="FechaAlta" Caption="Fecha Alta" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
                    <dx:GridViewDataDateColumn FieldName="FechaBaja" Caption="Fecha Baja" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
                    <dx:GridViewDataTextColumn FieldName="ValorMonetario" Caption="Valor Monetario" PropertiesTextEdit-DisplayFormatString="c2" />
                    <dx:GridViewDataTextColumn FieldName="AreaOficina" Caption="Área/Oficina" />
                    <dx:GridViewDataTextColumn FieldName="NombreResponsable" Caption="Responsable" />
                    <dx:GridViewDataDateColumn FieldName="FechaAsignacion" Caption="Fecha Asignación" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
                    <dx:GridViewDataCheckColumn FieldName="Activo" Caption="Activo" />
                    <dx:GridViewDataTextColumn FieldName="Observaciones" Caption="Observaciones" />
                </Columns>

                <SettingsEditing Mode="PopupEditForm" PopupEditFormHeight="400px" PopupEditFormWidth="600px" />
                <Settings ShowFilterRow="true" ShowGroupPanel="false" ShowFilterBar="Visible" />
                <SettingsBehavior AllowFocusedRow="true" /> 
            </dx:ASPxGridView>

            <dx:ASPxGridViewExporter ID="gridExporter" runat="server" GridViewID="gridActivos" />

            <br />
        </div>
    </form>
</body>
</html>


