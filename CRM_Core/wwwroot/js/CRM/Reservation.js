var salonServices = null;
var customerServices = [];
var hasComboClerks = false;
var optionCustomerType = ["مشتری سالن", "مشتری شخصی"];
var newSalonService = [];
var repeatedItemsService = '';
var customerServiceIsNullMessage = '';
var customerEmptyMessage = '';

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

function loadItemsSalonServicesDiv(serviceId) {
    var arrayIndex = serviceId > 0 ? findIndexArrayWithAttr(salonServices, 'id', serviceId) : null;
    var clerkId = arrayIndex > 0 ? salonServices[arrayIndex].clerkServiceId : document.getElementById("cmbClerk").value;

    if (clerkId > 0) {
        newSalonService = [];
        var length = salonServices.length;
        for (var i = 0; i < length; i++) {
            if (salonServices[i].clerkId == clerkId)
                newSalonService.push({ TBASServiceId: salonServices[i].tbasServiceId, clerkServiceId: salonServices[i].clerkServiceId, serviceName: salonServices[i].serviceName, ClerkId: salonServices[i].clerkId });
        }
        AddItemsToDivSalonService(newSalonService);
        return;
    }
    getSalonServices(clerkId);
}

function getSalonServices(clerkId) {
    if (salonServices == null) {
        $.ajax({
            type: "POST",
            url: "/Reservation/GetSalonServices",
            data: {
                clerkId: clerkId
            },
            success: function (result) {
                if (result.result != '') {
                    ErrorMessage(result.result);
                    return;
                }
                salonServices = result.serviceList;
                AddItemsToDivSalonService(result.serviceList);

            },
            error: function (result) {
                ErrorMessage();
            }
        });
    }
    AddItemsToDivSalonService(salonServices);
}

function AddItemsToDivSalonService(salonServices) {
    var parentDiv = document.getElementById("salonServices");
    parentDiv.innerHTML = "";
    var count = 1;
    if (salonServices == null) return;

    document.getElementById("pSalonService").innerHTML = 'خدمات سالن ' + "- تعداد : " + salonServices.length;;
    document.getElementById("pCustomerService").innerHTML = 'خدمات مشتری ' + "- تعداد : " + customerServices.length;;

    for (var i = 0; i < salonServices.length; i++) {
        var divServiceDetails = document.createElement('div'); // parent element div 
        divServiceDetails.className = 'divServiceDetails';

        //// add hidden field item
        //var hiddenField = document.createElement("input");
        //hiddenField.id = "serviceId" + count;
        //hiddenField.setAttribute("type", "hidden");
        //hiddenField.setAttribute("value", salonServices[i].tbasServiceId);

        //divServiceDetails.appendChild(hiddenField);
        ////////// 

        // add hidden field item
        var hiddenField = document.createElement("input");
        hiddenField.id = "clerkServiceId" + count;
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("value", salonServices[i].clerkServiceId);

        divServiceDetails.appendChild(hiddenField);
        ////////

        //add row number 
        var row = document.createElement("p");
        row.style.width = "30px";
        row.style.float = "right";
        row.innerHTML = count + ")";
        divServiceDetails.appendChild(row);
        ///////

        // add serviceTitle item
        var service = document.createElement("p");
        service.style.width = "299px";
        service.style.float = "right";
        service.innerHTML = salonServices[i].serviceName;
        divServiceDetails.appendChild(service);
        //////////////


        // add service button select 
        var button = document.createElement("input");
        button.setAttribute("type", "button");
        button.className = "btn btn-success";
        button.style.width = "75px";
        button.id = "btnAddItem" + count;
        button.addEventListener("click", function (e) {
            refreshDivItemsService(e, salonServices, 'add');
        });
        button.value = "اضافه";
        divServiceDetails.appendChild(button);
        //////////

        parentDiv.appendChild(divServiceDetails);

        count++;
    }
}

function addItemsToCustomerServiceDiv(array, arrayIndex, isdelete) {
    if (!isdelete) {
        customerServices.push({ TBASServiceId: array[arrayIndex].tbasServiceId, clerkServiceId: array[arrayIndex].clerkServiceId, serviceName: array[arrayIndex].serviceName, ClerkId: array[arrayIndex].clerkId });
    }
    var parentDiv = document.getElementById("customerServices");
    var count = 1;
    parentDiv.innerHTML = "";
    document.getElementById("pCustomerService").innerHTML = 'خدمات مشتری ' + "- تعداد : " + customerServices.length;;

    for (var i = 0; i < customerServices.length; i++) {
        var divServiceDetails = document.createElement('div');
        divServiceDetails.className = 'divServiceDetails';
        divServiceDetails.style.width = '554px !important';

        // add hidden field item
        var hiddenField = document.createElement("input");
        hiddenField.id = "clerkServiceId" + count;
        hiddenField.className = "specialFields";
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("value", customerServices[i].clerkServiceId);

        divServiceDetails.appendChild(hiddenField);
        //////// 

        //add row number 
        var row = document.createElement("p");
        row.style.width = "30px";
        row.style.float = "right";
        row.innerHTML = count + ")";
        divServiceDetails.appendChild(row);
        ///////

        // add serviceTitle item
        var service = document.createElement("p");
        service.style.width = "212px";
        service.style.float = "right";
        service.innerHTML = customerServices[i].serviceName;
        divServiceDetails.appendChild(service);
        //////////////

        //// add combo customerType
        var comboBox = document.createElement("SELECT");
        comboBox.setAttribute("id", "customerType" + count)
        comboBox.className = "cmbCustomerServiceType";

        for (var j = 0; j < optionCustomerType.length; j++) {
            var option = document.createElement("option");
            option.value = j;
            option.text = optionCustomerType[j];
            comboBox.appendChild(option);
        }
        divServiceDetails.appendChild(comboBox);

        /////

        // add service button select 
        var button = document.createElement("input");
        button.setAttribute("type", "button");
        button.className = "btn btn-danger";
        button.id = "btnDeleteCustomerService" + count;
        button.addEventListener("click", function (e) {
            refreshDivItemsService(e, salonServices, 'delete');
        });
        button.value = "حذف";
        divServiceDetails.appendChild(button);
        //////////

        parentDiv.appendChild(divServiceDetails);

        count++;
    }
}

function refreshDivItemsService(e, salonServices, state) {
    if (state == 'add') {
        debugger;
        var id = e.currentTarget.id;
        var serviceId = document.getElementById(id).parentElement.childNodes[0].attributes.value;
        var arrayIndex = findIndexArrayWithAttr(salonServices, 'clerkServiceId', serviceId.value);
        for (var i = 0; i < customerServices.length; i++) {
            if (serviceId.value == customerServices[i].clerkServiceId) {
                ErrorMessage(repeatedItemsService);
                return;
            }
        }
        addItemsToCustomerServiceDiv(salonServices, arrayIndex, false);

    }
    else {
        var id = e.currentTarget.id;
        var serviceId = document.getElementById(id).parentElement.childNodes[0].attributes.value;
        var arrayIndex = findIndexArrayWithAttr(customerServices, 'clerkServiceId', serviceId.value);
        document.getElementById('cmbClerk').selectedIndex = SelectComboIndexByValue(customerServices[arrayIndex].clerkServiceId, 'cmbClerk');;
        customerServices.splice(arrayIndex, 1);
        addItemsToCustomerServiceDiv(customerServices, arrayIndex, true);
        //loadItemsSalonServicesDiv();
    }
}

function getClerkCombo(e) {
    var target = $(e.target).attr("href")
    if (target == '#Services') {
        $("#AddEditReservation").hide();
        $("#btnCloseReservation").hide();
        if (!hasComboClerks) {
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
                    fillClerks(result.clerk);
                    hasComboClerks = true;

                },
                error: function (result) {
                    ErrorMessage();
                }
            });

        }
    }
    else {
        $("#AddEditReservation").show();
        $("#btnCloseReservation").show();

    }
}

function fillClerks(clerk) {
    $("#cmbClerk").html("");
    for (var i = 0; i < clerk.length; i++) {
        var item = clerk[i];
        $("#cmbClerk").append(
            $("<option></option>").val(item.value).html(item.text)
        );
    }
}

function btnAddEditReservation() {
    var parent = document.getElementById("customerServices");
    var introducId = $("#PeopleSelector_Id")[0].innerText;

    if (introducId == '') {
        ErrorMessage(customerEmptyMessage);
        return;
    }

    var children = parent.childNodes;
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
        var parent = document.getElementById("customerServices");
        if (children[0] != undefined && children[0].nodeName == '#text') {
            mandatoryMessage += customerServiceIsNullMessage;
        }
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }

    var getCustomerServices = [];
    for (var i = 0; i < children.length; i++) {
        for (var j = 0; j < children[i].childNodes.length; j++) {
            if (children[i].childNodes[j].className == 'specialFields')
                clerkServiceId = children[i].childNodes[j].value;
            else if (children[i].childNodes[j].className == 'cmbCustomerServiceType') {
                var cmbCustomerServiceTypeElem = document.getElementById("customerType" + (i + 1));
                var valcmb = cmbCustomerServiceTypeElem.selectedIndex;
                var isSalonCustomer = valcmb == 0 ? true : false;
            }
        }
        getCustomerServices.push({ ClerkServicesId: clerkServiceId, isSalonCustomer: isSalonCustomer });
    }

    var reservationFields = {
        Id: $("#ReservationId").val(),
        PeopleId: $("#PeopleSelector_Id")[0].innerHTML,
        P_ReservationDate: $("#txtOrderDate").val(),
        FromTime: $("#txtFromTimeOrder").val(),
        ToTime: $("#txtToTimeOrder").val(),
        Price: $("#txtFee").val(),
        TBASPayTypeId: $("#cmbPaidType").val(),
        Description: $("#txtDescription").val(),
    };

    var isEdit = false;
    if (reservationFields.Id != '')
        isEdit = true;


    $.ajax({
        type: "POST",
        url: "/Reservation/AddEditReservationMethod",
        data: {
            isEdit: isEdit,
            peopleServices: getCustomerServices,
            reservation: reservationFields,
        },
        success: function (result) {
            debugger;
            if (result.errorMessage != '') {
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