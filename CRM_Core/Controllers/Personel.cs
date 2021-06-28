using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI_Presentation.Controllers
{
    public class Personel : Controller
    {
        public IActionResult AddEditPersonel()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.Personel.Title.AddPersonel;
            return PartialView();
        }
    }
}
