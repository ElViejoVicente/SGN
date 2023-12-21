<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits=" GPS.Web.header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="Content/all.css" crossorigin="anonymous" />
    <script src="../Scripts/jquery-latest.js"></script>
    <link rel="stylesheet" type="text/css" href="Content/headers.css" />
    
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
   
     <script src="../Scripts/umd/popper.min.js"></script>
   
    
    <script src="../Scripts/bootstrap/js/bootstrap.min.js"></script>
    
    <script src="../Scripts/bootstrap/js/bootstrap.js"></script>
    
    <script src="../Scripts/bootstrap/js/bootstrap.bundle.min.js"></script>
    
    <script src="../Scripts/bootstrap/js/bootstrap.bundle.js"></script>

    <link href="../Scripts/bootstrap/css/bootstrap-grid.css" rel="stylesheet" />
    
    <link href="../Scripts/bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />
    
    <link href="../Scripts/bootstrap/css/bootstrap-reboot.css" rel="stylesheet" />
    
    <link href="../Scripts/bootstrap/css/bootstrap-reboot.min.css" rel="stylesheet" />
    
    <link href="../Scripts/bootstrap/css/bootstrap.css" rel="stylesheet" />
    
    <link href="../Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../Scripts/mensajes.js"></script>

    <link rel="stylesheet" type="text/css" href="Content/headers.css" />
     <style type="text/css">

        /*.btn {
            background    : lightgrey;
            border-radius : 2px;
            border        : 1px solid darkgrey;
            color         : grey;
            display       : inline-block;
            font-size     : 0.5rem;
            padding       : 0px;
        }*/
        .btn:hover {
            cursor: pointer;
        }
        .custom-btn {
            width:30px;
            height:30px;
            font-family : monospace;
            margin      : 0px;
            padding-top : 0px;
        }
        .vertical {
            transform: rotate(-90deg);
        }
    </style>
     <script type="text/javascript">

        //function setHeartbeat() {
        //      console.log("colocando timeout heartbeat...");           
        //    setTimeout(heartbeat, 10000);
        //}

        //function heartbeat() {
        //    console.log("realizando bloqueo Usuario...");
        //    var obj = {};          
        //    obj.usuario = document.getElementById("HidUsuario").value;         
        //    console.log("jso envio:"+ Object.values(obj));
       
        //    var jsonData = JSON.stringify(obj);

        //    $.post("/SessionHeartbeat.ashx",
        //        jsonData,
        //        function (data, status) {
        //            console.log("Data: " + data + "\nStatus: " + status);
        //            console.log("respuesta success islive...");
        //            setHeartbeat();
        //        });

        //}
       // setHeartbeat();
     </script>
</head>
<body>
    <form id="frmCabecero" runat="server" class="clsCabecero">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <header>
            <div class="wrapper">
                <asp:HiddenField ID="HidUsuario" runat="server" ClientIDMode="Static" />
                <div style="position: absolute; width: 13%; height: 100%; display: inline-block; padding-top: 0.5%">
                    <img class="imagenGG" src="imagenes/header/logo-siknoT.png" height="30px" alt="Grupo Gallardo" />
                    <div class="logo" style="">GPS</div>
                </div>
                <div style="position: absolute; width: 71%; height: 100%; display: inline-block; margin-left: 14%; padding-top: 12px;">
                    <div style="width: 70%; height: 100%; display: inline-block;">
                        <dx:ASPxLabel ID="lblNombrePagina" CssClass="etiquetas" runat="server" Text="" Font-Bold="false"></dx:ASPxLabel>
                    </div>
                    <div style="width: 30%; height: 100%; display: inline-block; float: right">
                        <dx:ASPxLabel ID="lblVersion" CssClass="etiquetas" runat="server" Text="" Font-Bold="true"></dx:ASPxLabel>
                    </div>
                </div>
                <div style="position: absolute; width: 9%; height: 100%; display: inline-block; margin-left: 85%; padding-top: 0.5%">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="65%" CssClass="clsidioma"
                        ImageUrlField="ImageUrl" TextField="Text" ValueField="Name" ValueType="System.String"
                        ShowImageInEditBox="True" SelectedIndex="0" Enabled="false">
                        <ItemImage Height="24px" Width="23px" />
                        <Items>
                            <dx:ListEditItem Text="Español" Value="esp" Selected="True" ImageUrl="imagenes/header/ban-spain.png" />
                            <dx:ListEditItem Text="Inglés" Value="us" Selected="True" ImageUrl="imagenes/header/ban-usa.png" />
                        </Items>
                    </dx:ASPxComboBox>
                </div>
                <div style="position: absolute; width: 5.5%; height: 100%; display: inline-block; margin-left: 94%; padding-top: 0.5%">
                    <dx:ASPxButton runat="server" ID="btnCerrarSesion" Text="..." OnClick="btnCerrarSesion_Click" CssClass="custom-btn vertical clsSalir" RenderMode="Danger"
                        ToolTip="Salir GPS">
                        <%--<ClientSideEvents Init="setHeartbeat" />--%>
                    </dx:ASPxButton>
                </div>
                <div style="position: absolute; width: 0.5%; height: 100%; display: inline-block; margin-left: 99.5%">
                </div>
            </div>
        </header>
    </form>



    <%--<form id="frmCabecero" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <header>
            <div class="wrapper">
                <img class="imagenGG" src="imagenes/header/logo-siknoT.png" height="70" alt="" />
                <div id="EndSession" class="close">
                    <dx:ASPxButton runat="server" ID="btnCerrarSesion" Text="" OnClick="btnCerrarSesion_Click" CssClass="btn float-right login_btn" ToolTip="Salir">
                        <HoverStyle BackgroundImage-ImageUrl="imagenes/header/cerrarSessionMinHoverN.png"></HoverStyle>
                    </dx:ASPxButton>
                </div>
                <img class="imagenCL" src="imagenes/logo-cl.png" height="70" alt="CL Grupo Industrial" />
                <div style="display: block">
                    <div class="logo">GPS</div>
                    <div class="Title">Control y Gestión de la planificación y producción</div>
                </div>
                <div id="bienvenida" class="menuitem">
                    <br />
                    <dx:ASPxLabel ID="lblBienvenido" runat="server" Text="BIENVENIDO" ForeColor="Black" Font-Bold="true"></dx:ASPxLabel>
                    <br />
                    <dx:ASPxLabel ID="lblNomUsuario" runat="server" Text="NombreUsuario" Font-Bold="true" ForeColor="Black" CssClass="usuario"></dx:ASPxLabel>
                </div>
                <div id="bienvenida" class="menuitem2">
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="updHora">
                        <ContentTemplate>
                            <dx:ASPxLabel ID="lblHora" runat="server" Font-Size="Smaller" Font-Bold="True" Width="90%" ToolTip="Fecha del Sistema" Height="40px" ForeColor="Black"></dx:ASPxLabel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </header>
    </form>--%>
</body>
</html>

