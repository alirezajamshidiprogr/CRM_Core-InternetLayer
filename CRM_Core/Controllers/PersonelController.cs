using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using UI_Presentation.Models;
using CRM_Core.Infrastructure;
using System.Data;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.GridViewModels;
using CRM_Core.Application.ViewModels.People;
using MyCRM.Layered.Model.Utility;
using Microsoft.AspNetCore.Authorization;
using CRM_Core.Application.Services;
using CRM_Core.Entities.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRM_Core.Application.ViewModels.Personnel;
using CRM_Core.Entities.Reservation;
using CRM_Core.Entities.Models;

namespace UI_Presentation.Controllers
{
    public class PersonelController : Controller
    {
        #region CONSTANT
        ITBASServiceService _tbasService;
        IPersonnelService _personnelService;
        ITBASAgreementTypePersonnel _agreementType;
        IPersonnelSkillsService _personnelSkillService;

        public PersonelController(ITBASServiceService tbasService, IPersonnelService personnelService, ITBASAgreementTypePersonnel agreementType, IPersonnelSkillsService personnelSkillService)
        {
            _tbasService = tbasService;
            _personnelService = personnelService;
            _agreementType = agreementType;
            _personnelSkillService = personnelSkillService;
        }
        #endregion

        #region Action

        public IActionResult AddEditPersonnel(int? personnelId)
        {
            string errorMessage = string.Empty;
            try
            {
                IEnumerable<TBASServices> tbasService = new List<TBASServices>();
                tbasService = _tbasService.GetAllServices();

                PersonnelViewModel personnelViewModel = null;
                if (personnelId.HasValue)
                {
                    Personnel personnel = _personnelService.GetPersonnelById(personnelId.Value).FirstOrDefault();
                    personnelViewModel.PersonnelName = personnel.PersonnelName;
                    personnelViewModel.PersonnelLastName = personnel.PersonnelLastName;
                    personnelViewModel.PersonnelFatherName = personnel.PersonnelFatherName;
                    personnelViewModel.InsuranceNumber = personnel.InsuranceNumber;
                    personnelViewModel.Mobile = personnel.Mobile;
                    personnelViewModel.Tel = personnel.Tel;
                    personnelViewModel.TBASAgreementTypeId = personnel.TBASAgreementTypeId;
                    personnelViewModel.CertificateCode = personnel.CertificateCode;
                    personnelViewModel.HomeTel = personnel.HomeTel;
                    personnelViewModel.P_Birthday = personnel.P_Birthday;
                    personnelViewModel.Description = personnel.Description;


                    IEnumerable<PersonnelSkils> getPersonnelSkills = _personnelSkillService.GetPersonnelSkillsByPersonnelId(personnelId.Value);

                    //var query = (from a in context.Appointment
                    //             join b in context.AppointmentFormula on a.AppointmentId equals b.AppointmentId into temp
                    //             from c in temp.DefaultIfEmpty()
                    //             join d in context.AppointmentForm on a.AppointmentID equals e.AppointmentID into temp2
                    //             from e in temp2.DefaultIfEmpty()
                    //             where a.RowStatus == 1 && c.RowStatus == 1 && a.Type == 1
                    //             select new { a.AppointmentId, a.Status, a.Type, a.Title, c.Days ?? 0, a.Type.Description, e.FormID ?? 0 }).OrderBy(a.Type);

                    List<PersonnelSkilsViewModel> personnelSkillModel = new List<PersonnelSkilsViewModel>();

                    personnelSkillModel = (from service in tbasService
                                           join personnelSkill in getPersonnelSkills on service.Id equals personnelSkill.TBASServicesId into temp
                                           from t in temp.DefaultIfEmpty()
                                           select new PersonnelSkilsViewModel
                                           {
                                               Id = service.Id,
                                               Name = service.Name,
                                               Level = t.Level,
                                           }).ToList();


                    //foreach (var item in personnelSkillModel)
                    //    personnelViewModel.personnelSkilsViewModel.Add(item);

                }

                List<TBASAgreementType> agreemetType = _agreementType.GetAllAgreements().ToList();
                //agreemetType.Add(new TBASAgreementType { Id = 0 , Name = UI_Presentation.wwwroot.Resources.General.Title.SelectItem});
                ViewBag.agreemetType = agreemetType;

                List<PersonnelSkilsViewModel> pSkillsList = new List<PersonnelSkilsViewModel>();
                foreach (var item in tbasService)
                {
                    pSkillsList.Add(new PersonnelSkilsViewModel { Id = item.Id, Level = null, Name = item.Name });
                }

                ViewBag.pSkillsList = pSkillsList;

                return PartialView("AddEditPersonel", personnelViewModel);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        [HttpPost]
        public ActionResult AddEditPersonnelMethod(bool isEdit, PersonnelViewModel personnelViewModel)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            PersonnelSkils personnelSkills = new PersonnelSkils();

            try
            {
                if (!isEdit)
                {
                    Personnel personnel = new Personnel();
                    personnel.PersonnelName = personnelViewModel.PersonnelName;
                    personnel.PersonnelLastName = personnelViewModel.PersonnelLastName;
                    personnel.PersonnelFatherName = personnelViewModel.PersonnelFatherName;
                    personnel.InsuranceNumber = personnelViewModel.InsuranceNumber;
                    personnel.Mobile = personnelViewModel.Mobile;
                    personnel.Tel = personnelViewModel.Tel;
                    personnel.TBASAgreementTypeId = personnelViewModel.TBASAgreementTypeId;
                    personnel.CertificateCode = personnelViewModel.CertificateCode;
                    personnel.HomeTel = personnelViewModel.HomeTel;
                    //personnel.P_Birthday = personnelViewModel.P_Birthday;
                    //personnel.M_Birthday = personnelViewModel.P_Birthday != null ? personnelViewModel.P_Birthday.ToDateTime() : null;
                    personnel.M_InsertDate = DateTime.Now;
                    //personnel.M_EditDate = personnelViewModel.PersonnelName;
                    personnel.Description = personnelViewModel.Description;
                    personnel.IsActive = true;

                    _personnelService.AddPersonnel(personnel);

                    if (personnelViewModel.personnelSkilsViewModel != null)
                    {
                        foreach (var item in personnelViewModel.personnelSkilsViewModel)
                        {
                            personnelSkills.Level = item.Level;
                            personnelSkills.TBASServicesId = item.Id;
                        }
                        personnel.PersonnelSkils.Add(personnelSkills);
                    }
                }

                else
                {
                    Personnel getPersonnel = _personnelService.GetPersonnelById((int)personnelViewModel.Id).FirstOrDefault();
                    getPersonnel.PersonnelName = personnelViewModel.PersonnelName;
                    getPersonnel.PersonnelLastName = personnelViewModel.PersonnelLastName;
                    getPersonnel.PersonnelFatherName = personnelViewModel.PersonnelFatherName;
                    getPersonnel.InsuranceNumber = personnelViewModel.InsuranceNumber;
                    getPersonnel.Mobile = personnelViewModel.Mobile;
                    getPersonnel.Tel = personnelViewModel.Tel;
                    getPersonnel.TBASAgreementTypeId = personnelViewModel.TBASAgreementTypeId;
                    getPersonnel.CertificateCode = personnelViewModel.CertificateCode;
                    getPersonnel.HomeTel = personnelViewModel.HomeTel;
                    getPersonnel.P_Birthday = personnelViewModel.P_Birthday;
                    getPersonnel.M_Birthday = personnelViewModel.P_Birthday != null ? personnelViewModel.P_Birthday.ToDateTime() : null;
                    getPersonnel.M_EditDate = DateTime.Now;
                    getPersonnel.Description = personnelViewModel.Description;

                    _personnelService.UpdatePersonnel(getPersonnel);


                    // FIRST OF ALL DELETE ALL PERSONNEL SKILLS 
                    List<PersonnelSkils> getPerosonnelSkills = _personnelSkillService.GetPersonnelSkillsByPersonnelId((int)personnelViewModel.Id).ToList();
                    foreach (var item in getPerosonnelSkills)
                    {
                        _personnelSkillService.DeletePersonnelSkills(item);
                    }

                    // ADD AGAIN PERSONNEL SKILLS 
                    foreach (var item in personnelViewModel.personnelSkilsViewModel)
                    {
                        personnelSkills.TBASServicesId = item.Id;
                        personnelSkills.Level = item.Level;
                    }
                }

                _personnelService.SaveChanges();
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;

            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return Json(new { message = message, errorMessage = errorMessage });
        }

        [HttpPost]
        public ActionResult DeleteContact(int contactId)
        {
            string result = string.Empty;
            string message = string.Empty;
            try
            {
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

        #endregion

        #region Methods
        [HttpPost]
        public IActionResult FillContactItemData()
        {
            string errorMessage = string.Empty;

            try
            {


                //peopleList.Records = MappingUtility.DataTableToList<PeopleModel>(dsData.Tables[0]).AsQueryable();
                //peopleList.TotalCount = peopleList.Records.Count();

                //peopleList.Records = _peopleService.GetPeopleByADO("[dbo].[People_Search]", searchParameter, searchValues);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillContactsData");
        }

        #endregion
    }
}
