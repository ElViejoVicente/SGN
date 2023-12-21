(function () {

    function ActualizaBotones() {
        btasigna.SetEnabled(lbAgencias.GetItemCount() > 0);
        btquita.SetEnabled(LB_AgenciasSeleccionadas.GetItemCount() > 0);
        btasignatodo.SetEnabled(lbAgencias.GetSelectedItems().length > 0);
        btquitatodo.SetEnabled(LB_AgenciasSeleccionadas.GetSelectedItems().length > 0);
    }
    function AddItems() {
        MoveSelectedItems(lbAgencias, LB_AgenciasSeleccionadas);
        
        ActualizaBotones();
    }
    function AddAllItems() {
        MoveAllItems(lbAgencias, LB_AgenciasSeleccionadas);
        ActualizaBotones();
    }
    function RemoveSelectedItems() {
        MoveSelectedItems(LB_AgenciasSeleccionadas, lbAgencias);
        ActualizaBotones();
    }
    function RemoveAllItems() {
        MoveAllItems(LB_AgenciasSeleccionadas, lbAgencias);
        ActualizaBotones();
    }
    

    function MoveSelectedItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        dstListBox.BeginUpdate();
        var items = srcListBox.GetSelectedItems();
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            dstListBox.AddItem(items[i].text, items[i].value);
            srcListBox.RemoveItem(items[i].index);
        }
        srcListBox.EndUpdate();
        dstListBox.EndUpdate();
    }
    function MoveAllItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        var count = srcListBox.GetItemCount();
        for (var i = 0; i < count; i++) {
            var item = srcListBox.GetItem(i);
            dstListBox.AddItem(item.text, item.value);
        }
        srcListBox.EndUpdate();
        srcListBox.ClearItems();
    }

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
    function Refrescaexpedientes(s, e)
    {
        __doPostBack("Refrescaexpedientes", "true");
        Callback.PerformCallback();
    }

    window.ActualizaBotones = ActualizaBotones;
    window.AddItems = AddItems;
    window.AddAllItems = AddAllItems;
    window.RemoveSelectedItems = RemoveSelectedItems;
    window.RemoveAllItems = RemoveAllItems;
    window.AsignarExpedicion = AsignarExpedicion;
    window.FiltrarSeleccionados = FiltrarSeleccionados;
    window.VerAgencias = VerAgencias;
    window.AsignarMatriculas = AsignarMatriculas;
    window.Refrescaexpedientes = Refrescaexpedientes;
  
})();
