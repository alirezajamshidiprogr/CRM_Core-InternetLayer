using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI_Presentation.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return PartialView();
        }

        public IActionResult AddEditProfile()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.EditProfile;
            return PartialView();
        }
    }
}
