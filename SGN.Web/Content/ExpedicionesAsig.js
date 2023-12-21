(function () {

    function VerAgencias(s, e) {

       
       PUp_VerAgencias.Show();
    }
    function AsignarMatriculas(s, e) {
        var id = document.getElementById("BtIDExpedicion").text;
        var url = "..\\Logistica\\AsignacionMat.aspx?initCod=0&idExp=" + id;
        
        
        puasignaragencias.Show();
    }
   
    function Refrescaexpedientes(s, e) {
        __doPostBack("Refrescaexpedientes", s);      
    }

    window.VerAgencias = VerAgencias;
    window.AsignarMatriculas = AsignarMatriculas;
    window.Refrescaexpedientes = Refrescaexpedientes;
    
})();