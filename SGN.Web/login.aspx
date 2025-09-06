﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SGN.Web.Login" %>
<%@ Register Assembly="DevExpress.Web.v25.1, Version=25.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/Controles/Usuario/InfoMsgBox.ascx" TagPrefix="uc1" TagName="cuInfoMsgbox" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SLE</title>
    <link rel="icon" href="imagenes/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Content/NuevoMenu/login.css" />
    <script src="Scripts/jquery-3.3.1.min.js"></script>
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
                    <h4 class="subtitle">Sistema de Gestión Notarial v1.5</h4>
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
                </div>
            </section>
        </main>

        <footer class="footer">
            © 2025 Derechos Reservados |
            <a href="http://www.consultoria-it.com" target="_blank">
                Consultoria IT | 55 4800 5547 | Francisco I. Madero 3A Humantla, Tlax | Sistema Orgullosamente Tlaxcalteca
            </a>
        </footer>
    </div>
</form>
</body>
</html>
