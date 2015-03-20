(function () {
    var alerta = $.connection.alerta;
    $.connection.hub.logging = true;
    $.connection.hub.start();

    alerta.client.newMessage = function (serverTime) {
        toastr.info(serverTime);
    };
}());