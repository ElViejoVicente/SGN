<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SGN.Web.Login" %>

<%@ Register Assembly="DevExpress.Web.v25.2, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/Controles/Usuario/InfoMsgBox.ascx" TagPrefix="uc1" TagName="cuInfoMsgbox" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SGN</title>
    <link rel="icon" href="imagenes/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Content/NuevoMenu/login.css" />
    <script src="Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="Scripts/sweetalert2.min.css" />
    <script src="../Scripts/mensajes.js"></script>

</head>
<body>
    <form id="frmLogin" runat="server">
        <!-- Estructura página: header (opcional) / main centrado / footer sticky -->
        <div class="page">
            <main class="main">
                <section class="card animate-slide-up">
                    <header class="card-header">
                        <img src="imagenes/login/LogoNotaria.svg" alt="Logo Sistema" class="logo-animated" />
                        <h1 class="title">Bienvenido</h1>
                        <h4 class="subtitle">Sistema de Gestión Notarial v1.3</h4>
                    </header>

                    <div class="card-body">
                        <uc1:cuInfoMsgbox runat="server" ID="cuInfoMsgbox1" OnRespuestaClick="cuInfoMsgbox1_RespuestaClicked" />

                        <div class="form-row">
                            <dx:ASPxTextBox runat="server" ID="txtUsername" Width="100%" NullText="Usuario">
                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom"
                                    SetFocusOnError="true" ValidateOnLeave="false">
                                    <RegularExpression ErrorText="Usuario no válido" ValidationExpression=".+" />
                                    <RequiredField IsRequired="True" ErrorText="El campo 'Usuario' es obligatorio" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </div>

                        <div class="form-row">
                            <dx:ASPxTextBox ID="txtPassword" runat="server" Password="true" Width="100%" NullText="Contraseña">
                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom"
                                    SetFocusOnError="true" ValidateOnLeave="false">
                                    <RequiredField IsRequired="True" ErrorText="El campo 'Contraseña' es obligatorio" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </div>

                        <div class="form-row">
                            <dx:ASPxButton ID="BT_ok" runat="server" OnClick="BT_ok_Click" Text="Ingresar" CssClass="btn-primary glow-on-hover"></dx:ASPxButton>
                        </div>
                        <!-- Alerta crítica colocada debajo del login -->
                 <%--       <div class="update-alert-critical" style="background: #f8d7da; border: 2px solid #f5c2c7; color: #842029; padding: 14px; border-radius: 6px; margin-top: 18px; box-shadow: 0 0 10px rgba(184, 28, 28, 0.2);">
                            <div style="display: flex; align-items: center;">
                                <div style="flex: 0 0 auto; margin-right: 12px; font-size: 22px;">⚠️</div>
                                <div style="flex: 1 1 auto;">
                                    <strong style="font-size: 16px; display: block; margin-bottom: 6px;">CRITICAL SECURITY ALERT</strong>
                                    <div>Critical pending updates have been detected that affect system security. It is essential to apply the updates as soon as possible.</div>
                                    <ul style="margin: 8px 0 0 18px; padding: 0;">
                                        <li><strong>Windows Server Update</strong> — critical patches pending</li>
                                        <li><strong>.NET Framework 4.8</strong> — security update required</li>
                                        <li><strong>Security and Backups</strong> — the last security update is over 180 days old</li>
                                    </ul>

                                </div>
                            </div>
                        </div>--%>
                    </div>
                </section>
            </main>

            <footer class="footer">
                © 2026 Derechos Reservados |
            <a href="http://www.consultoria-it.com" target="_blank">Consultoria IT | 56 3731 8762 | Francisco I. Madero 3A Humantla, Tlax | Sistema Orgullosamente Tlaxcalteca
            </a>
            </footer>
        </div>
    </form>
</body>
</html>
