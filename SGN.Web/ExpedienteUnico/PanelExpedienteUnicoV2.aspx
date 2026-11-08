<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelExpedienteUnicoV2.aspx.cs" Inherits="SGN.Web.ExpedienteUnico.PanelExpedienteUnicoV2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="../Scripts/sweetalert2.min.css" />
    <link rel="stylesheet" href="PanelExpedienteUnicoV2.aspx.css" />
    <script src="../Scripts/mensajes.js"></script>
    <title>Expediente único</title>
    <script type="text/javascript">
        window.onresize = function () { adjustGridHeight(); };
        var temporizadorFiltroExpediente = null;
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
                aplicarColorCumplimiento(s.cp_KpiCumplimiento);
            }
            if (s.cp_swMsg) { mostrarMensajeSweet(s.cp_swType, s.cp_swMsg); s.cp_swType = null; s.cp_swMsg = null; }
            window.setTimeout(function () { buscarEnExpediente(false); }, 0);
        }
        function normalizarTextoExpediente(valor) {
            valor = String(valor || '').toLocaleLowerCase();
            return valor.normalize ? valor.normalize('NFD').replace(/[\u0300-\u036f]/g, '') : valor;
        }
        function limpiarResaltadosExpediente(raiz) {
            var resaltados = raiz.querySelectorAll('mark.exp-search-match');
            for (var i = resaltados.length - 1; i >= 0; i--) {
                var marca = resaltados[i];
                var padre = marca.parentNode;
                padre.replaceChild(document.createTextNode(marca.textContent), marca);
                padre.normalize();
            }
        }
        function crearIndiceNormalizado(texto) {
            var normalizado = '';
            var posiciones = [];
            for (var i = 0; i < texto.length; i++) {
                var caracter = normalizarTextoExpediente(texto.charAt(i));
                for (var j = 0; j < caracter.length; j++) {
                    normalizado += caracter.charAt(j);
                    posiciones.push(i);
                }
            }
            return { texto: normalizado, posiciones: posiciones };
        }
        function resaltarNodoExpediente(nodo, termino) {
            var contenido = nodo.nodeValue;
            var indice = crearIndiceNormalizado(contenido);
            var coincidencias = [];
            var desde = 0;
            var posicion;
            while ((posicion = indice.texto.indexOf(termino, desde)) >= 0) {
                var inicioOriginal = indice.posiciones[posicion];
                var finOriginal = indice.posiciones[posicion + termino.length - 1] + 1;
                coincidencias.push({ inicio: inicioOriginal, fin: finOriginal });
                desde = posicion + Math.max(termino.length, 1);
            }
            if (!coincidencias.length) { return; }

            var fragmento = document.createDocumentFragment();
            var cursor = 0;
            for (var i = 0; i < coincidencias.length; i++) {
                var coincidencia = coincidencias[i];
                if (coincidencia.inicio > cursor) { fragmento.appendChild(document.createTextNode(contenido.substring(cursor, coincidencia.inicio))); }
                var marca = document.createElement('mark');
                marca.className = 'exp-search-match';
                marca.appendChild(document.createTextNode(contenido.substring(coincidencia.inicio, coincidencia.fin)));
                fragmento.appendChild(marca);
                cursor = coincidencia.fin;
            }
            if (cursor < contenido.length) { fragmento.appendChild(document.createTextNode(contenido.substring(cursor))); }
            nodo.parentNode.replaceChild(fragmento, nodo);
        }
        function buscarEnExpediente(desplazar) {
            if (!window.gvExpedienteUnico || !window.txtFiltrarPor) { return; }
            var raiz = gvExpedienteUnico.GetMainElement();
            if (!raiz) { return; }
            limpiarResaltadosExpediente(raiz);

            var termino = normalizarTextoExpediente(txtFiltrarPor.GetText().trim());
            txtFiltrarPor.GetMainElement().classList.remove('filter-no-results');
            txtFiltrarPor.GetMainElement().removeAttribute('title');
            if (!termino) { return; }

            var walker = document.createTreeWalker(raiz, NodeFilter.SHOW_TEXT, {
                acceptNode: function (nodo) {
                    var padre = nodo.parentNode;
                    if (!padre || !nodo.nodeValue.trim() || /^(SCRIPT|STYLE|TEXTAREA|INPUT|OPTION)$/.test(padre.nodeName)) { return NodeFilter.FILTER_REJECT; }
                    return normalizarTextoExpediente(nodo.nodeValue).indexOf(termino) >= 0 ? NodeFilter.FILTER_ACCEPT : NodeFilter.FILTER_REJECT;
                }
            }, false);
            var nodos = [];
            var nodo;
            while ((nodo = walker.nextNode())) { nodos.push(nodo); }
            for (var i = 0; i < nodos.length; i++) { resaltarNodoExpediente(nodos[i], termino); }

            var coincidencias = raiz.querySelectorAll('mark.exp-search-match');
            if (!coincidencias.length) {
                txtFiltrarPor.GetMainElement().classList.add('filter-no-results');
                txtFiltrarPor.GetMainElement().setAttribute('title', 'No se encontraron coincidencias en el grid.');
                return;
            }
            var centroVisible = window.innerHeight / 2;
            var masProxima = coincidencias[0];
            var distanciaMenor = Number.MAX_VALUE;
            for (var j = 0; j < coincidencias.length; j++) {
                var rect = coincidencias[j].getBoundingClientRect();
                if (!rect.width && !rect.height) { continue; }
                var distancia = Math.abs((rect.top + rect.height / 2) - centroVisible);
                if (distancia < distanciaMenor) { distanciaMenor = distancia; masProxima = coincidencias[j]; }
            }
            masProxima.classList.add('exp-search-current');
            if (desplazar !== false) { masProxima.scrollIntoView({ behavior: 'smooth', block: 'center', inline: 'nearest' }); }
        }
        function programarFiltroExpediente() {
            window.clearTimeout(temporizadorFiltroExpediente);
            temporizadorFiltroExpediente = window.setTimeout(function () { buscarEnExpediente(true); }, 220);
        }
        function aplicarColorCumplimiento(texto) {
            if (!window.lblKpiCumplimiento) { return; }
            var valor = parseFloat(String(texto || '0').replace(',', '.')) || 0;
            var contenedor = lblKpiCumplimiento.GetMainElement();
            while (contenedor && !((' ' + contenedor.className + ' ').indexOf(' kpi-compliance ') >= 0)) {
                contenedor = contenedor.parentNode;
            }
            if (!contenedor) { return; }
            contenedor.className = contenedor.className.replace(/\s*compliance-(green|orange|red)/g, '');
            contenedor.className += valor >= 100 ? ' compliance-green' : (valor >= 90 ? ' compliance-orange' : ' compliance-red');
        }
        function abrirExpediente(idRegistro) {
            ppExpediente.Show();
            cpExpediente.PerformCallback('Abrir|' + idRegistro);
        }
        function guardarExpediente() {
            hidDocumentosAnexo.Set('Seleccionados', cblDocumentosAnexo.GetSelectedValues());
            var valido = ASPxClientEdit.ValidateGroup('Anexo');
            if (!txtActividadVulnerable.GetIsValid()) {
                pcExpediente.SetActiveTabIndex(0);
                txtActividadVulnerable.Focus();
            }
            if (valido) { cpExpediente.PerformCallback('Guardar'); }
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

                                <dx:ASPxTextBox runat="server" Caption="Filtrar Por" ClientInstanceName="txtFiltrarPor" ID="txtFiltrarPor" Width="170px" NullText="Buscar en el grid...">
                                    <ClientSideEvents KeyUp="function(s,e){programarFiltroExpediente();}" ValueChanged="function(s,e){programarFiltroExpediente();}" />
                                </dx:ASPxTextBox>


                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="KPI del rango seleccionado:" View="GroupBox" CssClass="exp-filter-kpi">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="exp-kpi-content">
                                <div class="kpi-item">
                                    <dx:ASPxImage runat="server" EmptyImage-IconID="actions_addfile_16x16" CssClass="kpi-icon" />
                                    <div class="kpi-text"><span class="kpi-title">Expedientes</span><dx:ASPxLabel ID="lblKpiExpedientes" ClientInstanceName="lblKpiExpedientes" runat="server" Text="0" CssClass="kpi-value" /></div>
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-warning">
                                    <dx:ASPxImage runat="server" EmptyImage-IconID="status_warning_16x16" CssClass="kpi-icon" />
                                    <div class="kpi-text"><span class="kpi-title">Sin Expediente</span><dx:ASPxLabel ID="lblKpiSinAnexo" ClientInstanceName="lblKpiSinAnexo" runat="server" Text="0" CssClass="kpi-value" /></div>
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-success">
                                    <dx:ASPxImage runat="server" EmptyImage-IconID="actions_apply_16x16" CssClass="kpi-icon" />
                                    <div class="kpi-text"><span class="kpi-title">Con Expediente</span><dx:ASPxLabel ID="lblKpiConAnexo" ClientInstanceName="lblKpiConAnexo" runat="server" Text="0" CssClass="kpi-value" /></div>
                                </div>
                                <div class="kpi-divider"></div>
                                <div class="kpi-item kpi-compliance">
                                    <dx:ASPxImage runat="server" EmptyImage-IconID="dashboards_scatterchartlabeloptions_svg_16x16" CssClass="kpi-icon" />
                                    <div class="kpi-text"><span class="kpi-title">Cumplimiento</span><dx:ASPxLabel ID="lblKpiCumplimiento" ClientInstanceName="lblKpiCumplimiento" runat="server" Text="0%" CssClass="kpi-value"><ClientSideEvents Init="function(s,e){aplicarColorCumplimiento(s.GetText());}" /></dx:ASPxLabel></div>
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </div>
            <div id="expContent" class="exp-content">
                <dx:ASPxGridView runat="server" ID="gvExpedienteUnico" ClientInstanceName="gvExpedienteUnico" AutoGenerateColumns="False" Width="100%" KeyFieldName="IdRegistro" CssClass="exp-grid" OnDataBinding="gvExpedienteUnico_DataBinding" OnCustomCallback="gvExpedienteUnico_CustomCallback" OnToolbarItemClick="gvExpedienteUnico_ToolbarItemClick" OnHtmlDataCellPrepared="gvExpedienteUnico_HtmlDataCellPrepared">
                    <ClientSideEvents Init="function(s,e){adjustGridHeight();window.setTimeout(function(){buscarEnExpediente(false);},0);}" EndCallback="gridView_EndCallback" />
                    <Settings ShowColumnHeaders="False" ShowGroupPanel="False" ShowFilterRow="False" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Hidden"  />
                    <SettingsPager Mode="ShowPager" PageSize="50" Position="Bottom" />
                    <SettingsBehavior AutoExpandAllGroups="true" AllowGroup="false" AllowSort="false" AllowFocusedRow="false" EnableRowHotTrack="false" />
                    <SettingsDataSecurity AllowInsert="false" AllowDelete="false" AllowEdit="false" />
                    <SettingsText EmptyDataRow="No se encontraron expedientes para el periodo seleccionado." />


                    <Templates>
                        <GroupRowContent>
                            <div class="exp-group-header">
                                <dx:ASPxImage runat="server" EmptyImage-IconID="arrows_movedown_16x16" CssClass="exp-group-icon" />
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
                                        <asp:Panel runat="server" CssClass="person-warning" Visible='<%# !TieneExpediente(Eval("IdExpedienteUnico")) %>'><dx:ASPxImage runat="server" EmptyImage-IconID="status_warning_16x16" CssClass="inline-icon" />Sin expediente generado</asp:Panel>
                                    </div>
                                    <div class="person-detail-column">
                                        <div class="person-meta" runat="server" visible='<%# TieneExpediente(Eval("IdExpedienteUnico")) %>'>
                                            <span class="meta-item"><dx:ASPxImage runat="server" EmptyImage-IconID="support_article_16x16" CssClass="meta-icon" /><dx:ASPxLabel runat="server" Text='<%# GetAnexo(Eval("NombreDelAnexo"),Eval("IdExpedienteUnico")) %>' /></span>
                                            <span class="meta-item"><dx:ASPxImage runat="server" EmptyImage-IconID="scheduling_calendar_16x16" CssClass="meta-icon" /><dx:ASPxLabel runat="server" Text='<%# FormatFecha(Eval("FechaCreacionVersion")) %>' /></span>
                                            <span class="meta-item"><dx:ASPxImage runat="server" EmptyImage-IconID="people_user_16x16" CssClass="meta-icon" /><dx:ASPxLabel runat="server" Text='<%# Eval("UsuarioAutor") %>' /></span>
                                        </div>
                                        <asp:Panel runat="server" CssClass="person-vulnerable-alert" Visible='<%# TieneActividadVulnerable(Eval("ActividadVulnerable")) %>'>
                                            <dx:ASPxImage runat="server" EmptyImage-IconID="iconbuilder_security_warningcircled1_svg_16x16" CssClass="vulnerable-icon" />
                                            <dx:ASPxLabel runat="server" Text='<%# "Actividad vulnerable: " + Convert.ToString(Eval("ActividadVulnerable")) %>' />
                                        </asp:Panel>
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
                                <div class="modal-notice"><dx:ASPxImage runat="server" EmptyImage-IconID="functionlibrary_information_16x16" CssClass="inline-icon" />Se generará un expediente individual para el participante seleccionado. Todos los cambios son registrados con tu usuario. Te recomiendo concentración.</div>

                                <dx:ASPxPageControl ID="pcExpediente" runat="server" ClientInstanceName="pcExpediente" Width="100%" ActiveTabIndex="0" CssClass="exp-tabs">
                                    <TabPages>
                                        <dx:TabPage Text="Persona" Name="Persona" TabImage-IconID="people_user_16x16">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <div class="persona-vulnerable-block">
                                                        <div class="persona-vulnerable-title"><dx:ASPxImage runat="server" EmptyImage-IconID="iconbuilder_security_warningcircled1_svg_16x16" CssClass="inline-icon" />Actividad vulnerable <span class="required-label">Obligatoria</span></div>
                                                        <dx:ASPxMemo ID="txtActividadVulnerable" runat="server" ClientInstanceName="txtActividadVulnerable" Width="100%" Rows="3" MaxLength="500" NullText="Describa la actividad vulnerable (mínimo 10 caracteres)">
                                                            <ValidationSettings ValidationGroup="Anexo" Display="Dynamic" ErrorDisplayMode="Text">
                                                                <RequiredField IsRequired="true" ErrorText="La actividad vulnerable es obligatoria." />
                                                                <RegularExpression ValidationExpression="^(?=[\s\S]{10,}$)(?=(?:[\s\S]*[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9]){3,})[\s\S]*$" ErrorText="Capture una descripción válida de al menos 10 caracteres." />
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </div>
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
                                                            <div class="modal-fields modal-fields-2">
                                                                <div class="modal-field">
                                                                    <span>Instrumento</span><dx:ASPxTextBox ID="txtInstrumento" runat="server" Width="100%" />
                                                                </div>
                                                                <div class="modal-field">
                                                                    <span>Volumen</span><dx:ASPxTextBox ID="txtVolumen" runat="server" Width="100%" />
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
                                        <ClientSideEvents Click="function(s,e){guardarExpediente();}" />
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
