function DesactivarEnter(event) {
    if (event.charCode == 13) {
        event.preventDefault();
    }
}

function PrevisualizarYValidarImagen(event) {
    var archivo = event.target.files[0];
    var preview = document.getElementById("imgUsuario");

    if (archivo) {
        var extension = archivo.name.split('.').pop().toLowerCase();
        var extensionesPermitidas = ["jpg", "jpeg", "png", "gif", "bmp"];

        if (extensionesPermitidas.includes(extension)) {
            preview.src = URL.createObjectURL(archivo);
            preview.onload = function () {
                URL.revokeObjectURL(preview.src);
            };
        }
        else {
            alert("Archivo no Valido. Solo se permiten imagenes (JPG, JPEG, PNG, GIF, BMP)")
            event.target.value = "";
            var imagenCargada = preview.getAttribute("src")
            preview.src = imagenCargada;
        }
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

function ValidarPrimerLetraMayuscula(event, inputId, labelId) {
    var input = $("#" + inputId).val();
    var caracter = event.key;
    var regularExpression = /^[A-Z]/;
    if (input.length < 1) {
        if (!regularExpression.test(caracter)) {
            $("#" + labelId).text("Debe comenzar con mayúscula");
            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}

function ConfirmarTextbox(inputId, TextBoxId, labelId) {
    $("#" + TextBoxId).css({ 'border': 'dark', 'color': 'dark' });
    var input = $("#" + inputId).val();
    var textBox = $("#" + TextBoxId).val();
    if (input !== textBox) {
        $("#" + labelId).text("No coinciden");
        $("#" + TextBoxId).css({ 'border': '3px solid red', 'color': 'red' });
        return false;
    } else {
        $("#" + labelId).text("");
        $("#" + TextBoxId).css({ 'border': '3px solid green', 'color': 'green' });
        return true;

function ValidarPassword(inputId, labelId, TextboxId, long) {
    var input = $("#" + inputId).val();
    var confirm = $("#" + TextboxId);
    var span = $("#spanPasswordConfirm");
    var boton = $("#btnPasswordConfirm")
    var repetidos = /(\d)\1/;
    var secuenciaAscendente = /(?:0(?=1)|1(?=2)|2(?=3)|3(?=4)|4(?=5)|5(?=6)|6(?=7)|7(?=8)|8(?=9)){3,}/;
    var secuenciaDescendente = /(?:9(?=8)|8(?=7)|7(?=6)|6(?=5)|5(?=4)|4(?=3)|3(?=2)|2(?=1)|1(?=0)){3,}/;
    if (!/[A-Z]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos una Mayuscula");
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (repetidos.test(input) || secuenciaAscendente.test(input) || secuenciaDescendente.test(input)) {
        $("#" + labelId).text("No debe tener numeros consecutivos o repetidos");
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (!/[a-z]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos una minuscula");
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (!/[@$!%*?&]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos un Caracter Especial (@$!%*?&)");
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (!/\d/.test(input)) {
        $("#" + labelId).text("Debe tener al menos un numero");
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (input.length < long) {
        $("#" + labelId).text(`Debe tener al menos ${long} caracteres`);
        confirm.hide();
        confirm.prop('disabled', true);
        span.hide();
        span.prop('disabled', true);
        boton.hide();
        boton.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else {
        confirm.show();
        confirm.prop('disabled', false);
        span.show();
        span.prop('disabled', false);
        boton.show();
        boton.prop('disabled', false);
        $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
        $("#" + labelId).text("");
    }
}
function ValidarEmail(inputId, TextBoxId, labelId) {
    $("#" + inputId).css({ 'border': 'dark', 'color': 'dark' });
    var input = $("#" + inputId).val();
    var span = $("#spanEmailConfirm");
    var expression = /[a-zA-Z0-9.*%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}/;
    var confirmEmail = $("#" + TextBoxId);
    if (expression.test(input)) {
        confirmEmail.show();
        confirmEmail.prop('disabled', false);
        span.show();
        span.prop('disabled', false)
        $("#" + labelId).text("");
        $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
        return true;
    }
    else {
        confirmEmail.hide();
        confirmEmail.prop('disabled', true);
        span.hide();
        span.prop('disabled', true)
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
        $("#" + labelId).text("Email no valido");
        return false
    }
}
function VerPassword(icon, input) {
    var icono = $("#" + icon);
    var textbox = $("#" + input);

    if (textbox.attr('type') === 'password') {
        textbox.attr('type', 'text');
        icono.removeClass("bi-eye-slash").addClass('bi-eye');
    }
    else {
        textbox.attr('type', 'password');
        icono.removeClass("bi-eye").addClass('bi-eye-slash');
    }
}
function ValidarCampos() {

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    });
}