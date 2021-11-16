using System;
using System.Collections.Generic;
using System.Linq;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_Presentation.Models;

namespace CRM_Core.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region CONSTANT
        private IMenuService _menuService;
        private IUserMenuService _userMenuService;
        private IReminderService _reminderService;
        List<MenuViewModel> menu = new List<MenuViewModel>();
        #endregion

        #region Actions
        public HomeController(IMenuService menuService , IUserMenuService userMenuService, IReminderService reminderService)
        {
            _menuService = menuService;
            _userMenuService = userMenuService;
            _reminderService = reminderService;
        }
        public IActionResult Index()
        {
            TempData["UserFullName"] = SessionProperty.FullName;
            List<MenuViewModel> getUserMenu = new List<MenuViewModel>();
            IEnumerable<ReminderViewModelSearch> reminderList = new List<ReminderViewModelSearch>();

            getUserMenu = (from m in _menuService.GetApplicationMenu().ToList()
                            join u in _userMenuService.GetUserMenuByUserIdentity(SessionProperty.UserID) on m.MenuId equals u.TBASMenuId into menuUsers
                            from userMenu in menuUsers.DefaultIfEmpty()
                            orderby m.Order ascending
                            where m.IsButton == false 
                            select new MenuViewModel
                            {
                                MenuName = m.Name,
                                Title = m.Title,
                                Order = m.Order,
                                Event = m.Event,
                                MenuId = m.MenuId,
                                ParentMenuId = m.ParentMenuId,
                                IsActive = m.Active,
                                IsButton = m.IsButton,
                                IconClass = m.IconClass,
                                IsVisible = m.Visible,
                                hasAccess = userMenu != null ? userMenu.MenueState : 0 
                            }).ToList();

            foreach (var item in getUserMenu)
            {
                this.menu.Add(new MenuViewModel
                {
                    MenuName = item.MenuName,
                    Title = item.Title,
                    Order = item.Order,
                    Event = item.Event,
                    MenuId = item.MenuId,
                    ParentMenuId = item.ParentMenuId,
                    IsActive = item.IsActive,
                    IsButton = item.IsButton,
                    IconClass = item.IconClass,
                    IsVisible = item.IsVisible,
                    hasAccess = item.hasAccess
                });
            }

            this.ViewData["MenuItems"] = this.menu;
            //reminderList = _remidnerService.GetCurrentReminderDay("[dbo].[Reminder_Search]");
            //TempData["reminderListData"] = reminderList;
            return View("HomeIndex");
        }

        [HttpPost]
        public IActionResult GetCurrentDayInfo()
        {
            string errorMessage = string.Empty;
            string message = string.Empty;
            IEnumerable<ReminderViewModelSearch> reminderList = new List<ReminderViewModelSearch>();
            try
            {
                reminderList = _reminderService.GetCurrentReminderDay("[dbo].[Reminder_Search]");
                ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.ReminderList;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new { message = message, errorMessage = errorMessage, reminderListData = reminderList });

        }

        #endregion Action
    }
}
