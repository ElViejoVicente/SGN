using DevExpress.CodeParser;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using SGN.Negocio.CRUD;
using SGN.Negocio.Expediente;
using SGN.Negocio.Operativa;
using SGN.Negocio.ORM;

using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SGN.Web.ExpedientesTramites
{
    public partial class PanelExpedientes : PageBase
    {

        #region Propiedades
        DatosCrud datosCrud = new DatosCrud();
        DatosUsuario datosUsuario = new DatosUsuario();
        DatosExpedientes datosExpediente = new DatosExpedientes();
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

        public List<Cat_Estatus> catEstatus
        {
            get

            {
                List<Cat_Estatus> sseCatEstatus = new List<Cat_Estatus>();
                if (this.Session["sseCatEstatus"] != null)
                {
                    sseCatEstatus = (List<Cat_Estatus>)this.Session["sseCatEstatus"];
                }

                return sseCatEstatus;
            }
            set
            {
                this.Session["sseCatEstatus"] = value;
            }

        }

        public List<Usuario> catProyectistas
        {
            get

            {
                List<Usuario> sseCatProyectistas = new List<Usuario>();
                if (this.Session["sseCatProyectistas"] != null)
                {
                    sseCatProyectistas = (List<Usuario>)this.Session["sseCatProyectistas"];
                }

                return sseCatProyectistas;
            }
            set
            {
                this.Session["sseCatProyectistas"] = value;
            }

        }


        public List<ListaExpedientes> lsExpediente
        {
            get

            {
                List<ListaExpedientes> sseListaExpediente = new List<ListaExpedientes>();
                if (this.Session["sseListaExpediente"] != null)
                {
                    sseListaExpediente = (List<ListaExpedientes>)this.Session["sseListaExpediente"];
                }

                return sseListaExpediente;
            }
            set
            {
                this.Session["sseListaExpediente"] = value;
            }

        }


        public Expedientes RegistroExistente
        {
            get

            {
                Expedientes ssRegistroExistente = new Expedientes();
                if (this.Session["ssRegistroExistente"] != null)
                {
                    ssRegistroExistente = (Expedientes)this.Session["ssRegistroExistente"];
                }

                return ssRegistroExistente;
            }
            set
            {
                this.Session["ssRegistroExistente"] = value;
            }

        }


        public string rutaArchivosRoot
        {
            get

            {
                string ssRutaArchivosRoot = "";
                if (this.Session["ssRutaArchivosRoot"] != null)
                {
                    ssRutaArchivosRoot = this.Session["ssRutaArchivosRoot"].ToString();
                }

                return ssRutaArchivosRoot;
            }
            set
            {
                this.Session["ssRutaArchivosRoot"] = value;
            }

        }




        #endregion

        #region Funciones
        private void DameCatalogos()
        {
            //UsuarioPagina.NombrePerfil


            catEstatus = datosCrud.ConsultaCatEstatus();
            catActos = datosCrud.ConsultaCatActos();
            catProyectistas = datosUsuario.DameDatosUsuario(-1).Where(x => x.EsProyectista == true).ToList();

            //cbActosNuevo.DataBind();
            cbPRfnProyectista.DataBind();

            //MostrarCampoPorPerfil(UsuarioPagina.NombrePerfil);

        }


        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (rutaArchivosRoot != "")
            {
                fmArchivosControl.Settings.RootFolder = rutaArchivosRoot;

                var rootFolder = GetRootFolder(fmArchivosControl.SelectedFolder);

                ApplyRules(rootFolder);

            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
        

            if (!Page.IsPostBack)
            {
                dtFechaInicio.Date = DateTime.Now.Date;
                dtFechaFin.Date = DateTime.Now.Date;
                DameCatalogos();
            }
        }

        protected void gvExpedientes_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView control = (ASPxGridView)sender;
            control.DataSource = lsExpediente;
        }

        protected void gvExpedientes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //ASPxGridView control = (ASPxGridView)sender;
            if (e.Parameters == "CargarRegistros")
            {
                lsExpediente = datosExpediente.DameListaExpediente(fechaInicial: dtFechaInicio.Date, fechaFinal: dtFechaFin.Date);// cargamos registros
                gvExpedientes.DataBind();
                return;
            }

            if (e.Parameters=="AsignarRutaExpediente")
            {

                ASPxGridView control = (ASPxGridView)sender;
                if (control.Selection.Count>0)
                {
                   var datosExpediente =  control.GetSelectedFieldValues("IdExpediente");
                    string numeroExpediente = datosExpediente[0].ToString().Replace("/","-");
                    rutaArchivosRoot = "~/GNArchivosRoot/"+ numeroExpediente;
                    fmArchivosControl.Settings.RootFolder = rutaArchivosRoot;
                    //fmArchivosControl.Refresh();
                    
                }
                return;
            }

        }

        protected void gvExpedientes_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
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

        protected void gvExpedientes_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {

        }

        //protected void ppOrdenNuevoExpediente_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        //{
        //    if (e.Parameter == "NuevoExpediente")
        //    {
        //        dtFechaIngresoNuevo.Date = DateTime.Now;
        //        return;

        //    }

        //    if (e.Parameter == "guardar")
        //    {
        //        Expedientes nuevoRegistro = new Expedientes();
        //        nuevoRegistro.IdExpediente = "";
        //        nuevoRegistro.numReciboPago = txtNumReciboNuevo.Text;
        //        nuevoRegistro.numReciboPago2 = "";

        //        nuevoRegistro.FechaIngreso = dtFechaIngresoNuevo.Date;
        //        nuevoRegistro.IdActo = Convert.ToInt32(cbActosNuevo.Value.ToString());
        //        nuevoRegistro.Otorga = txtOtorgaNuevo.Text;
        //        nuevoRegistro.AfavorDe = txtAfavorDeNuevo.Text;
        //        nuevoRegistro.OperacionProyectada = txtOperacionProyectadaNuevo.Text;
        //        nuevoRegistro.UbicacionPredio = txtUbicacionPredioNuevo.Text;
        //        nuevoRegistro.Faltantes = txtDocumentoFaltantesNuevo.Text;

        //        if (string.IsNullOrEmpty(txtDocumentoFaltantesNuevo.Text))
        //        {
        //            nuevoRegistro.IdEstatus = "EX1";
        //        }
        //        else
        //        {
        //            nuevoRegistro.IdEstatus = "EX2";
        //        }



        //        if (datosCrud.AltaExpediente(nuevoRegistro))
        //        {

        //            // 2024-01-22 creamos la carpetas necesarias para la gestion del expediente.

                    

        //            string directorioVirtual = "~/GNArchivosRoot";
        //            string directorioFisico = MapPath(directorioVirtual);




        //            string rutaFisicaCalculada = Path.Combine(directorioFisico, nuevoRegistro.IdExpediente);

        //            if (!Directory.Exists(rutaFisicaCalculada))
        //            {
        //                Directory.CreateDirectory(rutaFisicaCalculada);

        //                if (Directory.Exists(rutaFisicaCalculada))
        //                {
        //                    string carpetaAvisos = Path.Combine(rutaFisicaCalculada, "Avisos");
        //                    string carpetaFirmados = Path.Combine(rutaFisicaCalculada, "Firmados");
        //                    string carpetaPendientesFirma = Path.Combine(rutaFisicaCalculada, "PedientesFirma");
        //                    string carpetaProyecto = Path.Combine(rutaFisicaCalculada, "Proyecto");
        //                    string carpetaDocumentos = Path.Combine(rutaFisicaCalculada, "Documentos");

        //                    Directory.CreateDirectory(carpetaAvisos);

        //                    Directory.CreateDirectory(carpetaFirmados);

        //                    Directory.CreateDirectory(carpetaPendientesFirma);

        //                    Directory.CreateDirectory(carpetaProyecto);

        //                    Directory.CreateDirectory(carpetaDocumentos);




        //                }

        //            }


        //            ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Nuevo expediente: " + nuevoRegistro.IdExpediente + " listo.!";
        //            ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;
        //            return;
        //        }
        //        else
        //        {

        //            ppOrdenNuevoExpediente.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar guardar el registro.";
        //            ppOrdenNuevoExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;
        //            return;
        //        }



        //    }

        //}

        //protected void cbActosNuevo_DataBinding(object sender, EventArgs e)
        //{
        //    ASPxComboBox control = (ASPxComboBox)sender;

        //    control.ValueField = "IdActo";
        //    control.TextField = "TextoActo";
        //    control.DataSource = catActos;
        //}

        protected void gvAvisoPreventivo_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;

        }


        protected void gvProyecto_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvFirmas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvAvisoDefinitivo_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvEscrituracion_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }



        protected void gvEntregas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = (ASPxGridView)sender;
            string numExpediente = detailGrid.GetMasterRowKeyValue().ToString();
            var result = lsExpediente.Where(x => x.IdExpediente == numExpediente).ToList();
            detailGrid.DataSource = result;
        }

        protected void ppCambiarEstatus_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "CargarEstados")
            {

                string numExpedinte = gvExpedientes.GetSelectedFieldValues("IdExpediente")[0].ToString();


                txtProyecSelecEstatus.Text = numExpedinte;

                rbEstados.DataBind();
                return;

            }
        }

        protected void ppEditarExpediente_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (e.Parameter == "CargarRegistros")
            {
                RegistroExistente = new Expedientes();

                string numExpediente = gvExpedientes.GetSelectedFieldValues("IdExpediente")[0].ToString();

                RegistroExistente = datosCrud.ConsultaExpediente(numExp: numExpediente);

                if (RegistroExistente != null)
                {

                    // cargamos los campos en el form layaout

                    //Expediente 
                    txtNumExpediente.Text = RegistroExistente.IdExpediente;

                    txtExfnOtorga.Text = RegistroExistente.Otorga;
                    txtEXfnAfavorde.Text = RegistroExistente.AfavorDe;

                    //Aviso preventivo
                    dtAPfnFechaElaboracion.Date = RegistroExistente.FechaElaboracion;
                    dtAPfnFechaEnvioAlRPP.Date = RegistroExistente.FechaEnvioRPP;
                    chkAPfnEsTramitePorSistema.Checked = RegistroExistente.EsTramitePorSistema;
                    dtAPfnFechaPagoBoleta.Date = RegistroExistente.FechaPagoBoleta;
                    dtAPfnFechaRecibido.Date = RegistroExistente.FechaRecibidoRPP;

                    //Proyecto
                    cbPRfnProyectista.Value = RegistroExistente.NombreProyectista;
                    cbPRfnProyectista.SelectedIndex = catProyectistas.FindIndex(w => w.Nombre == RegistroExistente.NombreProyectista);

                    dtPRfnFechaAsignacionProyectista.Date = RegistroExistente.FechaAsignacionProyectista;
                    dtPRfnFechaPrevistaTermino.Date = RegistroExistente.FechaPrevistaTerminoProyectista;
                    dtPRfnFechaAvisoPreventivo.Date = RegistroExistente.FechaAvisoPreventivo;
                    txtPRfnISR.Value = RegistroExistente.ISR;

                    //Firmas
                    txtFIfnNotasFirmas.Text = RegistroExistente.NotasFirma;
                    txtFIfnNumEscritura.Value = RegistroExistente.Escritura;
                    txtFIfnNumVolumen.Value = RegistroExistente.Volumen;
                    chkFIfnAplicaTraslado.Checked = RegistroExistente.AplicaTraslado;
                    dtFIfnFechaRecepcionTerminoEscritura.Date = RegistroExistente.FechaRecepcionTerminoEscritura;

                    //Aviso definitivo
                    dtAdfnFechaElaboracion.Date = RegistroExistente.FechaElaboracionDefinitivo;
                    dtAdfnFechaEnvioRPP.Date = RegistroExistente.FechaEnvioRPPDefinitivo;
                    chkAdfnEsTramitePorSistema.Checked = RegistroExistente.EsTramitePorSistemaDefinitivo;
                    dtAdfnFechaPagoBoleta.Date = RegistroExistente.FechaPagoBoletaDefinitivo;
                    dtAdfnFechaRecibido.Date = RegistroExistente.FechaRecibidoRPPDefinitivo;

                    //Escrituracion 
                    dtEsfnRecibioTraslado.Date = RegistroExistente.FechaRecibioTraslado;
                    dtAdfnFechaAsignacionMesa.Date = RegistroExistente.FechaAsignacionMesa;
                    dtAdfnFechaTerminoTramite.Date = RegistroExistente.FechaTerminoMesa;

                    //Entregas
                    txtEnfnObservacionesEntrega.Text = RegistroExistente.ObservacionesEngrega;
                    chkEnfnRegistroSolicitado.Checked = RegistroExistente.RegistroEntrega;
                    dtEnfnFechaRegistro.Date = RegistroExistente.FechaRegistroEntrega;
                    dtEnfnFechaBoletaPago.Date = RegistroExistente.FechaBoletaPagoRegistroEntrega;
                    dtEnfnFechaRegresoRegistro.Date = RegistroExistente.FechaRegresoRegistro;
                    dtEnfnFechaSalida.Date = RegistroExistente.FechaSalida;
                    txtEnfnObservacionesSobreTramiteTerminado.Text = RegistroExistente.ObservacionesTramiteTerminado;

                }


                return;
            }

            if (e.Parameter == "guardarCambios")
            {
                if (RegistroExistente != null)
                {
                                      




                    RegistroExistente.Otorga = txtExfnOtorga.Text;
                    RegistroExistente.AfavorDe = txtEXfnAfavorde.Text;

                    RegistroExistente.UbicacionPredio = txtExfnUbicacionPredio.Text;


                    //Aviso preventivo
                    RegistroExistente.FechaElaboracion = dtAPfnFechaElaboracion.Date;
                    RegistroExistente.FechaEnvioRPP = dtAPfnFechaEnvioAlRPP.Date;
                    RegistroExistente.EsTramitePorSistema = chkAPfnEsTramitePorSistema.Checked;
                    RegistroExistente.FechaPagoBoleta = dtAPfnFechaPagoBoleta.Date;
                    RegistroExistente.FechaRecibidoRPP = dtAPfnFechaRecibido.Date;

                    //Proyecto
                    RegistroExistente.NombreProyectista = cbPRfnProyectista.Value == null ? "" :  cbPRfnProyectista.Value.ToString();
                    RegistroExistente.FechaAsignacionProyectista = dtPRfnFechaAsignacionProyectista.Date;
                    RegistroExistente.FechaPrevistaTerminoProyectista = dtPRfnFechaPrevistaTermino.Date;
                    RegistroExistente.FechaAvisoPreventivo = dtPRfnFechaAvisoPreventivo.Date;
                    RegistroExistente.ISR= txtPRfnISR.Value ==null ?0 : Convert.ToDecimal(txtPRfnISR.Value.ToString());

                    //Firmas
                    RegistroExistente.NotasFirma = txtFIfnNotasFirmas.Text;
                    RegistroExistente.Escritura = txtFIfnNumEscritura.Value == null ? 0 : Convert.ToInt32(txtFIfnNumEscritura.Value.ToString()); // validar nulos
                    RegistroExistente.Volumen = txtFIfnNumVolumen.Value == null ? 0 :  Convert.ToInt32(txtFIfnNumVolumen.Value.ToString());
                    RegistroExistente.AplicaTraslado = chkFIfnAplicaTraslado.Checked;
                    RegistroExistente.FechaRecepcionTerminoEscritura = dtFIfnFechaRecepcionTerminoEscritura.Date;

                    //Aviso definitivo
                    RegistroExistente.FechaElaboracionDefinitivo = dtAdfnFechaElaboracion.Date;
                    RegistroExistente.FechaEnvioRPPDefinitivo = dtAdfnFechaEnvioRPP.Date;
                    RegistroExistente.EsTramitePorSistemaDefinitivo = chkAdfnEsTramitePorSistema.Checked;
                    RegistroExistente.FechaPagoBoletaDefinitivo = dtAdfnFechaPagoBoleta.Date;
                    RegistroExistente.FechaRecibidoRPPDefinitivo = dtAdfnFechaRecibido.Date;

                    //Escrituracion 
                    RegistroExistente.FechaRecibioTraslado = dtEsfnRecibioTraslado.Date;
                    RegistroExistente.FechaAsignacionMesa = dtAdfnFechaAsignacionMesa.Date;
                    RegistroExistente.FechaTerminoMesa = dtAdfnFechaTerminoTramite.Date;

                    //Entregas
                    RegistroExistente.ObservacionesEngrega = txtEnfnObservacionesEntrega.Text;
                    RegistroExistente.RegistroEntrega = chkEnfnRegistroSolicitado.Checked;
                    RegistroExistente.FechaRegistroEntrega = dtEnfnFechaRegistro.Date;
                    RegistroExistente.FechaBoletaPagoRegistroEntrega = dtEnfnFechaBoletaPago.Date;
                    RegistroExistente.FechaRegresoRegistro = dtEnfnFechaRegresoRegistro.Date;
                    RegistroExistente.FechaSalida = dtEnfnFechaSalida.Date;
                    RegistroExistente.ObservacionesTramiteTerminado = txtEnfnObservacionesSobreTramiteTerminado.Text;



                    if (datosCrud.ActualizarExpediente(RegistroExistente))
                    {
                        ppEditarExpediente.JSProperties["cp_swMsg"] = "Registro Modificado!";
                        ppEditarExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.success;
                       
                    }
                    else
                    {

                        ppEditarExpediente.JSProperties["cp_swMsg"] = "Ocurrio un error al intentar Modificar el registro.";
                        ppEditarExpediente.JSProperties["cp_swType"] = Controles.Usuario.InfoMsgBox.tipoMsg.error;
                       
                    }

                }
                return;
            }
        }

        protected void rbEstados_DataBinding(object sender, EventArgs e)
        {
            rbEstados.ValueField = "IdEstatus";
            rbEstados.TextField = "Descripcion";
            rbEstados.DataSource = catEstatus;

        }

        protected void cbExfnActo_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "IdActo";
            control.TextField = "TextoActo";
            control.DataSource = catActos;
        }

        protected void cbPRfnProyectista_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox control = (ASPxComboBox)sender;

            control.ValueField = "Nombre";
            control.TextField = "Nombre";
            control.DataSource = catProyectistas;

        }
        void ApplyRules(FileManagerFolder folder)
        {
            FileManagerFolder[] folders = folder.GetFolders();

         

            for (int i = 0; i < folders.Length; i++)
            {
                FileManagerFolderAccessRule folderEditingRule = new FileManagerFolderAccessRule(folders[i].RelativeName);
                folderEditingRule.Edit = Rights.Deny;
                FileManagerFolderAccessRule folderContentEditingRule = new FileManagerFolderAccessRule(folders[i].RelativeName);
                folderContentEditingRule.EditContents = Rights.Allow;

                fmArchivosControl.SettingsPermissions.AccessRules.Add(folderEditingRule);
                fmArchivosControl.SettingsPermissions.AccessRules.Add(folderContentEditingRule);

                ApplyRules(folders[i]);
            }
        }

        FileManagerFolder root = null;
        FileManagerFolder  GetRootFolder (FileManagerFolder folder) 
        {
           

            if (folder.Parent==null)
            {
                root =folder;
            }
            else
            {
                GetRootFolder(folder.Parent);
            }

            return root;
        }


        protected void ppArchivos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {

        }

        protected void fmArchivosControl_CustomThumbnail(object source, FileManagerThumbnailCreateEventArgs e)
        {
            if (e.File==null)
            {
                return;
            }

            switch (((FileManagerFile)e.Item).Extension)
            {
                case ".pdf":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/application-pdf-2.ico";
                    break;
                case ".doc":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-document.ico";
                    break;
                case ".docx":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-document.ico";
                    break;
                case ".xlsx":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-spreadsheet.ico";
                    break;
                case ".xls":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/x-office-spreadsheet.ico";
                    break;
                case ".png":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".txt":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/text-x-generic.ico";
                    break;
                case ".rtf":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".gif":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".jpeg":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
                case ".jpg":
                    e.ThumbnailImage.Url = "../imagenes/Iconos/image-x-generic.ico";
                    break;
            }
        }
    
    }
}