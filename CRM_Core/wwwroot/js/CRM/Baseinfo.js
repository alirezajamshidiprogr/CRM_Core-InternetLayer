//var customerName = '';
//var reservationDate = '';
//var Description = '';
//var totalPriceCheque = 0;
//var mainTotalPrice = 0;

//function btnOpenEditCheque(e) {
//    //var peopleId = getValueTableById('PeopleId', e);
//    hasCallTelsAndMobiles = false;
//    enablePageloadding();
//    $.ajax({
//        type: "POST",
//        url: "/Cheque/AddEditCheque",
//        data: {},
//        success: function (data) {
//            setTimeout(function () {
//                disablePageloadding();
//            }, 2000);
//            $("#formContainer").html(data);

//        },
//        error: function (httpRequest, textStatus, errorThrown) {
//            disablePageloadding();
//            ErrorMessage();
//        }
//    });
//}

//function btnSearchReservation() {
//    var reservationId = $("#reservationId")[0].innerText;;
//    //if (reservationNumber == '') {
//    //    ShowMandatoryMessage(fillReservationNumber);
//    //    return;
//    //}
//    $.ajax({
//        type: "POST",
//        url: "/Cheque/FillCustomerServicesItemsData",
//        data: {
//            reservationId: 1009
//        },
//        success: function (result) {
//            if (result.errorMessage != undefined) {
//                ErrorMessage(result.errorMessage);
//                return;
//            }
//            disablePageloadding();
//            $('#customerServices').html(result);
//        },
//        error: function () {
//            disablePageloadding();
//            ErrorMessage();
//        }
//    });
//}

//function calcTotalChequeInfo() {
//    debugger;
//    document.getElementById("txtPaied").style.opacity = 1;
//    document.getElementById("txtPaied").style.pointerEvents = "auto";

//    var cmbPaidType = document.getElementById("cmbPaidType").value;
//    if (cmbPaidType == 2) {
//        document.getElementById("txtPaied").style.opacity = 0.5;
//        document.getElementById("txtPaied").style.pointerEvents = "none";
//    }

//    var totalReminderPrice = 0;
//    var txtPaied = parseInt($("#txtPaied").val());
//    var txtDiscount = parseInt($("#txtDiscount").val());

//    document.getElementById("totalChequePrice").innerText = mainTotalPrice;
//    document.getElementById("discount").innerText = txtDiscount;
//    document.getElementById("Paid").innerText = txtPaied;

//    //if (txtDiscount != '' || txtDiscount == '0')
//    //    txtDiscount = 0;

//    totalReminderPrice = txtDiscount == 0 ? mainTotalPrice : mainTotalPrice - ((mainTotalPrice / 100) * txtDiscount);
//    totalReminderPrice = totalReminderPrice - txtPaied;

//    document.getElementById("totalReminderCheque").innerText = totalReminderPrice;

//}

////function fillCustomerInfo(customerNameTitle, reservationDateTitle, descriptionTitle,customerName, reservationDate, description) {
////    debugger;
////    document.getElementById("customerName").innerText = customerNameTitle + " : " + customerName;
////    document.getElementById("ReservationRegisterDate").innerText = reservationDateTitle + " : " + reservationDate;
////    document.getElementById("Description").innerText = descriptionTitle + " : " + description;
////}