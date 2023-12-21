(function () {

    

    function AsignarExpedicion() {
        LB_AgenciasSeleccionadas.BeginUpdate();
        LB_AgenciasSeleccionadas.SelectAll();
        var items = LB_AgenciasSeleccionadas.GetSelectedItems();
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            gv_asignacion.PerformCallback(items[i].value, items[i].text);
        }
        LB_AgenciasSeleccionadas.EndUpdate();
    }
    function FiltrarSeleccionados() {
        lbAgencias.filterStrategy.filter = "selected=true";
    }
    function VerAgencias(s, e) {


        // PUp_VerAgencias.Show();
    }
    function AsignarMatriculas(s, e) {

        //  puasignaragencias.Show();
    }
    function Refrescaretornos(s, e) {
        __doPostBack("Refrescaretornos", "true");
        Callback.PerformCallback();
    }

    window.AsignarExpedicion = AsignarExpedicion;
    window.FiltrarSeleccionados = FiltrarSeleccionados;
    window.VerAgencias = VerAgencias;
    window.AsignarMatriculas = AsignarMatriculas;
    window.Refrescaretornos = Refrescaretornos;

})();