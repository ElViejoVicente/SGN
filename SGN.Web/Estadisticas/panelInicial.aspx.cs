using DevExpress.Utils;
using SGN.Negocio.Estadistica;
using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web.Estadisticas
{
    public partial class panelInicial : PageBase
    {
        DatosEstadisticas datosEstadisticas = new DatosEstadisticas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDatos();
            }

        }

        private void CargaDatos()
        {
            // Aquí puedes cargar los datos necesarios para el panel inicial
            // Por ejemplo, cargar estadísticas, gráficos, etc.
            // chartEstadisticaActosSimple.DataSource = ObtenerDatosEstadisticos();
            // chartEstadisticaActosSimple.DataBind();

            chartEstadisticaActosSimple.ToolTipEnabled = DefaultBoolean.True;

            var serie = chartEstadisticaActosSimple.Series[0];

            serie.Points.Clear();

            chartEstadisticaActosSimple.Titles[0].Text = "Operativa actual, Notaria 01, Año: " + DateTime.Now.Year.ToString();

            
            List<ListaEstatusExpedientes> listaEstatusExpedientes = new List<ListaEstatusExpedientes>();

            listaEstatusExpedientes= datosEstadisticas.DameEstatusExpedientesXAnual(DateTime.Now.Year);


            foreach (var item in listaEstatusExpedientes)
            {
                var punto = new DevExpress.XtraCharts.SeriesPoint(item.NombreEstatus , item.NumExpedientes );
                //punto.Tag = item.IdEstatus;
                serie.Points.Add(punto);
            }

            return;

        }


        protected void chartEstadisticaActosSimple_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {

        }
    }
}