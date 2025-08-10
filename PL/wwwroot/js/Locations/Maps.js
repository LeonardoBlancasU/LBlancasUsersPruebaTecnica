let map, marker;

function initMap() {
    let defaultLocation = { lat: 19.4326, lng: -99.1332 }; // CDMX por defecto

    map = new google.maps.Map(document.getElementById("map"), {
        center: defaultLocation,
        zoom: 14
    });

    marker = new google.maps.Marker({
        position: defaultLocation,
        map: map
    });

    map.addListener("click", function (event) {
        let lat = event.latLng.lat();
        let lng = event.latLng.lng();

        marker.setPosition({ lat, lng });

        let geocoder = new google.maps.Geocoder();
        geocoder.geocode({ location: { lat, lng } }, function (results, status) {
            if (status === "OK" && results[0]) {
                let address = results[0].formatted_address;
                let placeId = results[0].place_id;

                document.getElementById("placeId").value = placeId;
                document.getElementById("address").value = address;
                document.getElementById("latitude").value = lat;
                document.getElementById("longitude").value = lng;
            }
        });
    });
}

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("btnGuardar").addEventListener("click", function () {
        let locationData = {
            PlaceId: document.getElementById("placeId").value,
            Address: document.getElementById("address").value,
            Latitude: parseFloat(document.getElementById("latitude").value),
            Longitude: parseFloat(document.getElementById("longitude").value)
        };

        fetch("https://localhost:5001/api/Ubicacion/Add", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(locationData)
        })
            .then(response => {
                if (!response.ok) throw new Error("Error al guardar ubicación");
                return response.json();
            })
            .then(data => {
                alert("Ubicación guardada con éxito");
                window.location.href = "/Ubicacion/GetAll";
            })
            .catch(error => {
                console.error("Error:", error);
                alert("Ocurrió un error al guardar");
            });
    });
});
