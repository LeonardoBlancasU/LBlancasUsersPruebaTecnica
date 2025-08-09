function DesactivarEnter(event) {
    if (event.charCode == 13) {
        event.preventDefault();
    }
}
function ValidarSoloLetras(event, LabelId) {
    var noEnter = DesactivarEnter(event);
    var letra = event.key;
    var regularExpression = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]$/;

    if (regularExpression.test(letra)) {
        $("#" + labelId).text("");
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten letras")
        return false;
    }
}
function ValidarSoloNumeros(event, LabelId) {
    var noEnter = DesactivarEnter(event);
    var numero = event.key;
    var regularExpression = /^[0-9]+$/;

    if (regularExpression.test(numero)) {
        $("#" + LabelId).text("")
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten numeros")
        return false;
    }
}
function ValidarNumeroLetrasMay(event, LabelId) {
    var username = event.key;
    var regularExpression = /^[A-Z0-9]+$/;
    if (regularExpression.test(username)) {
        $("#" + LabelId).text("")
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten letras mayusculas y numeros")
        return false;
    }
}
