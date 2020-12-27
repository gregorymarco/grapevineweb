var data = $(function() {
    navigator.geolocation.getCurrentPosition(showPosition, positionError);
    function showPosition(position){
        var coords = position.coords;
        var data={ lat: JSON.stringify(coords.latitude), lng: JSON.stringify(coords.longitude) };
        $.post("/Home/updateSessionCoords", data);
    }
    function positionError(position){
        alert("Could not get location data");
    }
});
