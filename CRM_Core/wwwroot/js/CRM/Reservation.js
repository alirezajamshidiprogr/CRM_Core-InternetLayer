var salonServices = null;
var personnels = [];
var counterReservationReservation = 1;
var fillThisAddedServiceItem = ''
var oneServiceShouldBeThere = ''
var AreYouSureToDeleteCustomerService;
var cmbServiceOptions = '';
var TheEnterTimeHourIsNotValid = '';
var isEditCustomerService = false;
var isEditReservation = false;
var allowedDuplicatedReservation = false;

function btnOpenEditReservation() {
    var reservationId = getValueTableById('ReservationID');
    var isEdit = false;
    if (reservationId > 0)
        isEdit = true;

    $.ajax({
        type: "POST",
        url: "/Reservation/AddEditReservation",
        data: {
            reservationId: reservationId,
            isEdit: isEdit,
        },
        beforeSend: function () {
            enablePageloadding();
        },
        complete: function () {
            disablePageloadding();
        },
        success: function (data) {
            if (data.errorMessage != '' && data.errorMessage != undefined) {
                ErrorMessage(data.errorMessage);
                return;
            }
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function btnAddEditReservation(e, fillMandatoroyMessage) {
    var introducId = $("#PeopleSelector_Id")[0].innerText;

    if (introducId == '') {
        ErrorMessage(customerEmptyMessage);
        return;
    }

    var customerServices = new Array();
    var reservationId = $('#ReservationId').val();

    for (var i = 1; i <= counterReservation; i++) {
        Id = reservationId;
        var peopleSelector_Id = $("#PeopleSelector_Id")[0].innerText;
        var txtOrderDate = $('#txtOrderDate').val();
        var txtDescription = $('#txtDescription').val();
        var txtfromTime = $('#txtFromTime_' + i).val();
        var txttoTime = $('#txtToTime_' + i).val();
        var txtServices = document.getElementById("cmbServieces_" + i).value;
        var isSalonCustomer;
        let elem = "chbCustomerState_" + i;
        if (document.getElementById(elem).hasAttribute("checked"))
            isSalonCustomer = true;
        else
            isSalonCustomer = false;
        customerServices.push({ CustomerId: peopleSelector_Id, P_ReservationDate: txtOrderDate, ClerkServicesId: txtServices, isSalonCustomer: isSalonCustomer, FromTime: txtfromTime, ToTime: txttoTime, Description: txtDescription });
    }

    var isEdit = false;
    if (reservationId != '')
        isEdit = true;


    $.ajax({
        type: "POST",
        url: "/Reservation/AddEditReservationMethod",
        data: {
            isEdit: isEdit,
            reservationDetails: customerServices,
            reservationId: reservationId,
            allowedDuplicatedReservation: allowedDuplicatedReservation ,
        },
        beforeSend: function () {
            EnableProcess(e, true);
        },
        complete: function () {
            EnableProcess(e, false);
            hasIntroduction = false;
            validTelMobile = false;
        },
        success: function (result) {
            if (result.errorMessage != '' && result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            debugger;
            if (!allowedDuplicatedReservation && result.isQuestionMessage != '' && result.isQuestionMessage != undefined) {
                swal({
                    title: question,
                    text: result.message,
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#f44336',
                    cancelButtonColor: '#777',
                    confirmButtonText: yesTitle,
                    cancelButtonText: noTitle
                }).then(function () {
                    allowedDuplicatedReservation = true;
                    btnAddEditReservation(e, fillMandatoroyMessage);
                },
                ).catch(swal.noop);
                return;
            }

            SuccessMessage(result.message);
            counterReservation = 1;
            ShowDashboard(e);
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function showReservationList(e, quickSearch,isSelectMode) {
    debugger;
    var reservation = '';

    if (quickSearch)
        var txtSearchValue = $("#txt-search").val();
    else 
        reservation = {
            CustomerFirstName: $("#txtcustomerName").val(),
            CustomerFamily: $("#txtCustomerFamily").val(),
            FromReservationDate: $("#txtFromReservationDate").val(),
            ToReservationDate: $("#txtToReservationDate").val(),
            SystemCode: $("#txtReservationSystemCode").val(),
            PeopleCode: $("#txtPeopleSystemCode").val(),
            IsExpired: document.getElementById("cmbExpiredStatus").value,
            HasCheque: document.getElementById("cmbHasCheque").value,
            PageNumber: pageNumber,
        };
    GetReservationData(quickSearch, txtSearchValue, reservation, isSelectMode);
}

function GetReservationData(quickSearch, txtSearchValue, reservation,isSelectMode) {
    $.ajax({
        type: "POST",
        url: "/Reservation/FillReservationTableData",
        data: {
            quickSearch: quickSearch,
            fullName: txtSearchValue,
            searchParams: reservation,
            isSelectMode: isSelectMode == '' || isSelectMode == undefined ? false : isSelectMode ,
        },
        beforeSend: function () {
            enablePageloadding();
        },
        complete: function () {
            disablePageloadding();
        },
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            $('#ReservationList').html(result);
            $('#form-reservationSearch').modal('hide');
        },
        error: function (result) {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

function btnShowPeopleServicesList(e) {
    $.ajax({
        type: "GET",
        url: "/Reservation/Index",
        data: {},
        beforeSend: function () {
            enablePageloadding();
        },
        complete: function () {
            disablePageloadding();
        },
        success: function (data) {
            showReservationList(e,true);
            $("#formContainer").html(data);

        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function getCallbackPeopleSelect() {
    enablePageloadding();
    var peopleId = $("#PeopleSelector_Id")[0].innerHTML;
    if (parseInt(peopleId) > 0) {
        $.ajax({
            type: "POST",
            url: "/Reservation/GetPeopleReservationInfo",
            data: { peopleId: peopleId },
            success: function (result) {
                disablePageloadding();
                if (result.errorMessage != '') {
                    ErrorMessage(result.errorMessage);
                    return;
                }
                $("#totalBeCustomer").text(result.getpeopleReservationHistory.countOfBeCustomer);
                $("#totalIncome").text(result.getpeopleReservationHistory.customerIncomeForSalon);
                $("#peopleType").text(result.getpeopleReservationHistory.customerType);
            },
            error: function (httpRequest, textStatus, errorThrown) {
                disablePageloadding();
                ErrorMessage();
            }
        });
    }
}

function btnDeleteReservation() {
    var reservationId = getValueTableById('ReservationID');
    swal({
        title: deleteMessageQuestion,
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: confirmDeleteMessage,
        cancelButtonText: noTitle
    }).then(function () {
        $.ajax({
            type: "POST",
            url: "/Reservation/DeleteReservation",
            data: {
                reservationId: reservationId,
            },
            beforeSend: function () {
                enablePageloadding();
            },
            complete: function () {
                disablePageloadding();
            },
            success: function (result) {
                if (result.errorMessage != '') {
                    ErrorMessage(result.errorMessage);
                    return;
                }
                SuccessMessage(result.message);
                showReservationList(e,true);
            },
            error: function (result) {
                ErrorMessage();
            }
        });

    },
    ).catch(swal.noop);
}

function getPersonnel() {
    $.ajax({
        type: "POST",
        url: "/Reservation/GetClerks",
        data: {
        },
        success: function (result) {
            if (result.result != '') {
                ErrorMessage(result.result);
                return;
            }
            personnels = result.clerk;
            fillPersonnelCombo();
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function fillPersonnelCombo(selectedValue) {
    if (isEditCustomerService && isEditReservation == 'True' ) return;
    else {
        if (counterReservation == "0")
            counterReservation = 1; 

        var comboBox = document.getElementById("cmbPersonnel_" + counterReservation);
        comboBox.innerHTML = '';
        for (var j = 0; j < personnels.length; j++) {
            var option = document.createElement("option");
            option.value = personnels[j].value;
            option.text = personnels[j].text;
            if (selectedValue == personnels[j].value)
                option.setAttribute('selected', 'selected');
            comboBox.appendChild(option);
        }
    }
}

function getServices() {
    $.ajax({
        type: "POST",
        url: "/Reservation/GetSalonServices",
        data: {
        },
        success: function (result) {
            if (result.result != '') {
                ErrorMessage(result.result);
                return;
            }
            salonServices = result.serviceList;
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function fillServiceByPersonnel(e, isOnchangecmbPersonnel) {
    var personnelId;
    let value = e.id.indexOf("_");
    let result = e.id.substr(value + 1, 2);
    var cmbServiceValue = e.value;

    if (isOnchangecmbPersonnel)
        personnelId = document.getElementById(e.id).value;
    else
        personnelId = document.getElementById("cmbPersonnel_" + result).value;

    var comboBox = document.getElementById("cmbServieces_" + result);
    $("#cmbServieces_" + result).html("");
    for (var j = 0; j < salonServices.length; j++) {
        if (salonServices[j].personnelId == parseInt(personnelId) || salonServices[j].personnelId == null) {
            var option = document.createElement("option");
            option.value = salonServices[j].clerkServiceId;
            option.text = salonServices[j].serviceName;
            if (!isOnchangecmbPersonnel && cmbServiceValue == salonServices[j].clerkServiceId)
                option.setAttribute('selected', 'selected');

            comboBox.appendChild(option);
        }
    }

    if (isOnchangecmbPersonnel && !isEditCustomerService)
        fillPersonnelCombo(e.value);
}

function addServiceItemsDiv() {
    var txtfromTime = $('txtFromTime_' + counterReservation).val();
    var txttoTime = $('txtToTime_' + counterReservation).val();
    var txtServices = document.getElementById("cmbServieces_" + counterReservation).value;
    var txtPersonnel = document.getElementById("cmbPersonnel_" + counterReservation).value;

    if (txtfromTime == '' || txttoTime == '' || (txtServices == '' || txtServices == 0) || (txtPersonnel == '' || txtPersonnel == 'null')) {
        ErrorMessage(fillThisAddedServiceItem);
        return;
    }


    var txtfromTime = document.getElementById("txtFromTime_" + counterReservation);
    txtfromTime.setAttribute("value", $("#txtFromTime_" + counterReservation).val());
    var txttoTime = document.getElementById("txtToTime_" + counterReservation);
    txttoTime.setAttribute("value", $("#txtToTime_" + counterReservation).val());

    if (!ValidHhMmTime(txtfromTime.value) || !ValidHhMmTime(txttoTime.value)) {
        ErrorMessage('TheEnterTimeHourIsNotValid');
        return;
    }


    var cmbServiceValue = document.getElementById("cmbServieces_" + counterReservation).value;
    cmbServiceOptions = document.getElementById("cmbServieces_" + counterReservation).options;
    var optionCmbService = new Array();
    for (var i = 0; i < cmbServiceOptions.length; i++) {
        optionCmbService.push({ value: cmbServiceOptions[i].value, text: cmbServiceOptions[i].text });
    }

    var comboBox = document.getElementById("cmbServieces_" + counterReservation);
    $("#cmbServieces_" + counterReservation).html("");
    for (var j = 0; j < optionCmbService.length; j++) {
        var option = document.createElement("option");
        option.value = optionCmbService[j].value;
        option.text = optionCmbService[j].text;
        if (optionCmbService[j].value == cmbServiceValue)
            option.setAttribute('selected', 'selected');

        comboBox.appendChild(option);
    }
    counterReservation = parseInt(counterReservation) + 1;
    var parentDiv = document.getElementById("divServices");
    var divId = "divItem_" + counterReservation;
    var cmbPersonnelId = 'cmbPersonnel_' + counterReservation;
    var cmbServiceId = 'cmbServieces_' + counterReservation;
    var txtFromTime = 'txtFromTime_' + counterReservation;
    var txtToTime = 'txtToTime_' + counterReservation;
    var chbCustomerState = 'chbCustomerState_' + counterReservation;
    var btndeleteItemId = 'btndeleteItem_' + counterReservation;

    var html = `<div id='${divId}' data-repeater-item=''>
                <div class="row justify-content-between">
                <div style="width:17%; float: right; margin-left: 12px;"><select id='${cmbPersonnelId}' onchange="fillServiceByPersonnel(this,true)" class="form-control" aria-invalid="false"></select></div>
                <div style="width:17%; float: right; margin-left: 12px;"><select  id='${cmbServiceId}' onchange="" class="form-control" aria-invalid="false"></select></div>
                <div style="width: 8%; float: right; margin-left: 11px;"><input class="form-control" id='${txtFromTime}' type="text" placeholder="از ساعت" aria-invalid="false" value=""></div>
                <div style="width: 8%; float: right; margin-left: 11px;"><input class="form-control" id='${txtToTime}' type="text" placeholder="تا ساعت" aria-invalid="false" value=""></div>
                <div class="col-md-2 col-sm-12 form-group">
                <label for='txtToTime'>نوع مشتری</label>
                <fieldset>
                <div class="checkbox checkbox-primary checkbox-glow">
                <input type="checkbox" id='${chbCustomerState}' onchange="changeStateCustomer(this)">
                <label for='${chbCustomerState}'>مشتری شخصی</label>
                </div>
                </fieldset>
                </div>
                <div id="id_divRelationsBottons" class="col-md-3 col-sm-12 form-group">
                <icon-button id="btnAddService"><button type="button" style="margin-left:7px;" onclick="addServiceItemsDiv()" class="btn btn-primary"><i class="bx bxs-plus-circle"></i>&nbsp;افزودن </button></icon-button>
                <icon-button><button id=${btndeleteItemId} type="button" style="margin-left:7px;" onclick="deleteServiceItemsDiv(this)" class="btn btn-danger"><i class="bx bxs-minus-circle"></i>&nbsp;حذف </button></icon-button>
                </div>
                </div>
                <hr>
                </div>`;

    isEditCustomerService = false;
    // یه تابع بنوس قبل از اینکه اد کنه سلیکت ایندکس قبلی ها را تشخیص بده و صفر نکنه 
    parentDiv.innerHTML += html;
    fillPersonnelCombo();


    //   fillServiceByPersonnel(null, null, cmbServiceOptions)
}

function changeStateCustomer(e) {
    let elem = e.id;
    if (document.getElementById(elem).hasAttribute("checked"))
        document.getElementById(elem).removeAttribute("checked");
    else
        document.getElementById(elem).setAttribute("checked", "");
}

function deleteServiceItemsDiv(e, isFirstDiv) {
    if (isFirstDiv) {
        ErrorMessage(oneServiceShouldBeThere);
        return;
    }

    swal({
        title: question,
        text: 'آیا از حذف این سرویس به مشتری اطمینان دارید ؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: yesTitle,
        cancelButtonText: noTitle
    }).then(function () {
        let value = e.id.indexOf("_");
        let result = e.id.substr(value + 1, 2);
        var divServices = document.getElementById("divServices");
        var divChild = document.getElementById("divItem_" + result);

        for (var i = 0; i < divServices.childNodes.length; i++) {
            if (divServices.childNodes[i].id == divChild.id) {
                divServices.removeChild(divServices.childNodes[i]);
                counterReservation -= 1;
                break;
            }
        }
    },
    ).catch(swal.noop);
    return;
}

function onTimeChageText(e, state) {
    let value = e.id.indexOf("_");
    let result = e.id.substr(value + 1, 2);
    if (state == 'FromTime') {
        var fromTime = document.getElementById("txtFromTime_" + result);
        fromTime.setAttribute("value", $("#txtFromTime_" + result).val());
    }
    else {
        var toTime = document.getElementById("txtToTime_" + result);
        toTime.setAttribute("value", $("#txtToTime_" + result).val());
    }
}
