<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits=" GPS.Web.Login" %>

<%@ Register Assembly="DevExpress.Web.v23.1, Version=23.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/Controles/Usuario/InfoMsgBox.ascx" TagPrefix="uc1" TagName="cuInfoMsgbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>GPS</title>
    <%--<link rel="icon" href="imagenes/GPB.ico">--%>
    <link rel="icon" href="imagenes/siknoRIcono.ico">
    <link rel="stylesheet" href="Content/bootstrap.min.css" crossorigin="anonymous" />
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Content/all.css" crossorigin="anonymous" />

    <link rel="stylesheet" type="text/css" href="Content/styles.css" />

    
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/mensajes.js"></script>
    <script>

         $(document).ready(function () {
             //launchFullScreen(document.documentElement);
        });

        function modalboost() {
        $('#exampleModalCenter').modal('show');
        }

        $("#login-button").click(function (event) {
            event.preventDefault();

            $('form').fadeOut(500);
            $('.wrapper').addClass('form-success');
        });


        function launchFullScreen(element) {
            if (element.requestFullScreen) {
                element.requestFullScreen();
            } else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullScreen) {
                element.webkitRequestFullScreen();
            }
        }
    </script>
</head>
<body>
    <div style="display: inline-block; width: 100%; height: 100%">

        <div class="bb"></div>

        <div class="container">
            <div class="d-flex justify-content-center h-100">
                <div class="card">
                    <div class="card-header">
                        <h1 class="titles-header">Bienvenido  Sistema  SGN</h1>
                        <h3>Bienvenido</h3>
                        <h5>Sistema de gestion</h5>
                    </div>
                    <div class="card-body">
                        <form id="frmLogin" runat="server">
                            <uc1:cuInfoMsgbox runat="server" ID="cuInfoMsgbox1"  OnRespuestaClick="cuInfoMsgbox1_RespuestaClicked" />

                            <div class="input-group form-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                                </div>
                                <dx:ASPxTextBox runat="server" ID="txtUsername" Width="75%" 
                                    CssClass="form-control cajas" NullText="Username">
                                    <FocusedStyle CssClass="AccountNameFocused" />
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom"
                                        SetFocusOnError="true" ErrorFrameStyle-CssClass="AccountNameError" ValidateOnLeave="false">
                                        <RegularExpression ErrorText="Usuario No válido" ValidationExpression=".+" />

                                        <ErrorFrameStyle CssClass="AccountNameError"></ErrorFrameStyle>

                                        <RequiredField IsRequired="True" ErrorText="El campo 'Usuario' es obligatorio" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>

                            </div>
                            <div class="input-group form-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fas fa-key"></i></span>
                                </div>
                                <dx:ASPxTextBox ID="txtPassword" runat="server" Password="true" Width="75%" 
                                    CssClass="form-control cajas" NullText="Enter Password">
                                    <FocusedStyle CssClass="AccountNameFocused" />
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom"
                                        SetFocusOnError="true" ErrorFrameStyle-CssClass="AccountNameError" ValidateOnLeave="false">

                                        <RequiredField IsRequired="True" ErrorText="El campo 'Password' es obligatorio" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <%--<div class="row align-items-center remember">
                                <input type="checkbox" />Recordarme
                            </div>--%>
                            <div class="form-group">
                                <dx:ASPxButton ID="BT_ok" runat="server" OnClick="BT_ok_Click" Text="Ingresar" CssClass="btn float-right login_btn"></dx:ASPxButton>
                            </div>
                        </form>
                    </div>
                    <div class="card-footer">
                        <div class="textmin1 d-flex justify-content-center links">
                            Consultoria -IT  
                        </div>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
