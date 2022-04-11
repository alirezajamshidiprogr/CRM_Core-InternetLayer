var canNotEmptyTel = '';
var canNotEmptyMobile = '';
var hasCallTelsAndMobiles = false;
var tels = '';
var mobiles = '';
var hasVisitedTels = false;
var txtTelPlaceHolder = '';
var txtDescriptionPlaceHolder = '';
var phoneTelsTypeValue = [];
var counter = 0;
var validTelMobile = false;
var hasIntroduction = false;


function btnOpenEditPeople(e) {
    var peopleId = getValueTableById('PeopleId', e);
    hasCallTelsAndMobiles = false;
    enablePageloadding();
    $.ajax({
        type: "POST",
        url: "/People/AddEditPeople",
        data: { peopleId: peopleId },
        success: function (data) {
            setTimeout(function () {
                disablePageloadding();
            }, 2000);
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
            showPeopleList(true, 'isEditMode');
            disablePageloadding();
            $("#formContainer").html(data);
            showPeopleList(true, 'isEditMode');
        },
        error: function (httpRequest, textStatus, errorThrown) {
            disablePageloadding();
            ErrorMessage();
        }
    });
}

function showPeopleList(quickSearch, state) {
    debugger;
    enablePageloadding();
    if (quickSearch == 'true')
        var txtSearchValue = $("#txt-search").val();
    var people = '';
    if (state == 'isSelectedMode' && quickSearch == 'false') {// SEARCH ITEMS FOR SELECT PEOPLE SEARCH 
        people = {
            systemCode: $("#_txtsystemCode").val(),
            ManualCode: $("#_txtmanualCode").val(),
            CertificateCode: $("#_txtCertificateCode").val(),
            FirstName: $("#_txtName").val(),
            LastName: $("#_txtFamily").val(),
            PageNumber: pageNumber,
        };
    }
    else if (state == 'isEditMode' && quickSearch == 'false') {
        people = {
            FirstName: $("#txtNameSearch").val(),
            LastName: $("#txtFamilySearch").val(),
            FromBirthday: $("#txtFromBirthDaySearch").val(),
            ToBirthday: $("#txtToBirthDaySearch").val(),
            FromAge: $("#txtFromAgeSearch").val(),
            ToAge: $("#txtToAgeSearch").val(),
            TBASIntroductionTypeId: document.getElementById("cmbIntroductionTypeSearch").value,
            MariedType: document.getElementById("cmbMariedTypeSearch").value,
            TBASGradationsId: document.getElementById("cmbGradationsSearch").value,
            TBASCategoriyId: document.getElementById("cmbCategoriesSearch").value,
            TBASPrefixID: document.getElementById("cmbPrefixesSearch").value,
            TBASPotentialId: document.getElementById("cmbPotentialSearch").value,
            PageNumber: pageNumber,
        };
    }

    $.ajax({
        type: "POST",
        url: "/People/FillPeopleTableData",
        data: {
            quickSearch: quickSearch,
            fullName: txtSearchValue,
            searchParams: people,
            state: state
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

function btnAddEditPeople(e,introducePeopleMessage,noTelEntered) {
    debugger;
    var mandatoryMessage = CheckMandatoryFields();
    if (mandatoryMessage != '') {
        ShowMandatoryMessage(mandatoryMessage);
        return;
    }

    var introducId = $("#PeopleSelector_Id")[0].innerText;
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

    var otherRelationShips = new Array();
    var checkRepeatedTels = document.getElementById("chbCheckRepeatedTels").checked;

    ///// PeopleTelsAndMobiles
    var parentDivTels = document.getElementById("divPhoneTels");
    var lenTels = parentDivTels.getElementsByClassName("TelItems").length;
    for (var i = 1; i <= lenTels; i++) {
        var cmbTelPhoneType = document.getElementById("cmbTelType_" + i).value;
        var txtValue = document.getElementById("txtTel_" + i).value;
        var txtDescription = document.getElementById("txtDescription_" + i).value;
        if (cmbTelPhoneType != '' && txtValue != '')
            otherRelationShips.push({ TBASTelTypeId: cmbTelPhoneType, TelValue: txtValue, Description: txtDescription });
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

    if (otherRelationShips.length == 0 && !validTelMobile) {
        swal({
            title: question,
            text: noTelEntered,
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#f44336',
            cancelButtonColor: '#777',
            confirmButtonText: yesTitle,
            cancelButtonText: noTitle
        }).then(function () {
            validTelMobile = true;
            btnAddEditPeople(e, introducePeopleMessage, noTelEntered);
        },
        ).catch(swal.noop);
        return;
    }

    if (introducId == '' && !hasIntroduction) {
        swal({
            title: question,
            text: introducePeopleMessage,
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#f44336',
            cancelButtonColor: '#777',
            confirmButtonText: yesTitle,
            cancelButtonText: noTitle
        }).then(function () {
            //AddEditPeoplefunction(e,isEdit, checkRepeatedTels, otherRelationShips, peopleVirtual, address, people);
            hasIntroduction = true;
            btnAddEditPeople(e, introducePeopleMessage, noTelEntered);
        },
        ).catch(swal.noop);
        return;
    }

    AddEditPeoplefunction(e,isEdit, checkRepeatedTels, otherRelationShips, peopleVirtual, address, people);

}

function AddEditPeoplefunction(e,isEdit, checkRepeatedTels, otherRelationShips, peopleVirtual, address, people) {
    $.ajax({
        type: "POST",
        url: "/People/AddEditPeopleMethod",
        data: {
            isEdit: isEdit,
            checkRepeatedTels: checkRepeatedTels,
            hasVisitedTelsMobiles: hasVisitedTels,
            peopleVirtual: peopleVirtual,
            address: address,
            people: people,
            otherRelationShips: otherRelationShips,
        },
        beforeSend: function () {
            EnableProcess(e, true);
        },
        complete: function () {
            EnableProcess(e, false);
            hasIntroduction = false;
            validTelMobile = false;
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            SuccessMessage(result.message);
            ShowDashboard(e);
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
        title: deleteMessageQuestion,
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: confirmDeleteMessage,
        cancelButtonText: noTitle
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
    var len = parentDiv.getElementsByClassName("divTelsItems").length;
    var idCode = "txtCode" + (len + 1);
    var idTel = "txtTel" + (len + 1);
    var idComment = "txtComment" + (len + 1);

    for (var i = 1; i <= len; i++) {
        var txtTel = document.getElementById("txtTel" + i).value
        if (txtTel == '' || txtTel == null) {
            WarningMessage(canNotEmptyTel);
            return;
        }
    }

    var addList = document.getElementById('divTels');

    var divTelFields = document.createElement('div');
    divTelFields.className = 'input-group';
    divTelFields.style = 'margin-top:10px;';
    divTelFields.innerHTML = "<div class='divTelsItems' style='display:none;'> </div>";
    divTelFields.innerHTML += '<span class="input-group-addon"><i class="fa fa-phone-square" ></i ></span ><input class="form-control" id="' + idCode + '"type="text" maxlength="3" style="width:70px !important; margin-left: 7px;" placeholder="کد"><span class="input-group-addon"><i class="fa fa-phone"></i></span><input class="form-control" id="' + idTel + '" style="margin-left:7px;width:128px !important ;" type="text" maxlength="8" placeholder="تلفن"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idComment + '" type="text" placeholder="توضیحات" style="width:695px !important">'
    //document.getElementById(idTel).setAttribute("format", "Phone");

    addList.appendChild(divTelFields);
}

function AddNewRowMobile() {
    var parentDiv = document.getElementById("divMobiles");
    var len = parentDiv.getElementsByClassName("divMobileItems").length;
    var idMobile = "txtMobile" + (len + 1);
    var idMobComment = "txtMobComment" + (len + 1);

    for (var i = 1; i <= len; i++) {
        var txtMobile = document.getElementById("txtMobile" + i).value
        if (txtMobile == '' || txtMobile == null) {
            WarningMessage(canNotEmptyMobile);
            return;
        }
    }

    var addList = document.getElementById('divMobiles');

    var divMobileFields = document.createElement('div');
    divMobileFields.className = 'input-group';
    divMobileFields.style = 'margin-top:10px;';
    divMobileFields.innerHTML = "<div class='divMobileItems' style='display:none;'> </div>";
    divMobileFields.innerHTML += '<span class="input-group-addon"><i class="fa fa-mobile"></i></span><input class="form-control" id="' + idMobile + '" style="margin-left:7px;width:128px !important ;" type="text" maxlength="11" placeholder="همراه"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idMobComment + '" type="text" placeholder="توضیحات" style="width:836px !important">'
    addList.appendChild(divMobileFields);
    //document.getElementById(idMobile).setAttribute("format", "Mobile");

}

function getPeopleTelsAndMobile(e, peopleId) {
    hasVisitedTels = true;
    var target = $(e.target).attr("href")
    if (target == '#PeopleRelations') {
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
            divTelFields.innerHTML = "<div class='divTelsItems' style='display:none;'> </div>";
            divTelFields.innerHTML += '<span class="input-group-addon"><i class="fa fa-phone-square" ></i ></span ><input class="form-control" id="' + idCode + '"type="text" maxlength="3" value= " ' + tels[i].code + '" style="width:70px !important; margin-left: 7px;" placeholder="کد"><span class="input-group-addon"><i class="fa fa-phone"></i></span><input class="form-control" id="' + idTel + '" style="margin-left:7px;width:128px !important ;" type="text" value="' + tels[i].number + '" maxlength="8" placeholder="تلفن"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idComment + '" type="text" value="' + tels[i].comment + '"placeholder="توضیحات" style="width:695px !important">'
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
            divMobileFields.innerHTML = "<div class='divMobileItems' style='display:none;'> </div>";
            divMobileFields.innerHTML += '<span class="input-group-addon"><i class="fa fa-mobile"></i></span><input class="form-control" id="' + idMobile + '" style="margin-left:7px;width:128px !important ;" type="text" value="' + mobiles[i].mobile + '" maxlength="11" placeholder="همراه"><span class="input-group-addon"><i class="fa fa-comment"></i></span><input class="form-control" id="' + idMobComment + '" type="text" value="' + mobiles[i].comment + '" placeholder="توضیحات" style="width:836px !important">'
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

function selectPeople() {
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
    if (peopleId > 0)
        fillPeopleById(data, peopleId);
}

function fillPeopleById(result, peopleId) {
    document.getElementById("PeopleSelector_Id").innerText += result.peopleModel[0].id;
    document.getElementById("PeopleSelector_fullName").innerText += result.peopleModel[0].fullName;
    document.getElementById("PeopleSelector_age").innerText += result.peopleModel[0].age + ' سال '
    document.getElementById("PeopleSelector_birthday").innerText += result.peopleModel[0].p_Birthday;
    document.getElementById("PeopleSelector_certificateCode").innerText += result.peopleModel[0].certificateCode;
    document.getElementById("PeopleSelector_graduation").innerText += result.peopleModel[0].tbasGraduationName;
    document.getElementById("PeopleSelector_address").innerText += result.peopleModel[0].address;
}

function getSelectedDropDownValue(isEdit,peopleId,type) {
    if (isEdit != 'True') return; 

    $.ajax({
        type: "POST",
        url: "/People/GetDropDownSelectedValue",
        data: {
            peopleId: parseInt(peopleId),
            type : type ,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            var selectdItemCombo = result.phoneTelTypes;
            for (var i = 0; i < selectdItemCombo.length; i++) {
                var comboBox = document.getElementById("cmbTelType_" + (i+1));
                for (var j = 0; j < comboBox.options.length; j++) {
                    if (comboBox.options[j].attributes[0].value == selectdItemCombo[i].id) {
                        comboBox.selectedIndex = j;
                        break;
                    }
                }

            }
        },
        error: function (result) {
            ErrorMessage();
        }
    });
}
//function fillTelPhoneFieldsValue(peopleId) {
//    if (parseInt(peopleId) > 0) {
//        $.ajax({
//            type: "POST",
//            url: "/People/GetPeopleTelsAndMobiles",
//            data: {
//                peopleId: parseInt(peopleId),
//            },
//            success: function (result) {
//                if (result.errorMessage != '') {
//                    ErrorMessage(result.errorMessage);
//                    return;
//                }
//                var parentDiv = document.getElementById("divPhoneTels");
//                parentDiv.innerHTML = '';
//                for (var i = 0; i < result.listTelPhones.length; i++) {
//                    counter = i + 1;
//                    var divIdFields = "divRelationsFields_" + counter;
//                    var divIdButtons = "divRelationsBottons_" + counter;
//                    var cmbTelType = 'cmbTelType_' + counter;
//                    var txtTel = 'txtTel_' + counter;
//                    var txtDescription = 'txtDescription_' + counter;
//                    var btnDeletePhone = 'btnDelete_' + counter;
//                    var btnAddPhone = 'btnAddPhone_' + counter;

//                    var html = `<div class='col-md-10 col-sm-12 form-group TelItems' id='${divIdFields}'>
//                    <select id='${cmbTelType}'' class='form-control' aria-invalid='false' style='width: 24%;float: right;margin-left: 12px;'>
//                    </select>
//                    <input class='form-control' id='${txtTel}' type='text' placeholder='${txtTelPlaceHolder}' aria-invalid='false' style='width: 23%;float: right;margin-left: 11px;'>
//                    <input class='form-control' id='${txtDescription}' type='text' placeholder='${txtDescriptionPlaceHolder}' aria-invalid='false' style='width: 48%;'>
//                    </div>
//                    <div class='col-md-2 col-sm-12 form-group' id='${divIdButtons}'>
//                        <a href='#' id='${btnAddPhone}' style='font-size: 2.2em;color: green;' class='bx bxs-plus-circle' onclick='addServiceItemsTelDiv(this)'></a>
//                        <a href='#' id='${btnDeletePhone}' style='font-size: 2.2em;color:red ;' class='bx bxs-minus-circle' onclick='deleteTelPhonesItemsDiv(this)' ></a>
//                    </div>
//                </div>`;

//                    parentDiv.innerHTML += html;
//                    fillCmbPhoneTelType();
//                }
//            },
//            error: function (result) {
//                ErrorMessage();
//            }
//        });
//    }
//}


//function fillCmbPhoneTelType(isSelectedValue) {
//    var comboBox = document.getElementById("cmbTelType_" + counter);
//    var cmbTelTypeValue = document.getElementById("cmbTelType_" + counter).value;

//    comboBox.innerHTML = '';
//    for (var j = 0; j < phoneTelsType.length; j++) {
//        var option = document.createElement("option");
//        option.value = phoneTelsType[j].value;
//        option.text = phoneTelsType[j].text;
//        if (cmbTelTypeValue == phoneTelsType[j].value && isSelectedValue)
//            option.setAttribute('selected', 'selected');

//        comboBox.appendChild(option);
//    }
//}
