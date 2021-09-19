using System;
using System.Linq;
using System.Threading.Tasks;
using CRM_Core.Application.Interfaces;
using CRM_Core.DomainLayer;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;

namespace CRM_Core.Controllers
{
    public class LoginController : Controller
    {
        #region CONSTANT
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IApplicationUserService applicationUserSerice)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._applicationUserService = applicationUserSerice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateUser(string UserName, string Password)
        {
            string errorMessage = string.Empty;
            try
            {
                //var user = new IdentityUser { UserName = "alireza", Email = "alierezajamshidi@gmail.com"  };
                //var user2 = new ApplicationUser { FirstName = "Alireza", LastName = "Jamshidi" };
                //var result2 = await userManager.CreateAsync(user2, "A137011a_");
                var result = await signInManager.PasswordSignInAsync(UserName, Password, false, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(UserName);
                    ApplicationUser applicationUser = _applicationUserService.GetByIdentityUserID(user.Id).FirstOrDefault();

                    SessionProperty.UserName = user.UserName;
                    SessionProperty.FullName = applicationUser.FirstName + string.Empty + applicationUser.LastName;
                    SessionProperty.UserID = user.Id;
                    SessionProperty.LoginTime = DateTime.Now.ToShortTimeString();

                    return Json(new { Success = true });
                }

                return Json(new
                {
                    Success = false,
                    errorMessage = UI_Presentation.wwwroot.Resources.Mesages.UserNamePasswordIsNotValid
                });
            }

            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }
        [HttpGet]
        public async Task<IActionResult> IndexLogout()
        {
            {
                try
                {
                    await signInManager.SignOutAsync();
                    return View("Index");
                }
                catch (Exception ex)
                {
                    return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
                }

            }
        }
    }
}


