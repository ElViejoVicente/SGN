(function () {

    function VerAgencias(s, e) {

        var id = document.getElementById("BtIDExpedicion").text;
        document.getElementById("HidIdexpedicion").value = id;
        PUp_VerAgencias.Show();
            }
    function AsignarMatriculas(s, e) {
        var id = document.getElementById("HidIdexpedicion").text;
        var url = "..\\Logistica\\AsignacionMat.aspx?initCod=0&idExp=" + id;
        puasignaragencias.SetContentUrl(url);
        puasignaragencias.Show();
    }
    window.VerAgencias = VerAgencias;
    window.AsignarMatriculas = AsignarMatriculas;
})();