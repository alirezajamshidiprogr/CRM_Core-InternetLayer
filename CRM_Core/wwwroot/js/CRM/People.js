var canNotEmptyTel = '';
var canNotEmptyMobile = '';
var hasCallTelsAndMobiles = false;
var tels = '';
var mobiles = '';

function btnOpenEditPeople() {
    var peopleId = getValueTableById('PeopleId');
    hasCallTelsAndMobiles = false;
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/People/AddEditPeople",
        data: { peopleId: peopleId },
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

function btnShowPeopleList() {
    enablePageloadding();
    $.ajax({
        type: "GET",
        url: "/People/Index",
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

function showPeopleList(quickSearch, state) {
    enablePageloadding();
    if (quickSearch == true)
        var txtSearchValue = $("#txtPeopleSearch").val();
    var people = '';
    if (state == 'isSelectedMode' && quickSearch == false) {// SEARCH ITEMS FOR SELECT PEOPLE SEARCH 
        people = {
            systemCode : $("#_txtsystemCode").val(),
            ManualCode : $("#_txtmanualCode").val(),
            CertificateCode : $("#_txtCertificateCode").val(),
            FirstName : $("#_txtName").val(),
            LastName : $("#_txtFamily").val()
        };
    }
    else if (state == 'isEditMode' && quickSearch == false) {
        people = {
            FirstName: $("#txtNameSearch").val(),
            LastName: $("#txtFamilySearch").val(),
            Birthday: $("#txtBirthDaySearch").val(),
            Age: $("#txtAgeSearch").val(),
            TBASPotentialId: document.getElementById("cmbPotentialSearch").value,
            TBASIntroductionTypeId: document.getElementById("cmbIntroductionTypeSearch").value ,
            MariedType: document.getElementById("cmbMariedTypeSearch").value ,
            TBASGradationsId: document.getElementById("cmbGradationsSearch").value ,
            TBASCategoriyId: document.getElementById("cmbCategoriesSearch").value ,
            TBASPrefixID: document.getElementById("cmbPrefixesSearch").value ,
        };
    }

    $.ajax({
        type: "POST",
        url: "/People/FillPeopleTableData",
        data: {
            quickSearch: quickSearch,
            fullName: txtSearchValue,
            searchParams: people ,
            state : state
        },
        success: function (result) {
            if (result.errorMessage != undefined) {
                ErrorMessage(result.errorMessage);
                return;
            }
            disablePageloadding();
            $('#peopleList').html(result);
            $('#form-peopleSearch').modal('hide');
        },
        error: function () {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

function btnAddEditPeople() {
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
         ShowMandatoryMessage(mandatoryMessage);
        return;
    }
    var introducId = $("#PeopleSelector_Id")[0].innerText;
    
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
        IntroduceId: introducId,
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

    if (introducId == '') {
        swal({
            title: ' سوال... ',
            text: "برای ثبت شخص هیچ معرفی انتخاب نشده است آیا میخواهید ادامه دهید؟",
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#f44336',
            cancelButtonColor: '#777',
            confirmButtonText: 'بله  '
        }).then(function () {
            AddEditPeoplefunction(isEdit, checkRepeatedTels, checkRepeatedMobiles, tels, mobiles, peopleVirtual, address, people);
        },
        ).catch(swal.noop);
         return;
    }

    AddEditPeoplefunction(isEdit, checkRepeatedTels, checkRepeatedMobiles, tels, mobiles, peopleVirtual, address, people);

}

function AddEditPeoplefunction(isEdit, checkRepeatedTels, checkRepeatedMobiles, tels, mobiles, peopleVirtual, address, people) {
    $.ajax({
        type: "POST",
        url: "/People/AddEditPeopleMethod",
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
    var peopleId = getValueTableById('PeopleId');
    swal({
        title: 'آیا جهت حذف آیتم اطمینان دارید؟',
        text: "این عملیات برگشت پذیر نیست...",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        enablePageloadding();
        $.ajax({
            type: "POST",
            url: "/People/DeletePeople",
            data: {
                peopleId: peopleId,
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
    //document.getElementById(idTel).setAttribute("format", "Phone");

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
    //document.getElementById(idMobile).setAttribute("format", "Mobile");

}

function getPeopleTelsAndMobile(e, peopleId) {
    var target = $(e.target).attr("href")
    if (target == '#PeopleRelations') {
        //  if (!hasCallTelsAndMobiles) return;
        $.ajax({
            type: "POST",
            url: "/People/GetPeopleTelsAndMobiles",
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
    if (marriedType != 2) {
        $("#txtMariedDate").prop('disabled', true);       
        document.getElementById("txtMariedDate").value = '';
    }
    else
        $("#txtMariedDate").prop('disabled', false);        

}

function btnShowPeoplePopupClick(buttonTitle) {
    $.ajax({
        type: "POST",
        url: "/People/FillPeopleTableData",
        data: {
            quickSearch: true,
            fullName: ''
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

function selectPeople(e) {
    var peopleId = getValueTableById('PeopleId');
    if (peopleId > 0) {
        $(document).ready(function () {
            $("#PeopleSelectorModal").modal('hide');
            $("#peopleSelector").slideUp(1500).slideDown(1500);
            getPeopleSelectorInfo(peopleId);
        });
    }
}

function getPeopleSelectorInfo(peopleId) {
    $.ajax({
        type: "POST",
        url: "/People/GetPeoplePropertyById",
        data: { peopleId: peopleId },
        success: function (data) {
            refreshFieldsPeopleSelector(data, peopleId);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });

}

function refreshFieldsPeopleSelector(data, peopleId) {
    document.getElementById("PeopleSelector_Id").innerText = "";
    document.getElementById("PeopleSelector_fullName").innerText = ' نام و نام خانوادگی : ';
    document.getElementById("PeopleSelector_birthday").innerText = ' تاریخ تولد : ';
    document.getElementById("PeopleSelector_age").innerText = 'سن : ';
    document.getElementById("PeopleSelector_certificateCode").innerText = ' کد ملی : ';
    document.getElementById("PeopleSelector_graduation").innerText = ' تحصیلات : ';
    document.getElementById("PeopleSelector_address").innerText = ' آدرس : ';
    if (peopleId > 0 )
        fillPeopleById(data,peopleId);
}

function fillPeopleById(result, peopleId) {
    document.getElementById("PeopleSelector_Id").innerText += result.peopleModel[0].id;
    document.getElementById("PeopleSelector_fullName").innerText += result.peopleModel[0].fullName;
    document.getElementById("PeopleSelector_age").innerText += result.peopleModel[0].age
    document.getElementById("PeopleSelector_birthday").innerText += result.peopleModel[0].p_Birthday;
    document.getElementById("PeopleSelector_certificateCode").innerText += result.peopleModel[0].certificateCode;
    document.getElementById("PeopleSelector_graduation").innerText += result.peopleModel[0].tbasGraduationName;
    document.getElementById("PeopleSelector_address").innerText += result.peopleModel[0].address;
}
