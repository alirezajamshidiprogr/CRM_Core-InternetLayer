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
using CRM_Core.Entities.Models.General;
using CRM_Core.Entities.Models;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        #region CONSTANT
        private IChequService _chequeService;
        private IClerkServiceService _clerkServiceService;
        private ITBASServiceService _tbasServiceService;
        private IPeopleService _peopleService;
        private IGeneratedNumberService _generetedNumberService;
        private IPeopleServiceService _peopleServiceService;
        private IReservationService _reservationService;
        private IReservationDetailsService _reservationDetailsService;
        private ITBASPayTypeService _payTypeService;
        #endregion

        public ReservationController(IClerkServiceService clerkServiceService, ITBASServiceService tbasServiceService, IPeopleService peopleService, IGeneratedNumberService generetedNumberService, IPeopleServiceService peopleServiceService, IReservationService reservationService, ITBASPayTypeService payTypeService, IReservationDetailsService reservationDetailsService, IChequService chequeService)
        {
            this._clerkServiceService = clerkServiceService;
            this._chequeService = chequeService;
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
            bool savaException = false;
            try
            {
                Reservation reservation = new Reservation();
                if (isEdit)
                {
                    //  NOTE : CHECK THIS RESERVATION HAS ANY HISTORY ? 

                    Reservation getReservation = _reservationService.GetReservationById(reservationId.Value).LastOrDefault();
                    if (_reservationDetailsService.HasAnyRecordHistory(getReservation))
                        throw new CustomeException(string.Empty, UI_Presentation.wwwroot.Resources.Mesages.ThereIsRecordHistoryForThisReservationRecordHistory, false, null, ref errorMessage, ref savaException);

                    reservation = _reservationService.GetReservationById(reservationId.Value).FirstOrDefault();
                    ViewBag.isEdit = true;
                    ViewBag.peopleId = reservation.PeopleId;
                    string[] searchParameter;
                    object[] searchValues;
                    searchParameter = new string[] { "@ReservationId" };
                    searchValues = new object[] { reservationId };
                    DataSet ds = _reservationService.GetReservationDetailsADO_ByID("[dbo].[Sp_Reservation_GetReservationDetailsByID]", searchParameter, searchValues);
                    DataTable dtPersonnels = ds.Tables[0];
                    DataTable dtServices = ds.Tables[1];
                    DataTable dtReservationDetails = ds.Tables[2];

                    List<SelectListItem> personnels = new List<SelectListItem>() { };
                    List<TempClerkService> services = new List<TempClerkService>();
                    List<TempReservationDetails> reservationDetails = new List<TempReservationDetails>();

                    for (int i = 0; i < dtPersonnels.Rows.Count; i++)
                        personnels.Add(new SelectListItem() { Text = dtPersonnels.Rows[i]["FullName"].ToString(), Value = dtPersonnels.Rows[i]["Id"].ToString() });

                    for (int i = 0; i < dtServices.Rows.Count; i++)
                        services.Add(new TempClerkService() { ClerkServiceId = (int)dtServices.Rows[i]["ClerkServiceId"], PersonnelId = (int)dtServices.Rows[i]["PersonnelId"], ServiceName = dtServices.Rows[i]["ServiceName"].ToString(), TbasServiceId = (int)dtServices.Rows[i]["TBASServicesId"] });


                    for (int i = 0; i < dtReservationDetails.Rows.Count; i++)
                        reservationDetails.Add(new TempReservationDetails() { FromTime = dtReservationDetails.Rows[i]["FromTime"].ToString(), ToTime = dtReservationDetails.Rows[i]["ToTime"].ToString(), CustomerId = (int)dtReservationDetails.Rows[i]["PeopleId"], ClerkServicesId = (int)dtReservationDetails.Rows[i]["TBASServicesId"], isSalonCustomer = (bool)dtReservationDetails.Rows[i]["isSalonCustomer"] });

                    ViewBag.personnels = personnels;
                    ViewBag.services = services;
                    ViewBag.reservationDetails = reservationDetails;
                    //TempData["customerServices"];
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

        [HttpGet]
        public ActionResult Index()
        {
            string errorMessage = string.Empty;
            try
            {
                List<SelectListItem> reservationStatus = new List<SelectListItem>()
                {
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null , Selected = true },
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.Reservation.Title.IsExpired, Value = "0" , Selected = false },
                        new SelectListItem (){ Text =UI_Presentation.wwwroot.Resources.Reservation.Title.IsNotExpired, Value = "1", Selected = false },
                };

                List<SelectListItem> chequeStateus = new List<SelectListItem>()
                {
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null , Selected = true },
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.Reservation.Title.HasCheque, Value = "0" , Selected = false },
                        new SelectListItem (){ Text =UI_Presentation.wwwroot.Resources.Reservation.Title.HasNotCheque, Value = "1", Selected = false },
                };
                ViewBag.reservationStatus = reservationStatus;
                ViewBag.chequeStateus = chequeStateus;
                return PartialView("Reservation");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        public IActionResult FillReservationTableData(bool quickSearch, string fullName, ReservationViewModelSearch searchParams, bool isSelectMode)
        {
            string errorMessage = string.Empty;
            DataGridViewModel<ReservationViewModel> reservationList = new DataGridViewModel<ReservationViewModel>();
            try
            {
                string[] searchParameter;
                object[] searchValues;

                if (quickSearch)
                {
                    searchParameter = new string[] { "@CustomerFirstName", "@Page", "@PageSize", "@Sort" };
                    searchValues = new object[] { fullName, searchParams.PageNumber, 10, string.Empty };
                }
                else
                {
                    searchParameter = new string[]
                    {
                         "@CustomerFirstName"
                        ,"@CustomerFamily"
                        ,"@FromReservationDate"
                        ,"@ToReservationDate"
                        ,"@SystemCode"
                        ,"@PeopleCode"
                        ,"@IsExpired"
                        ,"@HasCheque    "
                        ,"@Page"
                        ,"@PageSize"
                        ,"@Sort"
                    };

                    searchValues = new object[]
                    {
                        searchParams.CustomerFirstName
                       ,searchParams.CustomerFamily
                       ,searchParams.FromReservationDate.ToDateTime()
                       ,searchParams.ToReservationDate.ToDateTime()
                       ,searchParams.SystemCode
                       ,searchParams.PeopleCode
                       ,searchParams.IsExpired
                       ,searchParams.HasCheque
                       ,searchParams.PageNumber
                       ,10
                       ,string.Empty
                    };

                }

                DataSet dsData = _reservationService.GetReservationByADO("[dbo].[Reservation_Search]", searchParameter, searchValues);
                ViewBag.totalAllRecords = (int)dsData.Tables[1].Rows[0][0];
                ViewBag.pageNumber = searchParams.PageNumber;
                ViewData["isSelectMode"] = isSelectMode;
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
            string errorMessage = string.Empty;
            bool saveException = false;
            try
            {
                // NOTE : CHECKE THIS RESERVATION HAS ANY RECORD HISTORY IN CHEQUE SERVICE 
                if (_reservationService.CheckReservationHasAnyHistoryRecord(reservationId))
                    throw new CustomeException(string.Empty, UI_Presentation.wwwroot.Resources.Mesages.TheEnterTimeIsNotValid, false, null, ref errorMessage, ref saveException);

                Reservation reservation = _reservationService.GetReservationById(reservationId).FirstOrDefault();
                reservation.IsActive = false;
                _reservationService.UpdateReservation(reservation);
                _peopleService.SaveChanges();
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
                                     ServiceName = _services.Name,
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

        [HttpPost]
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
        public ActionResult AddEditReservationMethod(int? reservationId, bool isEdit, List<TempReservationDetails> reservationDetails, bool allowedDuplicatedReservation)
        {
            string errorMessage = string.Empty;
            string message = string.Empty;
            bool saveException = false;
            TimeSpan _fromTime;
            TimeSpan _toTime;

            try
            {
                //ModelState["reservation.Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //ModelState["reservation.SystemCode"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //ModelState["reservation.Customer"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                Reservation getReservation = new Reservation();

                if (!ModelState.IsValid)
                    throw new CustomeException("Model Is Not Valid", true, null, ref saveException);

                if (isEdit && Utility.SubtractDaysDates((DateTime)reservationDetails[0].P_ReservationDate.ToDateTime(), DateTime.Now) < 0)
                    throw new CustomeException("Date Of Reservation Is Less Than DateTimeNow", "نوبت های منقضی شده قابل ویرایش نیستند ", true, null, ref errorMessage, ref saveException);
                else if (!isEdit && Utility.SubtractDaysDates((DateTime)reservationDetails[0].P_ReservationDate.ToDateTime(), DateTime.Now) < 0)
                    throw new CustomeException("Date Of Reservation Is Less Than DateTimeNow", UI_Presentation.wwwroot.Resources.Mesages.DateReservationCanNotBeLessDateTimeNow, true, null, ref errorMessage, ref saveException);

                Reservation reservation = new Reservation();
                //NOTE: CHECK IF HAS ANY RESERVATION FOR THIS CUSTOMER IN THIS DATE
                Reservation getCustoemrReservation = _reservationService.GetReservationByParam(reservationDetails[0].CustomerId, Convert.ToDateTime(reservationDetails[0].P_ReservationDate.ToDateTime())).LastOrDefault();
                if (allowedDuplicatedReservation == false && getCustoemrReservation != null)
                    return Json(new { isQuestionMessage = true, message = UI_Presentation.wwwroot.Resources.Mesages.ThereIsAReservationOnThisDateForThisCustomer });

                if (isEdit)
                {
                    getReservation = _reservationService.GetReservationById(reservationId.Value).LastOrDefault();


                    // NOTE : CHECK IF HAS ANY RESERVATION FOR THIS CUSTOMER IN THIS DATE 
                    // FOR UPDATE MUST CHECK DATE RESERVATION
                    getReservation.PeopleId = reservationDetails[0].CustomerId;
                    getReservation.M_ReservationEditDate = DateTime.Now;
                    getReservation.P_ReservationDate = reservationDetails[0].P_ReservationDate;
                    getReservation.M_ReservationDate = Convert.ToDateTime(reservationDetails[0].P_ReservationDate.ToDateTime());
                    getReservation.Description = reservationDetails[0].Description;

                    _reservationService.UpdateReservation(getReservation);

                    List<ReservationDetails> getReservationDetails = _reservationDetailsService.GetReservationDetailsByReservationId(reservationId.Value).ToList();
                    _reservationDetailsService.DeleteReservationDetails(getReservationDetails);
                }
                else
                {
                    reservation.SystemCode = _generetedNumberService.NewGenerateNumber(SessionProperty.UserID, (int)Enums.states.Reservation);
                    reservation.M_ReservationInsertDate = DateTime.Now;
                    reservation.P_ReservationDate = reservationDetails[0].P_ReservationDate;
                    reservation.M_ReservationDate = Convert.ToDateTime(reservationDetails[0].P_ReservationDate.ToDateTime());
                    reservation.PeopleId = reservationDetails[0].CustomerId;
                    reservation.Description = reservationDetails[0].Description;
                    reservation.IsActive = true;

                    _reservationService.InsertReservation(reservation);

                    // NOTE : IN EACH INSERT MUST ADD RELATIVE NUMBER INTO THIS TABLE FOR NEXT SESSIONS  
                    ActivityNumber activityNumber = new ActivityNumber();
                    activityNumber.TBASStateId = (int)Enums.states.Reservation;
                    activityNumber.RelatedNumber = reservation.SystemCode;
                    _generetedNumberService.InsertNumberInActivity(activityNumber);

                }
                // NOTE : INSERT DETAILS RESERVATION 
                for (int i = 0; i < reservationDetails.Count; i++)
                {
                    ReservationDetails reservationDetailInfo = new ReservationDetails();

                    string fromTime = reservationDetails[i].FromTime;
                    if (TimeSpan.TryParse(fromTime, out _fromTime) == false)
                        throw new CustomeException(string.Empty, UI_Presentation.wwwroot.Resources.Mesages.TheEnterTimeIsNotValid, false, null, ref errorMessage, ref saveException);

                    string toTime = reservationDetails[i].ToTime;
                    if (TimeSpan.TryParse(toTime, out _toTime) == false)
                        throw new CustomeException(string.Empty, UI_Presentation.wwwroot.Resources.Mesages.TheEnterTimeIsNotValid, false, null, ref errorMessage, ref saveException);


                    reservationDetailInfo.Reservation = isEdit ? getReservation : reservation;
                    reservationDetailInfo.ClerkServicesId = reservationDetails[i].ClerkServicesId;
                    reservationDetailInfo.isSalonCustomer = reservationDetails[i].isSalonCustomer;
                    reservationDetailInfo.FromTime = Convert.ToDateTime(reservationDetails[i].FromTime).TimeOfDay;
                    reservationDetailInfo.ToTime = Convert.ToDateTime(reservationDetails[i].ToTime).TimeOfDay;
                    reservationDetailInfo.isSalonCustomer = reservationDetails[i].isSalonCustomer;

                    _reservationDetailsService.InsertReservationDetails(reservationDetailInfo);
                }

                _reservationService.SaveChanges();
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;
            }
            catch (Exception ex)
            {
                if (saveException)
                    Utility.RegisterErrorLog(ex, SessionProperty.UserName);

                return Json(new { errorMessage = errorMessage != null && errorMessage != string.Empty ? errorMessage : UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return Json(new { message = message, errorMessage = errorMessage });
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
                getpeopleReservationHistory = getpeopleReservationHistory,
            });
        }

        public ActionResult GetReservationInfoById(string relativeNumber, string type)
        {
            string errorMessage = string.Empty;
            Reservation reservation = new Reservation();
            People people = new People();
            bool saveException = false;
            try
            {
                Cheque hasAnyCheque = new Cheque();
                if (type == "id")
                {
                    hasAnyCheque = _chequeService.GetChequeByReservationId(Convert.ToInt32(relativeNumber));
                    reservation = _reservationService.GetReservationById(Convert.ToInt32(relativeNumber)).FirstOrDefault();
                }
                else
                {
                    hasAnyCheque = _chequeService.GetChequeByReservationNubmer(relativeNumber);
                    reservation = _reservationService.GetReservationByNumber(relativeNumber);
                }


                if (hasAnyCheque != null)
                    throw new CustomeException(string.Empty, UI_Presentation.wwwroot.Resources.Mesages.ForThisReservationHasRegisterdCheque, false, null, ref errorMessage, ref saveException);

                people = _peopleService.GetPeopleById(reservation.PeopleId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                errorMessage = errorMessage,
                reservationDate = reservation.P_ReservationDate,
                reservationId = reservation.Id,
                reservationSystemCode = reservation.SystemCode,
                description = reservation.Description,
                peopleProperty = people.FirstName + " " + people.LastName,
            });
        }

        #endregion Methods

    }
}
