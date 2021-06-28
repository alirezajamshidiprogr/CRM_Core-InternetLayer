using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using CRM_Core.Entities.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_Presentation.Models;
using static CRM_Core.Infrastructure.Enums;
using CRM_Core.Infrastructure;
using MyCRM.Layered.Model.Utility;

namespace UI_Presentation.Controllers
{
    public class Reservation : Controller
    {
        #region Actions
        public IActionResult AddEditReservation()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.AddReservation;
            return PartialView();
        }

        #endregion Action

        #region Methods
        public ActionResult GetSalonServices(int? clerkId)
        {
            string result = string.Empty;
            List<TempClerkService> servicesItems = new List<TempClerkService>();
            try
            {
                //using (var context = new CRM_CoreDB())
                //{
                //    using var transaction = context.Database.BeginTransaction();
                //    var services = from _service in context.TBASServices
                //                   join _clerkService in context.ClerkServices
                //                   on _service.Id equals _clerkService.TBASServices.Id
                //                   where
                //                   _clerkService.Acitve == true
                //                   select new
                //                   {
                //                       serviceName = _service.Name,
                //                       serviceId = _clerkService.Id,
                //                       clerk = _clerkService.People.FirstName + " " + _clerkService.People.LastName,
                //                       clerckId = _clerkService.People.Id
                //                   };

                //    foreach (var item in services)
                //    {
                //        servicesItems.Add(new TempClerkService { Id = item.serviceId, Name = item.serviceName + "(" + item.clerk + ")", ClerkId = item.clerckId });
                //    }
                //}
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
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
            List<People> clerk = new List<People>();
            List<SelectListItem> clerks = null;
            try
            {
                //using (var context = new CRM_CoreDB())
                //{
                //    clerks = context.People.Where(item => item.TBASCategoryId == (int)tbasCategoryState.personnel).Select(item => new People
                //    {
                //        FirstName = item.FirstName + " " + item.LastName,
                //        Id = item.Id
                //    }).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.Id.ToString() }; });

                //    clerks.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.AllItems, Value = null });
                //}

            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                result = result,
                clerk = clerks,
            });
        }

        [Authorization]
        [HttpPost]
        public ActionResult AddEditReservationMethod(bool isEdit, List<PeopleServices> peopleServices, CRM_Core.Entities.Reservation.Reservation reservation)
        {
            string result = string.Empty;
            string message = string.Empty;
            //var context = new CRM_CoreDB();
            //ModelState["people.Category"].Errors.Clear();
            //ModelState["SystemCode"].Errors.Clear();

            //if (!ModelState.IsValid)
            //    throw new CustomeException("Model Is Not Valid", true, null);

            //using (var context = new CRM_CoreDB())
            //{
            //    using (var transaction = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            reservation.M_ReservationDate = reservation.P_ReservationDate.ToDateTime();
            //            reservation.SystemCode = !isEdit ? (Convert.ToInt64("00100000001") + 1).ToString() : reservation.SystemCode;

            //            if (isEdit)
            //            {
            //                List<PeopleServices> oldPeopleService = context.PeopleServices.Where(item => item.Reservation.Id == reservation.Id).ToList();
            //                foreach (var item in oldPeopleService)
            //                {
            //                    context.PeopleServices.Remove(item);
            //                }

            //                context.Reservation.Update(reservation);
            //                for (int i = 0; i < peopleServices.Count; i++)
            //                {
            //                    PeopleServices newPeopleService = new PeopleServices();
            //                    ClerkServices getClerkService = context.ClerkServices.Find(peopleServices[i].Id);
            //                    newPeopleService.ClerkServicesId = getClerkService.PeopleId;
            //                    newPeopleService.ReservationId = reservation.Id;
            //                    newPeopleService.isSalonCustomer = peopleServices[i].isSalonCustomer;

            //                    context.PeopleServices.Add(newPeopleService);
            //                }

            //            }
            //            else
            //            {
            //                reservation.TBASPayTypeId = 1;
            //                context.Reservation.Add(reservation);
            //                context.SaveChanges();
            //                for (int i = 0; i < peopleServices.Count; i++)
            //                {
            //                    PeopleServices newPeopleService = new PeopleServices();

            //                    newPeopleService.ClerkServicesId = peopleServices[i].ReservationId;
            //                    newPeopleService.ReservationId = context.Reservation.ToList().LastOrDefault().Id;
            //                    newPeopleService.isSalonCustomer = peopleServices[i].isSalonCustomer;

            //                    context.PeopleServices.Add(newPeopleService);
            //                    context.SaveChanges();
            //                }

            //            }

            //            var a = context.Reservation.ToList().LastOrDefault();
            //            transaction.Commit();
            //            message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;

            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.Rollback();
            //            result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            //        }




            //    }

            //}

            return Json(new
            {
                result = result,
                message = message,
            });
        }


        #endregion Methods

    }
}
