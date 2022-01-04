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
        public IActionResult AddEditReservation(int? reservationId , bool isEdit)
        {
            string errorMessage = string.Empty;
            try
            {
                Reservation reservation = new Reservation();
                ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.AddReservation;
                List<TBASPayType> payTypeService = _payTypeService.getAllPayTypes().ToList();
                ViewBag.payTypeItems = payTypeService;
                
                if(isEdit)
                {
                    reservation = _reservationService.GetReservationById(reservationId.Value).FirstOrDefault();
                    ViewBag.isEdit = true;
                    ViewBag.peopleId = reservation.PeopleId;
                }

                return PartialView("AddEditReservation", reservation);
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
                List<SelectListItem> payTypeList = _payTypeService.getAllPayTypes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                ViewBag.PayTypeList = payTypeList;
                return PartialView("Reservation");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        public IActionResult FillReservationTableData(bool quickSearch, string fullName, ReservationViewModelSearch searchParams, string state)
        {
            string errorMessage = string.Empty;
            DataGridViewModel<ReservationViewModel> reservationList = new DataGridViewModel<ReservationViewModel>();
            try
            {
                string[] searchParameter;
                object[] searchValues;

                if (quickSearch)
                {
                    searchParameter = new string[] { "@FullName", "@Page", "@PageSize", "@Sort" };
                    searchValues = new object[] { fullName, searchParams.PageNumber, 10, string.Empty };
                }
                else
                {
                    searchParameter = new string[]
                    {
                         "@CustomerFirstName"
                        ,"@CustomerFamily"
                        ,"@TBASServiceId"
                        ,"@Date"
                        ,"@PayTypeId"
                        ,"@SystemCode"
                        ,"@FromTime"
                        ,"@ToTime"
                        ,"@Page"
                        ,"@PageSize"
                        ,"@Sort"
                    };

                    searchValues = new object[]
                    {
                        searchParams.CustomerFirstName
                       ,searchParams.CustomerFamily
                       ,searchParams.TBASServiceId
                       ,searchParams.Date.ToMiladiDate()
                       ,searchParams.PayTypeId
                       ,searchParams.ReservationSystemCode
                       ,searchParams.FromTime
                       ,searchParams.ToTime
                       ,searchParams.PageNumber
                       ,10
                       ,string.Empty
                    };

                }
                
                DataSet dsData = _peopleService.GetPeopleDataTable("[dbo].[Reservation_Search]", searchParameter, searchValues);
                ViewBag.totalAllRecords = (int)dsData.Tables[1].Rows[0][0];
                ViewBag.pageNumber = searchParams.PageNumber;
                reservationList.Records = MappingUtility.DataTableToList<ReservationViewModel>(dsData.Tables[0]).AsQueryable();
                reservationList.TotalCount = reservationList.Records.Count();
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillReservationTableData", reservationList);
        }

        public ActionResult DeleteReservation(int reservationId)
        {
            string result = string.Empty;
            string message = string.Empty;
            try
            {
                Reservation reservation = _reservationService.GetReservationById(reservationId).FirstOrDefault();
                reservation.IsActive = false;
                _reservationService.UpdateReservation(reservation);
                _peopleService.SaveChanges();
                message = UI_Presentation.wwwroot.Resources.Mesages.DeleteSuccessfullyApplied;
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                result = result,
                message = message,
            });
        }


        #endregion Action

        #region Methods
        [HttpPost]
        public ActionResult GetSalonServices()
        {
            string result = string.Empty;
            List<TempClerkService> servicesItems = new List<TempClerkService>();
            try
            {
                // NOTE : THIS QUESRY IS NOT BENIFIT QUERY 
                List<People> listPeople = (from _p in _peopleService.GetPeople() select _p).ToList();
                List<TBASServices> listTBASServices = (from _p in _tbasServiceService.GetAllServices() select _p).ToList();
                List<ClerkServices> listClerkServices = (from _p in _clerkServiceService.GetAllSalonServices() select _p).ToList();


                servicesItems = (from _services in listTBASServices
                                join _clerkService in listClerkServices on _services.Id equals _clerkService.TBASServicesId
                                join _people in listPeople on _clerkService.PeopleId equals _people.Id
                                select new TempClerkService
                                {
                                    TbasServiceId = _services.Id,
                                    ClerkServiceId = _clerkService.Id,
                                    ServiceName = _services.Name ,
                                    PersonnelId = _clerkService.PeopleId,
                                }).ToList();

                servicesItems.Insert(0, new TempClerkService() { ServiceName = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, PersonnelId = null });

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
                clerks.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
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
        public ActionResult AddEditReservationMethod(bool isEdit, List<TempReservationDetails> reservationDetails)
        {
            string result = string.Empty;
            string message = string.Empty;
            try
            {
                //ModelState["reservation.Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //ModelState["reservation.SystemCode"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //ModelState["reservation.Customer"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

                if (!ModelState.IsValid)
                    throw new CustomeException("Model Is Not Valid", true, null);

                if (Utility.SubtractDaysDates((DateTime)reservationDetails[0].P_ReservationDate.ToDateTime(), DateTime.Now) < 0)
                {
                    result = wwwroot.Resources.Mesages.DateReservationCanNotBeLessDateTimeNow;
                    throw new CustomeException("Date Of Reservation Is Less Than DateTimeNow", true, null);
                }

                //List<PeopleServices> listPeopleService = _peopleServiceService.getPeopleServiceByReservationId
                //foreach (var item in peopleServices)
                //{
                //    if (item.ClerkServicesId ==)
                //}
                Reservation reservation = new Reservation();
                reservation.M_ReservationDate = (DateTime)reservationDetails[0].P_ReservationDate.ToDateTime();
                reservation.P_ReservationDate = reservationDetails[0].P_ReservationDate;
                reservation.PeopleId = reservationDetails[0].CustomerId;
                reservation.SystemCode = !isEdit ? _generetedNumberService.NewGenerateNumber(SessionProperty.UserID, (int)Enums.states.Reservation) : reservation.SystemCode;
                if (isEdit)
                {
                    List<ReservationDetails> oldPeopleService = _peopleServiceService.getPeopleServiceByReservationId(reservation.Id).ToList();
                    foreach (var item in oldPeopleService)
                    {
                        _peopleServiceService.removePeopleServiceByReservationId(item);
                    }

                    _reservationService.UpdateReservation(reservation);
                    for (int i = 0; i < reservationDetails.Count; i++)
                    {
                        ReservationDetails newPeopleService = new ReservationDetails();
                        newPeopleService.ClerkServicesId = reservationDetails[i].ClerkServicesId;
                        newPeopleService.ReservationId = _reservationService.GetLastReservationId().Id;
                        newPeopleService.isSalonCustomer = reservationDetails[i].isSalonCustomer;

                        _peopleServiceService.insertPeopleService(newPeopleService);

                    }
                }
                else
                {
                    reservation.IsActive = true;
                    reservation.M_ReservationInsertDate = DateTime.Now;
                    reservation.Description = reservationDetails[0].Description;
                    _reservationService.insertReservation(reservation);
                    _reservationService.SaveChanges();

                    for (int i = 0; i < reservationDetails.Count; i++)
                    {
                        ReservationDetails reservationDetailInfo = new ReservationDetails();

                       reservationDetailInfo.ClerkServicesId = reservationDetails[i].ClerkServicesId;
                       reservationDetailInfo.ReservationId = reservation.Id;
                       reservationDetailInfo.isSalonCustomer = reservationDetails[i].isSalonCustomer;
                       reservationDetailInfo.FromTime = reservationDetails[i].FromTime;
                       reservationDetailInfo.ToTime = reservationDetails[i].ToTime;
                       reservationDetailInfo.isSalonCustomer = reservationDetails[i].isSalonCustomer;

                        _peopleServiceService.insertPeopleService(reservationDetailInfo);
                    }
                }
                _reservationService.SaveChanges();
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;
            }
            catch (Exception ex)
            {
                result = result != string.Empty ? result : UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = result });
            }
            return Json(new
            {
                result = result,
                message = message,
            });
        }

        [HttpPost]
        public ActionResult GetPeopleReservationInfo(int peopleId)
        {
            string result = string.Empty;
            PeopleReservationHistoryInfo getpeopleReservationHistory = new PeopleReservationHistoryInfo();
            try
            {
                getpeopleReservationHistory = _reservationService.GetPeopleHistoryReservation(peopleId);
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                errorMessage = result,
                getpeopleReservationHistory= getpeopleReservationHistory ,
            });
        }


        #endregion Methods

    }
}
