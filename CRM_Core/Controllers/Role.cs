using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI_Presentation.Controllers
{
    public class Role : Controller
    {
        public IActionResult AddEditRole()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.CreateNewRole;
            return PartialView();
        }
    }
}
