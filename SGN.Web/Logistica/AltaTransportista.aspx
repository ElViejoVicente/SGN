﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaTransportista.aspx.cs" Inherits="GPB.Web.Logistica.AltaTransportista" %>


<%@ Register Src="~/Controles/Usuario/InfoMsgBox.ascx" TagPrefix="uc1" TagName="cuInfoMsgbox" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v24.2, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
  <link rel="stylesheet" href="../SwitcherResources/Content/Simplex/bootstrap.min.css" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/all.css" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/generic/pageStyle.css" crossorigin="anonymous" />
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function iconMaxMin() {
            var i = $('#iconCollapse');
            i.attr('class', i.hasClass('fas fa-angle-double-down') ? 'fas fa-angle-double-up' : i.attr('data-original'));
        }

    </script>
</head>

<body>
    <form id="frmPage" runat="server" class="Principal">
        <uc1:cuInfoMsgbox runat="server" ID="cuInfoMsgbox1" />
        <%--<header class="CLPageHeader">
            <dx:ASPxImage runat="server" ID="imagenLogo" CssClass="imagenLogo"></dx:ASPxImage>
            <dx:ASPxLabel ID="lblNombrePagina" CssClass="titleHeader" runat="server" Text="Administración de Usuarios" Font-Bold="true"></dx:ASPxLabel>
            <dx:ASPxLabel ID="lblVersion" CssClass="titleHeader version" runat="server" Text="Versión: 1.0 Beta" Font-Bold="true"></dx:ASPxLabel>
        </header>--%>
        <div style="margin-left: 30px;">
            <a class="btn-box-tool" data-toggle="collapse" data-target="#controles" role="button" onclick="iconMaxMin()" aria-expanded="false" aria-controls="controles"><i id="iconCollapse" data-original="fas fa-angle-double-down" class="fas fa-angle-double-down"></i></a>
        </div>

        <section class="CLPageControls" id="controles" class="collapse show" aria-labelledby="controles">
            <div class="" style="width: 100%; padding-left: 10px;">
                <div class="row">
                    <div class="">
                       <%-- <dx:BootstrapButton ID="btnNuevo" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-file" ToolTip="Nuevo"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small">
                        </dx:BootstrapButton>--%>

                        <dx:BootstrapButton ID="btnGuaqrdar" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-save" OnClick="btnGuaqrdar_Click" ToolTip="Guardar"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small">
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnCancelar" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-window-close" ToolTip="Cancelar"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small" OnClick="btnCancelar_Click">
                        </dx:BootstrapButton>

                       <%-- <dx:BootstrapButton ID="btnBorrar" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-trash-alt" ToolTip="Borrar"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small">
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnBuscar" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-binoculars" ToolTip="Buscar"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small">
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnExportar" runat="server" AutoPostBack="false" Text="" CssClasses-Icon="fas fa-print" ToolTip="Exportar"
                            Style='font-size: 17px' SettingsBootstrap-Sizing="Small">
                        </dx:BootstrapButton>--%>
                    </div>
                </div>
            </div>
        </section>

        <section class="CLPageContent">
            <div class="">
                <dx:BootstrapFormLayout ID="BootstrapFormLayout1" runat="server">

                     <Items >
                        <dx:BootstrapLayoutItem Caption="Nombre" ColSpanLg="4" ColSpanMd="6" >
                            <ContentCollection>
                                <dx:ContentControl>
                                
                                    <dx:BootstrapTextBox ID="Txt_nombre" runat="server" NullText="Nombre completo" Enabled="true" >
                                       <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Nombre es obligatorio" />
                                        </ValidationSettings>
                                        </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                        <dx:BootstrapLayoutItem Caption="NIF/CIF" ColSpanLg="4" ColSpanMd="6">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                 
                                    <dx:BootstrapTextBox ID="Txt_nifcif" runat="server" NullText="Nif" Enabled="true">
                                         <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Nif/CIF es obligatorio" />
                                        </ValidationSettings>
                                         </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                         <dx:BootstrapLayoutItem Caption="Dirección" ColSpanLg="4" ColSpanMd="6">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                 
                                    <dx:BootstrapTextBox ID="txt_Calle" runat="server" NullText="Dirección" Enabled="true" >
                                        <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Direccion es obligatorio" />
                                        </ValidationSettings>
                                         </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                         <dx:BootstrapLayoutItem Caption="Cod. Postal" ColSpanLg="4" ColSpanMd="6">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                 
                                    <dx:BootstrapTextBox ID="txt_codpostal" runat="server" NullText="Código Postal" Enabled="true" >
                                        <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Codigo Postal es obligatorio" />
                                        </ValidationSettings>
                                         </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                         <dx:BootstrapLayoutItem Caption="Población" ColSpanLg="4" ColSpanMd="6">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                 
                                    <dx:BootstrapTextBox ID="txt_poblacion" runat="server" NullText="Población" Enabled="true" >
                                         <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Población es obligatorio" />
                                        </ValidationSettings>
                                         </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                         <dx:BootstrapLayoutItem Caption="Teléfono" ColSpanLg="4" ColSpanMd="6">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                 
                                    <dx:BootstrapTextBox ID="txt_telefono" runat="server" NullText="Teléfono" Enabled="true" >
                                         <ValidationSettings>
                                                 <RequiredField IsRequired="true" ErrorText="Teléfono es obligatorio" />
                                        </ValidationSettings>
                                         </dx:BootstrapTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:BootstrapLayoutItem>
                         </Items>
                </dx:BootstrapFormLayout>
               </div>
        </section>

       <%-- <footer class="CLPageFooter">
            © Derechos Reservados 2020-2021 CL Grupo Industrial Todos los Derechos Reservados.
        </footer>--%>
    </form>

</body>
</html>.
