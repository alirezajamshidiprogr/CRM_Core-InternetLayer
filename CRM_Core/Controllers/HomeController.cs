using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_Presentation.Models;

namespace CRM_Core.Controllers
{
    public class HomeController : Controller
    {
        #region CONSTANT
        private IUserService _userService;
        #endregion
        public HomeController(IUserService peopleService)
        {
            _userService = peopleService;
        }

        [HttpGet]
        public IActionResult Index(User user)
        {
            string result = string.Empty;
            string message = string.Empty;
            var list = _userService.GetUsers();

            try
            {
                var getUser = _userService.GetUserByUserNamePassword(user.UserName, user.Password).FirstOrDefault();
                if (getUser != null)
                {
                    SessionProperty.UserName = getUser.UserName;
                    SessionProperty.FullName = getUser.FirstName+ " " + getUser.LastName;
                    SessionProperty.UserID = getUser.Id;
                    SessionProperty.LoginTime = DateTime.Now.ToShortTimeString() ;
                    
                   //HttpContext.Session.SetString("UserName", user.UserName);
                    return View();
                }
                TempData["isValidUser"] = false;
                return RedirectToAction("Index", "Login");
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
