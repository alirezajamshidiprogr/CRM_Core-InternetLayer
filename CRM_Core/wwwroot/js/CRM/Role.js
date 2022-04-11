function btnOpenEditRole(e) {
    //var peopleId = getValueTableById('PeopleId', e);
    hasCallTelsAndMobiles = false;
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Role/AddEditRole",
        data: { roleId: null },
        success: function (data) {
            setTimeout(function () {
                disablePageloadding();
            }, 2000);
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            disablePageloadding();
            ErrorMessage();
        }
    });
}
