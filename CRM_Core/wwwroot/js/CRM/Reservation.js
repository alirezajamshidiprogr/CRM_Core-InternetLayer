var salonServices = null;
var personnels = [];
var counter = 1;
var fillThisAddedServiceItem = ''
var oneServiceShouldBeThere = ''

function btnOpenEditReservation() {
    var reservationId = getValueTableById('ReservationId');
    var isEdit = false;
    if (reservationId > 0)
        isEdit = true;

    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Reservation/AddEditReservation",
        data: {
            reservationId: reservationId,
            isEdit: isEdit,
        },
        success: function (data) {
            disablePageloadding();
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function btnAddEditReservation(fillMandatoroyMessage) {
    var introducId = $("#PeopleSelector_Id")[0].innerText;

    if (introducId == '') {
        ErrorMessage(customerEmptyMessage);
        return;
    }
   
    var customerServices = new Array();
    var reservationId = $('#ReservationId').val();

    for (var i = 1; i <= counter; i++) {
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
            isSalonCustomer = true ;
        else
            isSalonCustomer = false ;
        customerServices.push({ CustomerId: peopleSelector_Id, P_ReservationDate: txtOrderDate, ClerkServicesId: txtServices, isSalonCustomer: isSalonCustomer, FromTime: txtfromTime, ToTime: txttoTime, Description:txtDescription });
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
        },
        success: function (result) {
            debugger;
            if (result.errorMessage != '' && result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }

            SuccessMessage(result.message);
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function showReservationList(quickSearch, state) {
    enablePageloadding();
    var reservation = '';

    if (quickSearch == 'true')
        var txtSearchValue = $("#txt-search").val();
    else
        reservation = {
            CustomerFirstName: $("#txtcustomerName").val(),
            CustomerFamily: $("#txtCustomerFamily").val(),
            TBASServiceId: $("#cmbServiceType").val(),
            Date: $("#txtDate").val(),
            ReservationSystemCode: $("#txtFromTime").val(),
            FromTime: $("#txtToTime").val(),
            ToTime: $("#txtReservationSystemCode").val(),
            PageNumber: pageNumber,
        };

    $.ajax({
        type: "POST",
        url: "/Reservation/FillReservationTableData",
        data: {
            quickSearch: quickSearch,
            fullName: txtSearchValue,
            searchParams: reservation,
            state: state
        },
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            disablePageloadding();
            $('#ReservationList').html(result);
            $('#form-reservationSearch').modal('hide');
        },
        error: function () {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

function btnShowPeopleServicesList() {
    enablePageloadding();
    $.ajax({
        type: "GET",
        url: "/Reservation/Index",
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
    var reservationId = getValueTableById('ReservationId');
    swal({
        title: deleteMessageQuestion,
        text: thisActionIsNotRestore,
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: confirmDeleteMessage
    }).then(function () {
        enablePageloadding();
        $.ajax({
            type: "POST",
            url: "/Reservation/DeleteReservation",
            data: {
                reservationId: reservationId,
            },
            success: function (result) {
                if (result.result != '') {
                    ErrorMessage(result.result);
                    return;
                }
                disablePageloadding();
                SuccessMessage(result.message);
                showPeopleList(true);
            },
            error: function (result) {
                disablePageloadding();
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
    var comboBox = document.getElementById("cmbPersonnel_" + counter);
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
        debugger;
        if (salonServices[j].personnelId == parseInt(personnelId) || salonServices[j].personnelId == null) {
            var option = document.createElement("option");
            option.value = salonServices[j].clerkServiceId;
            option.text = salonServices[j].serviceName;
            if (!isOnchangecmbPersonnel && cmbServiceValue == salonServices[j].clerkServiceId)
                option.setAttribute('selected', 'selected');

            comboBox.appendChild(option);
        }
    }

    if (isOnchangecmbPersonnel)
        fillPersonnelCombo(e.value);
}

function addServiceItemsDiv() {
    var txtfromTime = $('txtFromTime_' + counter).val(); 
    var txttoTime = $('txtToTime_' + counter).val(); 
    var txtServices = document.getElementById("cmbServieces_" + counter).value;   
    var txtPersonnel = document.getElementById("cmbPersonnel_" + counter).value ; 

    if (txtfromTime == '' || txttoTime == '' || (txtServices == '' || txtServices == 0 ) || (txtPersonnel == '' || txtPersonnel  == 'null' )) {
        ErrorMessage(fillThisAddedServiceItem);
        return;
    }

    counter += 1;
    var parentDiv = document.getElementById("divServices");
    var divId = "divItem_" + counter;
    var cmbPersonnelId = 'cmbPersonnel_' + counter;
    var cmbServiceId = 'cmbServieces_' + counter;
    var txtFromTime = 'txtFromTime_' + counter;
    var txtToTime = 'txtToTime_' + counter;
    var chbCustomerState = 'chbCustomerState_' + counter;
    var chbCustomerState = 'chbCustomerState_' + counter;
    var btndeleteItemId = 'btndeleteItem_' + counter;

    var html = `<div id='${divId}' data-repeater-item=''>
        <div class='row justify-content-between' >
            <div class='col-md-2 col-sm-12 form-group'>
                <label for='personnel'>پرسنل</label>
                <select id='${cmbPersonnelId}' onchange='fillServiceByPersonnel(this,true)' class='form-control'>
                </select>
            </div>
            <div class='col-md-2 col-sm-12 form-group'>
                <label for='cmbServiceId'>خدمات</label>
                <select id='${cmbServiceId}' onchange='fillServiceByPersonnel(this,false)' class='form-control'>
                </select>
            </div>
            <div class='col-md-2 col-sm-12 form-group'>
                <label for='txtFromTime'>از ساعت</label>
                <input class='form-control time' id='${txtFromTime}' type='text' name='time' onchange="onTimeChageText(this,'FromTime')" autocomplete="off">
               </div>
                <div class='col-md-2 col-sm-12 form-group'>
                    <label for='txtFromTime'>تاساعت</label>
                    <input class='form-control time' id='${txtToTime}' type='text' name='time'  onchange="onTimeChageText(this,'ToTime')" autocomplete="off">
                 </div>
                    <div class='col-md-2 col-sm-12 form-group'>
                        <label for='txtToTime'>نوع مشتری</label>
                        <fieldset>
                            <div class="checkbox checkbox-primary checkbox-glow">
                                    <input type='checkbox' id='${chbCustomerState}' onchange="changeStateCustomer(this)">
                                     <label for='${chbCustomerState}'>مشتری شخصی</label>
                               </div>
                        </fieldset>
                   </div>
                        <div class='col-md-2 col-sm-12 form-group d-flex align-items-center pt-2'>
                            <button id='${btndeleteItemId}' class='btn btn-danger text-nowrap px-1' data-repeater-delete='' type='button' onclick='deleteServiceItemsDiv(this)'>
                                <i class='bx bx-x'></i>حذف</button>
                        </div>
                    </div>
                    <hr>
</div>`;



    parentDiv.innerHTML += html;
    fillPersonnelCombo();
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

    let value = e.id.indexOf("_");
    let result = e.id.substr(value + 1, 2);
    var divServices = document.getElementById("divServices");
    var divChild = document.getElementById("divItem_" + result);

    for (var i = 0; i < divServices.childNodes.length; i++) {
        if (divServices.childNodes[i].id == divChild.id) {
            divServices.removeChild(divServices.childNodes[i]);
            counter -= 1;
            break;
        }
    }
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
