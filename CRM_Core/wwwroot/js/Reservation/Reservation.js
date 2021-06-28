var salonServices = null;
var customerServices = [];
var hasComboClerks = false;
var optionCustomerType = ["مشتری سالن", "مشتری شخصی"];
var newSalonService = [];

function btnOpenEditReservation() {
    $.ajax({
        type: "POST",
        url: "/Reservation/AddEditReservation",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function loadItemsSalonServicesDiv(serviceId) {
    debugger;
    var arrayIndex = serviceId > 0 ? findIndexArrayWithAttr(salonServices, 'id', serviceId) : null;
    var clerkId = arrayIndex > 0 ? salonServices[arrayIndex].clerkId : document.getElementById("cmbClerk").value;

    if (clerkId > 0) {

        if (arrayIndex == null) {
            newSalonService = [];
            var length = salonServices.length;
            for (var i = 0; i < length; i++) {
                if (salonServices[i].clerkId == clerkId)
                    newSalonService.push({ id: salonServices[i].id, name: salonServices[i].name, clerkId: salonServices[i].clerkId });
            }
        }

        if (arrayIndex != null) {
            newSalonService.push({ id: salonServices[arrayIndex].id, name: salonServices[arrayIndex].name, clerkId: salonServices[arrayIndex].clerkId });
        }

    }
    else if (salonServices != null && clerkId == '') {
        for (var i = 0; i < salonServices.length; i++) {
             newSalonService.push({ id: salonServices[i].id, name: salonServices[i].name, clerkId: salonServices[i].clerkId });
        }
    }
    else {
        $.ajax({
            type: "POST",
            url: "Reservation/GetSalonServices",
            data: {
                clerkId: clerkId
            },
            success: function (result) {
                if (result.result != '') {
                    ErrorMessage(result.result);
                    return;
                }
                salonServices = result.serviceList;
                AddItemsToDivSalonService(salonServices);

            },
            error: function (result) {
                ErrorMessage();
            }
        });
    }

    if (customerServices != null && arrayIndex == null) { // از خدمات دیو کارمند حذف شود چون در لیست خدمات مشتری وجود دارد
        debugger;
        for (var i = 0; i < customerServices.length; i++) {
            var arrayIndex = findIndexArrayWithAttr(newSalonService, 'id', customerServices[i].id);
            if (arrayIndex > -1)
                newSalonService.splice(arrayIndex, 1);
        }
    }

    AddItemsToDivSalonService(newSalonService);

}

function AddItemsToDivSalonService(salonServices) {
    var parentDiv = document.getElementById("salonServices");
    parentDiv.innerHTML = "";
    var count = 1;
    for (var i = 0; i < salonServices.length; i++) {
        var divServiceDetails = document.createElement('div'); // parent element div 
        divServiceDetails.className = 'divServiceDetails';

        // add hidden field item
        var hiddenField = document.createElement("input");
        hiddenField.id = "serviceId" + count;
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("value", salonServices[i].id);

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
        service.innerHTML = salonServices[i].name;
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
    debugger;
    if (!isdelete) {
        customerServices.push({ id: array[arrayIndex].id, clerkId: array[arrayIndex].clerkId, name: array[arrayIndex].name });
    }
    var parentDiv = document.getElementById("customerServices");
    var count = 1;
    parentDiv.innerHTML = "";

    for (var i = 0; i < customerServices.length; i++) {
        var divServiceDetails = document.createElement('div');
        divServiceDetails.className = 'divServiceDetails';
        divServiceDetails.style.width = '554px !important';

        // add hidden field item
        var hiddenField = document.createElement("input");
        hiddenField.id = "serviceId" + count;
        hiddenField.className = "specialFields";
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("value", customerServices[i].id);

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
        service.innerHTML = customerServices[i].name;
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
        var id = e.currentTarget.id;
        var serviceId = document.getElementById(id).parentElement.childNodes[0].attributes.value;
        var arrayIndex = findIndexArrayWithAttr(salonServices, 'id', serviceId.value);

        // DONT CHANGE THE ORDER OF THESE THREE LINES BELLOW 
        addItemsToCustomerServiceDiv(salonServices, arrayIndex, false);
        salonServices.splice(arrayIndex, 1);
        AddItemsToDivSalonService(salonServices);

    }
    else {
        var id = e.currentTarget.id;
        var serviceId = document.getElementById(id).parentElement.childNodes[0].attributes.value;
        var arrayIndex = findIndexArrayWithAttr(customerServices, 'id', serviceId.value);
        document.getElementById('cmbClerk').selectedIndex = SelectComboIndexByValue(customerServices[arrayIndex].clerkId, 'cmbClerk');;
        customerServices.splice(arrayIndex, 1);
        addItemsToCustomerServiceDiv(customerServices, arrayIndex, true);
        loadItemsSalonServicesDiv();
    }
}

function getClerkCombo(e) {
    var target = $(e.target).attr("href")
    if (target == '#Services') {
        if (!hasComboClerks) {
            $.ajax({
                type: "POST",
                url: "Reservation/GetClerks",
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
    var children = parent.childNodes; 
    var getCustomerServices = [];

    for (var i = 0; i < children.length; i++) {
        for (var j = 0; j < children[i].childNodes.length; j++) {
            if (children[i].childNodes[j].className == 'specialFields') {
                getCustomerServices.push({ reservationId: children[i].childNodes[j].value, isSalonCustomer: true });
            }
        }
    }

    var reservationFields = {
        Id: $("#ReservationId").val(),
        PeopleId:4,
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
    debugger;

    $.ajax({
        type: "POST",
        url: "Reservation/AddEditReservationMethod",
        data: {
            isEdit: isEdit,
            reservation: reservationFields,
            peopleServices: getCustomerServices,
        },
        success: function (result) {
            if (result.result != '') {
                ErrorMessage(result.result);
                return;
            }

            SuccessMessage(result.message);
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}