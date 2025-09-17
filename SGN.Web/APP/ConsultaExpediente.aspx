<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaExpediente.aspx.cs" Inherits="SGN.Web.APP.ConsultaExpediente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sistema de Gestión Notarial v1.5</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        body {
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
            width: 100px;
            height: 100px;
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
            max-width: 700px;
            background: rgba(255, 255, 255, 0.95);
            border-radius: 8px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            margin: 60px 0 80px 0;
            backdrop-filter: blur(5px);
        }
        
        .header {
            background: #2c3e50;
            color: white;
            padding: 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }
        
        .header h1 {
            font-weight: 300;
            font-size: 24px;
            margin-bottom: 5px;
        }
        
        .header p {
            font-size: 14px;
            opacity: 0.9;
        }
        
        .content {
            padding: 25px;
        }
        
        .form-container {
            display: flex;
            flex-direction: column;
            gap: 20px;
        }
        
        .form-group {
            display: flex;
            flex-direction: column;
            gap: 8px;
            align-items: center; /* Centrado horizontal */
            text-align: center; /* Centrado del texto */
        }
        
        .form-label {
            font-weight: 500;
            color: #2c3e50;
            font-size: 15px;
            width: 100%;
        }
        
        .input-field {
            width: 100%;
            max-width: 300px; /* Ancho máximo para centrado estético */
            padding: 12px 14px;
            border: 2px solid #ddd;
            border-radius: 4px;
            font-size: 15px;
            transition: border-color 0.3s;
            text-align: center; /* Centrado del texto dentro del input */
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
            padding: 12px 20px;
            border-radius: 4px;
            font-size: 15px;
            cursor: pointer;
            transition: background 0.3s;
            width: 100%;
            max-width: 300px; /* Mismo ancho que el input para consistencia */
            font-weight: 500;
            margin: 5px auto 0; /* Centrado horizontal */
            display: block; /* Necesario para que el margin auto funcione */
        }
        
        .consult-btn:hover {
            background: #2980b9;
        }
        
        .captcha-container {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 4px;
            border: 2px solid #eee;
            margin-bottom: 10px;
            text-align: center; /* Centrado del captcha */
        }
        
        .captcha-label {
            display: block;
            margin-bottom: 10px;
            font-weight: 500;
            color: #2c3e50;
            font-size: 15px;
        }
        
        .footer {
            text-align: center;
            padding: 15px;
            font-size: 12px;
            color: white;
            width: 100%;
            max-width: 700px;
            background: #2c3e50;
            border-radius: 4px;
            position: fixed;
            bottom: 20px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 99;
        }
        
        .footer a {
            color: #fff;
            text-decoration: none;
        }
        
        .footer a:hover {
            text-decoration: underline;
        }
        
        /* Estilos para los controles de DevExpress */
        .dxTextBox, .dxMemo, .dxButton, .dxCaptcha {
            width: 100% !important;
            border-radius: 4px !important;
        }
        
        .dxTextBox {
            max-width: 300px !important; /* Mismo ancho máximo que el input normal */
            margin: 0 auto !important; /* Centrado horizontal */
        }
        
        .dxTextBox input, .dxMemo textarea {
            padding: 12px 14px !important;
            font-size: 15px !important;
            border: 2px solid #ddd !important;
            border-radius: 4px !important;
            text-align: center !important; /* Centrado del texto dentro del input */
        }
        
        .dxButton {
            background-color: #3498db !important;
            color: white !important;
            border: none !important;
            padding: 12px 20px !important;
            border-radius: 4px !important;
            transition: background-color 0.3s !important;
            font-size: 15px !important;
            font-weight: 500 !important;
            max-width: 300px !important; /* Mismo ancho que el input para consistencia */
            margin: 5px auto 0 !important; /* Centrado horizontal */
            display: block !important; /* Necesario para que el margin auto funcione */
        }
        
        .dxButton:hover {
            background-color: #2980b9 !important;
        }
        
        .dxCaptcha {
            border: 2px solid #eee !important;
            border-radius: 4px !important;
            padding: 15px !important;
            background: #f8f9fa !important;
            width: 100% !important;
            height: auto !important;
            max-width: 330px !important; /* Ancho máximo para centrado */
            margin: 0 auto !important; /* Centrado horizontal */
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
            min-height: 180px;
        }
        
        .status-label {
            font-weight: 500;
            color: #2c3e50;
            margin-bottom: 8px;
            display: block;
            font-size: 15px;
            text-align: center; /* Centrado del texto de estatus */
        }
        
        .dxMemo {
            min-height: 150px !important;
            text-align: center !important; /* Centrado del texto dentro del memo */
        }
        
        /* Media queries para responsividad */
        @media (max-width: 768px) {
            .logo-personalizado {
                width: 80px;
                height: 80px;
                top: 10px;
                right: 10px;
            }
            
            .container {
                margin: 50px 0 90px 0;
            }
            
            .header {
                padding: 15px;
            }
            
            .header h1 {
                font-size: 20px;
            }
            
            .header p {
                font-size: 13px;
            }
            
            .content {
                padding: 20px;
            }
            
            .form-container {
                gap: 15px;
            }
            
            .footer {
                font-size: 11px;
                padding: 12px;
                bottom: 15px;
            }
            
            .status-container {
                min-height: 160px;
            }
            
            .dxMemo {
                min-height: 130px !important;
            }
            
            .input-field, .dxTextBox {
                max-width: 250px !important;
            }
            
            .consult-btn, .dxButton {
                max-width: 250px !important;
            }
            
            .dxCaptcha {
                max-width: 280px !important;
            }
        }
        
        @media (max-width: 480px) {
            body {
                padding: 10px;
            }
            
            .logo-personalizado {
                position: relative;
                margin: 0 auto 15px auto;
                top: 0;
                right: 0;
            }
            
            .container {
                margin: 0 0 90px 0;
            }
            
            .header h1 {
                font-size: 18px;
            }
            
            .input-field, .dxTextBox input, .dxMemo textarea {
                padding: 10px 12px !important;
                font-size: 14px !important;
            }
            
            .consult-btn, .dxButton {
                padding: 10px 15px !important;
                font-size: 14px !important;
            }
            
            .captcha-container {
                padding: 12px;
            }
            
            .footer {
                position: relative;
                bottom: 0;
                margin-top: 20px;
                transform: none;
                left: 0;
                width: 100%;
            }
            
            .status-container {
                min-height: 140px;
            }
            
            .dxMemo {
                min-height: 120px !important;
            }
            
            .input-field, .dxTextBox {
                max-width: 100% !important;
            }
            
            .consult-btn, .dxButton {
                max-width: 100% !important;
            }
            
            .dxCaptcha {
                max-width: 100% !important;
            }
        }
    </style>
</head>
<body>
    <!-- Logo personalizado -->
    <div class="logo-personalizado">
        <img src="imagenes/login/LogoNotaria.svg" alt="Logo Personalizado" onerror="this.style.display='none'; this.parentNode.innerHTML='<span style=\'color: #888; text-align: center; padding: 10px;\'>Imagen no encontrada</span>';" />
    </div>

    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Notaria 01 Huamantla</h1>
                <p>Consulta de Folio</p>
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
                            Width="100%" Rows="8" NullText="Nesecita pasar la verificacion de seguridad para consultar su estatus" ReadOnly="true" />
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
