<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgendaAppointmentForm.ascx.cs" Inherits="SGN.Web.Agenda.CustomForms.AgendaAppointmentForm" %>
<%@ Register Assembly="DevExpress.Web.v25.2, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v25.2, Version=25.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>

<style type="text/css">
    .sgn-agenda-form {
        width: 100%;
        table-layout: fixed;
        border-collapse: separate;
        border-spacing: 0 8px;
        padding: 8px 14px 2px;
        box-sizing: border-box;
    }
    .sgn-agenda-form .label {
        white-space: nowrap;
        text-align: right;
        vertical-align: middle;
        padding: 0 10px 0 0;
    }
    .sgn-agenda-form .field {
        min-width: 0;
        vertical-align: middle;
        padding: 0 10px 0 0;
    }
    .sgn-agenda-form .field-last { padding-right: 0; }
    .sgn-agenda-form .search {
        display: grid;
        grid-template-columns: minmax(180px, 280px) 80px;
        gap: 10px;
        align-items: center;
    }
    .sgn-agenda-form .message-cell { padding: 0 0 0 140px; }
    .sgn-agenda-form .message {
        min-height: 18px;
        line-height: 18px;
        color: #b42318;
    }
    .sgn-agenda-form .description-label {
        vertical-align: top;
        padding-top: 5px;
    }
    .sgn-recurrence { padding: 2px 14px 8px 164px; }
    .sgn-form-actions {
        display: flex;
        justify-content: center;
        gap: 6px;
        padding: 4px 0 10px;
    }
</style>

<div runat="server" id="ValidationContainer">
    <table class="sgn-agenda-form">
        <colgroup>
            <col style="width:140px;" />
            <col />
            <col style="width:140px;" />
            <col />
        </colgroup>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblExpediente" runat="server" Text="Expediente:" AssociatedControlID="tbExpediente" /></td>
            <td colspan="3" class="field field-last">
                <div class="search">
                    <dx:ASPxTextBox ID="tbExpediente" runat="server" Width="100%" ClientInstanceName="agendaExpediente"
                        Text='<%# ((SGN.Web.Agenda.CustomForms.AgendaAppointmentFormTemplateContainer)Container).IdExpediente %>' MaxLength="50">
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="true" ErrorText="Capture y busque un expediente." />
                            <RegularExpression ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-\d+[A-Za-z]?$" ErrorText="Use el formato a&#241;o-mes-consecutivo y letra opcional." />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                    <dx:ASPxButton ID="btnBuscarExpediente" runat="server" Text="Buscar" Width="80px" AutoPostBack="false" CausesValidation="false" ClientInstanceName="agendaBtnBuscar">
                        <ClientSideEvents Click="function(s,e){ BuscarExpedienteAgenda(); }" />
                    </dx:ASPxButton>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="message-cell"><div id="agendaBusquedaMensaje" class="message"></div></td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblSubject" runat="server" Text="Acto:" AssociatedControlID="tbSubject" /></td>
            <td colspan="3" class="field field-last">
                <dx:ASPxTextBox ID="tbSubject" runat="server" Width="100%" ClientInstanceName="agendaActo"
                    Text='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Subject %>' MaxLength="200">
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"><RequiredField IsRequired="true" ErrorText="El acto es obligatorio." /></ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblLocation" runat="server" Text="Proyectista:" AssociatedControlID="tbLocation" /></td>
            <td class="field"><dx:ASPxTextBox ID="tbLocation" runat="server" Width="100%" ClientInstanceName="agendaProyectista" Text='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Appointment.Location %>' /></td>
            <td class="label"><dx:ASPxLabel ID="lblLabel" runat="server" Text="Etiqueta:" AssociatedControlID="edtLabel" /></td>
            <td class="field field-last"><dx:ASPxComboBox ID="edtLabel" runat="server" Width="100%" DataSource='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).LabelDataSource %>' /></td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblValorOperacion" runat="server" Text="Valor operaci&#243;n:" AssociatedControlID="tbValorOperacion" /></td>
            <td class="field">
                <dx:ASPxSpinEdit ID="tbValorOperacion" runat="server" Width="100%" ClientInstanceName="agendaValorOperacion"
                    NumberType="Float" DecimalPlaces="2" DisplayFormatString="N2" MinValue="0" AllowNull="false"
                    Value='<%# ((SGN.Web.Agenda.CustomForms.AgendaAppointmentFormTemplateContainer)Container).ValorOperacion %>' />
            </td>
            <td class="label"><dx:ASPxLabel ID="lblISR" runat="server" Text="ISR:" AssociatedControlID="tbISR" /></td>
            <td class="field field-last">
                <dx:ASPxSpinEdit ID="tbISR" runat="server" Width="100%" ClientInstanceName="agendaISR"
                    NumberType="Float" DecimalPlaces="2" DisplayFormatString="N2" MinValue="0" AllowNull="false"
                    Value='<%# ((SGN.Web.Agenda.CustomForms.AgendaAppointmentFormTemplateContainer)Container).ISR %>' />
            </td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblNumeroEscritura" runat="server" Text="N&#176; escritura:" AssociatedControlID="tbNumeroEscritura" /></td>
            <td class="field">
                <dx:ASPxTextBox ID="tbNumeroEscritura" runat="server" Width="100%" MaxLength="250"
                    ClientInstanceName="agendaNumeroEscritura"
                    Text='<%# ((SGN.Web.Agenda.CustomForms.AgendaAppointmentFormTemplateContainer)Container).NumeroEscritura %>' />
            </td>
            <td class="label"><dx:ASPxLabel ID="lblActividadVulnerable" runat="server" Text="Actividad vulnerable:" AssociatedControlID="tbActividadVulnerable" /></td>
            <td class="field field-last">
                <dx:ASPxTextBox ID="tbActividadVulnerable" runat="server" Width="100%" MaxLength="250"
                    ClientInstanceName="agendaActividadVulnerable"
                    Text='<%# ((SGN.Web.Agenda.CustomForms.AgendaAppointmentFormTemplateContainer)Container).ActividadVulnerable %>' />
            </td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblStartDate" runat="server" Text="Hora de inicio:" AssociatedControlID="edtStartDate" /></td>
            <td class="field">
                <dx:ASPxDateEdit ID="edtStartDate" runat="server" Width="100%" ClientInstanceName="agendaInicio"
                    Date='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Start %>'
                    DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" AllowNull="false">
                    <TimeSectionProperties Visible="true">
                        <TimeEditProperties DisplayFormatString="HH:mm" EditFormatString="HH:mm" />
                    </TimeSectionProperties>
                    <ValidationSettings EnableCustomValidation="true" ErrorDisplayMode="ImageWithTooltip"><RequiredField IsRequired="true" ErrorText="La fecha inicial es obligatoria." /></ValidationSettings>
                    <ClientSideEvents DateChanged="function(s, e) {
                        var inicio = s.GetDate();
                        if (!inicio || typeof agendaFin === 'undefined') return;
                        agendaFin.SetDate(new Date(inicio.getTime() + (60 * 60 * 1000)));
                        agendaFin.SetIsValid(true);
                    }" Validation="function(s, e) {
                        if (!e.isValid) return;
                        var inicio = agendaInicio.GetDate();
                        var fin = agendaFin.GetDate();
                        e.isValid = !inicio || !fin || inicio &lt; fin;
                        e.errorText = 'La hora de inicio debe ser anterior a la hora final.';
                    }" />
                </dx:ASPxDateEdit>
            </td>
            <td class="label"><dx:ASPxLabel ID="lblEndDate" runat="server" Text="Hora finalizaci&#243;n:" AssociatedControlID="edtEndDate" /></td>
            <td class="field field-last">
                <dx:ASPxDateEdit ID="edtEndDate" runat="server" Width="100%" ClientInstanceName="agendaFin"
                    Date='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).End %>'
                    DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" AllowNull="false">
                    <TimeSectionProperties Visible="true">
                        <TimeEditProperties DisplayFormatString="HH:mm" EditFormatString="HH:mm" />
                    </TimeSectionProperties>
                    <ValidationSettings EnableCustomValidation="true" ErrorDisplayMode="ImageWithTooltip"><RequiredField IsRequired="true" ErrorText="La fecha final es obligatoria." /></ValidationSettings>
                    <ClientSideEvents Validation="function(s, e) {
                        if (!e.isValid) return;
                        var inicio = agendaInicio.GetDate();
                        var fin = agendaFin.GetDate();
                        e.isValid = !inicio || !fin || inicio &lt; fin;
                        e.errorText = 'La hora de inicio debe ser anterior a la hora final.';
                    }" />
                </dx:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblStatus" runat="server" Text="Mostrar hora como:" AssociatedControlID="edtStatus" /></td>
            <td class="field"><dx:ASPxComboBox ID="edtStatus" runat="server" Width="100%" DataSource='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).StatusDataSource %>' /></td>
            <td colspan="2"><dx:ASPxCheckBox ID="chkAllDay" runat="server" Text="Acontecimiento de todo el d&#237;a" Checked='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Appointment.AllDay %>' /></td>
        </tr>
        <tr>
            <td class="label"><dx:ASPxLabel ID="lblResource" runat="server" Text="Lugar:" AssociatedControlID="edtResource" /></td>
            <td colspan="3" class="field field-last">
                <dx:ASPxComboBox ID="edtResource" runat="server" Width="100%" DataSource='<%# ResourceDataSource %>' Enabled='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).CanEditResource %>'>
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"><RequiredField IsRequired="true" ErrorText="Seleccione un lugar." /></ValidationSettings>
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="label description-label"><dx:ASPxLabel ID="lblDescription" runat="server" Text="Descripci&#243;n:" AssociatedControlID="tbDescription" /></td>
            <td colspan="3" class="field field-last"><dx:ASPxMemo ID="tbDescription" runat="server" Width="100%" Rows="6" ClientInstanceName="agendaDescripcion" Text='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Appointment.Description %>' /></td>
        </tr>
    </table>

</div>

<div class="sgn-recurrence">
    <dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
        IsRecurring='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Appointment.IsRecurring %>'
        DayNumber='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceDayNumber %>'
        End='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceEnd %>'
        Month='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceMonth %>'
        OccurrenceCount='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceOccurrenceCount %>'
        Periodicity='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrencePeriodicity %>'
        RecurrenceRange='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceRange %>'
        Start='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceStart %>'
        WeekDays='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceWeekDays %>'
        WeekOfMonth='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceWeekOfMonth %>'
        RecurrenceType='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceType %>'
        IsFormRecreated='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).IsFormRecreated %>' />
</div>

<div class="sgn-form-actions">
    <dx:ASPxButton runat="server" ID="btnOk" Text="Aceptar" UseSubmitBehavior="false" AutoPostBack="false" Width="98px" />
    <dx:ASPxButton runat="server" ID="btnCancel" Text="Cancelar" UseSubmitBehavior="false" AutoPostBack="false" Width="98px" CausesValidation="false" />
    <dx:ASPxButton runat="server" ID="btnDelete" Text="Eliminar" UseSubmitBehavior="false" AutoPostBack="false" Width="98px" CausesValidation="false" Enabled='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).CanDeleteAppointment %>' />
</div>
<dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1" MasterControlId='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).ControlId %>' />
