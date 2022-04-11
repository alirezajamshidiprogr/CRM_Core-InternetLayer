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
using CRM_Core.Entities.Models;
using CRM_Core.Application.ViewModels.CustomViewModel;

namespace CRM_Core.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        #region CONSTANT
        private ITBASTelPhoneTypesService _telPhoneType;
        private IPeopleService _peopleService;
        private ICategoryService _categoryService;
        private IPrefixService _prefixService;
        private IIntroductionTypeService _introductionTypeService;
        private IPotentialService _potentialService;
        private IGraduationService _graduationService;
        private IAddressService _addressService;
        private IPeopleVirtualService _peopleVirtualService;
        private IPeoplePropertyService _peoplePropertyService;
        private IGeneratedNumberService _generetedNumberService;
        private ITelPhonesService _telPhonesService;

        #endregion

        public PeopleController(IPeopleService peopleService, ICategoryService categoryService, IPotentialService potentialService, IPrefixService prefixService, IIntroductionTypeService introductionService, IGraduationService graduationService, IAddressService addressService, IPeopleVirtualService peopleVirtualService, IPeoplePropertyService peoplePropertyService, IGeneratedNumberService generetedNumberService, ITelPhonesService telPhonesService, ITBASTelPhoneTypesService telPhoneType)
        {
            _peopleService = peopleService;
            _categoryService = categoryService;
            _potentialService = potentialService;
            _introductionTypeService = introductionService;
            _prefixService = prefixService;
            _graduationService = graduationService;
            _addressService = addressService;
            _peopleVirtualService = peopleVirtualService;
            _peoplePropertyService = peoplePropertyService;
            _generetedNumberService = generetedNumberService;
            _telPhonesService = telPhonesService;
            _telPhoneType = telPhoneType;
        }

        #region Action

        [HttpGet]
        public IActionResult Index()
        {
            string errorMessage = string.Empty;
            try
            {
                ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.ShowPeople;

                List<SelectListItem> PotentialsItems = _potentialService.GetPotentials().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                List<SelectListItem> GradationsItems = _graduationService.GetGraduations().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                List<SelectListItem> CategoriesItems = _categoryService.GetCategories().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                List<SelectListItem> PrefixesItems = _prefixService.GetPrefixes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });
                List<SelectListItem> IntroductionTypesItems = _introductionTypeService.GetIntroductionTypes().ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }; });

                PotentialsItems.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                GradationsItems.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                CategoriesItems.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                PrefixesItems.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                IntroductionTypesItems.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });

                List<SelectListItem> MarriedItems = new List<SelectListItem>()
                {
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.General.Title.AllItems, Value = null , Selected = false },
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.Single, Value = "1", Selected = false },
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.Married, Value = "2", Selected = false },
                        new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.divorced, Value = "3", Selected = false },
                };

                ViewBag.MarriedItems = MarriedItems;
                ViewBag.PotentialItems = PotentialsItems;
                ViewBag.GradationsItems = GradationsItems;
                ViewBag.CategoriesItems = CategoriesItems;
                ViewBag.PrefixesItems = PrefixesItems;
                ViewBag.IntroductionTypesItems = IntroductionTypesItems;

                return PartialView("People");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

        }

        public IActionResult FillPeopleTableData(bool quickSearch, string fullName, peopleViewModelSearch searchParams, string state)
        {
            string errorMessage = string.Empty;

            if (state == "isEditMode")
                ViewBag.isEditMode = true;
            else if (state == "isSelectedMode")
            {
                ViewBag.isSelectedMode = true;
                TempData["IsSelectMode"] = true;
            }
            else if (state == "isPrintMode")
                ViewBag.isPrintMode = true;

            DataGridViewModel<PeopleModel> peopleList = new DataGridViewModel<PeopleModel>();
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
                     "@FirstName"
                    ,"@LastName"
                    ,"@FromAge"
                    ,"@ToAge"
                    ,"@FromBirthday"
                    ,"@ToBirthday"
                    ,"@TBASPotentialId"
                    ,"@TBASIntroduceId"
                    ,"@TBASIntroductionTypeId"
                    ,"@MariedType"
                    ,"@TBASGradationsId"
                    ,"@TBASCategoriyId"
                    ,"@TBASPrefixID"
                    ,"@CertificateCode"
                    ,"@ManualCode"
                    ,"@SystemCode"
                    ,"@Page"
                    ,"@PageSize"
                    ,"@Sort"
                    };

                    searchValues = new object[]
                    {
                        searchParams.FirstName
                       ,searchParams.LastName
                       ,searchParams.FromAge
                       ,searchParams.ToAge
                       ,searchParams.FromBirthday.ToDateTime()
                       ,searchParams.ToBirthday.ToDateTime()
                       ,searchParams.TBASPotentialId
                       ,searchParams.TBASIntroduceId
                       ,searchParams.TBASIntroductionTypeId
                       ,searchParams.MariedType
                       ,searchParams.TBASGradationsId
                       ,searchParams.TBASCategoriyId
                       ,searchParams.TBASPrefixID
                       ,searchParams.CertificateCode
                       ,searchParams.ManualCode
                       ,searchParams.SystemCode
                       ,searchParams.PageNumber
                       ,10
                       ,string.Empty
                    };
                }
                DataSet dsData = _peopleService.GetPeopleDataTable("[dbo].[People_Search]", searchParameter, searchValues);
                ViewBag.totalAllRecords = (int)dsData.Tables[1].Rows[0][0];
                ViewBag.pageNumber = searchParams.PageNumber;
                peopleList.Records = MappingUtility.DataTableToList<PeopleModel>(dsData.Tables[0]).AsQueryable();
                peopleList.TotalCount = peopleList.Records.Count();

                //peopleList.Records = _peopleService.GetPeopleByADO("[dbo].[People_Search]", searchParameter, searchValues);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillPeopleTableData", peopleList);
        }

        [HttpPost]
        public IActionResult AddEditPeople(int? peopleId)
        {
            string errorMessage = string.Empty;
            ViewBag.isEdit = false;
            try
            {
                peopleViewModel peopleViewModel = new peopleViewModel();
                peopleViewModel.GraduationItems = _graduationService.GetGraduations();
                peopleViewModel.CategoriyItem = _categoryService.GetCategories();
                peopleViewModel.PotentialItems = _potentialService.GetPotentials();
                peopleViewModel.PrefixItems = _prefixService.GetPrefixes();
                peopleViewModel.TBASIntroductionTypeItems = _introductionTypeService.GetIntroductionTypes();

                List<SelectListItem> marriedItems = new List<SelectListItem>()
                {
                   new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.Single, Value = "1", Selected = false },
                   new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.Married, Value = "2", Selected = false },
                   new SelectListItem (){ Text = UI_Presentation.wwwroot.Resources.People.Title.divorced, Value = "3", Selected = false },
                };

                ViewBag.marriedItems = marriedItems;
                List<TBASPhoneTypes> tbasTelPhoneTypes = _telPhoneType.GetTelPhoneTypes().OrderBy(item => item.Id).ToList();

                TempData["tbasTelPhoneTypes"] = _telPhoneType.GetTelPhoneTypes().ToList();
                if (peopleId.HasValue)
                {
                    ViewBag.isEdit = true;

                    ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.EditPeople;
                    ViewBag.PeopleID = peopleId;
                    var people = _peopleService.GetPeopleById(peopleId.Value);
                    ViewBag.graduationSelected = people.FirstOrDefault().TBASGraduationId.ToString();
                    ViewBag.introduceSelected = people.FirstOrDefault().IntroduceId.ToString();
                    ViewBag.categorySelected = people.FirstOrDefault().TBASCategoryId.ToString();
                    ViewBag.prefixSelected = people.FirstOrDefault().TBASPrefixId.ToString();
                    ViewBag.potentialSelected = people.FirstOrDefault().TBASPrefixId.ToString();
                    ViewBag.introductionTypeSelected = people.FirstOrDefault().TBASIntroductionTypeId.ToString();

                    int addressId = _addressService.GetAddressByPeopleId(people.FirstOrDefault().Id).FirstOrDefault().Id;
                    var peopleVirtual = _peopleVirtualService.GetPeopleVirtualByPeopleId(people.FirstOrDefault().Id).FirstOrDefault();

                    peopleViewModel.People = _peopleService.GetPeopleById(peopleId.Value).FirstOrDefault();
                    peopleViewModel.Address = _addressService.GetAddressById(addressId).FirstOrDefault();
                    peopleViewModel.PeopleVirtual = peopleVirtual != null ? _peopleVirtualService.GetPeopleVirtualById(peopleVirtual.Id).FirstOrDefault() : null;

                    List<TelPhones> phoneTelTypes = null;
                    List<TelPhones> telPhones = _telPhonesService.GetTelPhonesByType(peopleId.Value, (int)Enums.PeopleType.people).ToList();


                    phoneTelTypes = (from _telPhones in telPhones
                                     select new TelPhones
                                     {
                                         Description = _telPhones.Description,
                                         Value = _telPhones.Value,
                                     }
                                ).ToList();

                    TempData["phoneTelTypes"] = phoneTelTypes;

                }
                else
                {
                    ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.AddPeople;
                }

                return PartialView("AddEditPeople", peopleViewModel);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

        }

        public IActionResult AddEditIntroducePopup()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.ShowPeople;
            return PartialView();
        }

        [HttpPost]
        public IActionResult PeopleSelectorPopup()
        {
            return Json(new { });
        }
        #endregion

        #region Methods

        //[RangeExceptionOn]
        //[Authorization]
        [HttpPost]
        public ActionResult AddEditPeopleMethod(bool isEdit, bool checkRepeatedTels, List<TelPhoneType> otherRelationShips, PeopleVirtual peopleVirtual, Address address, People people)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            ModelState["people.Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
           
            if (ModelState["people.MarriedType"].ValidationState.ToString() == "Invalid")
              ModelState["people.MarriedType"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState["people.TBASCategoryId"].ValidationState.ToString() == "Invalid")
              ModelState["people.TBASCategoryId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState["people.TBASPrefixId"].ValidationState.ToString() == "Invalid")
              ModelState["people.TBASPrefixId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState["people.TBASPotentialId"].ValidationState.ToString() == "Invalid")
              ModelState["people.TBASPotentialId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState["people.TBASGraduationId"].ValidationState.ToString() == "Invalid")
              ModelState["people.TBASGraduationId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState["people.TBASIntroductionTypeId"].ValidationState.ToString() == "Invalid")
              ModelState["people.TBASIntroductionTypeId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
           

            try
            {
                if (!ModelState.IsValid)
                    throw new CustomeException("Model Is Not Valid", true, null);

                if ((!isEdit && _peopleService.GetPeopleByManualCode(people.ManualCode).Count() > 0))
                    throw new CustomeException("ManualCode Is Exists In Table People...", UI_Presentation.wwwroot.Resources.Mesages.ManaulCodeIsDuplicated, true, null, ref errorMessage);

                if (checkRepeatedTels && checkRepeatedTelPhones(otherRelationShips))
                    throw new CustomeException(UI_Presentation.wwwroot.Resources.Mesages.DoesNotAlllowedToRegisterRepeatedMobiles, UI_Presentation.wwwroot.Resources.Mesages.ThereIsDuplicatedTelPhones, true, null, ref errorMessage);

                people.M_Birthday = people.P_Birthday != null ? people.P_Birthday.ToDateTime() : (DateTime?)null;
                people.M_MariedDate = people.P_MariedDate != null ? people.P_MariedDate.ToDateTime() : (DateTime?)null;

                if (isEdit)
                {
                    if (people.Id == people.IntroduceId) // PEOPLE_ID CAN NOT BE SAME AS INTRODUCE ID 
                        throw new CustomeException("PeopleID CanNot Be Same As IntroduceId...", UI_Presentation.wwwroot.Resources.Mesages.PeopleCanNotBeSameAsIntroductionPeople, true, null,ref errorMessage);

                    PeopleVirtual peopleVirtualOld = _peopleVirtualService.GetPeopleVirtualByPeopleId(people.Id).FirstOrDefault();
                    Address addressOld = _addressService.GetAddressByPeopleId(people.Id).FirstOrDefault();
                    People oldPeople = _peopleService.GetPeopleById(people.Id).OrderBy(item=>item.Id).FirstOrDefault();

                    People getManualCodePeople = _peopleService.GetPeopleByManualCode(people.ManualCode).OrderBy(item=>item.Id).LastOrDefault();
                    if ((getManualCodePeople != null  && getManualCodePeople.Id != people.Id && _peopleService.GetPeopleByManualCode(people.ManualCode).Count() > 0))
                        throw new CustomeException("ManualCode Is Exists In Table People...", UI_Presentation.wwwroot.Resources.Mesages.ManaulCodeIsDuplicated, true, null, ref errorMessage);


                    if (peopleVirtualOld != null)
                    {
                        peopleVirtualOld.Email = peopleVirtual.Email;
                        peopleVirtualOld.WebSite = peopleVirtual.WebSite;
                        peopleVirtualOld.Telegram = peopleVirtual.Telegram;
                        peopleVirtualOld.Instagram = peopleVirtual.Instagram;
                        peopleVirtualOld.WhatsApp = peopleVirtual.WhatsApp;
                    }

                    addressOld.Province = address.Province;
                    addressOld.Province = address.City;
                    addressOld.Area = address.Area;
                    addressOld.Street = address.Street;
                    addressOld.Alley = address.Alley;
                    addressOld.OtherAddress = address.OtherAddress;


                    oldPeople.SystemCode = people.SystemCode;
                    oldPeople.ManualCode = people.ManualCode;
                    oldPeople.FirstName = people.FirstName;
                    oldPeople.LastName = people.LastName;
                    oldPeople.CertificateCode = people.CertificateCode;
                    oldPeople.Job = people.Job;
                    oldPeople.P_Birthday = people.P_Birthday;
                    oldPeople.M_Birthday = people.P_Birthday.ToDateTime();
                    oldPeople.P_MariedDate = people.P_MariedDate;
                    oldPeople.M_MariedDate = people.P_MariedDate.ToDateTime();
                    oldPeople.Description = people.Description;
                    oldPeople.MarriedType = people.MarriedType;
                    oldPeople.IntroduceId = people.IntroduceId;
                    oldPeople.TBASCategoryId = people.TBASCategoryId;
                    oldPeople.TBASPotentialId = people.TBASPotentialId;
                    oldPeople.TBASPrefixId = people.TBASPrefixId;
                    oldPeople.TBASGraduationId = people.TBASGraduationId;
                    oldPeople.TBASIntroductionTypeId = people.TBASIntroductionTypeId;

                    _peopleService.UpdatePeople(oldPeople);

                    if (peopleVirtualOld != null)
                        _peopleVirtualService.UpdatePeopleVirtual(peopleVirtualOld);
                    else if (peopleVirtual.Email != null || peopleVirtual.WebSite != null || peopleVirtual.Telegram != null || peopleVirtual.Instagram != null || peopleVirtual.WhatsApp != null)
                    {
                        peopleVirtual.RelativeId = people.Id;
                        _peopleVirtualService.AddPeopleVirtual(peopleVirtual);
                    }

                    _addressService.UpdateAddress(addressOld);

                    if (otherRelationShips != null)
                    {
                        TelPhones telPhones = new TelPhones();
                        List<TelPhones> getTelPhones = _telPhonesService.GetTelPhonesByType(people.Id, (int)Enums.PeopleType.people).ToList();
                        _telPhonesService.DeleteTelPhones(getTelPhones);

                        if (checkRepeatedTels && checkRepeatedTelPhones(otherRelationShips))
                            throw new CustomeException("There Is Duplicated Tel Phones ", UI_Presentation.wwwroot.Resources.Mesages.ThereIsDuplicatedTelPhones , true, null, ref errorMessage);

                        _telPhonesService.AddTelPhones(otherRelationShips, people.Id, (int)Enums.PeopleType.people);
                    }

                }

                else
                {
                    people.SystemCode = _generetedNumberService.NewGenerateNumber(SessionProperty.UserID, (int)Enums.states.People);

                    // NOTE : IN EACH INSERT MUST ADD RELATIVE NUMBER INTO THIS TABLE FOR NEXT SESSIONS  
                    ActivityNumber activityNumber = new ActivityNumber();
                    activityNumber.TBASStateId = (int)Enums.states.People;
                    activityNumber.RelatedNumber = people.SystemCode;
                    _generetedNumberService.InsertNumberInActivity(activityNumber);

                    people.IsActive = true; // Here i set This Fields Manually 
                    _peopleService.AddPeople(people);
                    _peopleService.SaveChanges();

                    if (peopleVirtual.Email != null || peopleVirtual.WebSite != null || peopleVirtual.Telegram != null || peopleVirtual.Instagram != null || peopleVirtual.WhatsApp != null)
                    {
                        peopleVirtual.RelativeId = people.Id;
                        peopleVirtual.Type = (int)Enums.PeopleType.people;
                        _peopleVirtualService.AddPeopleVirtual(peopleVirtual);
                    }

                    if (address.Province != null || address.City != null || address.Area != null || address.Street != null || address.Alley != null || address.OtherAddress != null)
                    {
                        address.RelativeId = people.Id;
                        address.Type = (int)Enums.PeopleType.people;
                        _addressService.AddAddress(address);
                    }

                    if (otherRelationShips != null && otherRelationShips.Count != 0)
                        _telPhonesService.AddTelPhones(otherRelationShips, people.Id, (int)Enums.PeopleType.people);
                }
                
                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;
                _peopleService.SaveChanges();
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = errorMessage != null ? errorMessage : UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return Json(new { message = message, errorMessage = errorMessage });
        }

        public ActionResult ShowPeopleSelectorPopup()
        {
            return PartialView();
        }

        public ActionResult DeletePeople(int peopleId)
        {
            string result = string.Empty;
            string message = string.Empty;
            try
            {
                // NOTE :  BEFORE DELETE I SHOULD CHECK THE POEPLE HAS NOT ANY LOG IN SYSTEM SUCH AS RESERVATION AND ETC ... 
                People people = _peopleService.GetPeopleById(peopleId).FirstOrDefault();
                people.IsActive = false;
                _peopleService.UpdatePeople(people);
                _peopleService.SaveChanges();
                //  _peopleService.DeletePeople(people);
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

        //[NonAction]
        //private bool CheckForRepeatedMobiles(List<TempMobiles> mobiles)
        //{
        //    bool repeatedMobiles = false;
        //    var registeredMobiles = _peoplePropertyService.GetPeoplePrpoertyByMobileNumber((int)Enums.tbasPeopePropertyState.mobile).ToList();

        //    foreach (var item in mobiles)
        //    {
        //        for (int i = 0; i < registeredMobiles.Count; i++)
        //        {
        //            if (item.Mobile == registeredMobiles[i].Value)
        //            {
        //                repeatedMobiles = true;
        //                break;
        //            }
        //        }
        //    }

        //    return repeatedMobiles;
        //}

        [NonAction]
        private bool checkRepeatedTelPhones(List<TelPhoneType> telPhoneType)
        {
            return _telPhonesService.IsRepeatedTelPhones(telPhoneType);
        }

        [HttpPost]
        public ActionResult GetPeopleTelsAndMobiles(int peopleId)
        {
            string errorMessage = string.Empty;
            List<TelPhones> listTelPhones = null;
            try
            {
                listTelPhones = _telPhonesService.GetTelPhonesByType(peopleId, (int)Enums.PeopleType.people).ToList();

                return Json(new
                {
                    errorMessage = errorMessage,
                    listTelPhones = listTelPhones,
                });
            }
            catch (Exception ex)
            {
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }

        [HttpPost]
        public JsonResult GetPeopleList()
        {
            var dt = new DataTable();
            string people = string.Empty;
            string result = string.Empty;

            try
            {
                //using (var context = new CRM_CoreDB())
                //{

                //    var cmd = context.Database.GetDbConnection().CreateCommand();
                //    var param = cmd.CreateParameter();
                //    cmd.CommandText = "[dbo].[People_Search]";
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    //param.ParameterName = "@PeopleId";
                //    //param.Value = 3;
                //    //cmd.Parameters.Add(param);
                //    context.Database.OpenConnection();
                //    var dataReader = cmd.ExecuteReader();
                //    dt.Load(dataReader);
                //    people = JsonConvert.SerializeObject(dt);
                //}
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                people = people,
                result = result,
            });

        }

        [HttpPost]
        public JsonResult GetPeoplePropertyById(int peopleId)
        {
            string errorMessage = string.Empty;
            string result = string.Empty;
            IEnumerable<PeopleModel> peopleModel = new List<PeopleModel>();
            try
            {
                peopleModel = _peopleService.GetPeopleByAdoById(peopleId);
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return Json(new
            {
                errorMessage = errorMessage,
                peopleModel = peopleModel,
            });
        }

        [HttpPost]
        public ActionResult GetDropDownSelectedValue(int peopleId,int type)
        {
            string errorMessage = string.Empty;
            string result = string.Empty;
            List<TelPhones> telPhones = _telPhonesService.GetTelPhonesByType(peopleId, type).ToList();
            try
            {

                //phoneTelTypes = (from _tel in tbasTelPhoneTypes
                //                 join _telPhones in telPhones 
                //                 on _tel.Id equals _telPhones.TBASPhoneTypesId
                //                 //into temp
                //                 //from _temp in temp.DefaultIfEmpty()
                //                 select new
                //                 {
                //                     Title = _tel.Title,
                //                     Id = _tel.Id,
                //                     //SelectedItem = _temp != null && _tel.Id == _temp.TBASPhoneTypesId ? true : false ,
                //                 }).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.Title, Value = item.Id.ToString() , Selected = item.SelectedItem }; });

                telPhones = (from _telPhones in telPhones
                             select new TelPhones
                             {
                                 Id = (int)_telPhones.TBASPhoneTypesId,
                             }
                             ).OrderBy(item => item.Id).ToList();

            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return Json(new
            {
                errorMessage = errorMessage,
                phoneTelTypes = telPhones,
            });
        }

    }
    #endregion

}
