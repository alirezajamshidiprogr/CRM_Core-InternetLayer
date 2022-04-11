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
using CRM_Core.Entities.Models;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ChequeController : Controller
    {
        #region CONSTANT
        private IPeopleService _peopleService;
        private IClerkServiceService _clerckService;
        private IReservationService _reservationService;
        private IReservationDetailsService _reservationDetailsService;
        private ITBASServiceService _tbasService;
        private IChequService _chequeService;
        ITBASPayTypeService _payTypeService ; 
        #endregion

        public ChequeController(IClerkServiceService clerckService, IReservationDetailsService reservationDetailsService, ITBASServiceService tbasService, IPeopleService peopleService, IChequService chequeService, IReservationService reservationService, ITBASPayTypeService payTypeService)
        {
            _clerckService = clerckService;
            _reservationDetailsService = reservationDetailsService;
            _tbasService = tbasService;
            _peopleService = peopleService;
            _chequeService = chequeService;
            _reservationService = reservationService;
            _payTypeService = payTypeService;
        }
        #region Actions
        public IActionResult AddEditCheque(int? reservationId, bool isEdit)
        {
            string errorMessage = string.Empty;
            try
            {
                Cheque getLastCheque = _chequeService.getAllCheque().LastOrDefault();
                List<SelectListItem> payTypeList = _payTypeService.getAllPayTypes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                ViewBag.PayTypeList = payTypeList;
                if (getLastCheque == null)
                    ViewBag.ChequeNumber = "100";
                else
                    ViewBag.ChequeNumber = Convert.ToInt32(getLastCheque) + 1;

                if (isEdit)
                {

                }

                return PartialView("AddEditCheque");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        public IActionResult FillCustomerServicesItemsData(int reservationId)
        {
            string errorMessage = string.Empty;
            List<ChequCustomerServiceViewModel> chequeCustomerService = new List<ChequCustomerServiceViewModel>();
            DataGridViewModel<ChequCustomerServiceViewModel> chequeList = new DataGridViewModel<ChequCustomerServiceViewModel>();
            try
            {
                Reservation reservation= _reservationService.GetReservationById(reservationId).FirstOrDefault();
                IEnumerable<ReservationDetails> reservationDetials = _reservationDetailsService.GetReservationDetailsByReservationId(reservationId).ToList();
                IEnumerable<ClerkServices> clerkServiveList = _clerckService.GetAllSalonServices();
                IEnumerable<TBASServices> getServices = _tbasService.GetAllServices();
                IEnumerable<People> getPeople = _peopleService.GetPeople();


                chequeCustomerService = (from _r in reservationDetials
                                         join _c in clerkServiveList on _r.ClerkServicesId equals _c.Id
                                         join _service in getServices on _c.TBASServicesId equals _service.Id
                                         join _p in getPeople on _c.PeopleId equals _p.Id
                                         select new ChequCustomerServiceViewModel
                                         {
                                                ServiceName = _service.Name ,
                                                ClerkName = _p.FirstName + " - " + _p.LastName,
                                                TotalPrice = _c.Salary , 
                                                PersonnelPortion = _r.isSalonCustomer ? (_c.Salary / 100 ) * _c.PersonnelPortionPercentage : 0 , // اگه مشتری شخصی هست درصد حساب کن برای پرسنل
                                                SalonPortion = _r.isSalonCustomer ? _c.Salary - ((_c.Salary / 100 ) * _c.PersonnelPortionPercentage) : _c.Salary
                                         }).ToList();

                People getCustomerInfo = _peopleService.GetPeopleById(reservation.PeopleId).FirstOrDefault();

                ViewBag.customerName = getCustomerInfo.FirstName + " - " + getCustomerInfo.LastName;
                ViewBag.reservationDate = reservation.P_ReservationDate;
                ViewBag.Description = reservation.Description;

                decimal totalPriceCheque = 0;
                foreach (var item in chequeCustomerService)
                    totalPriceCheque += item.TotalPrice;

                ViewBag.totalPriceCheque = totalPriceCheque;
                chequeList.Records = chequeCustomerService;
                chequeList.TotalCount = chequeCustomerService.Count;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillCustomerServicesItems", chequeList);
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
