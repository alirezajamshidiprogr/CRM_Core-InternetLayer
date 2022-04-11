var txtTelPlaceHolder = '';
var txtDescriptionPlaceHolder = '';
var phoneTelsType = [];
var fillTelPhoneFields = '';

function btnOpenEditContacts() {
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Contacts/AddEditContact",
        data: {},
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

function getPhoneTelTypes() {
    $.ajax({
        type: "POST",
        url: "/Contacts/GetTelPhoneType",
        data: {
        },
        success: function (result) {
            if (result.result != '') {
                ErrorMessage(result.result);
                return;
            }
            phoneTelsType = result.phoneTelTypes;
            //fillCmbPhoneTelType();
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function btnAddEditContact(e) {
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }

    var isEdit = false;

    var contactInfo = {
        Id: $("#contactId").val(),
        FirstName: $("#txtName").val(),
        LastName: $("#txtFamily").val(),
        Tel: $("#txtTel").val(),
        Mobile: $("#txtMobile").val(),
        //otherInfoContacts
        FatherName: $("#txtFatherName").val(),
        Job: $("#txtJob").val(),
        P_BirthDay: $("#txtBirthDay").val(),
        Description: $("#txtDescription").val(),
        Province: $("#txtProvince").val(),
        City: $("#txtCity").val(),
        Area: $("#txtArea").val(),
        Street: $("#txtStreet").val(),
        Alley: $("#txtAlley").val(),
        OtherAddress: $("#txtOtherAddress").val(),
        //virtualRelations
        Email: $("#txtEmail").val(),
        WhatsApp: $("#txtWhatsApp").val(),
        Telegram: $("#txtTelegram").val(),
        Instagram: $("#txtInstagram").val(),
        YouTube: $("#txtYoutube").val(),
        WebSiteWebSite: $("#txtWebSite").val(),
    };

    if ($("#contactId").val() > 0)
        isEdit = true;

    var otherRelationShips = new Array();

    debugger;

    var parentDivTels = document.getElementById("divPhoneTels");
    var lenTels = parentDivTels.getElementsByClassName("TelItems").length;
    for (var i = 1; i <= lenTels; i++) {
        var cmbTelPhoneType = document.getElementById("cmbTelType_" + i).value;
        var txtValue = document.getElementById("txtTel_" + i).value;
        var txtDescription = document.getElementById("txtDescription_" + i).value;
        if (cmbTelPhoneType != '' && txtValue != '' )
            otherRelationShips.push({ TBASTelTypeId: cmbTelPhoneType, TelValue: txtValue, Description: txtDescription });
    }

    $.ajax({
        type: "POST",
        url: "/Contacts/AddEditContactMethod",
        data: {
            isEdit: isEdit,
            contactInfo: contactInfo,
            otherRelationShips: otherRelationShips,
        },
        beforeSend: function () {
            EnableProcess(e, true);
        },
        complete: function () {
            EnableProcess(e, false);
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            SuccessMessage(result.message);
            disablePageloadding();
            ShowDashboard();

        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function btnContactSearchClick() {
    //var reservationId = $("#reservationId")[0].innerText;;
    //if (reservationNumber == '') {
    //    ShowMandatoryMessage(fillReservationNumber);
    //    return;
    //}

    $.ajax({
        type: "POST",
        url: "/Contacts/FillContactItemData",
        data: {
        },
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            disablePageloadding();
            $('#contactData').html(result);
        },
        error: function () {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

