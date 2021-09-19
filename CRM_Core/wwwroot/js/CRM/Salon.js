function btnOpenEditSalon() {
    $.ajax({
        type: "POST",
        url: "/Salon/AddEditSalon",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}


