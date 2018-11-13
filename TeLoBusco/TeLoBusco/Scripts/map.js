var map;

function initMap(zoom, lat, lng) {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: lat, lng: lng },
        zoom: zoom
    });
}

function adaptarTamañoMapa() {
    var h = window.innerHeight;
    var heigth = h * 0.7 + "px";
    $("#map").css("height", heigth);
}

var markersPedidos = [];

function añadirPedido(lat, lng, pedido) {
    var marker = new google.maps.Marker({
        map: map,
        position: { lat: lat, lng: lng },
        animation: google.maps.Animation.DROP,
        title: 'Pedido',
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            scale: 6, //tamaño
            strokeColor: '#000000', //color del borde 
            strokeWeight: 1, //grosor del borde 
            fillColor: '#ff0000', //color de relleno
            fillOpacity: 1// opacidad del relleno
        }
    });

    var urlPostulaciones = "/Pedidos/Postularse?idPedido=" + pedido.IdPedido;

    var contentString = '<div id="content">' +
        '<div id="infoPedido">' +
        '<h4 class="firstHeading">Información del pedido</h4>' +
        '<div class="infoPedidoMap">' +
        '<p><b>Cliente: </b>' + pedido.NombreCliente + '</p>' +
        '<p><b>Descripción pedido: </b>' + pedido.DescripcionPedido + '</p>' +
        '<p><b>Observaciones pedido: </b>' + pedido.ObservacionesPedido + '</p>' +
        '<p><b>Dirección origen: </b>' + pedido.DireccionOrigen + '</p>' +
        '<p><b>Dirección destino: </b>' + pedido.DireccionDestino + '</p>' +
        '<p><b>Precio: </b>' + '$' + pedido.Precio + '</p>' +
        (pedido.Postulado ? '<label style="font-weight: bold; font-size: 16px;"><b>Ya se postuló</b></label>' : '<a class="btn btn-secondary" href="' + urlPostulaciones + '">Postularse!</a>') +
        '</div>' +
        '</div>' +
        '</div>';
    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
    markersPedidos.push(marker);
}

function posicionarPedidosMapa(obj) {
    limpiarPedidosMapa();
    var pedidos = obj.Pedidos;
    for (var i = 0; i < pedidos.length; i++) {
        var lat = pedidos[i].LatOrigen;
        var lng = pedidos[i].LngOrigen;
        añadirPedido(lat, lng, pedidos[i]);     
    }
}

function limpiarPedidosMapa() {
    for (var i = 0; i < markersPedidos.length; i++) {
        markersPedidos[i].setMap(null);
    }
    markersPedidos = [];
}


markerPedidoSeguimiento = [];

function PosicionarPedidoSeguimiento(lat, lng) {
    LimpiarPedidoSeguimiento();

    var marker = new google.maps.Marker({
        map: map,
        position: { lat: lat, lng: lng },
        animation: google.maps.Animation.DROP,
        title: 'Pedido',
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            scale: 6, //tamaño
            strokeColor: '#000000', //color del borde 
            strokeWeight: 1, //grosor del borde 
            fillColor: '#ff0000', //color de relleno
            fillOpacity: 1// opacidad del relleno
        }
    });
    markerPedidoSeguimiento.push(marker);
}

function LimpiarPedidoSeguimiento() {
    for (var i = 0; i < markerPedidoSeguimiento.length; i++) {
        markerPedidoSeguimiento[i].setMap(null);
    }
    markerPedidoSeguimiento = [];
}

markerDelivery = [];
function PosicionarDelivery(obj) {
    LimpiarPosicionarDelivery();

    lat = obj.Posicion.lat;
    lng = obj.Posicion.lng;
    var marker = new google.maps.Marker({
        map: map,
        position: { lat: lat, lng: lng },
        animation: google.maps.Animation.DROP,
        title: 'Delivery',
        icon: '/Images/delivery.png'
    });

    var fecha = new Date(obj.Posicion.FechaHoraUltimaPos)
    fecha.toLocaleDateString("es-ES");

    var dia = fecha.getDate();
    var mes = fecha.getMonth() + 1;
    var año = fecha.getFullYear();

    var hora = fecha.getHours();
    var minutos = fecha.getMinutes();
    var segundos = fecha.getSeconds();

    var fechaHora = dia + '/' + mes + '/' + año + ' ' + hora + ':' + minutos + ':' + segundos;

    var contentString = '<div id="content">' +
        '<div id="infoDelivery">' +
        '<h4 class="firstHeading">Delivery</h4>' +
        '<div>' +
        '<p><b>Última actualización de la posición: </b>' + fechaHora + '</p>' +     
        '</div>' +
        '</div>' +
        '</div>';
    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
    markerDelivery.push(marker);
}

function LimpiarPosicionarDelivery() {
    for (var i = 0; i < markerDelivery.length; i++) {
        markerDelivery[i].setMap(null);
    }
    markerDelivery = [];
}