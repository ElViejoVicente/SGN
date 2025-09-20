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
            color: 333;
            line-height: 1.6;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 0;
            position: relative;
        }
        
        .container {
            width: 100%;
            height: 100vh;
            background: rgba(255, 255, 255, 0.98);
            overflow: auto;
            backdrop-filter: blur(5px);
            display: flex;
            flex-direction: column;
        }
        
        .header {
            background: #2c3e50;
            color: white;
            padding: 25px;
            text-align: center;
        }
        
        .header h1 {
            font-weight: 300;
            font-size: 26px;
            margin-bottom: 8px;
        }
        
        .header p {
            font-size: 16px;
            opacity: 0.9;
        }
        
        .content {
            padding: 30px;
            flex: 1;
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
        }
        
        .form-container {
            display: flex;
            flex-direction: column;
            gap: 30px;
            max-width: 600px;
            margin: 0 auto;
            width: 100%;
            height: 100%;
        }
        
        .form-group {
            display: flex;
            flex-direction: column;
            gap: 12px;
            align-items: center;
            text-align: center;
        }
        
        .form-label {
            font-weight: 500;
            color: #2c3e50;
            font-size: 16px;
            width: 100%;
        }
        
        .input-field {
            width: 100%;
            max-width: 350px;
            padding: 14px 16px;
            border: 2px solid #ddd;
            border-radius: 4px;
            font-size: 16px;
            transition: border-color 0.3s;
            text-align: center;
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
            max-width: 350px;
            font-weight: 500;
            margin: 100px auto 0;
            display: block;
        }
        
        .consult-btn:hover {
            background: #2980b9;
        }
        
        /* CAPTCHA MEJORADO - CENTRADO PERFECTO */
        .captcha-container {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 4px;
            border: 2px solid #eee;
            margin: -10px auto 0;
            text-align: center;
            max-width: 350px;
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
        }
        
        .captcha-label {
            display: block;
            margin-bottom: 15px;
            font-weight: 500;
            color: #2c3e50;
            font-size: 16px;
            width: 100%;
            text-align: center;
        }
        
        .footer {
            text-align: center;
            padding: 18px;
            font-size: 12px;
            color: white;
            width: 100%;
            background: #2c3e50;
        }
        
        .footer a {
            color: #fff;
            text-decoration: none;
        }
        
        .footer a:hover {
            text-decoration: underline;
        }
        
        /* ESTILOS MEJORADOS PARA EL CAPTCHA - CENTRADO PERFECTO */
        .dxCaptcha {
            border: 2px solid #eee !important;
            border-radius: 4px !important;
            padding: 15px !important;
            background: #f8f9fa !important;
            width: 100% !important;
            height: auto !important;
            max-width: 350px !important;
            margin: 0 auto 0 !important;
            display: flex !important;
            flex-direction: column !important;
            align-items: center !important;
            justify-content: center !important;
        }
        
        /* Contenedor principal del captcha */
        .dxCaptcha > div {
            display: flex !important;
            flex-direction: column !important;
            align-items: center !important;
            justify-content: center !important;
            width: 100% !important;
            text-align: center !important;
        }
        
        /* Imagen del captcha perfectamente centrada */
        .dxCaptcha .dxCaptcha-image {
            height: 60px !important;
            margin-bottom: 20px !important;
            display: block !important;
            margin-left: auto !important;
            margin-right: auto !important;
        }
        
        /* Campo de texto del captcha centrado */
        .dxCaptcha .dx-texteditor {
            margin-bottom: 20px !important;
            width: 250px !important; /* Ancho fijo para mejor centrado */
            margin-left: auto !important;
            margin-right: auto !important;
        }
        
        .dxCaptcha .dx-texteditor-container {
            display: flex !important;
            justify-content: center !important;
            width: 100% !important;
        }
        
        .dxCaptcha .dx-texteditor-input {
            text-align: center !important;
            width: 100% !important;
        }
        
        /* Botón de refresco perfectamente centrado */
        .dxCaptcha .dxCaptcha-refresh-button {
            padding: 8px 15px !important;
            font-size: 14px !important;
            margin-top: 15px !important;
            display: block !important;
            width: auto !important;
            margin-left: auto !important;
            margin-right: auto !important;
        }
        
        .dxCaptcha-RefreshButton {
            background: #3498db !important;
            border-radius: 4px !important;
            padding: 8px 15px !important;
        }
        
        /* Estilos para los demás controles de DevExpress (sin cambios) */
        .dxTextBox, .dxMemo, .dxButton {
            width: 100% !important;
            border-radius: 4px !important;
        }
        
        .dxTextBox {
            max-width: 350px !important;
            margin: 0 auto !important;
        }
        
        .dxTextBox input, .dxMemo textarea {
            padding: 14px 16px !important;
            font-size: 16px !important;
            border: 2px solid #ddd !important;
            border-radius: 4px !important;
            text-align: center !important;
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
            max-width: 350px !important;
            margin: 100px auto 0 !important;
            display: block !important;
        }
        
        .dxButton:hover {
            background-color: #2980b9 !important;
        }
        
        .status-container {
            margin-top: 25px;
            padding: 20px;
            background: #f8f9fa;
            border-radius: 4px;
            border: 2px solid #eee;
            min-height: 300px;
            flex: 1;
            display: flex;
            flex-direction: column;
            align-items: center;
            text-align: center;
        }
        
        .status-label {
            font-weight: 500;
            color: #2c3e50;
            margin-bottom: 15px;
            display: block;
            font-size: 16px;
            text-align: center;
            width: 100%;
        }
        
        .dxMemo {
            min-height: 250px !important;
            text-align: center !important;
            flex: 1;
            resize: none !important;
            width: 100% !important;
            margin: 0 auto !important;
        }
        
        /* Contenedor para el botón de consultar debajo del captcha */
        .captcha-button-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            max-width: 350px;
            margin: 60px auto 0;
            width: 100%;
            text-align: center;
        }
        
        /* Media queries para responsividad */
        @media (max-width: 768px) {
            .header {
                padding: 20px;
            }
            
            .header h1 {
                font-size: 22px;
            }
            
            .header p {
                font-size: 14px;
            }
            
            .content {
                padding: 20px;
            }
            
            .form-container {
                gap: 25px;
            }
            
            .footer {
                font-size: 11px;
                padding: 15px;
            }
            
            .status-container {
                min-height: 250px;
                padding: 15px;
            }
            
            .dxMemo {
                min-height: 200px !important;
            }
            
            .input-field, .dxTextBox {
                max-width: 300px !important;
            }
            
            .consult-btn, .dxButton {
                max-width: 300px !important;
                margin-top: 90px !important;
            }
            
            .captcha-container, .dxCaptcha, .captcha-button-container {
                max-width: 300px !important;
            }
            
            .dxCaptcha .dxCaptcha-image {
                margin-bottom: 15px !important;
            }
            
            .dxCaptcha .dx-texteditor {
                margin-bottom: 15px !important;
                width: 220px !important;
            }
            
            .dxCaptcha .dxCaptcha-refresh-button {
                margin-top: 12px !important;
            }
            
            .captcha-button-container {
                margin-top: 50px !important;
            }
            
            .captcha-container {
                margin: -8px auto 0;
            }
        }
        
        @media (max-width: 480px) {
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
                padding: 15px;
            }
            
            .input-field, .dxTextBox input, .dxMemo textarea {
                padding: 12px 14px !important;
                font-size: 15px !important;
            }
            
            .consult-btn, .dxButton {
                padding: 12px 20px !important;
                font-size: 15px !important;
                margin-top: 80px !important;
            }
            
            .captcha-container {
                padding: 15px;
                margin: -8px auto 0;
            }
            
            .status-container {
                min-height: 200px;
                padding: 15px;
                margin-top: 20px;
            }
            
            .dxMemo {
                min-height: 150px !important;
            }
            
            .input-field, .dxTextBox {
                max-width: 100% !important;
            }
            
            .consult-btn, .dxButton {
                max-width: 100% !important;
            }
            
            .captcha-container, .dxCaptcha, .captcha-button-container {
                max-width: 100% !important;
            }
            
            .captcha-button-container {
                margin-top: 60px !important;
            }
            
            .dxCaptcha .dx-texteditor {
                width: 200px !important;
            }
        }
    </style>
</head>
<body>
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
                                TextBox-Position="Bottom" Width="100%" Height="90px"
                                CharacterSet="1234567890" CodeLength="4">
                                <ValidationSettings SetFocusOnError="true"
                                    ErrorDisplayMode="Text"
                                    ValidationGroup="ValidacionCapCha" />
                                <RefreshButton Text="Mostrar otro código" />
                            </dx:ASPxCaptcha>
                        </div>
                        
                        <!-- Botón de consulta debajo del captcha -->
                        <div class="captcha-button-container">
                            <dx:ASPxButton runat="server" ID="btnConsultar" ClientInstanceName="btnConsultar"
                                Text="Consultar" AutoPostBack="true" ClientEnabled="true" OnClick="btnConsultar_Click">
                            </dx:ASPxButton>
                        </div>
                    </div>
                    
                    <!-- Campo de estatus -->
                    <div class="status-container">
                        <label class="status-label">Estatus del folio:</label>
                        <dx:ASPxMemo runat="server" ID="txtEstatusFolio" ClientInstanceName="txtEstatusFolio" 
                            Width="100%" Rows="15" NullText="Necesita pasar la verificación de seguridad para consultar su estatus" ReadOnly="true" />
                    </div>
                </div>
            </div>

            <footer class="footer">
                © 2025 Derechos Reservados |
                <a href="http://www.consultoria-it.com" target="_blank">
                    Consultoria IT | 55 4800 5547 | Francisco I. Madero 3A Humantla, Tlax | Sistema Orgullosamente Tlaxcalteca
                </a>
            </footer>
        </div>

        <!-- Eventos globales de cliente de DevExpress -->
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents
                ControlsInitialized="OnControlsInitialized"
                ValidationCompleted="OnValidationCompleted" />
        </dx:ASPxGlobalEvents>
    </form>
</body>
</html>