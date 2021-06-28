
function getValueTableById(relativeId) {
    var val = null ; 
    var child = event.target.parentNode.parentNode.childNodes;
    for (var i = 0; i < child.length; i++) {
        if (child[i].className != 'undefiend' && child[i].className == "specialTd") {
            var specialFields = child[i].childNodes;
            if (specialFields[i].className != 'undefiend' && specialFields[i].className == "specialFields") {
                getId = specialFields[i].id;
                if (getId == relativeId) {
                    val = specialFields[i].innerHTML;
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
    // $.ajax({
    //    type: "POST",
    //    url: "/People/AddEditPeople",
    //    data: {},
    //    success: function (data) {
    //        //$("#formContainer").html(data);
    //    },
    //    error: function (httpRequest, textStatus, errorThrown) {
    //        alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
    //    }
    //});
}

function ShowDashboard() {
    $.ajax({
        type: "GET",
        url: "Dashboard/Index",
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
        window.open("/Login/IndexLogout", target = "_self");
    },

    ).catch(swal.noop);
}

function btnAddEditProfile() {
    $.ajax({
        type: "POST",
        url: "Dashboard/AddEditProfile",
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
    var selectedIndex = null ;
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
    //$("#txtBirthDay").datepicker({
    //    dateFormat: './dd/mm/yy.html',
    //    dateFormat: 'yy/mm/dd',
    //    altField: '#alternate2',
    //    altFormat: 'DD، d MM yy'
    //});

    //$("#txtBirthDay").datepicker({
    //    dateFormat: './dd/mm/yy.html',
    //    dateFormat: 'yy/mm/dd',
    //    altField: '#alternate2',
    //    altFormat: 'DD، d MM yy'
    //});

