function btnOpenEditPersonnel(e) {
    //var personnelId = getValueTableById('PeopleId', e);
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Personel/AddEditPersonnel",
        data: { personnelId: null },
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

function btnAddEditPersonnel() {
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }


    var personnel = {
        Id: $("#PersonnelId").val(),
        PersonnelName: $("#txtPersonnelName").val(),
        PersonnelLastName: $("#txtPersonnelLastName").val(),
        PersonnelFatherName: $("#txtFatherName").val(),
        InsuranceNumber: $("#txtInsuranceNumber").val(),
        Mobile: $("#txtMobile").val(),
        Tel: $("#txtTel").val(),
        TBASAgreementTypeId: document.getElementById("cmbAgreementType").value,
        CertificateCode: $("#txtCertificateCode").val(),
        HomeTel: $("#txtHomeTel").val(),
        P_Birthday: $("#txtBirthDay").val(),
        Description: $("#txtDescription").val(),
        personnelSkilsViewModel: new Array(),
    };


    //personnel.personnelSkilsViewModel.push();
  
    var isEdit = false;
    if (personnel.Id > 0)
        isEdit = true;
    $.ajax({
        type: "POST",
        url: "/Personel/AddEditPersonnelMethod",
        data: {
            isEdit: isEdit,
            personnelViewModel: personnel,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            SuccessMessage(result.message);
            ShowDashboard();

        },
        error: function (result) {
            ErrorMessage();
        }
    });


}
