<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="SGN.Web.Agenda.Agenda" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v25.2, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.XtraScheduler.v25.2.Core.Desktop, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraScheduler" tagprefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />

    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="../Scripts/sweetalert2.min.css" />
    <script src="../Scripts/mensajes.js"></script>

    <title>SGN - Agenda</title>

    <style type="text/css">
        #maindiv { position: relative; }
        .agenda-print-button {
            position: absolute;
            top: 10px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 20;
        }
        .agenda-report-content { padding: 8px 14px 12px; text-align: center; }
        .agenda-report-actions {
            display: flex;
            justify-content: center;
            gap: 8px;
            padding-top: 14px;
        }
    </style>

    <script type="text/javascript">
        var agendaResizeTimer = null;

        function ScheduleAdjustSize(delay) {
            window.clearTimeout(agendaResizeTimer);
            agendaResizeTimer = window.setTimeout(AdjustSize, delay || 50);
        }

        window.addEventListener('load', function () { ScheduleAdjustSize(100); });
        window.addEventListener('resize', function () { ScheduleAdjustSize(50); });

        function AdjustSize() {
            if (typeof scAgenda === 'undefined') return;

            var main = document.getElementById('maindiv');
            var height = Math.max(400, window.innerHeight || document.documentElement.clientHeight || main.clientHeight);
            scAgenda.SetHeight(height);
        }

        function OnSchedulerEndCallback(s, e) {
            ScheduleAdjustSize(50);
        }

        function BuscarExpedienteAgenda() {
            var mensaje = document.getElementById('agendaBusquedaMensaje');
            if (typeof agendaExpediente === 'undefined' || !mensaje) return;

            var expediente = (agendaExpediente.GetText() || '').trim().toUpperCase();
            agendaExpediente.SetText(expediente);
            mensaje.style.color = '#b42318';
            mensaje.textContent = '';

            if (!/^\d{4}-(0[1-9]|1[0-2])-\d+[A-Z]?$/.test(expediente)) {
                mensaje.textContent = 'Use el formato a\u00f1o-mes-consecutivo y letra opcional.';
                return;
            }

            agendaBtnBuscar.SetEnabled(false);
            agendaBusquedaCallback.PerformCallback(expediente);
        }

        function OnAgendaBusquedaComplete(s, e) {
            var mensaje = document.getElementById('agendaBusquedaMensaje');
            if (typeof agendaBtnBuscar !== 'undefined') agendaBtnBuscar.SetEnabled(true);
            if (!mensaje) return;

            try {
                var result = JSON.parse(e.result);
                if (!result || !result.Exito) {
                    mensaje.style.color = '#b42318';
                    mensaje.textContent = result && result.Mensaje ? result.Mensaje : 'No se encontr\u00f3 el expediente.';
                    return;
                }

                agendaExpediente.SetText(result.Expediente);
                agendaActo.SetText(result.Acto || '');
                agendaProyectista.SetText(result.Proyectista || '');
                agendaDescripcion.SetText(result.Descripcion || '');
                agendaValorOperacion.SetValue(result.ValorOperacion || 0);
                agendaISR.SetValue(result.ISR || 0);
                agendaNumeroEscritura.SetText(result.NumeroEscritura || '');
                agendaActividadVulnerable.SetText(result.ActividadVulnerable || '');
                mensaje.style.color = '#067647';
                mensaje.textContent = 'Expediente cargado correctamente.';
            } catch (error) {
                mensaje.style.color = '#b42318';
                mensaje.textContent = 'La respuesta de la consulta no es v\u00e1lida.';
            }
        }

        function OnAgendaBusquedaError(s, e) {
            var mensaje = document.getElementById('agendaBusquedaMensaje');
            if (typeof agendaBtnBuscar !== 'undefined') agendaBtnBuscar.SetEnabled(true);
            if (mensaje) {
                mensaje.style.color = '#b42318';
                mensaje.textContent = 'No fue posible consultar el expediente.';
            }
            e.handled = true;
        }

        function MostrarSelectorReporteAgenda() {
            cpAgendaReporte.PerformCallback();
        }

        function ImprimirAgendaSeleccionada() {
            var fecha = calAgendaReporte.GetSelectedDate();
            if (!fecha) return;

            var yyyy = fecha.getFullYear();
            var mm = ('0' + (fecha.getMonth() + 1)).slice(-2);
            var dd = ('0' + fecha.getDate()).slice(-2);
            var baseUrl = document.getElementById('hfReporteAgendaUrl').value;

            window.open(baseUrl + '?fechaAgenda=' + yyyy + '-' + mm + '-' + dd, '_blank', 'noopener');
            ppAgendaReporte.Hide();
        }

    </script>

    <script type="text/javascript">
        setInterval(function () {
            if (typeof scAgenda !== "undefined") {
                scAgenda.Refresh();
            }
        }, 120000); // cada 30 segundos
    </script>

</head>

<body>
    <form id="form1" runat="server" class="Principal">
        <asp:HiddenField ID="hfReporteAgendaUrl" runat="server" ClientIDMode="Static" />

        <dx:ASPxCallback ID="cbBuscarExpediente" runat="server"
            ClientInstanceName="agendaBusquedaCallback"
            OnCallback="cbBuscarExpediente_Callback">
            <ClientSideEvents CallbackComplete="OnAgendaBusquedaComplete"
                CallbackError="OnAgendaBusquedaError" />
        </dx:ASPxCallback>

        <section class="CLPageContent" id="maindiv">
            <div class="agenda-print-button">
                <dx:ASPxButton ID="btnMostrarReporteAgenda" runat="server" Text="Imprimir"
                    Width="130px" Height="34px" AutoPostBack="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s,e){ MostrarSelectorReporteAgenda(); }" />
                </dx:ASPxButton>
            </div>
            <dx:ASPxScheduler ID="scAgenda" runat="server" ActiveViewType="Day"
                OnAppointmentFormShowing="scAgenda_AppointmentFormShowing"
                OnPrepareAppointmentFormPopupContainer="scAgenda_PrepareAppointmentFormPopupContainer"
                OnBeforeExecuteCallbackCommand="scAgenda_BeforeExecuteCallbackCommand"
                Images-Menu-NewAppointment-AlternateText="Nueva Cita" Width="100%">
                <ClientSideEvents EndCallback="OnSchedulerEndCallback" />
                <OptionsForms AppointmentFormTemplateUrl="~/Agenda/CustomForms/AgendaAppointmentForm.ascx" />
                <Views>
                    <DayView Enabled="true" />
                    <WorkWeekView Enabled="true"  />
                    <FullWeekView Enabled="false" />
                    <WeekView Enabled="false" />
                </Views>
                

                <Storage EnableReminders="false">
                    <Appointments AutoRetrieveId="true" />
                </Storage>
            </dx:ASPxScheduler>

            <asp:ObjectDataSource ID="appointmentDataSource" runat="server"
                DataObjectTypeName="SGN.Negocio.Agenda.AgendaCitas"
                TypeName="SGN.Negocio.Agenda.AgendaCitaDataSource"
                SelectMethod="SelectMethodHandler"
                InsertMethod="InsertMethodHandler"
                UpdateMethod="UpdateMethodHandler"
                DeleteMethod="DeleteMethodHandler" />

            <asp:ObjectDataSource ID="resourceDataSource" runat="server"
                DataObjectTypeName="SGN.Negocio.Agenda.CatAgendaRecurso"
                TypeName="SGN.Negocio.Agenda.CatAgendaRecursoDataSource"
                SelectMethod="SelectMethodHandler" />

            <asp:ObjectDataSource ID="labelDataSource" runat="server"
                DataObjectTypeName="SGN.Negocio.Agenda.CatAgendaEtiqueta"
                TypeName="SGN.Negocio.Agenda.CatAgendaEtiquetaDataSource"
                SelectMethod="SelectMethodHandler" />

        </section>

        <dx:ASPxPopupControl ID="ppAgendaReporte" runat="server"
            ClientInstanceName="ppAgendaReporte" HeaderText="Imprimir agenda por d&#237;a"
            Width="350px" Modal="true" CloseAction="CloseButton" CloseOnEscape="true"
            AllowDragging="false" AllowResize="false" AutoUpdatePosition="true"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="cpAgendaReporte" runat="server"
                        ClientInstanceName="cpAgendaReporte" OnCallback="cpAgendaReporte_Callback">
                        <ClientSideEvents EndCallback="function(s,e){ ppAgendaReporte.Show(); }" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="agenda-report-content">
                                    <dx:ASPxCalendar ID="calAgendaReporte" runat="server"
                                        ClientInstanceName="calAgendaReporte" Width="100%"
                                        EnableMultiSelect="false"
                                        OnCustomDisabledDate="calAgendaReporte_CustomDisabledDate">
                                        <ClientSideEvents SelectionChanged="function(s,e){ btnAceptarReporteAgenda.SetEnabled(s.GetSelectedDate() != null); }" />
                                    </dx:ASPxCalendar>
                                    <div class="agenda-report-actions">
                                        <dx:ASPxButton ID="btnAceptarReporteAgenda" runat="server" Text="Aceptar"
                                            ClientInstanceName="btnAceptarReporteAgenda" Width="95px"
                                            AutoPostBack="false" CausesValidation="false">
                                            <ClientSideEvents Click="function(s,e){ ImprimirAgendaSeleccionada(); }" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btnCancelarReporteAgenda" runat="server" Text="Cancelar"
                                            Width="95px" AutoPostBack="false" CausesValidation="false">
                                            <ClientSideEvents Click="function(s,e){ ppAgendaReporte.Hide(); }" />
                                        </dx:ASPxButton>
                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
