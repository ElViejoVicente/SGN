<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaExpediente.aspx.cs" Inherits="SGN.Web.APP.ConsultaExpediente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
    
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox runat="server" ID="txtFolioIterno" ClientInstanceName="txtFolioIterno" />
                                </td>

                                <td>
                                    <dx:ASPxButton runat="server" ID="btnConsultar" ClientInstanceName="btnConsultar"
                                        Text="Consultar" AutoPostBack="true" ClientEnabled="true" OnClick="btnConsultar_Click">
                                       
                                    </dx:ASPxButton>
                                </td>

                                 <td>
                                    <dx:ASPxMemo runat="server" ID="txtEstatusFolio" ClientInstanceName="txtEstatusFolio" Width="100%" />
                                </td>


                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxCaptcha runat="server" ID="captcha" ClientInstanceName="captcha"
                                        TextBox-Position="Bottom" Width="300px" Height="100px"
                                        CharacterSet="1234567890" CodeLength="4">
                                        <ValidationSettings SetFocusOnError="true"
                                            ErrorDisplayMode="Text"
                                            ValidationGroup="ValidacionCapCha" />
                                    </dx:ASPxCaptcha>
                                </td>
                            </tr>
                          
                          
                        </table>
       

            <!-- Eventos globales de cliente de DevExpress -->
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <ClientSideEvents
                    ControlsInitialized="OnControlsInitialized"
                    ValidationCompleted="OnValidationCompleted" />
            </dx:ASPxGlobalEvents>
        </div>
    </form>
</body>
</html>
