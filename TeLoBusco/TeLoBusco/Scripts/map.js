var map;

function initMap(zoom) {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -31.2531394, lng: -61.4937747 },
        zoom: zoom
    });
}

function adaptarTamañoMapa() {
    var h = window.innerHeight;
    var heigth = h * 0.7 + "px";
    $("#map").css("height", heigth);
}

var markersPedidos = [];
function posicionarPedidosMapa(obj) {
    limpiarPedidosMapa();
    var pedidos = obj.Pedidos;
    for (var i = 0; i < pedidos.length; i++) {
        var lat = pedidos[i].latOrigen;
        var lng = pedidos[i].lngOrigen;

        var marker = new google.maps.Marker({
            map: map,
            position: { lat: lat, lng: lng },
            animation: google.maps.Animation.DROP,
            title: 'Pedido',
            icon: {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 6, //tamaño
                strokeColor: '#000000', //color del borde - podría ir 'color' 
                strokeWeight: 1, //grosor del borde 
                fillColor: '#ff0000', //color de relleno
                fillOpacity: 1// opacidad del relleno
            }
        });
        var contentString = '<div id="content">' +
            '<div id="infoPedido">' +
            '<h4 class="firstHeading">Información del pedido</h4>' +
            '<div class="infoPedidoMap">' +
            '<p><b>Cliente: </b>' + pedidos[i].NombreCliente + '</p>' +
            '<p><b>Descripción pedido: </b>' + pedidos[i].DescripcionPedido + '</p>' +
            '<p><b>Observaciones pedido: </b>' + pedidos[i].ObservacionesPedido + '</p>' +
            '<p><b>Dirección origen: </b>' + pedidos[i].DireccionOrigen + '</p>' +
            '<p><b>Dirección destino: </b>' + pedidos[i].DireccionDestino +'</p>' +
            '<p><b>Precio: </b>' + '$' + pedidos[i].Precio + '</p>' + 
            '<a class="btn btn-secondary" href="#">Postularse!</a>' +
            '</div>' +
            '</div>' +
            '</div>' ;
        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });
        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });
        markersPedidos.push(marker);
    }
}

function limpiarPedidosMapa() {
    for (var i = 0; i < markersPedidos.length; i++) {
        markersPedidos[i].setMap(null);
    }
    markersPedidos = [];
}

