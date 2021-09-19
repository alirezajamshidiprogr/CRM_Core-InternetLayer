using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.WebPages.Html;
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
using System.Data.Entity;
using CRM_Core.Application.GridViewModels;
using CRM_Core.Application.ViewModels.CustomViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        #region CONSTANT
        private IClerkServiceService _clerkServiceService;
        private ITBASServiceService _tbasServiceService;
        private IPeopleService _peopleService;
        private IGeneratedNumberService _generetedNumberService;
        private IPeopleServiceService _peopleServiceService;
        private IReservationService _reservationService;
        private ITBASPayTypeService _payTypeService;
        #endregion

        public ReservationController(IClerkServiceService clerkServiceService, ITBASServiceService tbasServiceService, IPeopleService peopleService, IGeneratedNumberService generetedNumberService, IPeopleServiceService peopleServiceService, IReservationService reservationService, ITBASPayTypeService payTypeService)
        {
            this._clerkServiceService = clerkServiceService;
            this._tbasServiceService = tbasServiceService;
            this._peopleService = peopleService;
            this._generetedNumberService = generetedNumberService;
            this._peopleServiceService = peopleServiceService;
            this._reservationService = reservationService;
            this._payTypeService = payTypeService;
        }
        #region Actions
        public IActionResult AddEditReservation(int? peopleId)
        {
            ViewBag.peopleId = peopleId;
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.AddReservation;
            List<SelectListItem> payTypeService = _payTypeService.getAllPayTypes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
            ViewBag.payTypeItems = payTypeService;
            return PartialView();
        }

        public ActionResult Index(ReservationViewModelSearch searchParams)
        {
            string errorMessage = string.Empty;
            try
            {
                DataGridViewModel<ReservationViewModel> reservationList = new DataGridViewModel<ReservationViewModel>();
                string[] searchParameter;
                object[] searchValues;
                searchParameter = new string[]
                {
                     "@PeopleName"
                    ,"@PeopleLastName"
                    ,"@CustomerName"
                    ,"@CustomerLastName"
                    ,"@TBASServiceId"
                    ,"@Date"
                    ,"@PayTypeId"
                    ,"@SystemCode"
                    ,"@FromTime"
                    ,"@ToTime"
                };

                searchValues = new object[]
                {
                    searchParams.PeopleName
                   ,searchParams.PeopleLastName
                   ,searchParams.CustomerName
                   ,searchParams.CustomerLastName
                   ,searchParams.TBASServiceId
                   ,searchParams.Date.ToMiladiDate() 
                   ,searchParams.PayTypeId
                   ,searchParams.SystemCode
                   ,searchParams.FromTime
                   ,searchParams.ToTime
                };
                ViewBag.isEditMode = true;
                ViewBag.isSelectedMode = true;
                ViewBag.isPrintMode = true;
                
                List<SelectListItem> payTypeList = _payTypeService.getAllPayTypes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                ViewBag.PayTypeList = payTypeList;

                reservationList.Records = _reservationService.GetReservationByADO("[dbo].[Reservation_Search]", searchParameter, searchValues);
                reservationList.TotalCount = reservationList.Records.Count();
                return PartialView("Reservation",reservationList);
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
        public ActionResult GetSalonServices(int? clerkId)
        {
            string result = string.Empty;
            IEnumerable<TempClerkService> servicesItems = new List<TempClerkService>();
            try
            {
                List<People> listPeople = (from _p in _peopleService.GetPeople() select _p).ToList();
                List<TBASServices> listTBASServices = (from _p in _tbasServiceService.GetAllServices() select _p).ToList();
                List<ClerkServices> listClerkServices = (from _p in _clerkServiceService.GetAllSalonServices() select _p).ToList();


                servicesItems = from _services in listTBASServices
                                join _clerkService in listClerkServices on _services.Id equals _clerkService.TBASServicesId
                                join _people in listPeople on _clerkService.PeopleId equals _people.Id
                                select new TempClerkService
                                {
                                    TbasServiceId = _services.Id,
                                    ClerkServiceId = _clerkService.Id,
                                    ServiceName = _services.Name + "(" + _people.FirstName + " " + _people.LastName + ")",
                                    ClerkId = _clerkService.PeopleId,
                                };
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new
            {
                result = result,
                serviceList = servicesItems,
            });
        }

        public ActionResult GetClerks(int peopleId)
        {
            string result = string.Empty;
            List<SelectListItem> clerks = null;
            try
            {
                clerks = (from _people in _peopleService.GetPeopleByCategoryId((int)tbasCategoryState.personnel)
                          select new
                          {
                              Fullname = _people.FirstName + " " + _people.LastName,
                              Id = _people.Id
                          }).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Fullname, Value = item.Id.ToString() }; });
                clerks.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.AllItems, Value = null });
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new
            {
                result = result,
                clerk = clerks,
            });
        }

        [HttpPost]
        public ActionResult AddEditReservationMethod(bool isEdit, List<TempPeopleServices> peopleServices, Reservation reservation)
        {
            string result = string.Empty;
            string message = string.Empty;
            try
            {
                ModelState["reservation.Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                ModelState["reservation.SystemCode"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                ModelState["reservation.People"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

                if (!ModelState.IsValid)
                    throw new CustomeException("Model Is Not Valid", true, null);
                reservation.M_ReservationDate = reservation.P_ReservationDate.ToDateTime();
                reservation.SystemCode = !isEdit ? _generetedNumberService.NewGenerateNumber(SessionProperty.UserID, (int)Enums.states.Reservation) : reservation.SystemCode;
                if (isEdit)
                {
                    List<PeopleServices> oldPeopleService = _peopleServiceService.getPeopleServiceByReservationId(reservation.Id).ToList();
                    foreach (var item in oldPeopleService)
                    {
                        _peopleServiceService.removePeopleServiceByReservationId(item);
                    }

                    _reservationService.UpdateReservation(reservation);
                    for (int i = 0; i < peopleServices.Count; i++)
                    {
                        PeopleServices newPeopleService = new PeopleServices();
                        newPeopleService.ClerkServicesId = peopleServices[i].ClerkServicesId;
                        newPeopleService.ReservationId = _reservationService.GetLastReservationId().Id;
                        newPeopleService.isSalonCustomer = peopleServices[i].isSalonCustomer;

                        _peopleServiceService.insertPeopleService(newPeopleService);

                    }
                }
                else
                {
                    _reservationService.insertReservation(reservation);
                    _reservationService.SaveChanges();

                    for (int i = 0; i < peopleServices.Count; i++)
                    {
                        PeopleServices newPeopleService = new PeopleServices();

                        newPeopleService.ClerkServicesId = peopleServices[i].ClerkServicesId;
                        newPeopleService.ReservationId = reservation.Id;
                        newPeopleService.isSalonCustomer = peopleServices[i].isSalonCustomer;

                        _peopleServiceService.insertPeopleService(newPeopleService);
                    }
                }
                _reservationService.SaveChanges();
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new
            {
                result = result,
                message = message,
            });
        }


        #endregion Methods

    }
}
