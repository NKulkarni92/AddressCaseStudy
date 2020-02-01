google.maps.event.addDomListener(window, 'load', function () {  
    var input = document.getElementById('destination');
    var places = new google.maps.places.Autocomplete(input);


});