using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        #region CONSTANT
        private IReminderService _remidnerService;
        #endregion

        public DashboardController(IReminderService remidnerService)
        {
            _remidnerService = remidnerService;
        }

        #region Action
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ReminderViewModelSearch> reminderList = new List<ReminderViewModelSearch>();
            reminderList = _remidnerService.GetCurrentReminderDay("[dbo].[Reminder_Search]");
            ViewBag.reminderListData = reminderList;
            return PartialView("Dashboard");
        }

        public IActionResult AddEditProfile()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.EditProfile;
            return PartialView();
        }
        #endregion Action
    }
}
