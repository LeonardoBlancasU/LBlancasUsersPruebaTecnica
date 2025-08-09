function UpdateStatus(IdOrder) {
    var idOrder = IdOrder;
    $("lblStatus").text('').removeClass('alert alert-success alert-danger');
    var idStatus = $("#ddlStatus").val();
    $.ajax({
        type: 'PUT',
        url: urlUpdateStatus,
        dataType: 'json',

        contentType: 'application/json',
        data: { IdOrder: idOrder, IdStatus: idStatus },
        success: function (result) {
            if (result.Correct) {
                $("#lblStatus"+idOrder).addClass('bi bi-check-all alert alert-success').text('Estatus Actualizado');
            }
            else {
                $("#lblStatus"+idOrder).addClass('bi bi-x-square alert alert-danger').text('Hubo un Error al Actualizar el Estatus');
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}