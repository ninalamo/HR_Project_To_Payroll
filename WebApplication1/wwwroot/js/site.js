// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var latitude = 0;
var longitude = 0;
// Write your JavaScript code.
$(document).ready(function () {
    
    getLocation();


    $('#employee').DataTable();


    $('#dtr').DataTable({
            dom: 'Bfrtip',
         pageinfo: "true",
         buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });

        $(".logbutton").on("click", function (e) {
            e.preventDefault(); 


            $('#latitude').val(latitude);
           
            $('#longitude').val(longitude);

            var id = this.id;


            var requestData = {
                EmployeeNumber: $('#employeeid').val(),
                Lat: $('#latitude').val(),
                Long: $('#longitude').val(),
                InOrOut :id
            };


            $.ajax({
                url: '/biologs/login',
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
                   // alert('Error: ' + xhr.statusText);
                },
                success: function (result) {
                    //CheckIfInvoiceFound(result);
                },
                async: true,
                processData: false
            });

            $('#employeeid').val('');
        });

      
     
    });

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
    else { x.innerHTML = "Geolocation is not supported by this browser."; }
}

function showPosition(position) {
    latitude = position.coords.latitude;
    longitude = position.coords.longitude;

    console.log(latitude);
    console.log(longitude);
}

function showError(error) {
    if (error.code === 1) {
        x.innerHTML = "User denied the request for Geolocation."
    }
    else if (err.code === 2) {
        x.innerHTML = "Location information is unavailable."
    }
    else if (err.code === 3) {
        x.innerHTML = "The request to get user location timed out."
    }
    else {
        x.innerHTML = "An unknown error occurred."
    }
}