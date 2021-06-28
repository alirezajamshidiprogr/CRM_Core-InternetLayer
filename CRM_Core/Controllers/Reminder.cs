using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI_Presentation.Controllers
{
    public class Reminder : Controller
    {
        public IActionResult AddEditReminder()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.AddReminder;
            return PartialView();
        }

        public IActionResult ShowReminder()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.ReminderList;
            return PartialView();
        }
    }
}
