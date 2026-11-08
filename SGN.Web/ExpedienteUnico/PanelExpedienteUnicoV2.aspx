<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedienteUnicoV2.aspx.cs" Inherits="SGN.Web.ExpedienteUnico.PanelExpedienteUnicoV2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="../Scripts/sweetalert2.min.css" />
    <link rel="stylesheet" href="PanelExpedienteUnicoV2.aspx.css" />
    <script src="../Scripts/mensajes.js"></script>
    <title>Expediente único</title>
    <script type="text/javascript">
        window.onresize = function () { adjustGridHeight(); };
        function adjustGridHeight() {
            var content = document.getElementById('expContent');
            if (content && window.gvExpedienteUnico) {
                gvExpedienteUnico.SetHeight(Math.max(240, content.clientHeight - 2));
            }
        }
        function gridView_EndCallback(s, e) {
            adjustGridHeight();
            if (s.cp_KpiExpedientes !== undefined) {
                lblKpiExpedientes.SetText(s.cp_KpiExpedientes);
                lblKpiSinAnexo.SetText(s.cp_KpiSinAnexo);
                lblKpiConAnexo.SetText(s.cp_KpiConAnexo);
                lblKpiCumplimiento.SetText(s.cp_KpiCumplimiento);
            }
            if (s.cp_swMsg) { mostrarMensajeSweet(s.cp_swType, s.cp_swMsg); s.cp_swType = null; s.cp_swMsg = null; }
        }
        function abrirExpediente(idRegistro) {
            ppExpediente.Show();
            cpExpediente.PerformCallback('Abrir|' + idRegistro);
        }
        function expediente_EndCallback(s, e) {
            if (s.cp_Error) { mostrarMensajeSweet('error', s.cp_Error); s.cp_Error = null; }
            if (s.cp_Guardado) {
                ppExpediente.Hide();
                mostrarMensajeSweet('success', s.cp_Guardado);
                s.cp_Guardado = null;
                gvExpedienteUnico.PerformCallback('CargarRegistros');
            }
        }
    </script>
</head>
<body>
    <form id="frmPage" runat="server" class="Principal">
        <div class="exp-page">
            <div class="exp-filter">
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Opciones de consulta:" View="GroupBox" CssClass="exp-filter-options">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="exp-filter-row">
                                <dx:ASPxDateEdit Caption="Inicio" runat="server" ID="dtFechaInicio" ClientInstanceName="dtFechaInicio" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="125px" />
                                <dx:ASPxDateEdit Caption="Fin" runat="server" ID="dtFechaFin" ClientInstanceName="dtFechaFin" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="125px" />
                                <dx:ASPxButton ID="btnActualizar" runat="server" Text="Actualizar" AutoPostBack="false">
                                    <Image IconID="xaf_action_reload_svg_16x16" />
                                    <ClientSideEvents Click="function(s,e){gvExpedienteUnico.PerformCallback('CargarRegistros');}" />
                                </dx:ASPxButton>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="KPI:" View="GroupBox" CssClass="exp-filter-kpi">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="exp-kpi-content">
                                <div class="kpi-item">
                                    <span class="kpi-title">Expedientes</span><dx:ASPxLabel ID="lblKpiExpedientes" ClientInstanceName="lblKpiExpedientes" runat="server" Text="0" CssClass="kpi-value" />
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-warning">
                                    <span class="kpi-title">Sin Expediente</span><dx:ASPxLabel ID="lblKpiSinAnexo" ClientInstanceName="lblKpiSinAnexo" runat="server" Text="0" CssClass="kpi-value" />
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-success">
                                    <span class="kpi-title">Con Expediente</span><dx:ASPxLabel ID="lblKpiConAnexo" ClientInstanceName="lblKpiConAnexo" runat="server" Text="0" CssClass="kpi-value" />
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-compliance">
                                    <span class="kpi-title">Cumplimiento</span><dx:ASPxLabel ID="lblKpiCumplimiento" ClientInstanceName="lblKpiCumplimiento" runat="server" Text="0%" CssClass="kpi-value" />
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </div>
            <div id="expContent" class="exp-content">
                <dx:ASPxGridView runat="server" ID="gvExpedienteUnico" ClientInstanceName="gvExpedienteUnico" AutoGenerateColumns="False" Width="100%" KeyFieldName="IdRegistro" CssClass="exp-grid" OnDataBinding="gvExpedienteUnico_DataBinding" OnCustomCallback="gvExpedienteUnico_CustomCallback" OnToolbarItemClick="gvExpedienteUnico_ToolbarItemClick" OnHtmlDataCellPrepared="gvExpedienteUnico_HtmlDataCellPrepared">
                    <ClientSideEvents Init="function(s,e){adjustGridHeight();}" EndCallback="gridView_EndCallback" />
                    <Settings ShowColumnHeaders="False" ShowGroupPanel="False" ShowFilterRow="False" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Hidden" />
                    <SettingsPager Mode="ShowPager" PageSize="50" Position="Bottom" />
                    <SettingsBehavior AutoExpandAllGroups="true" AllowGroup="false" AllowSort="false" AllowFocusedRow="false" EnableRowHotTrack="false" />
                    <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="false" />
                    <SettingsText EmptyDataRow="No se encontraron expedientes para el periodo seleccionado." />
                    <Templates>
                        <GroupRowContent>
                            <div class="exp-group-header">
                                <i class="fas fa-chevron-down exp-group-icon"></i>
                                <dx:ASPxLabel runat="server" CssClass="exp-group-title" Text='<%# GetTextoEncabezado(Container.GroupText) %>' />
                            </div>
                        </GroupRowContent>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Resumen" GroupIndex="0" VisibleIndex="0" />
                        <dx:GridViewDataTextColumn FieldName="Nombres" VisibleIndex="1">
                            <DataItemTemplate>
                                <div class="person-card">
                                    <div>
                                        <dx:ASPxLabel runat="server" CssClass="person-name" Text='<%# GetNombreCompleto(Eval("Nombres"),Eval("ApellidoPaterno"),Eval("ApellidoMaterno")) %>' />
                                        <dx:ASPxLabel runat="server" CssClass="person-subtitle" Text='<%# GetDescripcionParticipante(Eval("RolOperacion"),Eval("Sexo"),Eval("Ocupacion")) %>' />
                                        <asp:Panel runat="server" CssClass="person-warning" Visible='<%# !TieneExpediente(Eval("IdExpedienteUnico")) %>'><i class="fas fa-exclamation-triangle"></i>&nbsp;Sin expediente generado</asp:Panel>
                                    </div>
                                    <div class="person-meta" runat="server" visible='<%# TieneExpediente(Eval("IdExpedienteUnico")) %>'>
                                        <span class="meta-item"><i class="fas fa-file-alt meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# GetAnexo(Eval("NombreDelAnexo"),Eval("IdExpedienteUnico")) %>' />
                                        </span>
                                        <span class="meta-item"><i class="fas fa-calendar-alt meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# FormatFecha(Eval("FechaCreacionVersion")) %>' />
                                        </span>
                                        <%--                    <span class="meta-item"><i class="fas fa-clock meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# FormatFecha(Eval("FechaUltimaModificacion")) %>' />
                                        </span>--%>
                                        <span class="meta-item"><i class="fas fa-user meta-icon"></i>
                                            <dx:ASPxLabel runat="server" Text='<%# Eval("UsuarioAutor") %>' />
                                        </span>
                                    </div>
                                    <div class="person-actions">
                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            Text='<%# TieneExpediente(Eval("IdExpedienteUnico")) ? "Abrir expediente" : "Generar expediente" %>'
                                            data-id-registro='<%# Eval("IdRegistro") %>'>
                                            <ClientSideEvents Click="function(s,e){abrirExpediente(s.GetMainElement().getAttribute('data-id-registro'));}" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton runat="server" AutoPostBack="false" Text="Generar PDF" />
                                    </div>
                                </div>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>

        <dx:ASPxPopupControl ID="ppExpediente" runat="server" ClientInstanceName="ppExpediente"
            HeaderText="Generar Expediente Individual" Modal="true" CloseAction="CloseButton"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="1100px"
            AllowDragging="true" CssClass="exp-modal">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="cpExpediente" runat="server" ClientInstanceName="cpExpediente"
                        OnCallback="cpExpediente_Callback">
                        <ClientSideEvents EndCallback="expediente_EndCallback" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="modal-subtitle">Validación previa de la información del participante</div>
                                <div class="modal-notice"><i class="fas fa-info-circle"></i> Se generará un expediente individual para el participante seleccionado. Todos los cambios son registrados con tu usuario !, Te recomiendo concentracion.</div>

                                <dx:ASPxPageControl ID="pcExpediente" runat="server" ClientInstanceName="pcExpediente" Width="100%" ActiveTabIndex="0" CssClass="exp-tabs">
                                    <TabPages>
                                        <dx:TabPage Text="Persona" Name="Persona" TabImage-IconID="people_user_16x16">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <div class="modal-section-title">Participante</div>
                                                    <div class="person-modal-name">
                                                        <dx:ASPxLabel ID="lblPersonaNombre" runat="server" />
                                                    </div>
                                                    <div class="modal-fields modal-fields-3">
                                                        <div class="modal-field">
                                                            <span>Rol</span><dx:ASPxTextBox ID="txtPersonaRol" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Sexo</span><dx:ASPxTextBox ID="txtPersonaSexo" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Edad</span><dx:ASPxTextBox ID="txtPersonaEdad" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Fecha de nacimiento</span><dx:ASPxTextBox ID="txtPersonaFechaNacimiento" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field modal-field-span-2">
                                                            <span>Actividad / Ocupación</span><dx:ASPxTextBox ID="txtPersonaOcupacion" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                    </div>
                                                    <div class="modal-section-title modal-section-space">Operación relacionada</div>
                                                    <div class="modal-fields modal-fields-3">
                                                        <div class="modal-field modal-field-span-3">
                                                            <span>Operación</span><dx:ASPxTextBox ID="txtOperacion" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Expediente</span><dx:ASPxTextBox ID="txtNumeroExpediente" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Estado</span><dx:ASPxTextBox ID="txtEstado" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                        <div class="modal-field">
                                                            <span>Usuario responsable</span><dx:ASPxTextBox ID="txtUsuarioResponsable" runat="server" ReadOnly="true" Width="100%" />
                                                        </div>
                                                    </div>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Anexo" Name="Anexo" TabImage-IconID="support_article_16x16">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <div class="anexo-layout">
                                                        <div class="anexo-fixed-top">
                                                            <div class="modal-section-title">Tipo de anexo</div>
                                                            <div class="modal-field">
                                                                <span>Seleccione un anexo</span>
                                                                <dx:ASPxComboBox ID="cbAnexo" runat="server" ClientInstanceName="cbAnexo" Width="100%"
                                                                    ValueField="IdAnexo" TextField="NombreAnexo" ValueType="System.String"
                                                                    OnDataBinding="cbAnexo_DataBinding">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="NombreAnexo" Caption="Anexo" Width="220px" />
                                                                        <dx:ListBoxColumn FieldName="Descripcion" Caption="Descripción" Width="580px" />
                                                                    </Columns>
                                                                    <ValidationSettings ValidationGroup="Anexo" Display="Dynamic">
                                                                        <RequiredField IsRequired="true" ErrorText="Seleccione un anexo." />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){cpExpediente.PerformCallback('Anexo|' + s.GetValue());}" />
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="anexo-main-row">
                                                            <div class="dynamic-form-shell">
                                                                <asp:PlaceHolder ID="phCamposAnexo" runat="server" />
                                                            </div>
                                                            <div class="documents-panel">
                                                                <div class="modal-section-title">Documentación relacionada</div>
                                                            <dx:ASPxCheckBoxList ID="cblDocumentosAnexo" runat="server" ClientInstanceName="cblDocumentosAnexo" Width="100%"
                                                                    ValueField="IdDocumento" TextField="TextoDocuemento"
                                                                ValueType="System.String" RepeatDirection="Vertical" RepeatLayout="Flow" CssClass="documents-list">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){hidDocumentosAnexo.Set('Seleccionados', s.GetSelectedValues());}" />
                                                            </dx:ASPxCheckBoxList>
                                                            <dx:ASPxHiddenField ID="hidDocumentosAnexo" runat="server" ClientInstanceName="hidDocumentosAnexo" />
                                                            </div>
                                                        </div>
                                                        <div class="anexo-fixed-bottom">
                                                            <div class="modal-section-title">Datos complementarios <span class="optional-label">Opcionales</span></div>
                                                            <div class="modal-fields modal-fields-3">
                                                                <div class="modal-field">
                                                                    <span>Instrumento</span><dx:ASPxTextBox ID="txtInstrumento" runat="server" Width="100%" />
                                                                </div>
                                                                <div class="modal-field">
                                                                    <span>Volumen</span><dx:ASPxTextBox ID="txtVolumen" runat="server" Width="100%" />
                                                                </div>
                                                                <div class="modal-field">
                                                                    <span>Actividad vulnerable</span><dx:ASPxTextBox ID="txtActividadVulnerable" runat="server" Width="100%" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>

                                <div class="modal-actions">
                                    <dx:ASPxButton runat="server" Text="Cancelar" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){ppExpediente.Hide();}" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnGuardarExpediente" runat="server" Text="Generar expediente" AutoPostBack="false">
                                        <Image IconID="actions_addfile_16x16" />
                                        <ClientSideEvents Click="function(s,e){hidDocumentosAnexo.Set('Seleccionados', cblDocumentosAnexo.GetSelectedValues());if(ASPxClientEdit.ValidateGroup('Anexo')){cpExpediente.PerformCallback('Guardar');}}" />
                                    </dx:ASPxButton>
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
