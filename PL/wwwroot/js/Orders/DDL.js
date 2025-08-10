function UpdateStatus(input) {
    var idStatus = input.value;
    var idOrder = input.dataset.idorder;
    $("lblStatus").text('').removeClass('alert alert-success alert-danger');
    $.ajax({
        type: 'PUT',
        url: urlUpdateStatus + '?IdOrder=' + idOrder + '&IdStatus=' + idStatus,
        success: function (result) {
            if (result.Correct) {
                $("#lblStatus_"+idOrder).addClass('bi bi-check-all alert alert-success').text('Estatus Actualizado');
            }
            else {
                $("#lblStatus_"+idOrder).addClass('bi bi-x-square alert alert-danger').text('Hubo un Error al Actualizar el Estatus');
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}