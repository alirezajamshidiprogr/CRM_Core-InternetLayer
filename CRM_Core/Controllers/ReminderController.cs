using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_Core.Entities.Models;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;

namespace UI_Presentation.Controllers
{
    public class ReminderController : Controller
    {
        public IActionResult AddEditReminder()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.SalonInfo;
            string errorMessage = string.Empty;

            Reminder getSalon = null;
            try
            {
                //getSalon = _salonInfoService.GetSalon().LastOrDefault();
                ViewBag.isEditMode = getSalon != null ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return PartialView();
        }

        public IActionResult ShowReminder()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.ReminderList;
            return PartialView();
        }
    }
}
