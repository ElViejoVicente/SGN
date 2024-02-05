using DevExpress.Web;
using DevExpress.XtraPrinting;
using SGN.Negocio.CRUD;
using SGN.Negocio.ORM;
using SGN.Web.Controles.Servidor;
using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Export;
using SGN.Negocio.Expediente;
using SGN.Negocio.WS_GESAG;

namespace SGN.Web.ExpedientesTramites
{
    public partial class HojaDeDatos : PageBase
    {

        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
        DatosExpedientes datosExpediente = new DatosExpedientes();

        public List<Cat_DocumentosPorActo> catDocumentoOtorgaSolicita
        {
            get

            {
                List<Cat_DocumentosPorActo> sseCatDocumentoOtorgaSolicita = new List<Cat_DocumentosPorActo>();
                if (this.Session["sseCatDocumentoOtorgaSolicita"] != null)
                {
                    sseCatDocumentoOtorgaSolicita = (List<Cat_DocumentosPorActo>)this.Session["sseCatDocumentoOtorgaSolicita"];
                }

                return sseCatDocumentoOtorgaSolicita;
            }
            set
            {
                this.Session["sseCatDocumentoOtorgaSolicita"] = value;
            }

        }

        public List<Cat_DocumentosPorActo> catDocumentoAfavorDe
        {
            get

            {
                List<Cat_DocumentosPorActo> sseCatDocumentoAfavorDe = new List<Cat_DocumentosPorActo>();
                if (this.Session["sseCatDocumentoAfavorDe"] != null)
                {
                    sseCatDocumentoAfavorDe = (List<Cat_DocumentosPorActo>)this.Session["sseCatDocumentoAfavorDe"];
                }

                return sseCatDocumentoAfavorDe;
            }
            set
            {
                this.Session["sseCatDocumentoAfavorDe"] = value;
            }

        }

        public List<Cat_Actos> catActos
        {
            get

            {
                List<Cat_Actos> sseCatActos = new List<Cat_Actos>();
                if (this.Session["sseCatActos"] != null)
                {
                    sseCatActos = (List<Cat_Actos>)this.Session["sseCatActos"];
                }

                return sseCatActos;
            }
            set
            {
                this.Session["sseCatActos"] = value;
            }

        }

        public List<Cat_VariantesPorActo> catVarientesPorActo
        {
            get

            {
                List<Cat_VariantesPorActo> ssecatVarientesPorActo = new List<Cat_VariantesPorActo>();
                if (this.Session["ssecatVarientesPorActo"] != null)
                {
                    ssecatVarientesPorActo = (List<Cat_VariantesPorActo>)this.Session["ssecatVarientesPorActo"];
                }

                return ssecatVarientesPorActo;
            }
            set
            {
                this.Session["ssecatVarientesPorActo"] = value;
            }

        }

        public List<Cat_RolParticipantes> catRolParticipantesOtorgaSolicita
        {
            get

            {
                List<Cat_RolParticipantes> ssecaRolParticipantesOS = new List<Cat_RolParticipantes>();
                if (this.Session["ssecaRolParticipantesOS"] != null)
                {
                    ssecaRolParticipantesOS = (List<Cat_RolParticipantes>)this.Session["ssecaRolParticipantesOS"];
                }

                return ssecaRolParticipantesOS;
            }
            set
            {
                this.Session["ssecaRolParticipantesOS"] = value;
            }

        }

        public List<Cat_RolParticipantes> catRolParticipantesAfavorDe
        {
            get

            {
                List<Cat_RolParticipantes> ssecatRolParticipantesAfavorDeOS = new List<Cat_RolParticipantes>();
                if (this.Session["ssecatRolParticipantesAfavorDeOS"] != null)
                {
                    ssecatRolParticipantesAfavorDeOS = (List<Cat_RolParticipantes>)this.Session["ssecatRolParticipantesAfavorDeOS"];
                }

                return ssecatRolParticipantesAfavorDeOS;
            }
            set
            {
                this.Session["ssecatRolParticipantesAfavorDeOS"] = value;
            }

        }


        public List<DatosParticipantes> lsOtorgaSolicitante
        {
            get

            {
                List<DatosParticipantes> sselsOtorgaOsolicitante = new List<DatosParticipantes>();
                if (this.Session["sselsOtorgaOsolicitante"] != null)
                {
                    sselsOtorgaOsolicitante = (List<DatosParticipantes>)this.Session["sselsOtorgaOsolicitante"];
                }

                return sselsOtorgaOsolicitante;
            }
            set
            {
                this.Session["sselsOtorgaOsolicitante"] = value;
            }

        }

        public List<DatosParticipantes> lsAfavorDE
        {
            get

            {
                List<DatosParticipantes> sselsAfavorDE = new List<DatosParticipantes>();
                if (this.Session["sselsAfavorDE"] != null)
                {
                    sselsAfavorDE = (List<DatosParticipantes>)this.Session["sselsAfavorDE"];
                }

                return sselsAfavorDE;
            }
            set
            {
                this.Session["sselsAfavorDE"] = value;
            }

        }

        public string OtorgaSolicitanteSeleccion
        {
            get

            {
                string ssOtorgaSolicitanteSeleccion = "";
                if (this.Session["ssOtorgaSolicitanteSeleccion"] != null)
                {
                    ssOtorgaSolicitanteSeleccion = Session["ssOtorgaSolicitanteSeleccion"].ToString();
                }

                return ssOtorgaSolicitanteSeleccion;
            }
            set
            {
                this.Session["ssOtorgaSolicitanteSeleccion"] = value;
            }

        }

        public string AfavorDESeleccion
        {
            get

            {
                string ssAfavorDESeleccion = "";
                if (this.Session["ssAfavorDESeleccion"] != null)
                {
                    ssAfavorDESeleccion = Session["ssAfavorDESeleccion"].ToString();
                }

                return ssAfavorDESeleccion;
            }
            set
            {
                this.Session["ssAfavorDESeleccion"] = value;
            }

        }


        public List<ListaHojaDatos> lsHojaDatos
        {
            get

            {
                List<ListaHojaDatos> sselsHojaDatos = new List<ListaHojaDatos>();
                if (this.Session["sselsHojaDatos"] != null)
                {
                    sselsHojaDatos = (List<ListaHojaDatos>)this.Session["sselsHojaDatos"];
                }

                return sselsHojaDatos;
            }
            set
            {
                this.Session["sselsHojaDatos"] = value;
            }

        }



        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                dtFechaInicio.Date=  DateTime.Now.Date;
                dtFechaFin.Date  = DateTime.Now.Date;

                dtFechaIngreso.Date = DateTime.Now.Date;
                txtNombreAsesor.Text = UsuarioPagina.Nombre;
                DameCatalogos();

            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            cbVarienteNuevo.DataBind();
            lbDocumentacionOtorgaSolicita.DataBind();
            lbDocumentacionAfavorDe.DataBind();
        }




        protected void gvHojaDatos_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsHojaDatos;
        }

        protected void gvHojaDatos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "CargarRegistros")
            {
                lsHojaDatos = datosExpediente.DameListaHojaDatos(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date);// cargamos registros
                gvHojaDatos.DataBind();
                return;
            }
        }

        protected void gvHojaDatos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {

                case "CustomExportToXLS":
                    ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;
                case "CustomExportToXLSX":
                    ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;

                default:
                    break;
            }
        }



        protected void ppEditarHojaDatos_WindowCallback1(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {

        }

        protected void ppNuevaHojaDatos_WindowCallback1(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "NuevaHojaDatos")
            {
                lsOtorgaSolicitante = new List<DatosParticipantes>();
                lsAfavorDE = new List<DatosParticipantes>();
                OtorgaSolicitanteSeleccion = "";
                AfavorDESeleccion = "";
                gvOtorgaSolicita.DataBind();
                gvaFavorDe.DataBind();
                return;
            }

            if (e.Parameter.Contains("DocSelecOtorgaSolicita"))
            {
                OtorgaSolicitanteSeleccion = e.Parameter.Split('~')[1].ToString();
                return;
            }


            if (e.Parameter.Contains("DocSelecAfavorDe"))
            {
                AfavorDESeleccion = e.Parameter.Split('~')[1].ToString();
                return;
            }



            if (e.Parameter.Contains("CargarDocXvariantes"))
            {
                string idVariante = e.Parameter.Split('~')[1].ToString();
                var idActo = cbActosNuevo.Value;


                catDocumentoOtorgaSolicita = datosCrud.ConsultaCatDocumentosPorActo();

                catDocumentoAfavorDe = datosCrud.ConsultaCatDocumentosPorActo();


                if (idActo != null && catDocumentoOtorgaSolicita.Count > 0)
                {
                    catDocumentoOtorgaSolicita = catDocumentoOtorgaSolicita.Where(x => x.IdActo.ToString() == idActo.ToString() &
                                                                                  x.IdVariente.ToString() == idVariante.ToString() &
                                                                                  x.TextoFigura == "Otorga o Solicita").ToList();
                }

                if (idActo != null && catDocumentoAfavorDe.Count > 0)
                {
                    catDocumentoAfavorDe = catDocumentoAfavorDe.Where(x => x.IdActo.ToString() == idActo.ToString() &
                                                                      x.IdVariente.ToString() == idVariante.ToString() &
                                                                      x.TextoFigura == "A favor de").ToList();
                }



                lbDocumentacionOtorgaSolicita.DataBind();
                lbDocumentacionAfavorDe.DataBind();



                return;

            }

            if (e.Parameter.Contains("CargarVariantes"))
            {
                string idActo = e.Parameter.Split('~')[1].ToString();


                catDocumentoOtorgaSolicita = new List<Cat_DocumentosPorActo>();
                catDocumentoAfavorDe = new List<Cat_DocumentosPorActo>();

                lbDocumentacionOtorgaSolicita.DataBind() ;
                lbDocumentacionAfavorDe.DataBind();

                catVarientesPorActo = catVarientesPorActo = datosCrud.ConsultaCatVariantesPorActo();

                catRolParticipantesOtorgaSolicita = datosCrud.ConsultaCatRolParticipantes();

                catRolParticipantesAfavorDe = datosCrud.ConsultaCatRolParticipantes();





                if (catVarientesPorActo.Count > 0)
                {
                    catVarientesPorActo = catVarientesPorActo.Where(x => x.IdActo.ToString() == idActo).ToList();
                }

                if (catRolParticipantesOtorgaSolicita.Count > 0)
                {
                    catRolParticipantesOtorgaSolicita = catRolParticipantesOtorgaSolicita.Where(x => x.IdActo.ToString() == idActo && x.TextoFigura == "Otorga o Solicita").ToList();
                }


                if (catRolParticipantesAfavorDe.Count > 0)
                {
                    catRolParticipantesAfavorDe = catRolParticipantesAfavorDe.Where(x => x.IdActo.ToString() == idActo && x.TextoFigura == "A favor de").ToList();
                }


                cbVarienteNuevo.SelectedIndex = -1;
                cbVarienteNuevo.DataBind();



                return;
            }

            if (e.Parameter.Contains("guardar"))
            {
                Boolean existeError = false;

                // guardamos en base de datos la HojaDatos ,  DatosParticipantes , DatosDocumentos
                // ademas se crea el numero de expediente en la tabla  Expedientes con los datos del solicitante y ortorgante 
                // tambien hay que crear la ruta de carpeta

                // 1- se guarda la hoja de datos

                HojaDatos nuevaHoja = new HojaDatos();
                DatosVariantes nuevaHojaComplemento = new DatosVariantes();
                DatosDocumentos nuevahojaDocumentos = new DatosDocumentos();
                RecibosDePago nuevaHojaReciboPago = new RecibosDePago();
                Expedientes nuevoExpediente = new Expedientes();

                nuevaHoja.IdHojaDatos = 0;
                nuevaHoja.FechaIngreso = dtFechaIngreso.Date;
                nuevaHoja.NombreAsesor = txtNombreAsesor.Text;
                nuevaHoja.NumbreUsuarioTramita = txtClienteTramita.Text;
                nuevaHoja.NumTelCelular1 = txtNumCelular.Text;
                nuevaHoja.CorreoElectronico = txtCorreoElectronico.Text;

                if (datosCrud.AltaHojaDatos(ref nuevaHoja))
                {
                    nuevaHojaComplemento.IdHojaDatos = nuevaHoja.IdHojaDatos;
                    nuevaHojaComplemento.IdActo = cbActosNuevo.Value == null ? 0 : Convert.ToInt32(cbActosNuevo.Value);
                    nuevaHojaComplemento.IdVariante = cbVarienteNuevo.Value == null ? 0 : Convert.ToInt32(cbVarienteNuevo.Value);
                    nuevaHojaComplemento.TextoActo = cbActosNuevo.Text == null ? "" : cbActosNuevo.Text;
                    nuevaHojaComplemento.TextoVariante = cbVarienteNuevo.Text == null ? "" : cbVarienteNuevo.Text;
                    // se guardan los datos extras de al hoja 

                    if (!datosCrud.AltaDatosVariantes(nuevaHojaComplemento))
                    {
                        existeError = true;
                    }


                    // guardamos a los participantes del acto
                    foreach (var item in lsOtorgaSolicitante)
                    {
                        item.IdHojaDatos = nuevaHoja.IdHojaDatos;

                        if (!datosCrud.AltaDatosParticipantes(item))
                        {
                            existeError = true;
                        }
                    }

                    foreach (var item in lsAfavorDE)
                    {
                        item.IdHojaDatos = nuevaHoja.IdHojaDatos;

                        if (!datosCrud.AltaDatosParticipantes(item))
                        {
                            existeError = true;
                        }
                    }

                    // se rellenan  la lista de documento para proceder al guardado


                 

                    foreach (var item in OtorgaSolicitanteSeleccion.Split(',').ToList())
                    {
                        nuevahojaDocumentos = new DatosDocumentos();
                        nuevahojaDocumentos.IdHojaDatos = nuevaHoja.IdHojaDatos;                        
                        nuevahojaDocumentos.IdVariente = nuevaHojaComplemento.IdVariante;
                        nuevahojaDocumentos.TextoVariante = nuevaHojaComplemento.TextoVariante;
                        nuevahojaDocumentos.TextoFigura = "Otorga o Solicita";
                        nuevahojaDocumentos.IdDoc = Convert.ToInt32(item);
                        nuevahojaDocumentos.Observaciones = "";

                        datosCrud.AltaDatosDocumentos(nuevahojaDocumentos);

                    }

                    foreach (var item in AfavorDESeleccion.Split(',').ToList())
                    {
                        nuevahojaDocumentos = new DatosDocumentos();
                        nuevahojaDocumentos.IdHojaDatos = nuevaHoja.IdHojaDatos;
                        nuevahojaDocumentos.IdVariente = nuevaHojaComplemento.IdVariante;
                        nuevahojaDocumentos.TextoVariante = nuevaHojaComplemento.TextoVariante;
                        nuevahojaDocumentos.TextoFigura = "A favor de";
                        nuevahojaDocumentos.IdDoc = Convert.ToInt32(item);
                        nuevahojaDocumentos.Observaciones = "";

                        datosCrud.AltaDatosDocumentos(nuevahojaDocumentos);
                    }


                    // se registra el primer recibo de pago

                    nuevaHojaReciboPago.IdHojaDatos = nuevaHoja.IdHojaDatos;
                    nuevaHojaReciboPago.NumRecibo = txtReciboPagoIni.Text;
                    nuevaHojaReciboPago.CantidadTotal = 0;
                    nuevaHojaReciboPago.CantidadAbonada = 0;
                    nuevaHojaReciboPago.Concepto = "Recibo Inicial Expediente";
                    nuevaHojaReciboPago.UsuarioRecibe = "No Definido";                    
                    nuevaHojaReciboPago.NotaComentario = "Recibo no controlado por sistema.";

                    if (!datosCrud.AltaRecibosDePago(nuevaHojaReciboPago))
                    {
                        existeError = true;
                    }


                    //se crear el registro del expediente

                    nuevoExpediente.IdExpediente = nuevaHoja.numExpediente;
                    nuevoExpediente.IdHojaDatos = nuevaHoja.IdHojaDatos;
                    nuevoExpediente.FechaElaboracion = nuevaHoja.FechaIngreso;
                    

                    foreach (var item in lsOtorgaSolicitante)
                    {
                        nuevoExpediente.Otorga += item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + " / ";
                    }

                    foreach (var item in lsAfavorDE)
                    {
                        nuevoExpediente.AfavorDe += item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + " / ";
                    }


                    if (!datosCrud.AltaExpediente(nuevoExpediente))
                    {
                        existeError = true;
                    }



                    //  se crean las carpetas necesarias para el expediente


                    string directorioVirtual = "~/GNArchivosRoot";
                    string directorioFisico = MapPath(directorioVirtual);




                    string rutaFisicaCalculada = Path.Combine(directorioFisico, nuevaHoja.numExpediente);

                    if (!Directory.Exists(rutaFisicaCalculada))
                    {
                        Directory.CreateDirectory(rutaFisicaCalculada);

                        if (Directory.Exists(rutaFisicaCalculada))
                        {
                            string carpetaAvisos = Path.Combine(rutaFisicaCalculada, "Avisos");
                            string carpetaFirmados = Path.Combine(rutaFisicaCalculada, "Firmados");
                            string carpetaPendientesFirma = Path.Combine(rutaFisicaCalculada, "PedientesFirma");
                            string carpetaProyecto = Path.Combine(rutaFisicaCalculada, "Proyecto");
                            string carpetaDocumentos = Path.Combine(rutaFisicaCalculada, "Documentos");

                            Directory.CreateDirectory(carpetaAvisos);

                            Directory.CreateDirectory(carpetaFirmados);

                            Directory.CreateDirectory(carpetaPendientesFirma);

                            Directory.CreateDirectory(carpetaProyecto);

                            Directory.CreateDirectory(carpetaDocumentos);




                        }

                    }


                }


                if (!existeError)
                {


                    ppNuevaHojaDatos.JSProperties["cp_swMsg"] = "Nuevo expediente: " + nuevaHoja.numExpediente + " listo.!";
                    ppNuevaHojaDatos.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;
                    return;
                }
                else
                {
                    ppNuevaHojaDatos.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar guardar el registro.";
                    ppNuevaHojaDatos.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;
                    return;
                }
                
            }


        }

        protected void gvOtorgaSolicita_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsOtorgaSolicitante;
        }



        protected void cbActosNuevo_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "IdActo";
            control.TextField = "TextoActo";
            control.DataSource = catActos;
        }

        protected void cbVarienteNuevo_DataBinding(object sender, EventArgs e)
        {
            {
                ASPxComboBox control = (ASPxComboBox)sender;

                control.ValueField = "IdVariante";
                control.TextField = "TextoVariante";
                control.DataSource = catVarientesPorActo;
            }
            #endregion
        }
        #region Funciones

        private void DameCatalogos()
        {
            catActos = datosCrud.ConsultaCatActos();
            cbActosNuevo.DataBind();

        }




        #endregion

        protected void cbRolOtorgaSolicita_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            cb.DataSource = catRolParticipantesOtorgaSolicita;
            cb.TextField = "TextoRol";
            cb.ValueField = "TextoRol";
            cb.ValueType = typeof(string);

            cb.SelectedIndex = -1;
        }

        protected void cbAnafabetaOtorgaSolicita_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            Cat_RolParticipantes rol = catRolParticipantesOtorgaSolicita.Where(x => x.TextoRol.ToString() == e.Parameter).FirstOrDefault();

            if (!rol.PreguntarSiEsAnafabeta)
            {
                cb.Value = "No Aplica";
                cb.Text = "No Aplica";
            }
            else
            {
                cb.Value = "Si";
                cb.Text = "Si";
            }



        }

        protected void gvOtorgaSolicita_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            object rolOperacion = ((ASPxComboBox)gvOtorgaSolicita.FindEditRowCellTemplateControl(
                  gvOtorgaSolicita.Columns["RolOperacion"] as GridViewDataComboBoxColumn,
                  "cbRolOtorgaSolicita")).Value;

            object sexo = ((ASPxComboBox)gvOtorgaSolicita.FindEditRowCellTemplateControl(
                gvOtorgaSolicita.Columns["Sexo"] as GridViewDataComboBoxColumn,
                "cbSexoOtorgaSolicita")).Value;

            object estadoCivil = ((ASPxComboBox)gvOtorgaSolicita.FindEditRowCellTemplateControl(
                gvOtorgaSolicita.Columns["EstadoCivil"] as GridViewDataComboBoxColumn,
                "cbEstadoCivilOtorgaSolicita")).Value;

            object RegimenConyugal = ((ASPxComboBox)gvOtorgaSolicita.FindEditRowCellTemplateControl(
                gvOtorgaSolicita.Columns["RegimenConyugal"] as GridViewDataComboBoxColumn,
                "cbRegimenConyugalOtorgaSolicita")).Value;


            if (rolOperacion == null)
            {
                e.RowError += "El campo Rol Operacion es obligatorio.\n ";
            }

            if (sexo == null)
            {
                e.RowError += "El campo sexo  es obligatorio.\n ";
            }

            if (e.NewValues["FechaNacimiento"] == null)
            {
                e.RowError += "El fecha nacimiento es obligatorio.\n ";
            }

            if (estadoCivil == null)
            {
                e.RowError += "El campo estado civil Operacion es obligatorio.\n ";
            }
            if (RegimenConyugal == null)
            {
                e.RowError += "El campo Regimen conyugal Operacion es obligatorio.\n ";
            }

            if (e.NewValues["Nombres"] == null)
            {
                e.RowError += "El campo Nombre es obligatorio.\n ";
            }

            if (e.NewValues["ApellidoPaterno"] == null)
            {
                e.RowError += "El campo Apellido Paterno es obligatorio.\n ";
            }

            if (e.NewValues["ApellidoMaterno"] == null)
            {
                e.RowError += "El campo Apellido Materno es obligatorio.\n ";
            }


        }



        protected void cbSexoOtorgaSolicita_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbEstadoCivilOtorgaSolicita_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbRegimenConyugalOtorgaSolicita_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }



        protected void gvOtorgaSolicita_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DatosParticipantes datos = new DatosParticipantes();
            datos.IdHojaDatos = 0;
            datos.FiguraOperacion = "Otorga o Solicita";
            datos.RolOperacion = e.NewValues["RolOperacion"].ToString();
            datos.Nombres = e.NewValues["Nombres"].ToString();
            datos.ApellidoPaterno = e.NewValues["ApellidoPaterno"].ToString();
            datos.ApellidoMaterno = e.NewValues["ApellidoMaterno"].ToString();
            datos.Sexo = e.NewValues["Sexo"].ToString();
            datos.FechaNacimiento= Convert.ToDateTime(e.NewValues["FechaNacimiento"].ToString());
            datos.Ocupacion = e.NewValues["Ocupacion"] == null ? "" : e.NewValues["Ocupacion"].ToString();
            datos.EstadoCivil = e.NewValues["EstadoCivil"].ToString();
            datos.RegimenConyugal = e.NewValues["RegimenConyugal"].ToString();
            datos.SabeLeerEscribir = e.NewValues["SabeLeerEscribir"].ToString();
            datos.Notas = e.NewValues["Notas"] == null ? "" : e.NewValues["Notas"].ToString();

            lsOtorgaSolicitante.Add(datos);

            e.Cancel = true;
            gvOtorgaSolicita.CancelEdit();

            gvOtorgaSolicita.DataBind();

        }

        protected void gvaFavorDe_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsAfavorDE;
        }

        protected void gvaFavorDe_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            object rolOperacion = ((ASPxComboBox)gvaFavorDe.FindEditRowCellTemplateControl(
                    gvaFavorDe.Columns["RolOperacion"] as GridViewDataComboBoxColumn,
                    "cbRolAfavorDe")).Value;

            object sexo = ((ASPxComboBox)gvaFavorDe.FindEditRowCellTemplateControl(
                gvaFavorDe.Columns["Sexo"] as GridViewDataComboBoxColumn,
                "cbSexoAfavorDe")).Value;

            object estadoCivil = ((ASPxComboBox)gvaFavorDe.FindEditRowCellTemplateControl(
                gvaFavorDe.Columns["EstadoCivil"] as GridViewDataComboBoxColumn,
                "cbEstadoCivilAfavorDe")).Value;

            object RegimenConyugal = ((ASPxComboBox)gvaFavorDe.FindEditRowCellTemplateControl(
                gvaFavorDe.Columns["RegimenConyugal"] as GridViewDataComboBoxColumn,
                "cbRegimenConyugalAfavorDe")).Value;


            if (rolOperacion == null)
            {
                e.RowError += "El campo Rol Operacion es obligatorio.\n ";
            }

            if (sexo == null)
            {
                e.RowError += "El campo sexo  es obligatorio.\n ";
            }

            if (e.NewValues["FechaNacimiento"] == null)
            {
                e.RowError += "El fecha nacimiento es obligatorio.\n ";
            }

            if (estadoCivil == null)
            {
                e.RowError += "El campo estado civil Operacion es obligatorio.\n ";
            }
            if (RegimenConyugal == null)
            {
                e.RowError += "El campo Regimen conyugal Operacion es obligatorio.\n ";
            }

            if (e.NewValues["Nombres"] == null)
            {
                e.RowError += "El campo Nombre es obligatorio.\n ";
            }

            if (e.NewValues["ApellidoPaterno"] == null)
            {
                e.RowError += "El campo Apellido Paterno es obligatorio.\n ";
            }

            if (e.NewValues["ApellidoMaterno"] == null)
            {
                e.RowError += "El campo Apellido Materno es obligatorio.\n ";
            }


        }

        protected void gvaFavorDe_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DatosParticipantes datos = new DatosParticipantes();
            datos.IdHojaDatos = 0;
            datos.FiguraOperacion = "A favor de";
            datos.RolOperacion = e.NewValues["RolOperacion"].ToString();
            datos.Nombres = e.NewValues["Nombres"].ToString();
            datos.ApellidoPaterno = e.NewValues["ApellidoPaterno"].ToString();
            datos.ApellidoMaterno = e.NewValues["ApellidoMaterno"].ToString();
            datos.Sexo = e.NewValues["Sexo"].ToString();
            datos.FechaNacimiento = Convert.ToDateTime(e.NewValues["FechaNacimiento"].ToString());
            datos.Ocupacion = e.NewValues["Ocupacion"] == null ? "" : e.NewValues["Ocupacion"].ToString();
            datos.EstadoCivil = e.NewValues["EstadoCivil"].ToString();
            datos.RegimenConyugal = e.NewValues["RegimenConyugal"].ToString();
            datos.SabeLeerEscribir = e.NewValues["SabeLeerEscribir"].ToString();
            datos.Notas = e.NewValues["Notas"] == null ? "" : e.NewValues["Notas"].ToString();

            lsAfavorDE.Add(datos);

            e.Cancel = true;
            gvaFavorDe.CancelEdit();

            gvaFavorDe.DataBind();
        }

        protected void cbRolAfavorDe_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            cb.DataSource = catRolParticipantesAfavorDe;
            cb.TextField = "TextoRol";
            cb.ValueField = "TextoRol";
            cb.ValueType = typeof(string);

            cb.SelectedIndex = -1;
        }

        protected void cbSexoAfavorDe_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbEstadoCivilAfavorDe_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbRegimenConyugalAfavorDe_Init(object sender, EventArgs e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;
            cb.SelectedIndex = -1;
        }

        protected void cbAnafabetaAFavorDe_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxComboBox cb = (ASPxComboBox)sender;

            Cat_RolParticipantes rol = catRolParticipantesAfavorDe.Where(x => x.TextoRol.ToString() == e.Parameter).FirstOrDefault();

            if (!rol.PreguntarSiEsAnafabeta)
            {
                cb.Value = "No Aplica";
                cb.Text = "No Aplica";
            }
            else
            {
                cb.Value = "Si";
                cb.Text = "Si";
            }
        }

        protected void lbDocumentacionOtorgaSolicita_DataBinding(object sender, EventArgs e)
        {
            ASPxListBox control = (ASPxListBox)sender;


            control.DataSource = catDocumentoOtorgaSolicita; ;
            control.TextField = "TextoDocumento";
            control.ValueField = "IdDoc";
        }

        protected void lbDocumentacionAfavorDe_DataBinding(object sender, EventArgs e)
        {
            ASPxListBox control = (ASPxListBox)sender;


            control.DataSource = catDocumentoAfavorDe; ;
            control.TextField = "TextoDocumento";
            control.ValueField = "IdDoc";
        }

        protected void gvOtorgaSolictaDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            List<DatosParticipantes> detalle = new List<DatosParticipantes>();
            string numHojaDatos = detailGrid.GetMasterRowKeyValue().ToString();


            ListaHojaDatos registroHojaDatos = lsHojaDatos.Where(x => x.IdHojaDatos.ToString()== numHojaDatos).FirstOrDefault();

            if (registroHojaDatos != null)
            {
                detalle = registroHojaDatos.DetalleParticipantes.Where(x => x.FiguraOperacion == "Otorga o Solicita").ToList();
            }


            detailGrid.DataSource = detalle;
        }

        protected void gvAfavorDeDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            List<DatosParticipantes> detalle = new List<DatosParticipantes>();
            string numHojaDatos = detailGrid.GetMasterRowKeyValue().ToString();


            ListaHojaDatos registroHojaDatos = lsHojaDatos.Where(x => x.IdHojaDatos.ToString() == numHojaDatos).FirstOrDefault();

            if (registroHojaDatos != null)
            {
                detalle = registroHojaDatos.DetalleParticipantes.Where(x => x.FiguraOperacion == "A favor de").ToList();
            }


            detailGrid.DataSource = detalle;
        }
    }
}