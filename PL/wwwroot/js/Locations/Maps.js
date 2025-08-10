let map;
let marker;

function initMap() {
    // Usar coordenadas que vienen desde el modelo
    const initialLatLng = { lat: parseFloat(initialLat), lng: parseFloat(initialLng) };

    map = new google.maps.Map(document.getElementById("map"), {
        center: initialLatLng,
        zoom: 14
    });

    marker = new google.maps.Marker({
        position: initialLatLng,
        map: map,
        draggable: true
    });

    // Llenar inputs con valores iniciales
    updateLatLngInputs(initialLatLng.lat, initialLatLng.lng);

    // Mover pin con drag
    marker.addListener("dragend", function () {
        const pos = marker.getPosition();
        updateLatLngInputs(pos.lat(), pos.lng());
    });

    // Mover pin con click en el mapa
    map.addListener("click", function (event) {
        marker.setPosition(event.latLng);
        updateLatLngInputs(event.latLng.lat(), event.latLng.lng());
    });
}

function updateLatLngInputs(lat, lng) {
    document.getElementById("latitude").value = lat.toFixed(6);
    document.getElementById("longitude").value = lng.toFixed(6);
}
