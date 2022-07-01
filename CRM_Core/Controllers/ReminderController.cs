using System;
using System.Collections.Generic;
using System.Linq;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Models;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MyCRM.Layered.Model.Utility;
using UI_Presentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI_Presentation.Controllers
{
    [Authorization]
    public class ReminderController : Controller
    {
        #region CONSTANT
        private IReminderService _remidnerService;
        private IPeopleService _peopleService;
        private IReminderDayDetailsService _reminderDayDetailsService;

        public ReminderController(IReminderService remidnerService, IReminderDayDetailsService reminderDayDetailsService,IPeopleService peopleService)
        {
            _remidnerService = remidnerService;
            _reminderDayDetailsService = reminderDayDetailsService;
            _peopleService = peopleService;
        }
        #endregion

        #region Action
        [HttpPost]
        public IActionResult AddEditReminder(int? reminderId)
        {
            string errorMessage = string.Empty;
            ReminderViewModel reminder = new ReminderViewModel();
            try
            {
                if (reminderId.HasValue)
                {
                    var getReminder = _remidnerService.GetReminderById(reminderId.Value).FirstOrDefault();

                    reminder.Id = getReminder.Id; 
                    reminder.Time = getReminder.Time; 
                    reminder.F_ReminderDate = getReminder.IsRepeatReminder ? string.Empty :  getReminder.F_ReminderDate; 
                    reminder.ToPersonelId = getReminder.ToPersonelId; 
                    reminder.ReminderTitle = getReminder.ReminderTitle; 
                    reminder.Description = getReminder.Description; 
                    reminder.IsRepeatReminder = getReminder.IsRepeatReminder; 
                    
                    if (reminder.IsRepeatReminder)
                    {
                      List<ReminderDayDetails> reminderDayDetails = _reminderDayDetailsService.GetReminderDayDetailsByReminderId(reminderId.Value).ToList();
                        foreach (var item in reminderDayDetails)
                        {
                            switch (item.Day)
                            {
                                case 0:
                                    reminder.Satuarday = true;
                                    break;
                                case 1:
                                    reminder.Sunday = true;
                                    break;
                                case 2:
                                    reminder.Monday = true;
                                    break;
                                case 3:
                                    reminder.Tuesday = true;
                                    break;
                                case 4:
                                    reminder.Wensday = true;
                                    break;
                                case 5:
                                    reminder.Thursday = true;
                                    break;
                                default:
                                    reminder.Friday = true;
                                    break;
                            }
                        }
                    }

                    ViewBag.isEditMode = true;
                }
                ViewBag.isRepeatableReminder = reminder.IsRepeatReminder;
                ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.AddReminder;
                List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listPersonnel = _peopleService.GetPeopleByCategoryId((int)Enums.PeopleCategory.Personnel).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.Id.ToString(), Selected = reminderId.HasValue && item.Id == reminder.ToPersonelId ? true : false }; }).ToList();
                ViewBag.Personnels = listPersonnel;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return PartialView("AddEditReminder",reminder);
        }

        [HttpGet]
        public IActionResult Index()
        {
            string errorMessage = string.Empty;
            try
            {
                IEnumerable<ReminderViewModelSearch> reminderList = _remidnerService.GetRemindersByADO("[dbo].[Reminder_Search]", null ,null );
                ViewBag.Title = UI_Presentation.wwwroot.Resources.Reminder.Title.ReminderList;
                return PartialView("Reminder", reminderList);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

        }

        #endregion

        #region Methods
        [HttpPost]
        public ActionResult AddEditReminderMethod(bool isEdit, List<DayWeek> days, Reminder reminderInfo)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            bool saveException = false ;

            IEnumerable<ReminderViewModelSearch> reminderList = new List<ReminderViewModelSearch>();
            //ModelState["reminder.IsActive"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (!ModelState.IsValid)
                throw new CustomeException("Model Is Not Valid", true, null, ref saveException);

            try
            {
                if (isEdit)
                {
                    Reminder getReminder = _remidnerService.GetReminderById(reminderInfo.Id).FirstOrDefault();
                    getReminder.ReminderTitle = reminderInfo.ReminderTitle;
                    getReminder.F_EditDate = DateTime.Now.ToPersianDate();
                    getReminder.M_EditDate = DateTime.Now;
                    getReminder.Time = reminderInfo.Time;
                    getReminder.IsRepeatReminder = reminderInfo.IsRepeatReminder;
                    getReminder.Description = reminderInfo.Description;

                    if (reminderInfo.IsRepeatReminder == false)
                    {
                        getReminder.F_ReminderDate = reminderInfo.IsRepeatReminder ? string.Empty : reminderInfo.F_ReminderDate;
                        getReminder.M_ReminderDate = reminderInfo.IsRepeatReminder ? (DateTime?)null : reminderInfo.F_ReminderDate.ToDateTime();
                    }
                    else
                    {
                        getReminder.F_ReminderDate = string.Empty;
                        getReminder.M_ReminderDate = (DateTime?)null;

                        List<ReminderDayDetails> getReminderDayDetails = _reminderDayDetailsService.GetReminderDayDetailsByReminderId(reminderInfo.Id).ToList();
                        foreach (var item in getReminderDayDetails)
                        {
                            _reminderDayDetailsService.DeleteReminderDayDetails(item);
                        }

                        foreach (var item in days)
                        {
                            ReminderDayDetails reminderDayDetails = new ReminderDayDetails();
                            reminderDayDetails.Day = item.Day;
                            reminderDayDetails.ReminderId = reminderInfo.Id;
                            _reminderDayDetailsService.AddReminderDayDetails(reminderDayDetails);
                        }
                    }

                    getReminder.IsActive = true;
                    _remidnerService.UpdateReminder(getReminder);
                }
                else
                {
                    Reminder reminder = new Reminder();
                    reminder.ReminderTitle = reminderInfo.ReminderTitle;
                    reminder.Time = reminderInfo.Time;
                    reminder.IsRepeatReminder = reminderInfo.IsRepeatReminder;
                    reminder.ToPersonelId = reminderInfo.ToPersonelId;
                    reminder.IsActive = true;
                    reminder.Description = reminderInfo.Description;
                    if (reminderInfo.IsRepeatReminder)
                    {
                        _remidnerService.AddReminder(reminder);
                        _remidnerService.SaveChanges();
                        int getReminderkeyId = _remidnerService.GetLasReminder().Id;

                        foreach (var item in days)
                        {
                            ReminderDayDetails reminderDayDetails = new ReminderDayDetails();
                            reminderDayDetails.Day = item.Day;
                            reminderDayDetails.ReminderId = getReminderkeyId;
                            _reminderDayDetailsService.AddReminderDayDetails(reminderDayDetails);
                        }
                    }
                    else
                    {
                        reminder.F_ReminderDate = reminderInfo.F_ReminderDate;
                        reminder.M_ReminderDate = reminderInfo.F_ReminderDate.ToDateTime();
                        _remidnerService.AddReminder(reminder);
                    }
                }

                _remidnerService.SaveChanges();

                //reminderList = _remidnerService.GetCurrentReminderDay("[dbo].[Reminder_Search]");
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;

            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return Json(new { message = message, errorMessage = errorMessage/*, reminderListData = reminderList */});
        }

        #endregion
    }
}
