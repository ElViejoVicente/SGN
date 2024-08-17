/*document javascript para creaciòn de mensajes informativos para el usuario del sistema GPB
 * se consume la libreria https://sweetalert.js.org/guides/
 * para mayor informacion recurrir a la doc oficial
 */

//llamar esta funcion cuando la ejecuciòn sea desde un evento callback de devExpress en lugar del control de usuario
function mostrarMensajeSweet(type, message) {
    if (type != null && message != null) {
        console.log("llamando a la funcion del mensaje Sweet...." + "[" + type+"]");
        switch (type) {
            case "info":
                msgInfo(message);
                break;
            case "success": 
                msgSuccess(message);
                break; 
            case "warning": 
                msgWarning(message);
                break; 
            case "error":
                msgError(message);
                break;
            case "preguntar": 
                msgQuestion(message);
                break; 
            default:
                console.log('opcion incorrecta al llamar mensaje sweet');
        }
        
    } else {
        console.log("Parametros CN con valores nulos");
    }

}


function msgInfo(mensaje) {
    console.log(mensaje);
    swal(mensaje, "Información importante!", "info");
}

function msgSuccess(mensaje) {
    console.log(mensaje);
    swal(mensaje, "Proceso realizado con éxito!", "success");
}

function msgWarning(mensaje) {
    console.log(mensaje);
    swal(mensaje, "Precaución!", "warning");
}

function msgError(mensaje) {
    console.log(mensaje);
    swal(mensaje, "Ha ocurrido un error en sistema SGN.", "error");
}

function msgQuestion(mensaje) {
    swal({
        title: "Atención",
        text: mensaje,
        icon: "warning",
        buttons: true,
        dangerMode: false,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Acción realizada con éxito", {
                    icon: "success",
                });
            }
            //else {
            //    swal("Your imaginary file is safe!");
            //}
        });
}