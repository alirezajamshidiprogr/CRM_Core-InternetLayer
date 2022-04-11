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
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Models;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        #region CONSTANT
        private ITBASTelPhoneTypesService _telPhoneType;
        private IContactService _contactService;
        private IAddressService _addressService;
        private IPeopleVirtualService _contactVirtualService;
        private ITelPhonesService _telPhonesService;
        public ContactsController(ITBASTelPhoneTypesService telPhoneType, IContactService contactService, IAddressService addressService, IPeopleVirtualService contactVirtual, ITelPhonesService telPhonesService)
        {
            _telPhoneType = telPhoneType;
            _contactService = contactService;
            _addressService = addressService;
            _contactVirtualService = contactVirtual;
            _telPhonesService = telPhonesService;
        }
        #endregion

        #region Action

        public IActionResult AddEditContact()
        {
            string errorMessage = string.Empty;
            try
            {
                TempData["Title"] = "Test";
                return PartialView("AddEditContact");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        [HttpPost]
        public ActionResult GetTelPhoneType(int peopleId)
        {
            string result = string.Empty;
            List<SelectListItem> phoneTelTypes = null;
            try
            {
                phoneTelTypes = (from _tel in _telPhoneType.GetTelPhoneTypes()
                                 select new
                                 {
                                     Title = _tel.Title,
                                     Id = _tel.Id
                                 }).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Title, Value = item.Id.ToString() }; });

                phoneTelTypes.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });

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
                phoneTelTypes = phoneTelTypes,
            });
        }

        [HttpPost]
        public ActionResult AddEditContactMethod(bool isEdit, ContactViewModel contactInfo, List<TelPhoneType> otherRelationShips)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            ModelState["contactInfo.Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            try
            {
                if (!ModelState.IsValid)
                    throw new CustomeException("Model Is Not Valid", true, null);

                if (isEdit)
                {
                    Contact getContact = _contactService.GetContactById(contactInfo.Id).FirstOrDefault();
                    getContact.FirstName = contactInfo.FirstName;
                    getContact.LastName = contactInfo.LastName;
                    getContact.Tel = contactInfo.Tel;
                    getContact.Mobile = contactInfo.Mobile;
                    getContact.Emails = contactInfo.Email;
                    getContact.FatherName = contactInfo.FatherName;
                    getContact.Job = contactInfo.Job;
                    getContact.Description = contactInfo.Description;
                    getContact.M_ContactEditDate = DateTime.Now;
                    getContact.P_BirthDay = contactInfo.P_BirthDay;
                    getContact.M_BirthDay = contactInfo.P_BirthDay != null ? contactInfo.P_BirthDay.ToDateTime() : (DateTime?)null;

                    _contactService.UpdateContact(getContact);
                    _contactService.SaveChanges();
                    int getLastcontactId = _contactService.getLastContact().LastOrDefault().Id;

                    if (contactInfo.City != string.Empty || contactInfo.Province != string.Empty || contactInfo.Area != string.Empty || contactInfo.Street != string.Empty || contactInfo.OtherAddress != string.Empty)
                    {
                        Address getAddress = _addressService.GetAddressByContactId(getContact.Id).FirstOrDefault();
                        getAddress.Province = contactInfo.Province;
                        getAddress.City = contactInfo.City;
                        getAddress.Area = contactInfo.Area;
                        getAddress.Street = contactInfo.Street;
                        getAddress.Alley = contactInfo.Alley;
                        getAddress.OtherAddress = contactInfo.OtherAddress;
                        getAddress.RelativeId = getLastcontactId;

                        _addressService.UpdateAddress(getAddress);
                    }

                    if (contactInfo.WebSite != string.Empty || contactInfo.Email != string.Empty || contactInfo.WebSite != string.Empty || contactInfo.Instagram != string.Empty || contactInfo.Telegram != string.Empty || contactInfo.WhatsApp != string.Empty || contactInfo.YouTube != string.Empty)
                    {
                        PeopleVirtual getContactVirtual = _contactVirtualService.GetPeopleVirtualByContactId(getContact.Id).FirstOrDefault();

                        getContactVirtual.Telegram = contactInfo.Telegram;
                        getContactVirtual.WhatsApp = contactInfo.Telegram;
                        getContactVirtual.Instagram = contactInfo.Telegram;
                        getContactVirtual.Email = contactInfo.Telegram;
                        getContactVirtual.YouTube = contactInfo.Telegram;
                        getContactVirtual.WebSite = contactInfo.Telegram;
                        getContactVirtual.RelativeId = getLastcontactId;

                        _contactVirtualService.UpdatePeopleVirtual(getContactVirtual);
                    }

                    //delete first 
                    if (otherRelationShips != null)
                    {
                        TelPhones telPhones = new TelPhones();
                        List<PeopleVirtual> contactVirtual = _contactVirtualService.GetPeopleVirtualByContactId(contactInfo.Id).Where(item => item.Type == 1).ToList();
                        foreach (var contact in contactVirtual)
                            _contactVirtualService.DeleteContact(contact);

                        for (int i = 0; i < otherRelationShips.Count; i++)
                        {
                            telPhones.Value = otherRelationShips[i].TelValue;
                            telPhones.Description = otherRelationShips[i].Description;
                            telPhones.TBASPhoneTypesId = otherRelationShips[i].TBASTelTypeId;

                            //_telPhonesService.AddTelPhones(telPhones);
                        }
                    }
                }

                else
                {
                    Contact contact = new Contact();
                    contact.FirstName = contactInfo.FirstName;
                    contact.LastName = contactInfo.LastName;
                    contact.Tel = contactInfo.Tel;
                    contact.Mobile = contactInfo.Mobile;
                    contact.Emails = contactInfo.Email;
                    contact.FatherName = contactInfo.FatherName;
                    contact.Job = contactInfo.Job;
                    contact.Description = contactInfo.Description;
                    contact.M_ContactInsertDate = DateTime.Now;
                    contact.P_BirthDay = contactInfo.P_BirthDay;
                    contact.M_BirthDay = contactInfo.P_BirthDay != null ? contactInfo.P_BirthDay.ToDateTime() : (DateTime?)null;
                    contact.IsActive = true;

                    _contactService.AddContact(contact);
                    _contactService.SaveChanges();
                    int getLastContactId = _contactService.getLastContact().LastOrDefault().Id;

                    if (contactInfo.City != null || contactInfo.Province != null || contactInfo.Area != null || contactInfo.Street != null || contactInfo.OtherAddress != null)
                    {
                        Address address = new Address();
                        address.Province = contactInfo.Province;
                        address.City = contactInfo.City;
                        address.Area = contactInfo.Area;
                        address.Street = contactInfo.Street;
                        address.Alley = contactInfo.Alley;
                        address.OtherAddress = contactInfo.OtherAddress;
                        address.RelativeId = getLastContactId;
                        address.Type = 1;

                        _addressService.AddAddress(address);
                    }

                    if (contactInfo.WebSite != null || contactInfo.Email != null || contactInfo.WebSite != null || contactInfo.Instagram != null || contactInfo.Telegram != null || contactInfo.WhatsApp != null || contactInfo.YouTube != null)
                    {
                        PeopleVirtual contactVirtual = new PeopleVirtual();

                        contactVirtual.Telegram = contactInfo.Telegram;
                        contactVirtual.WhatsApp = contactInfo.WhatsApp;
                        contactVirtual.Instagram = contactInfo.Instagram;
                        contactVirtual.Email = contactInfo.Email;
                        contactVirtual.YouTube = contactInfo.YouTube;
                        contactVirtual.WebSite = contactInfo.WebSite;
                        contactVirtual.RelativeId = getLastContactId;
                        contactVirtual.Type = 1;

                        _contactVirtualService.AddPeopleVirtual(contactVirtual);
                    }

                    if (otherRelationShips != null)
                    {
                        TelPhones telPhones = new TelPhones();
                        for (int i = 0; i < otherRelationShips.Count; i++)
                        {
                            telPhones.Value = otherRelationShips[i].TelValue;
                            telPhones.Description = otherRelationShips[i].Description;
                            telPhones.TBASPhoneTypesId = otherRelationShips[i].TBASTelTypeId;
                            telPhones.RelativeId = getLastContactId;
                            telPhones.Type = 1;

                            //_telPhonesService.AddTelPhones(telPhones);
                        }
                    }

                }
                _contactService.SaveChanges();
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
                Contact getContact = _contactService.GetContactById(contactId).FirstOrDefault();
                getContact.IsActive = false;
                _contactService.SaveChanges();
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

            DataGridViewModel<ContactViewModelSearch> contactViewModel = new DataGridViewModel<ContactViewModelSearch>();
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
            return PartialView("FillContactsData", contactViewModel);
        }

        #endregion
    }
}
