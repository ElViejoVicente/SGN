<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoMsgBoxMovil.ascx.cs" Inherits=" SGN.Web.Controles.Usuario.InfoMsgBoxMovil" %>

<dx:ASPxPopupControl ID="ppControl" runat="server" Height="30px" Width="300px" EnableClientSideAPI="True" PopupElementID="grid" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AppearAfter="4000" DisappearAfter="4000" AllowResize="false" AllowDragging="false">

    <CloseButtonImage Url="../../imagenes/mensajes/close10Negro.png" AlternateText="No image" />
    <HeaderStyle ForeColor="WhiteSmoke" Font-Size="11px" BackColor="#9e311c" VerticalAlign="Middle" />
    <ContentStyle ForeColor="#666677" Font-Names="Tahoma" Font-Size="8px" VerticalAlign="Middle" HorizontalAlign="Left" />

    <controls>
        <asp:Table ID="Table2" runat="server" Height="100%" Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Left"  VerticalAlign="Middle" Width="20%">
                    <dx:ASPxImage ID="imgIcon" runat="server"  Width="15px" Height="15px" ShowLoadingImage="true" />
                </asp:TableCell>

                <asp:TableCell runat="server" Wrap="true"  HorizontalAlign="Left" VerticalAlign="Middle" Width="80%">
                    <dx:ASPxLabel ID="lblMensaje" runat="server" Text="Aquí va el mensaje" Width="95%" ></dx:ASPxLabel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </controls>  
</dx:ASPxPopupControl>

