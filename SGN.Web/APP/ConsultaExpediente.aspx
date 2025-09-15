<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaExpediente.aspx.cs" Inherits="SGN.Web.APP.ConsultaExpediente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sistema de Gestión Notarial v1.5</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        body {
            /* Reducida la opacidad del gradiente para ver mejor la imagen */
            background: linear-gradient(135deg, rgba(245, 247, 250, 0.4) 0%, rgba(228, 232, 240, 0.4) 100%), url("/imagenes/login/notaria-inicioOpa90.jpg") no-repeat center center fixed;
            background-size: cover;
            color: #333;
            line-height: 1.6;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 20px;
            position: relative;
        }
        
        .logo-personalizado {
            position: absolute;
            top: 20px;
            right: 20px;
            width: 120px;
            height: 120px;
            border: 3px solid #2c3e50;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 10px rgba(0,0,0,0.2);
            background: white;
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 100;
        }
        
        .logo-personalizado img {
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }
        
        .container {
            width: 100%;
            max-width: 600px;
            background: rgba(255, 255, 255, 0.95);
            border-radius: 8px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            margin: 60px 0 20px 0;
            backdrop-filter: blur(5px);
        }
        
        .header {
            background: #2c3e50;
            color: white;
            padding: 25px;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }
        
        .header h1 {
            font-weight: 300;
            font-size: 28px;
            margin-bottom: 5px;
        }
        
        .header p {
            font-size: 16px;
            opacity: 0.9;
        }
        
        .content {
            padding: 30px;
        }
        
        .form-container {
            display: flex;
            flex-direction: column;
            gap: 25px;
        }
        
        .form-group {
            display: flex;
            flex-direction: column;
            gap: 8px;
        }
        
        .form-label {
            font-weight: 500;
            color: #2c3e50;
            font-size: 16px;
        }
        
        .input-field {
            width: 100%;
            padding: 14px 16px;
            border: 2px solid #ddd;
            border-radius: 4px;
            font-size: 16px;
            transition: border-color 0.3s;
        }
        
        .input-field:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 2px rgba(52, 152, 219, 0.2);
        }
        
        .consult-btn {
            background: #3498db;
            color: white;
            border: none;
            padding: 14px 25px;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            transition: background 0.3s;
            width: 100%;
            font-weight: 500;
            margin-top: 5px;
        }
        
        .consult-btn:hover {
            background: #2980b9;
        }
        
        .captcha-container {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 4px;
            border: 2px solid #eee;
        }
        
        .captcha-label {
            display: block;
            margin-bottom: 12px;
            font-weight: 500;
            color: #2c3e50;
            font-size: 16px;
        }
        
        .footer {
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: white;
            width: 100%;
            background: #2c3e50;
            margin-top: auto;
            border-radius: 4px;
        }
        
        /* Estilos para los controles de DevExpress */
        .dxTextBox, .dxMemo, .dxButton, .dxCaptcha {
            width: 100% !important;
            border-radius: 4px !important;
        }
        
        .dxTextBox input, .dxMemo textarea {
            padding: 14px 16px !important;
            font-size: 16px !important;
            border: 2px solid #ddd !important;
            border-radius: 4px !important;
        }
        
        .dxButton {
            background-color: #3498db !important;
            color: white !important;
            border: none !important;
            padding: 14px 25px !important;
            border-radius: 4px !important;
            transition: background-color 0.3s !important;
            font-size: 16px !important;
            font-weight: 500 !important;
        }
        
        .dxButton:hover {
            background-color: #2980b9 !important;
        }
        
        .dxCaptcha {
            border: 2px solid #eee !important;
            border-radius: 4px !important;
            padding: 20px !important;
            background: #f8f9fa !important;
            width: 100% !important;
            height: auto !important;
        }
        
        .dxCaptcha-RefreshButton {
            background: #3498db !important;
            border-radius: 4px !important;
        }
        
        .status-container {
            margin-top: 20px;
            padding: 15px;
            background: #f8f9fa;
            border-radius: 4px;
            border: 2px solid #eee;
        }
        
        .status-label {
            font-weight: 500;
            color: #2c3e50;
            margin-bottom: 8px;
            display: block;
        }
    </style>
</head>
<body>
    <!-- Logo personalizado (cambia 'mi_logo.png' por el nombre de tu imagen) -->
    <div class="logo-personalizado">
        <img src="imagenes/login/LogoNotaria.svg" alt="Logo Personalizado" onerror="this.style.display='none'; this.parentNode.innerHTML='<span style=\'color: #888; text-align: center; padding: 10px;\'>Imagen no encontrada</span>';" />
    </div>

    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Bienvenido</h1>
                <p>Sistema de Gestión Notarial v1.5</p>
            </div>
            
            <div class="content">
                <div class="form-container">
                    <!-- Campo para ingresar número de folio -->
                    <div class="form-group">
                        <label class="form-label">Número de Folio:</label>
                        <dx:ASPxTextBox runat="server" ID="txtFolioIterno" ClientInstanceName="txtFolioIterno" 
                            NullText="Introduzca el número de folio" />
                    </div>
                    
                    <!-- Captcha -->
                    <div class="form-group">
                        <label class="captcha-label">Verificación de seguridad:</label>
                        <div class="captcha-container">
                            <dx:ASPxCaptcha runat="server" ID="captcha" ClientInstanceName="captcha"
                                TextBox-Position="Bottom" Width="100%" Height="120px"
                                CharacterSet="1234567890" CodeLength="4">
                                <ValidationSettings SetFocusOnError="true"
                                    ErrorDisplayMode="Text"
                                    ValidationGroup="ValidacionCapCha" />
                            </dx:ASPxCaptcha>
                        </div>
                    </div>
                    
                    <!-- Botón de consulta -->
                    <dx:ASPxButton runat="server" ID="btnConsultar" ClientInstanceName="btnConsultar"
                        Text="Consultar" AutoPostBack="true" ClientEnabled="true" OnClick="btnConsultar_Click">
                    </dx:ASPxButton>
                    
                    <!-- Campo de estatus -->
                    <div class="status-container">
                        <label class="status-label">Estatus del folio:</label>
                        <dx:ASPxMemo runat="server" ID="txtEstatusFolio" ClientInstanceName="txtEstatusFolio" 
                            Width="100%" Rows="3" NullText="El estatus aparecerá aquí después de consultar" ReadOnly="true" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Eventos globales de cliente de DevExpress -->
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents
                ControlsInitialized="OnControlsInitialized"
                ValidationCompleted="OnValidationCompleted" />
        </dx:ASPxGlobalEvents>
    </form>
    
    <footer class="footer">
    © 2025 Derechos Reservados |
    <a href="http://www.consultoria-it.com" target="_blank">
        Consultoria IT | 55 4800 5547 | Francisco I. Madero 3A Humantla, Tlax | Sistema Orgullosamente Tlaxcalteca
    </a>
</footer>
</body>
</html>