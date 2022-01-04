function btnOpenEditContacts() {
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Contacts/AddEditContact",
        data: { },
        success: function (data) {
            disablePageloadding();
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            disablePageloadding();
            ErrorMessage();
        }
    });
}