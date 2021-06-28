using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;

namespace CRM_Core.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            ViewBag.isValidUser = TempData["isValidUser"];
            TempData["UserFullName"] = SessionProperty.FullName;
            return View();
        }
        
        public IActionResult IndexLogout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
