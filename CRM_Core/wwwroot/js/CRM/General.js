var currentDivId = '';
var pageNumber = 0;
var reminderList = new Array();
var reminderIdArray = new Array();
var hasShownRemidner = false;
var offDisplayinReminder = false;
//setInterval(function () {
//    GetCurrentDayDataServer();
//}, 60000);

setInterval(function () {
    DisplayCurrentDayData();
}, 5000);

function onClickInput(e) {
    var inputId = e.id;
    var inputs = document.getElementById('#span' + inputId);
    //for (var j = 0; j < clearMandatorySpan.length; j++) {
    //    if (clearMandatorySpan[j].className == 'icon-info')
    //        inputs[i].parentElement.removeChild(inputs[i].parentElement.lastChild)
    //}
}

function CheckMandatoryFields() {
    var message = '';
    var requiredShape = '';
    var inputs = document.getElementsByTagName('input');

    for (var i = 0; i < inputs.length; i += 1) {
        var parent = inputs[i].parentElement;
        var clearMandatorySpan = inputs[i].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpan.length; j++) {
            if (clearMandatorySpan[j].className == 'icon-info')
                inputs[i].parentElement.removeChild(inputs[i].parentElement.lastChild)
        }
        var isRequired = inputs[i].required;
        var idInputs = '#' + inputs[i].id;

        /// CHECKING REQUIRED FIELDS 
        if (isRequired && $(idInputs).val() == '') {
            if (message == '') {
                message = ' برخی از فیلد ها اجباری هستند.  ' + ' </br>'
            }
            var td = inputs[i].parentElement.innerHTML;
            requiredShape = '<span id="span' + inputs[i].id + '" class="icon-info" style="color:red;font-size:20px;"></span>'
            parent.innerHTML = ' ';
            parent.innerHTML = td + '  ' + requiredShape;

            message += '<b>' + inputs[i].placeholder + '</b>' + '  را وارد نکرده اید!!!  ' + "<br />";
        }

        /////////////// CHECKING NATIONALCODE FIELDS FORMAT
        let result = inputs[i].hasAttribute('masked');
        if (result) {
            if (inputs[i].attributes.masked.value == 'NationalCode' && nationalCodeValidation($(idInputs).val()) == false && $(idInputs).val() != '')
                message += "فرمت وارده شده " + '<b>' + inputs[i].placeholder + '</b>' + " صحیح نیست !!!" + '</br>';
            //var isValidNationalCode = vmsNationalCode($(idInputs).val());

            /////////////// CHECKING MOBILE FIELDS FORMAT
            if (inputs[i].attributes.masked.value == 'Mobile' && mobileValidation($(idInputs).val()) == false && $(idInputs).val() != '')
                message += "فرمت وارده شده " + '<b>' + inputs[i].placeholder + '</b>' + " صحیح نیست !!!" + '</br>';

            /////CHECK DATA FIELD VALUE
            if (inputs[i].attributes.masked.value == 'Date' && !(dateValidation($(idInputs).val())) && $(idInputs).val() != '')
                message += 'فرمت وارده شده برای ' + '<b>' + inputs[i].placeholder + '</b>' + " صحیح نیست !!!" + '</br>';
        }
    }

    var comboBox = document.getElementsByTagName('select');

    for (var k = 0; k < comboBox.length; k++) {
        var parentCombo = comboBox[k].parentElement;
        var clearMandatorySpanCombo = comboBox[k].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpanCombo.length; j++) {
            if (clearMandatorySpanCombo[j].className == 'icon-info')
                comboBox[k].parentElement.removeChild(comboBox[k].parentElement.lastChild)
        }

        let result = comboBox[k].hasAttribute('required');
        if (result) {
            var comboId = comboBox[k].attributes.id.value;
            var index = document.getElementById(comboId).selectedIndex;
            if (comboBox[k].attributes.required.value == 'required' && index == 0) {
                var parentCombo = comboBox[k].parentElement;
                var tdCombo = comboBox[k].parentElement.innerHTML;
                requiredShapeCombo = '<span id="span' + comboBox[k].id + '" class="icon-info" style="color:red;font-size:20px;"></span>'
                parentCombo.innerHTML = ' ';
                parentCombo.innerHTML = tdCombo + '  ' + requiredShapeCombo;
                message += '<b>' + comboBox[k].attributes.placeholder.value + '</b>' + " اجباری است لطفا آیتمی را انتخاب کنید !!!" + '</br>';

            }
        }

    }
    return message;
}

function CheckMandatoryFieldsByDivId(divId) {
    if (currentDivId != '') {
        var clearMandatoryHiddenDiv = document.getElementById(currentDivId).getElementsByTagName('input');

        for (var i = 0; i < clearMandatoryHiddenDiv.length; i += 1) {
            var parent = clearMandatoryHiddenDiv[i].parentElement;
            var clearMandatorySpan = clearMandatoryHiddenDiv[i].parentElement.childNodes;
            for (var j = 0; j < clearMandatorySpan.length; j++) {
                if (clearMandatorySpan[j].className == 'icon-info')
                    clearMandatoryHiddenDiv[i].parentElement.removeChild(clearMandatoryHiddenDiv[i].parentElement.lastChild)
            }
        }
    }
    currentDivId = divId;

    ///////////////////////////////////
    var message = '';
    var requiredShape = '';
    var inputs = document.getElementById(divId).getElementsByTagName('input');

    for (var i = 0; i < inputs.length; i += 1) {
        var parent = inputs[i].parentElement;
        var clearMandatorySpan = inputs[i].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpan.length; j++) {
            if (clearMandatorySpan[j].className == 'icon-info')
                inputs[i].parentElement.removeChild(inputs[i].parentElement.lastChild)
        }
        var isRequired = inputs[i].required;
        var idInputs = '#' + inputs[i].id;

        /// CHECKING REQUIRED FIELDS 
        if (isRequired && $(idInputs).val() == '') {
            if (message == '') {
                message = ' برخی از فیلد ها اجباری هستند.  ' + ' </br>'
            }
            var td = inputs[i].parentElement.innerHTML;
            requiredShape = '<span id="span' + inputs[i].id + '" class="icon-info" style="color:red;font-size:20px;"></span>'
            parent.innerHTML = ' ';
            parent.innerHTML = td + '  ' + requiredShape;

            message += '<b>' + inputs[i].placeholder + '</b>' + '  را وارد نکرده اید!!!  ' + "<br />";
        }

    }

    var comboBox = document.getElementsByTagName('select');

    for (var k = 0; k < comboBox.length; k++) {
        var parentCombo = comboBox[k].parentElement;
        var clearMandatorySpanCombo = comboBox[k].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpanCombo.length; j++) {
            if (clearMandatorySpanCombo[j].className == 'icon-info')
                comboBox[k].parentElement.removeChild(comboBox[k].parentElement.lastChild)
        }

        let result = comboBox[k].hasAttribute('required');
        if (result) {
            var comboId = comboBox[k].attributes.id.value;
            var index = document.getElementById(comboId).selectedIndex;
            if (comboBox[k].attributes.required.value == 'required' && index == 0) {
                var parentCombo = comboBox[k].parentElement;
                var tdCombo = comboBox[k].parentElement.innerHTML;
                requiredShapeCombo = '<span id="span' + comboBox[k].id + '" class="icon-info" style="color:red;font-size:20px;"></span>'
                parentCombo.innerHTML = ' ';
                parentCombo.innerHTML = tdCombo + '  ' + requiredShapeCombo;
                message += '<b>' + comboBox[k].attributes.placeholder.value + '</b>' + " اجباری است لطفا آیتمی را انتخاب کنید !!!" + '</br>';

            }
        }

    }

    return message;
}

function mobileValidation(mobile) {
    var regexMobile = /^(\+98|0098|98|0)?9\d{9}$/;
    if (regexMobile.test(mobile))
        return true;
    else
        return false;
}

function nationalCodeValidation(input) {
    if (!/^\d{10}$/.test(input)
        || input == '0000000000'
        || input == '1111111111'
        || input == '2222222222'
        || input == '3333333333'
        || input == '4444444444'
        || input == '5555555555'
        || input == '6666666666'
        || input == '7777777777'
        || input == '8888888888'
        || input == '9999999999')
        return false;
    var check = parseInt(input[9]);
    var sum = 0;
    var i;
    for (i = 0; i < 9; ++i) {
        sum += parseInt(input[i]) * (10 - i);
    }
    sum %= 11;
    return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
}

function dateValidation(input) {
    var patt = /(13|14)([0-9][0-9])\/(((0?[1-6])\/((0?[1-9])|([12][0-9])|(3[0-1])))|(((0?[7-9])|(1[0-2]))\/((0?[1-9])|([12][0-9])|(30))))/g;
    var result = patt.test(input);
    if (result) {
        var pos = input.indexOf('/');
        var year = input.substring(0, pos);
        var nextPos = input.indexOf('/', pos + 1);
        var month = input.substring(pos + 1, nextPos);
        var day = input.substring(nextPos + 1);
        if (month == 12 && (year + 1) % 4 != 0 && day == 30) { // kabise = 1379, 1383, 1387,... (year +1) divides on 4 remains 0
            result = false;
        }
        return result;
    }
}

function getValueTableById(relativeId) {
    var val = null;
    //var child = event.target.parentNode.parentNode.childNodes;
    var child = event.target.parentNode.parentNode.parentNode.childNodes;
    for (var i = 0; i < child.length; i++) {
        if (child[i].className != 'undefiend' && child[i].className == "specialTd") {
            //var specialFields = child[i].childNodes;
            var specialFields = child[i].childNodes[0].firstElementChild.className;
            //if (specialFields[i].className != 'undefiend' && specialFields[i].className == "specialFields") {
            if (specialFields != 'undefiend' && specialFields == "specialFields") {
                getId = child[i].childNodes[0].firstElementChild.id;
                if (getId == relativeId) {
                    //val = specialFields[i].innerHTML;
                    val = child[i].childNodes[0].firstElementChild.innerHTML;
                    break;
                }
            }
        }
    }
    return val;
}

function concatColonToLables() {
    var value = document.getElementsByClassName('labelWidget').length;
    for (var i = 0; i < value; i++) {
        var innerHtml = document.getElementsByClassName('labelWidget')[i].innerHTML;
        document.getElementsByClassName('labelWidget')[i].innerHTML = innerHtml + ' : ';
    }

}

function btnDisplayHomePage() {
    window.open("/Home/Index", target = "_self");
}

function ShowDashboard() {
    $.ajax({
        type: "GET",
        url: "/Dashboard/Index",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });
}

function findIndexArrayWithAttr(array, attr, value) {
    for (var i = 0; i < array.length; i += 1) {
        if (array[i][attr] == value) {
            return i;
        }
    }
    return -1;
}

function logOutApp() {
    swal({
        title: 'آیا قصد خروج از برنامه را دارید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، خارج شو. '
    }).then(function () {
        //window.open("/Login/IndexLogout", target = "_self");
        window.location.href = "/Login/IndexLogout";
    },

    ).catch(swal.noop);
}

function btnLogin() {
    var UserName = $("#UserName").val();
    var Password = $("#Password").val();
    $("#divLoaderLogin").show();
    $.ajax({
        url: "/Login/validateuser",
        type: "POST",
        data: {
            UserName: UserName,
            Password: Password,
        },
        success: function (response) {
            if (response.success) {
                window.location.href = "/Home/Index";
                $("#divLoaderLogin").hide();
            }
            else
                ErrorMessage(response.errorMessage);
            $("#divLoaderLogin").hide();
        },
        error: function (httpRequest, textStatus, errorThrown) {

        }
    });
}

function btnAddEditProfile() {
    $.ajax({
        type: "POST",
        url: "/Dashboard/AddEditProfile",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function SelectComboIndexByValue(value, cmbName) {
    var selectedIndex = null;
    var getValue = value;
    var options = document.getElementById(cmbName).options;
    for (var i = 0, n = options.length; i < n; i++) {
        var val = options[i].attributes.value;
        if (val.value == getValue) {
            //document.getElementById(cmbName).selectedIndex = i;
            selectedIndex = i;
            break;
        }
    }
    return selectedIndex;
}

function enablePageloadding() {
    $("#divLoader").show();
    var element = document.getElementById('MainDivPage');
    element.style.opacity = "0.2";
    document.getElementById("page-content").style.pointerEvents = "none";
}

function disablePageloadding() {
    document.getElementById("page-content").style.pointerEvents = "auto";
    $("#divLoader").hide();
    var element = document.getElementById('MainDivPage');
    element.style.opacity = "1";
}

//function formatMoney(number, decPlaces, decSep, thouSep) {
//    decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 0 : decPlaces,
//        decSep = typeof decSep === "undefined" ? "." : decSep;
//    thouSep = typeof thouSep === "undefined" ? "," : thouSep;
//    var sign = number < 0 ? "-" : "";
//    var i = String(parseInt(number = Math.abs(Number(number) || 0).toFixed(decPlaces)));
//    var j = (j = i.length) > 3 ? j % 3 : 0;

//    return sign +
//        (j ? i.substr(0, j) + thouSep : "") +
//        i.substr(j).replace(/(\decSep{3})(?=\decSep)/g, "$1" + thouSep) +
//        (decPlaces ? decSep + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
//}

function separateNumAsMoney(value, input) {
    /* seprate number input 3 number */
    var nStr = value + '';
    nStr = nStr.replace(/\,/g, "");
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    if (input !== undefined) {

        input.value = x1 + x2;
    } else {
        return x1 + x2;
    }
}

function isValueNumber(value) {
    if (isNaN(value))
        return true;
    else
        return false;
}

function clearAllInputs() {
    var inputs = document.getElementsByTagName('input');
    var textArea = document.getElementsByTagName('textarea');
    var select = document.getElementsByTagName('select');

    for (var i = 0; i < textArea.length; i += 1) {
        var clearMandatorySpan = textArea[i].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpan.length; j++) {
            if (clearMandatorySpan[j].className == 'icon-info')
                textArea[i].parentElement.removeChild(textArea[i].parentElement.lastChild)
        }
        var idtextArea = '#' + textArea[i].id;
        $(idtextArea).val('');
    }


    for (var i = 0; i < inputs.length; i += 1) {
        var clearMandatorySpan = inputs[i].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpan.length; j++) {
            if (clearMandatorySpan[j].className == 'icon-info')
                inputs[i].parentElement.removeChild(inputs[i].parentElement.lastChild)
        }
        var idInputs = '#' + inputs[i].id;
        $(idInputs).val('');
    }

    for (var i = 0; i < select.length; i += 1) {
        var clearMandatorySpan = select[i].parentElement.childNodes;
        for (var j = 0; j < clearMandatorySpan.length; j++) {
            if (clearMandatorySpan[j].className == 'icon-info')
                select[i].parentElement.removeChild(select[i].parentElement.lastChild)
        }
        var idselect = select[i].id;
        if (document.getElementById(idselect) != null && document.getElementById(idselect) != undefined)
            document.getElementById(idselect).selectedIndex = 0;
    }
}

function ChangingGridPage(state, actionName, page_Number, totalAllRecords) {

    switch (state) {
        case "forward":
            pageNumber = (parseInt(totalAllRecords) % 10) == 0 ? parseInt(totalAllRecords) / 10 : parseInt((parseInt(totalAllRecords) / 10));
            break;
        case "right":
            pageNumber = parseInt(page_Number) - 1;
            break;
        case "left":
            pageNumber = parseInt(page_Number) + 1;
            break;
        default:
            pageNumber = 0;
            break;
    }
    //window["Show" + actionName + "sList"(true)]();
    ShowSalonCostsList(true);
}

function chbClickState(chbId) {
    var elem = document.getElementById(chbId).offsetParent;
    var className = elem.className;
    var isChecked = false;
    if (className == "icheckbox_square-grey") {
        document.getElementById(chbId).offsetParent.className = "icheckbox_square-grey checked";
        isChecked = true;
    }
    else {
        document.getElementById(chbId).offsetParent.className = "icheckbox_square-grey";
        isChecked = false;
    }


    if (chbId == "chbRepeated")
        onChangechbRepeatedClick(isChecked);
}

function GetCurrentDayDataServer() {
    $.ajax({
        type: "POST",
        url: "/Home/GetCurrentDayInfo",
        data: {},
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            for (var i = 0; i < result.reminderListData.length; i++)
                reminderList.push({ id: result.reminderListData[i].id, personnel: result.reminderListData[i].personnel, time: result.reminderListData[i].time, reminderTitle: result.reminderListData[i].reminderTitle });
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function DisplayCurrentDayData() {
    var hasAlertItem = false;
    var date = new Date();
    var options = { hour12: false };
    var getCurrentTime = date.toLocaleTimeString('en-US', options);
    var reminderElem = document.getElementById("divAlertItem");

    //reminderElem.style.display = 'none';

    if (reminderList.length > 0 && reminderList != undefined) {
        reminderElem.innerHTML = '';

        var divHeader = document.createElement('div');
        divHeader.className = 'item';
        divHeader.innerHTML = "<div><i class='fa fa-bell' aria-hidden='true' style='font-size: 47px;margin-bottom: 15px;color: darksalmon;'></i>";
        divHeader.innerHTML += "<span style='font-size:14px;color:black;'>" + "یادآور های ساعت :" + reminderList[0].time +"</span></div>";

        reminderElem.appendChild(divHeader);

        if (reminderList == undefined) return;

        for (var i = 0; i < (reminderList.length) + 4; i++) {
            for (var j = 0; j < reminderIdArray.length; j++) {
                if (reminderIdArray[j].id == reminderList[i].id) return;
            }
            var timeReminder = new Date("2020 08 08 " + reminderList[0].time);
            var timeCurrent = new Date("2020 08 08 " + getCurrentTime);
            var date1_ms = timeReminder.getMinutes();
            var date2_ms = timeCurrent.getMinutes();
            var date1_hours = timeReminder.getHours();
            var date2_hours = timeCurrent.getHours();

            if (offDisplayinReminder && (date2_ms - date1_ms == 0 || date2_ms - date1_ms == 1)) return;

            if (hasShownRemidner == true || ((date2_hours == date1_hours) && (date2_ms - date1_ms == 0 || date2_ms - date1_ms == 1))) {
                hasShownRemidner = true;
                offDisplayinReminder = false ;
                hasAlertItem = true;
                var alertItems = document.createElement('div');
                alertItems.className = 'alertItems';
                //alertItems.innerHTML += "<button onclick='SetReminderOff(this)' style='width: 108px;float:right;' class='btn btn-danger curve'><i class='fa fa-bell-slash-o' aria-hidden='true' style='font-size:22px; color:rebeccapurple;text-align:right;'></i>لغو هشدار<div class='paper-ripple'><div class='paper-ripple__background' style='opacity: 0.00216; '></div><div class='paper - ripple__waves'></div></div><div class='paper-ripple'><div class='paper-ripple__background' style='opacity: 0.0076;'></div><div class='paper-ripple__waves'></div></div></button>";
                alertItems.innerHTML += "<span style='color: rebeccapurple;text-align:center;font-size: 17px;'>" + reminderList[0].reminderTitle + "</span>";
                alertItems.innerHTML += "<span class='reminderId' style='display:none ;'>" + reminderList[0].id + "</span>";
                reminderElem.appendChild(alertItems);
            }
        }

        if (hasShownRemidner == true || ((date2_hours == date1_hours) && (date2_ms - date1_ms == 0 || date2_ms - date1_ms == 1))) {
            var dismissDiv = document.createElement('div');
            dismissDiv.innerHTML += "<button onclick='SetReminderOff()' style='width: 100%; float:left ;' class='btn btn-success curve'><i class='fa fa-bell-slash-o' aria-hidden='true' style='font-size: 22px;color: rebeccapurple;text-align: right;'></i>لغو یادآور ها<div class='paper-ripple'><div class='paper-ripple__background' style='opacity: 0.00216;'></div><div class='paper-ripple__waves'></div></div><div class='paper-ripple'><div class='paper-ripple__background' style='opacity: 0.0076;'></div><div class='paper-ripple__waves'></div></div></button>";
            reminderElem.appendChild(dismissDiv);
            $("#divAlertItem").slideDown(1500);
        }

        if (hasAlertItem)
            reminderElem.style.display = 'block';
    }
}

function SetReminderOff() {
    offDisplayinReminder = true;
    hasShownRemidner = false;
    $("#divAlertItem").fadeOut(1500);

}