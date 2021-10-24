var isRepeatedDaysChecked;

function btnOpenEditReminder() {
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Reminder/AddEditReminder",
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

function onChangechbRepeatedClick(isChecked) {
    isRepeatedDaysChecked = isChecked;
    var txtTime = document.getElementById('txtDate');
    if (isChecked) {
        $("#txtDate").val('');
        txtTime.style.opacity = "0.5";
        txtTime.style.pointerEvents = "none";
        $("#divDays").slideDown(1000);
    }
    else {
        txtTime.style.opacity = "1";
        txtTime.style.pointerEvents = "all";
        $("#divDays").slideUp(1000);
    }
}

function btnAddEditReminder() {
    enablePageloadding();
    if (isRepeatedDaysChecked)
        var getWeekDays = getWeekDayReminder();

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

function getWeekDayReminder() {
    var divRepeatedDays = document.getElementById("divDays");
    var days = new Array();
    for (var i = 0; i < divRepeatedDays.childNodes.length; i++) {
        if (divRepeatedDays.childNodes[i].nodeName == "CHECKBOX") {
            classNameCheckBox = divRepeatedDays.childNodes[i].childNodes[0].childNodes[0].childNodes[0].childNodes[0].className;
            if (classNameCheckBox == "icheckbox_square-grey checked") {
                var chbId = divRepeatedDays.childNodes[i].childNodes[0].childNodes[0].childNodes[0].childNodes[0].firstChild.id;
                switch (chbId) {
                    case 'chbSat':
                        days.push({ day: 0 });
                        break;
                    case 'chbSun':
                        days.push({ day: 1 });
                        break;
                    case 'chbMon':
                        days.push({ day: 2 });
                        break;
                    case 'chbThu':
                        days.push({ day: 3 });
                        break;
                    case 'chbWens':
                        days.push({ day: 4 });
                        break;
                    case 'chbTue':
                        days.push({ day: 5 });
                        break;
                    case 'chbFri':
                        days.push({ day: 6 });
                        break;
                }
            }
        }
    }
    return days;
}