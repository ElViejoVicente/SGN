<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits=" SGN.Web.header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="Content/all.css" crossorigin="anonymous" />
    <script src="Scripts/jquery-latest.js"></script>
    <script src="Scripts/jquery-3.3.1.min.js"></script>

    <%--    <script src="Scripts/jquery-3.3.1.slim.min.js"></script>--%>
   <%-- <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap/js/bootstrap.min.js"></script>

    <script src="Scripts/bootstrap/js/bootstrap.js"></script>

    <script src="Scripts/bootstrap/js/bootstrap.bundle.min.js"></script>

    <script src="Scripts/bootstrap/js/bootstrap.bundle.js"></script>

    <link href="Scripts/bootstrap/css/bootstrap-grid.css" rel="stylesheet" />

    <link href="Scripts/bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />

    <link href="Scripts/bootstrap/css/bootstrap-reboot.css" rel="stylesheet" />

    <link href="Scripts/bootstrap/css/bootstrap-reboot.min.css" rel="stylesheet" />

    <link href="Scripts/bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>

    <link rel="stylesheet" type="text/css" href="Content/headers.css" />

    <%--    <link rel="stylesheet" href="Content/gpx/css/sweetalert.css" type="text/css"/>--%>


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
            width: 30px;
            height: 30px;
            font-family: monospace;
            margin: 0px;
            padding-top: 0px;
            background: #2A385D !important;
        }

        .vertical {
            transform: rotate(-90deg);
        }
    </style>
    <script type="text/javascript">


       // setHeartbeat();
    </script>

</head>
<body>
    <form id="frmCabecero" runat="server" class="clsCabecero">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <header>
            <div class="wrapper">

                <asp:HiddenField ID="HidUsuario" runat="server" ClientIDMode="Static" />

                <div style="position: absolute; width: 13%; height: 100%; display: inline-block; padding-top: 0.3%">
                    <img class="imagenGG" src="imagenes/login/LogoNotaria.svg" height="30px"  />
                    <div class="logo" style="">SGN</div>
                </div>
                <div style="position: absolute; width: 71%; height: 100%; display: inline-block; margin-left: 14%; padding-top: 3px;">
                    <div style="width: 70%; height: 100%; display: inline-block;">
                        <dx:ASPxLabel ID="lblNombrePagina" CssClass="etiquetas" runat="server" Text="" Font-Bold="false"></dx:ASPxLabel>
                    </div>
                    
                </div>
              
                <div style="position: absolute; width: 5.5%; height: 100%; display: inline-block; margin-left: 94%; padding-top: 0.0%">
                    <dx:ASPxButton runat="server" ID="btnCerrarSesion" Text="<i class='fas fa-power-off fa-2x'></i>"
                        EncodeHtml="false" OnClick="btnCerrarSesion_Click" CssClass="custom-btn clsSalir" RenderMode="Danger"
                        ToolTip="Salir SGN">
                    </dx:ASPxButton>

                </div>
                <div style="position: absolute; width: 0.5%; height: 100%; display: inline-block; margin-left: 99.5%">
                </div>




            </div>
        </header>
    </form>
</body>
</html>
