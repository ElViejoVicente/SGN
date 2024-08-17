<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits=" SGN.Web.menu" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Menu</title>


    <link rel="stylesheet" href="Content/bootstrap.min.css" crossorigin="anonymous" />
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <link rel="stylesheet" href="Content/menu.css" crossorigin="anonymous" />   

    <script type="text/javascript">     

        var sw = 0;
        function MuestraOcult(s) {
            var rcFile = new Array();
            rcFile = s.split("/");

            var imagen = rcFile[rcFile.length - 1];
            var imgObj = document.getElementById('cntMosOcul');

            if (imagen == 'Ocultar.ico' && sw == 0) {
                //parent.document.getElementById('menu').cols = "25.5,*";
                parent.document.getElementById('menu').width = "10px !important";
                imgObj.src = "imagenes/menu/Mostrar.ico";
                imgObj.alt = "Mostrar Menú";
                sw = 1;
            }
            else if (imagen == 'Mostrar.ico' && sw == 1) {
                //parent.document.getElementById('menu').cols = "235,* ";
                parent.document.getElementById('menu').width = "13% !important";
                imgObj.src = "imagenes/menu/Ocultar.ico";
                imgObj.alt = "Ocultar Menú";
                sw = 0;
            }
        }

        function AdjustSize() {
            var height = document.getElementById('menudiv').clientHeight;
            var width = document.getElementById('menudiv').clientWidth;
            rtvMenu.SetHeight(height);
        }


        window.onresize = function (event) {
            AdjustSize();
        };
        var swm = 0;
        function rendererMenu() {
            var ifrMenu = parent.document.getElementById('menu');
            var ifrcontent = parent.document.getElementById('basefrm');
            var imgClip = document.getElementById('imgObj');

            if (swm == 0) {
                ifrMenu.style = "width:40px !important";
                ifrcontent.style = "width:97.6% !important;margin-left: 41px;";
                imgClip.src = "imagenes/menu/Mostrar.ico";
                swm = 1;
            }
            else if (swm == 1) {
                ifrMenu.style = "width:13% !important";
                ifrcontent.style = "width:86.5% !important;margin-left:13.4%;";
                imgClip.src = "imagenes/menu/Ocultar.ico";
                swm = 0;
            }
        }
    </script>
</head>
<body>
    <div style="width:100%;height:3.5%; float:left">
        <button type="button" class="navbar-toggle" data-toggle="collapse" 
                    data-target="#menudiv" onclick="rendererMenu()" style="background-color:#2A385D">
                    <img id="imgObj" src="imagenes/menu/Ocultar.ico" alt="Ocultar Menú" width="17" height="17"  />       
        </button>
    </div>
    <div id="menudiv" style="width:100%; height:96.5%" class="collapse show">
        <div id="credenciales" class="UserMenu" style="">
            <div style="width: 100%; padding-left: 25%">
                <dx:ASPxImage runat="server" ID="imagenUser" CssClass="imageCls" ImageAlign="Middle" ></dx:ASPxImage>
            </div>
            <div style="width: 100%;">
                <dx:ASPxLabel ID="lblPerfil" runat="server" Text="" Font-Bold="true" CssClass="clsperfil"></dx:ASPxLabel>
            </div>
            <div style="width: 100%">
                <dx:ASPxLabel ID="lblNomUsuario" runat="server" Text="" Font-Bold="true" CssClass="clsusuario"></dx:ASPxLabel>
            </div>
            <div style="width: 100%">
                <dx:ASPxLabel ID="lblMail" runat="server" Text="" Font-Bold="true" CssClass="clsmail"></dx:ASPxLabel>
            </div>
          
        </div>
        <hr/>
        <div id="menuPanel" style="overflow: hidden">
            <form id="frmMEnu" runat="server" style="height:1300px;width:100%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <dx:ASPxHiddenField ID="Hidden_usuario" runat="server" ClientIDMode="Static"></dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="hBandera" runat="server" ClientIDMode="Static"></dx:ASPxHiddenField>

                <div></div>
                <table  style="border-width: 0px; height: 100%; width: 100%; border:0px solid yellow">
                    <%--<tr style="height:20%;border:1px solid blue">--%>
                
                    <%--</tr>--%>
                    <tr style="height: 60%;border:0px solid green">
                        <%--<td style="height: 100%; width:1%">
                            <div id="EtiqMenu" style="width:100%; height: 100%; background-color: brown">
                                <img src="imagenes/menu/Ocultar.ico" alt="Ocultar Menú" width="17" height="17" id="cntMosOcul"
                                    onclick="MuestraOcult(this.src);" />
                            </div>
                        </td>--%>
                        <td style="height: 100%">
                            <div id="Div1" style="width: 100%; height: 100%">
                                <div id="ContMenu" class="contmenu">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <dx:ASPxTreeView  ID="rtvMenu" runat="server" Theme="MetropolisBlue"  OnNodeClick="rtvMenu_NodeClick" CssClass="treMenu" OnExpandedChanging="rtvMenu_ExpandedChanging">
                                            <ClientSideEvents Init="AdjustSize" />
                                            </dx:ASPxTreeView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</body>
</html>

