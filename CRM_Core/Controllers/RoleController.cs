using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using UI_Presentation.Models;

namespace UI_Presentation.Controllers
{
    public class RoleController : Controller
    {
        #region CONSTANT
        #endregion

        #region Actions
        public IActionResult AddEditRole(int? roleId, bool isEdit)
        {
            string errorMessage = string.Empty;
            try
            {


                if (isEdit)
                {

                }

                return PartialView("AddEditRole");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        public ActionResult Index()
        {
            string errorMessage = string.Empty;
            try
            {
                return PartialView("Reservation");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }



        #endregion Action

        #region Methods


        #endregion Methods
    }
}
