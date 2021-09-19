function btnOpenEditPersonel() {
    $.ajax({
        type: "POST",
        url: "/Personel/AddEditPersonel",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
