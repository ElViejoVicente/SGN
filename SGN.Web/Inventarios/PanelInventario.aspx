<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelInventario.aspx.cs" Inherits="SGN.Web.Inventarios.PanelInventario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxCheckBox runat="server" ID="Activo" Width="150px" ClientInstanceName="Activo"
                                                            Text="Activo" ToggleSwitchDisplayMode="Always" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnActualizar" runat="server" Image-IconID="xaf_action_reload_svg_16x16" Text="Actualizar"
                                                            AutoPostBack="false" Enabled="true">
                                                            <ClientSideEvents Click="function(s, e) { Inventario.PerformCallback('CargarLista'); }" />
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
    </form>
</body>
</html>
