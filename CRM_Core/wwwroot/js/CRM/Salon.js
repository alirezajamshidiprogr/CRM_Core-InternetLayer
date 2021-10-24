var costTypeId = "27";

function btnOpenEditSalon() {
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Salon/AddEditSalon",
        data: {},
        success: function (data) {
            disablePageloadding();
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function btnAddEditSalonInfo() {
    var isEdit = false;
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }
    var salonInfo = {
        Id: $("#salonId").val(),
        SalonName: $("#txtSalonName").val(),
        Manager: $("#txtSalonManager").val(),
        Tel: $("#txtTel").val(),
        Mobile: $("#txtMobile").val(),
        Telegram: $("#txtTelegram").val(),
        WhatsApp: $("#txtWhatsApp").val(),
        Instagram: $("#txtInstagram").val(),
        Website: $("#txtWebSite").val(),
        LisenceNumber: $("#txtLisenceNumber").val(),
        Description: $("#txtDescription").val(),
        Address: $("#txtAddress").val(),
    };

    if (salonInfo.Id > 0)
        isEdit = true;

    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Salon/AddEditSalonMethod",
        data: {
            isEdit: isEdit,
            salonInfo: salonInfo
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

function btnOpenEditSalonCost() {
    var salonCostId = getValueTableById('salonCostId');

    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Salon/AddEditSalonCost",
        data: { salonCostId: salonCostId },
        success: function (data) {
            disablePageloadding();
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function onChangeCmbSalonCostGroup(e) {
    var SalonCostGroupId = document.getElementById("cmbSalonCostGroup").value;
    if (parseInt(SalonCostGroupId) > 0) {
        $.ajax({
            type: "POST",
            url: "/Salon/GetCostTypeByCostGroup",
            data: { costGroupId: SalonCostGroupId },
            success: function (data) {
                fillCmbCostType(data);
            },
            error: function (httpRequest, textStatus, errorThrown) {
                ErrorMessage();
            }
        });
    }
}

function onChangeCmbSalonCostType() {
    var cmbCostDetails = document.getElementById("cmbCostDetails");
    cmbCostDetails.innerHTML = '';

    var costType = document.getElementById("cmbSalonCostType").value;

    if (parseInt(costType) == 1)
        $("#GeneralCosts").slideUp(1500).slideDown(1500);
    else if (parseInt(costType) == 2 || parseInt(costType) == 3)
        WarningMessage('این آیتم فعال موقتا غیر فعال است !');
    else
        $("#GeneralCosts").slideUp(1500).hide(1500);

    if (parseInt(costType) > 0) {
        $.ajax({
            type: "POST",
            url: "/Salon/GetCostTypeDetail",
            data: { costType: costType },
            success: function (data) {
                fillCmbCostTypeDetails(data);
            },
            error: function (httpRequest, textStatus, errorThrown) {
                ErrorMessage();
            }
        });
    }


}

function fillCmbCostTypeDetails(data) {
    var cmbCostDetails = document.getElementById("cmbCostDetails");
    cmbCostDetails.innerHTML = '';

    for (var j = 0; j < data.costType.length; j++) {
        var option = document.createElement("option");
        option.value = data.costType[j].id;
        option.text = data.costType[j].name;
        cmbCostDetails.appendChild(option);
    }
}

function fillCmbCostType(data) {
    var cmbCostType = document.getElementById("cmbSalonCostType");
    cmbCostType.innerHTML = '';

    for (var j = 0; j < data.costType.length; j++) {
        var option = document.createElement("option");
        option.value = data.costType[j].id;
        option.text = data.costType[j].name;
        cmbCostType.appendChild(option);
    }
}

function onChangeCmbCostDetails() {
    var cmbVal = document.getElementById("cmbCostDetails").value;
    $('.costDiv').hide(1500);

    switch (parseInt(cmbVal)) {
        case 5:
            costTypeId = cmbVal;
            $("#billLegend").slideUp(1500).slideDown(1500);
            break;
        case 6:
            costTypeId = cmbVal;
            $("#transferLegend").slideUp(1500).slideDown(1500);
            break;
        case 7:
            costTypeId = cmbVal;
            break;
        default:
            costTypeId = "0";
            $("#GeneralCosts").slideUp(1500).slideDown(1500);

    }
}

function btnAddEditSalonCost(btnAddContinue) {
    var isEdit = false;
    var divMandatoryId = '';

    if (costTypeId == "27")
        divMandatoryId = 'GeneralCosts'
    else if (costTypeId == "5")
        divMandatoryId = 'billLegend';
    else if (costTypeId == "6")
        divMandatoryId = 'transferLegend';

    var mandatoryMessage = CheckMandatoryFieldsByDivId(divMandatoryId);
    if (mandatoryMessage != '') {
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }
    var costs = '';
    switch (costTypeId) {
        case "27":
            var costs = {
                CostName: $("#txtCostName").val(),
                Price: $("#txtPrice").val(),
                F_CostDate: $("#txtPayDate").val(),
                Description: $("#txtDescription").val(),
            };
            break;
        case "5":
            var costs = {
                BillType: $("#cmbBillTypeId")[0].value,
                BillIdentity: $("#txtIdentityBillNumber").val(),
                PayIdentity: $("#txtPayBillNumber").val(),
                Price: $("#txtBillPrice").val(),
                F_CostDate: $("#txtBillPayDate").val(),
                Description: $("#txtBillDescription").val(),
            };
            break;
        case "6":
            var costs = {
                TransferType: $("#cmbTransferType")[0].value,
                ToDestination: $("#txtfromTarget").val(),
                FromTarget: $("#txtToDestination").val(),
                Price: $("#txtTransferPrice").val(),
                F_CostDate: $("#txtTransferPayDate").val(),
                Description: $("#txtTransferDescription").val(),
            };
            break;
    }

    $.ajax({
        type: "POST",
        url: "/Salon/AddEditSalonCostsMethod",
        data: {
            isEdit: isEdit,
            salonCosts: costs,
            costType: costTypeId,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            SuccessMessage(result.message);
            if (!btnAddContinue)
                ShowDashboard();
            else
                clearAllInputs();

        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function btnDeleteSalonCosts() {
    var salonCostId = getValueTableById('salonCostId');
    swal({
        title: 'آیا جهت حذف آیتم اطمینان دارید؟',
        text: "این عملیات برگشت پذیر نیست...",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        enablePageloadding();
        $.ajax({
            type: "POST",
            url: "/Salon/DeleteSalonCostsMethod",
            data: {
                salonCostId: salonCostId,
            },
            success: function (result) {
                if (result.result != '') {
                    ErrorMessage(result.result);
                    return;
                }
                disablePageloadding();
                SuccessMessage(result.message);
                ShowSalonCostsList(true);
            },
            error: function (result) {
                disablePageloadding();
                ErrorMessage();
            }
        });

    },
    ).catch(swal.noop);
}

function ShowSalonCostsList(quickSearch) {
    enablePageloadding();
    var salonCostSearchItems = '';
    var costTypeId; 
    if (document.getElementById("cmbCostDetails").value == "0")
        costTypeId = document.getElementById("cmbSalonCostType").value;
    else
        costTypeId = document.getElementById("cmbCostDetails").value;


    salonCostSearchItems = {
        TBASSalonCostId: costTypeId,
        FromPrice: $("#txtFromPrice").val(),
        ToPrice: $("#txtToPrice").val(),
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val(),
        PageNumber: pageNumber,
        CostDescription: quickSearch == "true"? $("#txt-search").val() : $("#txtCostDescription").val(),
    };
    $.ajax({
        type: "POST",
        url: "/Salon/FillSalonCostsTableData",
        data: { searchParams: salonCostSearchItems},
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            disablePageloadding();
            $('#salonCostList').html(result);
            $('#form-salonCostSearch').modal('hide');
        },
        error: function () {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

function btnShowSalonCostsList() {
    enablePageloadding();
    $.ajax({
        type: "GET",
        url: "/Salon/IndexSalonCosts",
        data: {},
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