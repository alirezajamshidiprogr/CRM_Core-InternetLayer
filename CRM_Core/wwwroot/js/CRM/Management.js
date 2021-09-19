var mainMenu = [];
var subMenu = [];
var menuBottons = [];
var userMenuAdd = [];
var hasActiveMenuMessage = '';
var isNotValidPassword = '';
var thereIsNotAnyActiveMenu = '';
var fillDateMessage = '';

function btnOpenEditUserMenuAccess() {
    window.open("/Managment/Index", target = "_blank");
}

function getUserMenuInfo() {
    var userName = $("#txtAdminUserName").val();
    var password = $("#txtAdminPassword").val();
    if (userName == '' || password == '') {
        ErrorMessage(isNotValidPassword);
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Managment/GetUserByParam",
        data: {
            userName: userName,
            password: password
        },
        success: function (result) {
            if (!result.success) {
                ErrorMessage(isNotValidPassword);
                return;
            }
            else
                fillComboUsers(result);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });
}

function fillComboUsers(result) {
    var comboUser = document.getElementById("cmbUsers");
    comboUser.innerHTML = '';
    var opt = document.createElement("option");
    opt.value = null;
    opt.innerHTML = ''; // whatever property it has
    comboUser.appendChild(opt);

    for (var i = 0; i < result.users.length; i++) {
        var opt = document.createElement("option");
        opt.value = result.users[i].value;
        opt.innerHTML = result.users[i].text; // whatever property it has
        comboUser.appendChild(opt);
    }
}

function onChangeCmbUser() {
    if (mainMenu.length > 0) {
        var parentMainMenu = document.getElementById("mainMenu");
        parentMainMenu.innerHTML = '';
        displayDivSetUserMenuAccess(mainMenu);
        return;
    }
    var cmbUser = document.getElementById("cmbUsers");
    var selectedUser = cmbUser.value;
    $.ajax({
        type: "POST",
        url: "/Managment/GetUserMenu",
        data: {
            userIdentityId: selectedUser,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            else
                displayDivSetUserMenuAccess(result);
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });
}

function displayDivSetUserMenuAccess(result) {
    if (result.mainMenu != null) {
        for (var i = 0; i < result.mainMenu.length; i++) {
            var title = result.mainMenu[i].menuTitle;
            var menuId = result.mainMenu[i].menuId;
            var hasAccess = result.mainMenu[i].menuState == 0 ? false : true;
            mainMenu.push({ title: title, menuId: menuId, hasAccess: hasAccess });
        }
    }

    if (result.subMenu != null) {
        for (var i = 0; i < result.subMenu.length; i++) {
            var title = result.subMenu[i].menuTitle;
            var menuId = result.subMenu[i].menuId;
            var parentMenuId = result.subMenu[i].parentMenuId;
            var hasAccess = result.subMenu[i].menuState == 0 ? false : true;
            subMenu.push({ title: title, menuId: menuId, hasAccess: hasAccess, parentMenuId: parentMenuId });
        }
    }

    if (result.menuButton != null) {
        for (var i = 0; i < result.menuButton.length; i++) {
            var title = result.menuButton[i].menuTitle;
            var menuId = result.menuButton[i].menuId;
            var parentMenuId = result.menuButton[i].parentMenuId;
            var hasAccess = result.menuButton[i].menuState == 0 ? false : true;
            menuBottons.push({ title: title, menuId: menuId, hasAccess: hasAccess, parentMenuId: parentMenuId });
        }
    }

    fillMainMenu(mainMenu)

    $("#mainMenu").slideDown(2000);
    $("#subMenu").slideDown(3000);
    $("#trDate").slideDown(2000);
    $("#menuBottions").slideDown(4000)
    $("#menuConfirm").slideDown(2000);
}

function fillMainMenu(mainMenu) {
    var parentMainMenu = document.getElementById("mainMenu");
    parentMainMenu.innerHTML = '';
    for (var i = 0; i < mainMenu.length; i++) {
        var divItem = document.createElement('div');
        divItem.className = "subItems";
        divItem.addEventListener("click", function (e) {
            subItemsClick(this, 'mainMenu');
        });
        parentMainMenu.appendChild(divItem);

        var spanTitleMenu = document.createElement('span');
        spanTitleMenu.style = "margin-right:25px;";
        spanTitleMenu.innerHTML = mainMenu[i].title;
        divItem.appendChild(spanTitleMenu);

        var spanMenuId = document.createElement('span');
        spanMenuId.className = "specrialSpan menuId";
        spanMenuId.id = 'menuId_' + mainMenu[i].menuId;
        spanMenuId.innerHTML = mainMenu[i].menuId;
        divItem.appendChild(spanMenuId);

        var spanMenuAccess = document.createElement('span');
        spanMenuAccess.className = "specrialSpan menuAccess";
        spanMenuAccess.id = 'hasAccess_' + mainMenu[i].menuId;
        spanMenuAccess.innerHTML = mainMenu[i].hasAccess;
        divItem.appendChild(spanMenuAccess);

        var button = document.createElement('button');
        if (mainMenu[i].hasAccess) {
            button.innerText = "فعال";
            button.className = "btn btn-primary";
        }
        else {
            button.innerText = "غیر فعال";
            button.className = "btn btn-danger";
        }
        button.style = "float:left;width:80px"
        button.id = 'btnMainMenu_' + mainMenu[i].menuId;

        var iconButton = document.createElement('i');
        //iconButton.className = mainMenu.hasAccess ? "glyphicon glyphicon-plus" : 'glyphicon glyphicon-minus' ;
        iconButton.style = "margin-right:11px";
        button.addEventListener("click", function (e) {
            onMenuButtonClick(this, 'mainMenu');
        });
        button.appendChild(iconButton);

        divItem.appendChild(button);
    }
}

function subItemsClick(e, menuType) {
    var childs = e.childNodes;
    var menuId;
    if (menuType == 'mainMenu') {
        for (var i = 0; i < childs.length; i++) {
            if (childs[i].className == 'specrialSpan menuId') {
                menuId = childs[i].innerText;
            }
        }
        fillSubMenuItemDiv(menuId, 'mainMenu');
    }

    else if (menuType == 'subMenu') {
        for (var i = 0; i < childs.length; i++) {
            if (childs[i].className == 'specrialSpan menuId') {
                menuId = childs[i].innerText;
            }
        }
        fillSubMenuItemDiv(menuId, 'subMenu');
    }
    //else {
    //    for (var i = 0; i < childs.length; i++) {
    //        if (childs[i].className == 'specrialSpan menuId') {
    //            menuId = childs[i].innerText;
    //        }
    //    }
    //    fillSubMenuItemDiv(menuId, 'menuBottons');
    //}
}

function fillSubMenuItemDiv(menuId, menuType) {
    if (menuType == 'mainMenu') {
        var parentsubMenu = document.getElementById("subMenu");
        parentsubMenu.innerHTML = '';
        for (var i = 0; i < subMenu.length; i++) {
            if (subMenu[i].parentMenuId == menuId) {
                var divSubMenuItem = document.createElement('div');
                divSubMenuItem.className = "subItems";
                divSubMenuItem.addEventListener("click", function (e) {
                    subItemsClick(this, 'subMenu');
                });
                parentsubMenu.appendChild(divSubMenuItem);

                var spanTitleMenu = document.createElement('span');
                spanTitleMenu.style = "margin-right:25px;";
                spanTitleMenu.innerHTML = subMenu[i].title;
                divSubMenuItem.appendChild(spanTitleMenu);

                var spanMenuId = document.createElement('span');
                spanMenuId.className = "specrialSpan menuId";
                spanMenuId.id = 'menuId_' + subMenu[i].menuId;
                spanMenuId.innerHTML = subMenu[i].menuId;
                divSubMenuItem.appendChild(spanMenuId);

                var spanMenuAccess = document.createElement('span');
                spanMenuAccess.className = "specrialSpan menuAccess";
                spanMenuAccess.id = 'hasAccess_' + subMenu[i].menuId;
                spanMenuAccess.innerHTML = subMenu[i].hasAccess;
                divSubMenuItem.appendChild(spanMenuAccess);

                var button = document.createElement('button');
                if (subMenu[i].hasAccess) {
                    button.innerText = "فعال";
                    button.className = "btn btn-primary";
                }
                else {
                    button.innerText = "غیر فعال";
                    button.className = "btn btn-danger";
                }
                button.style = "float:left;width:80px"
                button.id = 'btnsubMenu' + subMenu[i].menuId;

                var iconButton = document.createElement('i');
                //iconButton.className = subMenu.hasAccess ? "glyphicon glyphicon-plus" : 'glyphicon glyphicon-minus' ;
                iconButton.style = "margin-right:11px";
                button.addEventListener("click", function (e) {
                    onMenuButtonClick(this, 'subMenu');
                });
                button.appendChild(iconButton);

                divSubMenuItem.appendChild(button);

            }
        }
    }

    else {
        var parentmenuBottions = document.getElementById("menuBottions");
        //parentmenuBottions.innerHTML = menuType == 'subMenu' ? '' : parentmenuBottions.innerHTML;
        parentmenuBottions.innerHTML = '';

        for (var i = 0; i < menuBottons.length; i++) {
            if (menuBottons[i].parentMenuId == menuId) {
                var divSubMenuItem = document.createElement('div');
                divSubMenuItem.className = "subItems";
                divSubMenuItem.addEventListener("click", function (e) {
                    subItemsClick(this, 'menuBottons');
                });
                parentmenuBottions.appendChild(divSubMenuItem);

                var spanTitleMenu = document.createElement('span');
                spanTitleMenu.style = "margin-right:25px;";
                spanTitleMenu.innerHTML = menuBottons[i].title;
                divSubMenuItem.appendChild(spanTitleMenu);

                var spanMenuId = document.createElement('span');
                spanMenuId.className = "specrialSpan menuId";
                spanMenuId.id = 'menuId_' + menuBottons[i].menuId;
                spanMenuId.innerHTML = menuBottons[i].menuId;
                divSubMenuItem.appendChild(spanMenuId);

                var spanMenuAccess = document.createElement('span');
                spanMenuAccess.className = "specrialSpan menuAccess";
                spanMenuAccess.id = 'hasAccess_' + menuBottons[i].menuId;
                spanMenuAccess.innerHTML = menuBottons[i].hasAccess;
                divSubMenuItem.appendChild(spanMenuAccess);

                var button = document.createElement('button');
                if (menuBottons[i].hasAccess) {
                    button.innerText = "فعال";
                    button.className = "btn btn-primary";
                }
                else {
                    button.innerText = "غیر فعال";
                    button.className = "btn btn-danger";
                }
                button.style = "float:left;width:80px"
                button.id = 'btnmenuBottons' + menuBottons[i].menuId;

                var iconButton = document.createElement('i');
                //iconButton.className = menuBottons.hasAccess ? "glyphicon glyphicon-plus" : 'glyphicon glyphicon-minus' ;
                iconButton.style = "margin-right:11px";
                button.addEventListener("click", function (e) {
                    onMenuButtonClick(this, 'menuBottons');
                });
                button.appendChild(iconButton);

                divSubMenuItem.appendChild(button);
            }
        }

    }
}

function onMenuButtonClick(e, menuType) {
    var btnElement = e.id;
    var button = document.getElementById(btnElement);
    var parentDiv = e.parentElement.childNodes;

    if (menuType == 'mainMenu') {
        if (button.innerText == 'فعال') {
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < mainMenu.length; j++) {
                        if (mainMenu[j].menuId == menuId)
                            mainMenu[j].hasAccess = false;
                    }
                }
            }

            //اگر دکمه غیر فعال کلیک شد و دکمه فعال در زیرمنوهای دکمه ها دارد خطا بده
            for (var i = 0; i < subMenu.length; i++) {
                if (subMenu[i].parentMenuId == menuId && subMenu[i].hasAccess) {
                    ErrorMessage(hasActiveMenuMessage);
                    return;
                }
            }
        }

        else {
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < mainMenu.length; j++) {
                        if (mainMenu[j].menuId == menuId)
                            mainMenu[j].hasAccess = true;
                    }
                }
            }
        }
        fillMainMenu(mainMenu);
    }

    else if (menuType == 'subMenu') {
        var parentMenuId = '';
        if (button.innerText == 'فعال') {
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < subMenu.length; j++) {
                        if (subMenu[j].menuId == menuId) {
                            parentMenuId = subMenu[j].parentMenuId;
                            subMenu[j].hasAccess = false;
                        }
                    }
                }
            }
            //اگر دکمه غیر فعال کلیک شد و دکمه فعال در زیرمنوهای دکمه ها دارد خطا بده
            for (var i = 0; i < menuBottons.length; i++) {
                if (menuBottons[i].parentMenuId == menuId && menuBottons[i].hasAccess) {
                    ErrorMessage(hasActiveMenuMessage);
                    return;
                }
            }
        }

        else {
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < subMenu.length; j++) {
                        if (subMenu[j].menuId == menuId) {
                            parentMenuId = subMenu[j].parentMenuId;
                            subMenu[j].hasAccess = true;
                        }
                    }
                }
            }
            //اگر دکمه فعال زیر منو فعال شد باید منو اصلی آن نیز دکمش فعال بشه
            for (var i = 0; i < mainMenu.length; i++) {
                if (mainMenu[i].menuId == parentMenuId) {
                    mainMenu[i].hasAccess = true;
                }
            }
        }
        fillMainMenu(mainMenu);
        fillSubMenuItemDiv(parentMenuId, 'mainMenu');
    }

    else {
        var parentMenuId = '';
        if (button.innerText == 'فعال') {
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < menuBottons.length; j++) {
                        if (menuBottons[j].menuId == menuId) {
                            parentMenuId = menuBottons[j].parentMenuId;
                            menuBottons[j].hasAccess = false;
                        }
                    }
                }
            }
            fillSubMenuItemDiv(parentMenuId, 'menuBottons');
        }

        else {
            var firstParentMenu = '';
            for (var i = 0; i < parentDiv.length; i++) {
                if (parentDiv[i].className == 'specrialSpan menuId') {
                    var id = parentDiv[i].id;
                    var menuId = document.getElementById(id).innerText;
                    for (var j = 0; j < menuBottons.length; j++) {
                        if (menuBottons[j].menuId == menuId) {
                            parentMenuId = menuBottons[j].parentMenuId;
                            menuBottons[j].hasAccess = true;
                        }
                    }
                }
            }
            // اگر دکمه فعال منوی دکمه کلیک شد زیر منوی آن نیز فعال شود.
            for (var i = 0; i < subMenu.length; i++) {
                if (subMenu[i].menuId == parentMenuId) {
                    firstParentMenu = subMenu[i].parentMenuId;
                    subMenu[i].hasAccess = true;
                }
            }
            // با فعال کردن دکمه زیر منو باید منو اصلی هم دکمه منوی آن فعال شود .
            for (var i = 0; i < mainMenu.length; i++) {
                if (mainMenu[i].menuId == firstParentMenu) {
                    mainMenu[i].hasAccess = true;
                }
            }
            fillSubMenuItemDiv(firstParentMenu, 'mainMenu');
            fillMainMenu(mainMenu);
            fillSubMenuItemDiv(parentMenuId, 'menuBottons');
        }

    }
}

function btnAddUserMenuCick() {
    var hasAnyActiveMenu = false;
    for (var val of mainMenu) {
        if (val.hasAccess) {
            hasAnyActiveMenu = true;
            break;
        }
    }

    if (!hasAnyActiveMenu) {
        swal({
            title: thereIsNotAnyActiveMenu,
            text: "",
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#f44336',
            cancelButtonColor: '#777',
            confirmButtonText: 'بله '
        }).then(function () {
            addUserMenu();
        },
        ).catch(swal.noop);
    }

    addUserMenu();

}

function addUserMenu() {
    debugger;
    userMenuAdd = [];
    var userIdentity = document.getElementById("cmbUsers").value;
    var fromDate = document.getElementById("txtFromDate").value;
    var toDate = document.getElementById("txtToDate").value;

    if (fromDate == '' || toDate == '') {
        ErrorMessage(fillDateMessage);
        return;
    }

    for (var item of mainMenu) {
        var hasAccess = item.hasAccess ? 1 : 0;
        userMenuAdd.push({ TBASMenuId: item.menuId, FStartDate: fromDate, FEndDate: toDate, MenueState: hasAccess });
    }
    for (var item of subMenu) {
        var hasAccess = item.hasAccess ? 1 : 0;
        userMenuAdd.push({ TBASMenuId: item.menuId, FStartDate: fromDate, FEndDate: toDate, MenueState: hasAccess });
    }
    for (var item of menuBottons) {
        var hasAccess = item.hasAccess ? 1 : 0;
        userMenuAdd.push({ TBASMenuId: item.menuId, FStartDate: fromDate, FEndDate: toDate, MenueState: hasAccess });
    }

    $.ajax({
        type: "POST",
        url: "/Managment/AddUserMenu",
        data: {
            userMenu: userMenuAdd,
            identityUserId: userIdentity,
        },
        success: function (result) {
            if (result.errorMessage != '') {
                ErrorMessage(result.errorMessage);
                return;
            }
            else {
                SuccessMessage(result.message);
                setFormFieldEmplty();
            }
        },
        error: function (httpRequest, textStatus, errorThrown) {
            ErrorMessage();
        }
    });

}

function setFormFieldEmplty() {
    let element = document.getElementById("cmbUsers");
    element.selectedIndex = 0;
    document.getElementById("txtFromDate").value = '';
    document.getElementById("txtToDate").value = '';

    var mainMenu = document.getElementById("mainMenu");
    var subMenu = document.getElementById("subMenu") ;
    var menuBottions = document.getElementById("menuBottions");
    var menuConfirm = document.getElementById("menuConfirm");
    var trDate = document.getElementById("trDate");

    mainMenu.innerHTML = '';
    subMenu.innerHTML = '';
    menuBottions.innerHTML = '';

    mainMenu.style = 'display:none;';
    subMenu.style = 'display:none;';
    menuBottions.style = 'display:none;';
    menuConfirm.style = 'display:none;';
    trDate.style = 'display:none;';
}