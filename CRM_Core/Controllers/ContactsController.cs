using System;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        public IActionResult AddEditContact()
        {
            string errorMessage = string.Empty;
            try
            {
                TempData["Title"] = "Test";
                return PartialView("AddEditContact");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }
    }
}
