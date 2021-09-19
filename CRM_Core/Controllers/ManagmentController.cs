using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels;
using CRM_Core.DomainLayer;
using CRM_Core.Entities.Models.General;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ManagmentController : Controller
    {
        #region CONSTANT
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IApplicationUserService _applicationUserService;
        private IMenuService _menuService;
        private IUserMenuService _userMenuService;
        #endregion

        public ManagmentController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IApplicationUserService applicationUserSerice, IMenuService menuService, IUserMenuService userMenuService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationUserService = applicationUserSerice;
            _menuService = menuService;
            _userMenuService = userMenuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUserByParam(string userName, string password)
        {
            string errorMessage = string.Empty;
            List<SelectListItem> Users = null;
            bool isSuccess = false;
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
                if (result.Succeeded)
                {
                    isSuccess = true;
                    Users = _applicationUserService.GetAllUser().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.IdentityUserId, Selected = false }; });
                    //Users.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                }
                else
                {
                    isSuccess = false;
                    errorMessage = UI_Presentation.wwwroot.Resources.Mesages.UserNamePasswordIsNotValid;
                }

                return Json(new
                {
                    Success = isSuccess
                    ,
                    errorMessage = errorMessage
                    ,
                    Users = Users
                });

            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });

            }
        }

        public ActionResult GetUserMenu(string userIdentityId)
        {
            List<UserMenuViewModel> mainMenu = new List<UserMenuViewModel>() ;
            List<UserMenuViewModel> subMenu = new List<UserMenuViewModel>();
            List<UserMenuViewModel> menuButton = new List<UserMenuViewModel>();
            string errorMessage = string.Empty;

            try
            {

                mainMenu = (from menu in _menuService.GetApplicationMenu().ToList()
                            join userMenu in _userMenuService.GetUserMenuByUserIdentity(userIdentityId) on menu.MenuId equals userMenu.TBASMenuId into menuUser
                            from userMenu in menuUser.DefaultIfEmpty()
                            orderby menu.Order ascending
                            where
                            menu.ParentMenuId.Equals(null)
                            && menu.Active == true
                            select new UserMenuViewModel
                            {
                                MenuTitle = menu.Title,
                                MenuId = menu.MenuId,
                                IdentityUserId = userMenu != null ? userMenu.IdentityUserId : null ,
                                MenuState = userMenu != null ? userMenu.MenueState : 0,
                            }).ToList();

                subMenu = (from menu in _menuService.GetApplicationMenu().ToList()
                           join userMenu in _userMenuService.GetUserMenuByUserIdentity(userIdentityId) on menu.MenuId equals userMenu.TBASMenuId into menuUser
                           from userMenu in menuUser.DefaultIfEmpty()
                           orderby menu.Order ascending
                           where
                           menu.ParentMenuId != null
                           && menu.Active == true
                           && menu.IsButton == false 
                           select new UserMenuViewModel
                           {
                               MenuTitle = menu.Title,
                               MenuId = menu.MenuId,
                               ParentMenuId = menu.ParentMenuId,
                               IdentityUserId = userMenu != null ? userMenu.IdentityUserId : null,
                               MenuState = userMenu != null ? userMenu.MenueState : 0,
                           }).ToList();

                menuButton = (from menu in _menuService.GetApplicationMenu().ToList()
                              join userMenu in _userMenuService.GetUserMenuByUserIdentity(userIdentityId) on menu.MenuId equals userMenu.TBASMenuId into menuUser
                              from userMenu in menuUser.DefaultIfEmpty()
                              orderby menu.Order ascending
                              where
                              menu.IsButton == true
                              && menu.Active == true
                              select new UserMenuViewModel
                              {
                                  MenuTitle = menu.Title,
                                  MenuId = menu.MenuId,
                                  ParentMenuId = menu.ParentMenuId,
                                  IdentityUserId = userMenu != null ? userMenu.IdentityUserId : null,
                                  MenuState = userMenu != null ? userMenu.MenueState : 0,
                              }).ToList();


                return Json(new
                {
                    mainMenu = mainMenu,
                    subMenu = subMenu,
                    menuButton = menuButton,
                    errorMessage = errorMessage
                });
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        public JsonResult AddUserMenu(List<UserMenu> userMenu , string identityUserId)
        {
            string errorMessage = string.Empty;
            string message = string.Empty;
            try
            {
                _userMenuService.DeleteUserMenuByUserId(_userMenuService.GetUserMenuByUserIdentity(identityUserId).ToList());

                foreach (var item in userMenu)
                {
                    item.StartDate = item.FStartDate.ToString().ToDateTime();
                    item.EndDate = item.FEndDate.ToString().ToDateTime();
                }
                _userMenuService.AddUserMenu(userMenu, identityUserId);
                _userMenuService.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new 
            {
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess,
                errorMessage = errorMessage
            });
        }
    }
}
