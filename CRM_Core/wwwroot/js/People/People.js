
var canNotEmptyTel = '';
var canNotEmptyMobile = '';
var hasCallTelsAndMobiles = false;
var tels = '';
var mobiles = '';

function btnOpenEditPeople() {
    debugger;
    var peopleId = getValueTableById('PeopleId');
    hasCallTelsAndMobiles = false;
    $.ajax({
        type: "POST",
        url: "/People/AddEditPeople",
        data: { peopleId: peopleId },
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
            //alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function btnShowPeopleList() {
    $.ajax({
        type: "GET",
        url: "/People/Index",
        data: {},
        success: function (data) {
            $("#formContainer").html(data);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });
}

function showPeopleList(quickSearch) {
    if (quickSearch == true)
        var txtSearchValue = $("#txtPeopleSearch").val();
    $.ajax({
        type: "POST",
        url: "People/FillPeopleTableData",
        data: {
            quickSearch: quickSearch,
            fullName: txtSearchValue
        },
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            $('#peopleList').html(result);
            $('#form-peopleSearch').modal('hide');
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });
}

function btnAddEditPeople() {
    var tels = new Array();
    var mobiles = new Array();
    var checkRepeatedTels = document.getElementById("chbCheckRepeatedTels").checked;
    var checkRepeatedMobiles = document.getElementById("chbCheckRepeatedMobiles").checked;
    // General Info People 
    var people = {
        Id: $("#PeopleID").val(),
        ManualCode: $("#txtManualCode").val(),
        FirstName: $("#txtName").val(),
        LastName: $("#txtFamily").val(),
        CertificateCode: $("#txtCertificateCode").val(),
        Job: $("#txtJob").val(),
        P_Birthday: $("#txtBirthDay").val(),
        P_MariedDate: $("#txtMariedDate").val(),
        Description: $("#txtDescription").val(),
        TBASGraduationId: $("#cmbGraduaction").val(),
        //IntroduceId:$("#cmbPotential").val(),
        TBASIntroductionTypeId: $("#cmbIntroductionType").val(),
        TBASPrefixId: $("#cmbPrefix").val(),
        TBASCategoryId: $("#cmbCategory").val(),
        TBASPotentialId: $("#cmbPotential").val(),
        MarriedType: $("#cmbMarriedType").val(),
    };

    var address = {
        Province: $("#txtProvince").val(),
        City: $("#txtCity").val(),
        Area: $("#txtArea").val(),
        Street: $("#txtStreet").val(),
        Alley: $("#txtAlley").val(),
        OtherAddress: $("#txtOtherAddress").val(),
    };


    ///// PeopleTelsAndMobiles
    var parentDivTels = document.getElementById("divTels");
    var parentDivMobiles = document.getElementById("divMobiles");


    // Tels AND Mobiles
    var lenTels = parentDivTels.getElementsByClassName("input-group").length;
    var lenMobiles = parentDivMobiles.getElementsByClassName("input-group").length;
    for (var i = 1; i < lenTels; i++) {
        var txtCode = document.getElementById("txtCode" + i).value;
        var txtTel = document.getElementById("txtTel" + i).value;
        var txtComment = document.getElementById("txtComment" + i).value;
        if (txtTel != '' && txtTel != null) {
            tels.push({ Code: txtCode, Number: txtTel, Comment: txtComment });
        }

    }
    for (var i = 1; i < lenMobiles; i++) {
        var txtMobile = document.getElementById("txtMobile" + i).value;
        var txtMobComment = document.getElementById("txtMobComment" + i).value;
        if (txtMobile != '' && txtMobile != null) {
            mobiles.push({ Mobile: txtMobile, Comment: txtMobComment });
        }
    }

    /////// People OtherInfo

    ///// PeopleVirtual Info
    var peopleVirtual = {
        WebSite: $("#txtWebSite").val(),
        Telegram: $("#txtTelegram").val(),
        WhatsApp: $("#txtWhatsApp").val(),
        Email: $("#txtEmail").val(),
        Instagram: $("#txtInstagram").val(),
    };
    var isEdit = false;
    if (people.Id > 0)
        isEdit = true;

    $.ajax({
        type: "POST",
        url: "People/AddEditPeopleMethod",
        data: {
            isEdit: isEdit,
            checkRepeatedTels: checkRepeatedTels,
            checkRepeatedMobiles: checkRepeatedMobiles,
            tels: tels,
            mobiles: mobiles,
            peopleVirtual: peopleVirtual,
            address: address,
            people: people,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            SuccessMessage(result.message);
            ShowDashboard();

        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function btnAddEditPeopleContinue() {
    swal(
        'انجام گردید.',
        'عملیات ثبت با موفقیت به پایان رسید.',
        'success');
}

function btnCloseAddEditPeople() {
    swal({
        title: 'آیا اطمینان دارید؟',
        text: "ادامه عملیات لغو می شود...",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، بسته شود. '
    });
}

function btnDeletePeople() {
    //var confrimt = QuestionMessage();
    var peopleId = getValueTableById('PeopleId');
    $.ajax({
        type: "POST",
        url: "People/DeletePeople",
        data: {
            peopleId: peopleId,
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

function AddNewRowTel() {
    var parentDiv = document.getElementById("divTels");
    var len = parentDiv.getElementsByClassName("input-group").length;
    var idCode = "txtCode" + len;
    var idTel = "txtTel" + len;
    var idComment = "txtComment" + len;

    for (var i = len; i >= 2; i--) {
        var num = i - 1;
        var txtTel = document.getElementById("txtTel" + num).value
        if (txtTel == '' || txtTel == null) {
            WarningMessage(canNotEmptyTel);
            return;
        }
    }

    var addList = document.getElementById('divTels');

    var divTelFields = document.createElement('div');
    divTelFields.className = 'input-group';
    divTelFields.style = 'margin-top:10px;';

    divTelFields.innerHTML = '<span class="input-group-addon"><i class="fa fa-phone-square" ></i ></span ><input class="form-control" id="' + idCode + '"type="text" maxlength="3" style="width:70px !important; margin-left: 7px;" placeholder="کد"><span class="input-group-addon"><i class="fa fa-phone"></i></span><input class="form-control" id="' + idTel + '" style="margin-left:7px;width:128px !important ;" type="text" maxlength="8" placeholder="تلفن"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idComment + '" type="text" placeholder="توضیحات" style="width:695px !important">'
    addList.appendChild(divTelFields);
}

function AddNewRowMobile() {
    var parentDiv = document.getElementById("divMobiles");
    var len = parentDiv.getElementsByClassName("input-group").length;
    var idMobile = "txtMobile" + len;
    var idMobComment = "txtMobComment" + len;

    for (var i = len; i >= 2; i--) {
        var num = i - 1;
        var txtMobile = document.getElementById("txtMobile" + num).value
        if (txtMobile == '' || txtMobile == null) {
            WarningMessage(canNotEmptyMobile);
            return;
        }
    }

    var addList = document.getElementById('divMobiles');

    var divMobileFields = document.createElement('div');
    divMobileFields.className = 'input-group';
    divMobileFields.style = 'margin-top:10px;';

    divMobileFields.innerHTML = '<span class="input-group-addon"><i class="fa fa-mobile"></i></span><input class="form-control" id="' + idMobile + '" style="margin-left:7px;width:128px !important ;" type="text" maxlength="11" placeholder="همراه"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idMobComment + '" type="text" placeholder="توضیحات" style="width:836px !important">'
    addList.appendChild(divMobileFields);
}

function getPeopleTelsAndMobile(e, peopleId) {
    var target = $(e.target).attr("href")
    if (target == '#PeopleRelations') {
        if (!hasCallTelsAndMobiles) {
            $.ajax({
                type: "POST",
                url: "People/GetPeopleTelsAndMobiles",
                data: {
                    peopleId: peopleId
                },
                success: function (result) {
                    if (result.result != '') {
                        ErrorMessage(result.result);
                        return;
                    }
                    tels = result.telList;
                    mobiles = result.mobileList;
                    hasCallTelsAndMobiles = true;
                    FillTelsAndMobiles(tels, mobiles);

                },
                error: function (result) {
                    ErrorMessage();
                }
            });

        }
    }
}

function FillTelsAndMobiles(tels, mobiles) {
    var addListTels = document.getElementById('divTels');
    document.getElementById('txtCode1').value = tels[0].code;
    document.getElementById('txtTel1').value = tels[0].number;
    document.getElementById('txtComment1').value = tels[0].comment;

    if (tels.length > 1) {
        for (var i = 1; i < tels.length; i++) {
            var divTelFields = document.createElement('div');
            divTelFields.className = 'input-group';
            divTelFields.style = 'margin-top:10px;';

            var idCode = "txtCode" + (i + 1);
            var idTel = "txtTel" + (i + 1);
            var idComment = "txtComment" + (i + 1);
            divTelFields.innerHTML = '<span class="input-group-addon"><i class="fa fa-phone-square" ></i ></span ><input class="form-control" id="' + idCode + '"type="text" maxlength="3" value= " ' + tels[i].code + '" style="width:70px !important; margin-left: 7px;" placeholder="کد"><span class="input-group-addon"><i class="fa fa-phone"></i></span><input class="form-control" id="' + idTel + '" style="margin-left:7px;width:128px !important ;" type="text" value="' + tels[i].number + '" maxlength="8" placeholder="تلفن"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idComment + '" type="text" value="' + tels[i].comment + '"placeholder="توضیحات" style="width:695px !important">'
            addListTels.appendChild(divTelFields);
        }
    }

    var addListMobiles = document.getElementById('divMobiles');
    document.getElementById('txtMobile1').value = mobiles[0].mobile;
    document.getElementById('txtMobComment1').value = mobiles[0].comment;

    if (mobiles.length > 1) {
        for (var i = 1; i < mobiles.length; i++) {
            debugger;
            var divMobileFields = document.createElement('div');
            divMobileFields.className = 'input-group';
            divMobileFields.style = 'margin-top:10px;';

            var idMobile = "txtMobile" + (i + 1);
            var idMobComment = "txtMobComment" + (i + 1);

            divMobileFields.innerHTML = '<span class="input-group-addon"><i class="fa fa-mobile"></i></span><input class="form-control" id="' + idMobile + '" style="margin-left:7px;width:128px !important ;" type="text" value="' + mobiles[i].mobile + '" maxlength="11" placeholder="همراه"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idMobComment + '" type="text" value="' + mobiles[i].comment + '" placeholder="توضیحات" style="width:836px !important">'
            addListMobiles.appendChild(divMobileFields);
        }
    }
}

function checkMariedStateDate() {
    var marriedType = document.getElementById("cmbMarriedType").value;
    if (marriedType == 1)
        document.getElementById("txtMariedDate").value = '';
}

function btnShowPeoplePopupClick(buttonTitle) {
    var rowNum = 1;
    $.ajax({
        url: "People/GetPeopleList",
        type: "POST",
        success: function (people) {
            var row = "";
            var data = JSON.parse(people.people);
            $.each(data, function (index, item) {

                var manualCode = item.ManualCode == null ? '' : item.ManualCode;
                var systemCode = item.SystemCode == null ? '' : item.SystemCode;
                var tBASCategoryName = item.TBASCategoryName == null ? '' : item.TBASCategoryName;
                var tel = item.Tel == null ? '' : item.Tel;
                var mobile = item.Mobile == null ? '' : item.Mobile;
                var birthday = item.P_Birthday == null ? '' : item.P_Birthday;
                var address = item.Address == null ? '' : item.Address;
                var marriedType = item.MarriedType == null ? '' : item.MarriedType;
                var tBASGraduationName = item.GraduationName == null ? '' : item.GraduationName;
                var tBASPotentialName = item.TBASPotentialName == null ? '' : item.TBASPotentialName;
                var tBASIntroduceName = item.TBASIntroduceName == null ? '' : item.TBASIntroduceName;

                row += "<tr>";

                row += "<td class='specialTd'><span id='PeopleId' class='specialFields'>" + item.Id + "</span><span>" + rowNum + "</span></td>";
                row += "<td> <a class='btn btn-success  btn-round btnAction' href='#' onclick='selectPeople()'>" + buttonTitle + "<div class='paper-ripple'><div class='paper-ripple__background'></div><div class='paper-ripple__waves'></div></div></a></td>";
                row += "<td> <img class='img-person img-circle' src='/image/images/user/32.png'><span>" + item.FullName + "</span></td>";
                row += "<td>" + manualCode + "</td>";
                row += "<td>" + systemCode + "</td>";
                row += "<td>" + tBASCategoryName + "</td>";
                row += "<td>" + tel + "</td>";
                row += "<td>" + mobile + "</td>";
                row += "<td>" + birthday + "</td>";
                row += "<td>" + address + "</td>";
                row += "<td>" + marriedType + "</td>";
                row += "<td>" + tBASGraduationName + "</td>";
                row += "<td>" + tBASPotentialName + "</td>";
                row += "<td>" + tBASIntroduceName + "</td>";

                row += "</tr>";

                rowNum += 1;
            });
            $("#tbodyPeople").html(row);
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}

function selectPeople(e) {
    var peopleId = getValueTableById('PeopleId');
    if (peopleId > 0) {
        $(document).ready(function () {
            $("#PeopleSelectorModal").modal('hide');
            $("#peopleSelector").slideUp(1500).slideDown(1500);
            $.ajax({
                type: "POST",
                url: "/People/GetPeoplePropertyById",
                data: { peopleId: peopleId },
                success: function (data) {
                    refreshFieldsPeopleSelector();
                    var people = JSON.parse(data.people);
                    PeopleId.innerHTML += people[0].Id;
                    fullName.innerHTML += people[0].FullName;
                    birthday.innerHTML += people[0].Birthday;
                    age.innerHTML += people[0].Age;
                    certificateCode.innerHTML += people[0].CertificateCode;
                    graduation.innerHTML += people[0].GraduationName;
                    address.innerHTML += people[0].Address;
                },
                error: function (httpRequest, textStatus, errorThrown) {
                    ErrorMessage();
                }
            });
        });


    }
}

function refreshFieldsPeopleSelector() {
    //var peopleValue = '@peopleId';
    PeopleId.innerHTML = "";
    fullName.innerHTML = " مشخصات شخص: ";
    age.innerHTML = " سن: ";
    birthday.innerHTML = " تاریخ تولد:  ";
    certificateCode.innerHTML = " کد ملی : ";
    graduation.innerHTML = " تحصیلات: ";
    address.innerHTML = " آدرس: ";
}

