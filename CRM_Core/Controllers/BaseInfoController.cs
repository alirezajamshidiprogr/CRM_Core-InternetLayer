using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using CRM_Core.Entities.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_Presentation.Models;
using static CRM_Core.Infrastructure.Enums;
using CRM_Core.Infrastructure;
using MyCRM.Layered.Model.Utility;
using CRM_Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CRM_Core.Application.GridViewModels;
using CRM_Core.Application.ViewModels.CustomViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class BaseInfoController : Controller
    {
        #region CONSTANT
        private IClerkServiceService _clerkServiceService;
        private ITBASServiceService _tbasServiceService;
        private IPeopleService _peopleService;
        private IGeneratedNumberService _generetedNumberService;
        private IPeopleServiceService _peopleServiceService;
        private IReservationService _reservationService;
        private IReservationDetailsService _reservationDetailsService;
        private ITBASPayTypeService _payTypeService;
        #endregion

        public BaseInfoController(IClerkServiceService clerkServiceService, ITBASServiceService tbasServiceService, IPeopleService peopleService, IGeneratedNumberService generetedNumberService, IPeopleServiceService peopleServiceService, IReservationService reservationService, ITBASPayTypeService payTypeService, IReservationDetailsService reservationDetailsService)
        {
            this._clerkServiceService = clerkServiceService;
            this._tbasServiceService = tbasServiceService;
            this._peopleService = peopleService;
            this._generetedNumberService = generetedNumberService;
            this._peopleServiceService = peopleServiceService;
            this._reservationService = reservationService;
            this._payTypeService = payTypeService;
            this._reservationDetailsService = reservationDetailsService;
        }
        #region Actions
        public IActionResult AddEditReservation(int? reservationId, bool isEdit)
        {
            string errorMessage = string.Empty;
            try
            {
                if (isEdit)
                {
                    
                }

                return PartialView("AddEditReservation");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        [HttpGet]
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

        public IActionResult FillReservationTableData(bool quickSearch, string fullName, ReservationViewModelSearch searchParams)
        {
            string errorMessage = string.Empty;
            try
            {
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillReservationTableData");
        }

        public ActionResult DeleteReservation(int reservationId)
        {
            string errorMessage = string.Empty;
            bool saveException = false;
            try
            {
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                errorMessage = errorMessage,
                message = UI_Presentation.wwwroot.Resources.Mesages.DeleteSuccessfullyApplied,
            });
        }


        #endregion Action

        #region Methods



        #endregion Methods

    }
}
