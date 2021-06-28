using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI_Presentation.Controllers
{
    public class Managment : Controller
    {
        public IActionResult AddEditUser()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.CreateUser ?? UI_Presentation.wwwroot.Resources.General.Title.EditUser;
            return PartialView();
        }

        public IActionResult DefineUserRole()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.DefineUserRole;
            return PartialView();
        }
    }
}
