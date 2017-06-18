/*jslint browser:true */
'use strict';

var weatherConditions = new XMLHttpRequest();
var weatherForecast = new XMLHttpRequest();
var cObj;
var fObj;
var posLatitude;
var posLongitude;
    $(document).onload(function() {
        if ("geolocation" in navigator) { //check geolocation available 

            navigator.geolocation.getCurrentPosition(function(position) {
                posLatitude = position.coords.latitude;
                posLongitude = position.coords.longitude;


// GET THE CONDITIONS
//weatherConditions.open('GET', 'http://api.wunderground.com/api/c0a9cfc73b479d78/conditions/q/Sweden/Nordstaden.json', true);
                weatherConditions.open('GET',
                    "http://api.wunderground.com/api/c0a9cfc73b479d78/conditions/q/" +
                    posLatitude +
                    "," +
                    posLongitude +
                    ".json ",
                    true);
                weatherConditions.responseType = 'text';
                weatherConditions.send(null);

                weatherConditions.onload = function() {
                    if (weatherConditions.status === 200) {
                        cObj = JSON.parse(weatherConditions.responseText);
                        console.log(cObj);
                        $("#temperature").html("<img src=" +
                            "\"" +
                            cObj.current_observation.icon_url +
                            "\"/>" +
                            cObj.current_observation.temp_c +
                            "&deg;C" +
                            " " +
                            cObj.current_observation.display_location.city);

                    } //end if
                }; //end function

            });
        } else {
            console.log("Browser doesn't support geolocation!");
        }
    });