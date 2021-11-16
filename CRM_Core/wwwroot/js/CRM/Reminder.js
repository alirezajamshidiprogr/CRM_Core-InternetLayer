var isRepeatedDaysChecked = false;

function btnOpenEditReminder(id) {
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Reminder/AddEditReminder",
        data: { reminderId: id },
        success: function (data) {
            disablePageloadding();
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            disablePageloadding();
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

function btnAddEditReminder(id) {
    var isEdit = false;
    enablePageloadding();
    if (isRepeatedDaysChecked)
        var getWeekDays = getWeekDayReminder();

    var reminderInfo = {
        Id: $("#ReminderId").val(),
        ReminderTitle: $("#txtReminderTitle").val(),
        F_ReminderDate: $("#txtDate").val(),
        Time: $("#txtTime").val(),
        ToPersonelId: $("#cmbSendTo").val(),
        Description: $("#txtDescription").val(),
        IsRepeatReminder: isRepeatedDaysChecked,
    };
    if (reminderInfo.Id > 0)
        isEdit = true;

    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/Reminder/AddEditReminderMethod",
        data: {
            isEdit: isEdit,
            days: getWeekDays,
            reminderInfo: reminderInfo,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            //GetCurrentDayData(result.reminderListData,'reminder')
            SuccessMessage(result.message);
            disablePageloadding();
            ShowDashboard();
        },
        error: function (result) {
            ErrorMessage();
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

function btnShowReminderList() {
    enablePageloadding();
    $.ajax({
        type: "GET",
        url: "/Reminder/Index",
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

function collapseItemClick(e) {
    var Id;
    if (e.childNodes[1].childNodes[0].className == 'collapseId')
        Id = e.childNodes[1].childNodes[0].innerHTML;
    btnOpenEditReminder(Id);
}

function checkFieldsStatusReminder(isRepeatedReminder, model) {

    var txtDate = document.getElementById('txtDate');
    var chbRepeated = document.getElementById('chbRepeated');
    var divDays = document.getElementById('divDays');

    if (isRepeatedReminder == 'True') {
        isRepeatedDaysChecked = true;
        txtDate.style.opacity = "0.5";
        txtDate.style.pointerEvents = "none";
        chbRepeated.className = "icheckbox_square-grey checked";
        divDays.style.display = 'block';
        var chbSat = document.getElementById('chbSat');
        var chbSun = document.getElementById('chbSun');
        var chbMon = document.getElementById('chbMon');
        var chbTue = document.getElementById('chbTue');
        var chbWens = document.getElementById('chbWens');
        var chbThu = document.getElementById('chbThu');
        var chbFri = document.getElementById('chbFri');

        if (model.Satuarday == true)
            chbSat.className = "icheckbox_square-grey checked";
        if (model.Sunday == true)
            chbSun.className = "icheckbox_square-grey checked";
        if (model.Monday == true)
            chbMon.className = "icheckbox_square-grey checked";
        if (model.Thursday == true)
            chbTue.className = "icheckbox_square-grey checked";
        if (model.Wensday == true)
            chbWens.className = "icheckbox_square-grey checked";
        if (model.Tuesday == true)
            chbThu.className = "icheckbox_square-grey checked";
        if (model.Friday == true)
            chbFri.className = "icheckbox_square-grey checked";

    }
}

function btnCloseReminder() {

}
//function SetReminderOff(e) {
//    debugger; 
//    var parentDiv = e.parentNode;
//    for (var i = 0; i < parentDiv.childNodes.length; i++) {
//        if (parentDiv.childNodes[i].className == "reminderId") 
//                reminderIdArray.push({ id: parentDiv.childNodes[i].innerHTML });
//    }
//    $("#parentDiv").hide(3000);

//}