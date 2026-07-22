
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SGN.Web.Login" %>

<%@ Register Assembly="DevExpress.Web.v25.2, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web"
    TagPrefix="dx" %>

<%@ Register Src="~/Controles/Usuario/InfoMsgBox.ascx"
    TagPrefix="uc1"
    TagName="cuInfoMsgbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>SGN | Sistema de Gestión Notarial</title>

    <link rel="icon" href="imagenes/favicon.ico" />

    <link rel="stylesheet"
        type="text/css"
        href="Content/NuevoMenu/login.css" />

    <script src="Scripts/sweetalert2.all.min.js"></script>

    <link rel="stylesheet"
        href="Scripts/sweetalert2.min.css" />

    <script src="../Scripts/mensajes.js"></script>

</head>

<body>

    <form id="frmLogin" runat="server">

        <div class="login-page">

            <div class="login-overlay"></div>

            <main class="login-main">

                <section class="login-container">

                    <!-- PANEL INFORMATIVO -->
                    <aside class="login-brand-panel">

                        <div class="brand-content">

                            <div class="brand-badge">
                                SGN
                            </div>

                            <h1 class="brand-title">Sistema de Gestión Notarial
                            </h1>

                            <p class="brand-description">
                                Plataforma centralizada para la gestión eficiente,
                                segura y organizada de la operación notarial.
                            </p>

                            <div class="brand-divider"></div>

                            <div class="brand-feature">
                                <span class="feature-icon">✓</span>
                                <span>Gestión centralizada de expedientes</span>
                            </div>

                            <div class="brand-feature">
                                <span class="feature-icon">✓</span>
                                <span>Control y seguimiento de actividades</span>
                            </div>

                            <div class="brand-feature">
                                <span class="feature-icon">✓</span>
                                <span>Acceso seguro a la información</span>
                            </div>

                        </div>

                    </aside>


                    <!-- PANEL LOGIN -->
                    <section class="login-card">

                        <div class="login-card-content">

                            <header class="login-header">

                                <div class="logo-container">

                                    <img
                                        src="imagenes/login/LogoNotaria.svg"
                                        alt="Logo del Sistema de Gestión Notarial"
                                        class="login-logo" />

                                </div>

                                <h2 class="login-title">Bienvenido
                                </h2>

                                <p class="login-subtitle">
                                    Ingresa tus credenciales para continuar
                                </p>

                            </header>


                            <div class="login-body">

                                <uc1:cuInfoMsgbox
                                    runat="server"
                                    ID="cuInfoMsgbox1"
                                    OnRespuestaClick="cuInfoMsgbox1_RespuestaClicked" />


                                <!-- USUARIO -->
                                <div class="form-group">

                                    <label class="form-label">
                                        Usuario
                                    </label>

                                    <div class="input-wrapper">

                                        <span class="input-icon">

                                            <svg
                                                viewBox="0 0 24 24"
                                                aria-hidden="true">

                                                <path d="M12 12c2.761 0 5-2.239 5-5s-2.239-5-5-5-5 2.239-5 5 2.239 5 5 5zm0 2c-3.866 0-7 2.239-7 5v1h14v-1c0-2.761-3.134-5-7-5z"></path>

                                            </svg>

                                        </span>

                                        <dx:ASPxTextBox
                                            runat="server"
                                            ID="txtUsername"
                                            Width="100%"
                                            NullText="Escribe tu usuario"
                                            CssClass="modern-input">

                                            <ValidationSettings
                                                ErrorDisplayMode="Text"
                                                Display="Dynamic"
                                                ErrorTextPosition="Bottom"
                                                SetFocusOnError="true"
                                                ValidateOnLeave="false">

                                                <RegularExpression
                                                    ErrorText="Usuario no válido"
                                                    ValidationExpression=".+" />

                                                <RequiredField
                                                    IsRequired="True"
                                                    ErrorText="El campo Usuario es obligatorio" />

                                            </ValidationSettings>

                                        </dx:ASPxTextBox>

                                    </div>

                                </div>


                                <!-- CONTRASEÑA -->
                                <div class="form-group">

                                    <label class="form-label">
                                        Contraseña
                                    </label>

                                    <div class="input-wrapper">

                                        <span class="input-icon">

                                            <svg
                                                viewBox="0 0 24 24"
                                                aria-hidden="true">

                                                <path d="M17 8h-1V6a4 4 0 0 0-8 0v2H7a2 2 0 0 0-2 2v10h14V10a2 2 0 0 0-2-2zm-7-2a2 2 0 1 1 4 0v2h-4V6zm3 9.732V18h-2v-2.268a2 2 0 1 1 2 0z"></path>

                                            </svg>

                                        </span>

                                        <dx:ASPxTextBox
                                            ID="txtPassword"
                                            runat="server"
                                            Password="true"
                                            Width="100%"
                                            NullText="Escribe tu contraseña"
                                            CssClass="modern-input">

                                            <ValidationSettings
                                                ErrorDisplayMode="Text"
                                                Display="Dynamic"
                                                ErrorTextPosition="Bottom"
                                                SetFocusOnError="true"
                                                ValidateOnLeave="false">

                                                <RequiredField
                                                    IsRequired="True"
                                                    ErrorText="El campo Contraseña es obligatorio" />

                                            </ValidationSettings>

                                        </dx:ASPxTextBox>

                                    </div>

                                </div>


                                <!-- BOTÓN -->
                                <div class="form-actions">

                                    <dx:ASPxButton
                                        ID="BT_ok"
                                        runat="server"
                                        OnClick="BT_ok_Click"
                                        Text="Ingresar al sistema"
                                        CssClass="login-button">
                                    </dx:ASPxButton>

                                </div>


                                <div class="login-security">

                                    <span class="security-icon">🔒
                                    </span>

                                    <span>Acceso restringido a usuarios autorizados
                                    </span>

                                </div>

                            </div>


                            <footer class="login-card-footer">

                                <span>SGN
                                </span>

                                <span class="separator">•
                                </span>

                                <span>Versión 1.5
                                </span>

                            </footer>

                        </div>

                    </section>

                </section>

            </main>


            <footer class="page-footer">

                <span>© 2026 Derechos Reservados
                </span>

                <span class="footer-separator">|
                </span>

                <a
                    href="http://www.consultoria-it.com"
                    target="_blank">Consultoria IT
                </a>

                <span class="footer-separator">|
                </span>

                <span>56 3731 8762
                </span>

                <span class="footer-separator">|
                </span>

                <span>Francisco I. Madero 3A, Huamantla, Tlaxcala
                </span>

            </footer>

        </div>

    </form>

</body>

</html>
```
