using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using UI_Presentation.Models;
using CRM_Core.Infrastructure;
using System.Web.WebPages.Html;
using System.Data;
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.GridViewModels;
using CRM_Core.Application.ViewModels.People;
using MyCRM.Layered.Model.Utility;

namespace CRM_Core.Controllers
{
    //[Log]
    public class PeopleController : Controller
    {
        #region CONSTANT
        private IPeopleService _peopleService;
        private ICategoryService _categoryService;
        private IPrefixService _prefixService;
        private IIntroductionTypeService _introductionTypeService;
        private IPotentialService _potentialService;
        private IGraduationService _graduationService;
        private IAddressService _addressService;
        private IPeopleVirtualService _peopleVirtualService;
        private IPeoplePropertyService _peoplePropertyService;
        #endregion

        public PeopleController(IPeopleService peopleService, ICategoryService categoryService, IPotentialService potentialService, IPrefixService prefixService, IIntroductionTypeService introductionService, IGraduationService graduationService, IAddressService addressService, IPeopleVirtualService peopleVirtualService, IPeoplePropertyService peoplePropertyService)
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
        }

        #region Action

        [HttpGet]
        public IActionResult Index()
        {
            var a = SessionProperty.UserID;
            var aa = SessionProperty.UserName;
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.ShowPeople;
            return PartialView("People");
        }

        //[System.Web.Mvc.ChildActionOnly]
        public IActionResult FillPeopleTableData(bool quickSearch,string fullName)
        {
            string errorMessage = string.Empty;
            DataGridViewModel<Application.ViewModels.People.PeopleModel> peopleList = new Application.GridViewModels.DataGridViewModel<Application.ViewModels.People.PeopleModel>();
            DataTable dt = new DataTable();
            try
            {
                string[] searchParameter;
                object[] searchValues;

                if (quickSearch)
                {
                    searchParameter = new string[] { "@FullName"};
                    searchValues = new object[] { fullName};
                }
                searchParameter = new string[] { "@SystemCode", "@ManualCode", "@FirstName", "@LastName", "@Age" };
                searchValues  = new object[] { "2554", "144", "asdg", "aagas", 1 };
                peopleList.Records = _peopleService.GetPeopleByADO("[dbo].[People_Search]", null, null);
                peopleList.TotalCount = peopleList.Records.Count();
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                return Json(new { errorMessage = ex.Message == string.Empty || ex.Message == null ? UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation : ex.Message });
            }
            return PartialView(peopleList);
        }

        [HttpPost]
        public IActionResult AddEditPeople(int? peopleId)
        {
            ViewBag.isEdit = false;
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
                int peopleVirtualId = _peopleVirtualService.GetPeopleVirtualByPeopleId(people.FirstOrDefault().Id).FirstOrDefault().Id;

                peopleViewModel.People = _peopleService.GetPeopleById(peopleId.Value).FirstOrDefault();
                peopleViewModel.Address = _addressService.GetAddressById(addressId).FirstOrDefault();
                peopleViewModel.PeopleVirtual = _peopleVirtualService.GetPeopleVirtualById(peopleVirtualId).FirstOrDefault();
            }
            else
            {
                ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.AddPeople;
            }

            return PartialView(peopleViewModel);
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
        [Authorization]
        [HttpPost]
        public ActionResult AddEditPeopleMethod(bool isEdit, bool checkRepeatedTels, bool checkRepeatedMobiles, List<TempTels> tels, List<TempMobiles> mobiles, PeopleVirtual peopleVirtual, Address address, People people)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            //ModelState["people.Category"].Errors.Clear();
            ModelState["address.People"].Errors.Clear();

            //if (!ModelState.IsValid)
            //throw new CustomeException("Model Is Not Valid", true, null);
            try
            {
                people.M_Birthday = people.P_Birthday != null ? people.P_Birthday.ToDateTime() : (DateTime?)null;
                people.M_MariedDate = people.P_MariedDate != null ? people.P_MariedDate.ToDateTime() : (DateTime?)null;
                people.SystemCode = !isEdit ? (Convert.ToInt64("00100000001") + 1).ToString() : people.SystemCode;

                //var resulssst = context.People.FromSqlRaw("People_Search1");

                if (isEdit)
                {
                    PeopleVirtual peopleVirtualOld = _peopleVirtualService.GetPeopleVirtualByPeopleId(people.Id).FirstOrDefault();
                    Address addressOld = _addressService.GetAddressByPeopleId(people.Id).FirstOrDefault();

                    peopleVirtualOld.Email = peopleVirtual.Email;
                    peopleVirtualOld.WebSite = peopleVirtual.WebSite;
                    peopleVirtualOld.Telegram = peopleVirtual.Telegram;
                    peopleVirtualOld.Instagram = peopleVirtual.Instagram;
                    peopleVirtualOld.WhatsApp = peopleVirtual.WhatsApp;

                    addressOld.Province = address.Province;
                    addressOld.City = address.City;
                    addressOld.Area = address.Area;
                    addressOld.Street = address.Street;
                    addressOld.Alley = address.Alley;
                    addressOld.OtherAddress = address.OtherAddress;

                    _peopleService.UpdatePeople(people);
                    //_peopleService.PeopleVirtual.Update(peopleVirtualOld);
                    _peopleVirtualService.UpdatePeopleVirtual(peopleVirtualOld);
                    //context.Address.Update(addressOld);
                    _addressService.UpdateAddress(addressOld);

                    // Delete Previous Data For Tels
                    var oldPeoplePropertyTels = _peoplePropertyService.GetPeoplePrpoertyTels(people, (int)Enums.tbasPeopePropertyState.code, (int)Enums.tbasPeopePropertyState.number, (int)Enums.tbasPeopePropertyState.commentTel);
                    _peoplePropertyService.DeletePeopleProperty(oldPeoplePropertyTels.ToList());


                    if (tels.Count != 0)
                    {
                        if (checkRepeatedTels && CheckForRepeatedTels(tels))
                            throw new CustomeException(UI_Presentation.wwwroot.Resources.Mesages.DoesNotAlllowedToRegisterRepeatedTels);

                        for (int i = 0; i < tels.Count; i++)
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                string comment = tels[i].Comment == null ? string.Empty : tels[i].Comment.ToString();
                                _peoplePropertyService.AddPeopleProperty(new PeopleProperty
                                {
                                    People = people,
                                    TBASPeopleTypeField = j,
                                    Value = j == 1 ? tels[i].Code.ToString() : j == 2 ? tels[i].Number.ToString() : comment,
                                    Order = i,
                                });
                            }
                        }
                    }

                    // Delete Previous Data For Mobiles
                    var oldPeoplePropertyMobiles = _peoplePropertyService.GetPeoplePrpoertyMobiles(people, (int)Enums.tbasPeopePropertyState.mobile, (int)Enums.tbasPeopePropertyState.mobileComment);
                    _peoplePropertyService.DeletePeopleProperty(oldPeoplePropertyMobiles.ToList());


                    if (mobiles.Count != 0)
                    {
                        if (checkRepeatedMobiles && CheckForRepeatedMobiles(mobiles))
                            throw new CustomeException(UI_Presentation.wwwroot.Resources.Mesages.DoesNotAlllowedToRegisterRepeatedMobiles);

                        for (int i = 0; i < mobiles.Count; i++)
                        {
                            for (int j = 4; j <= 5; j++)
                            {
                                string comment = mobiles[i].Comment == null ? string.Empty : mobiles[i].Comment.ToString();
                                _peoplePropertyService.AddPeopleProperty(new PeopleProperty
                                {
                                    People = people,
                                    TBASPeopleTypeField = j,
                                    Value = j == 4 ? mobiles[i].Mobile.ToString() : comment,
                                    Order = i,
                                });
                            }
                        }
                    }

                }
                else
                {

                    _peopleService.AddPeople(people);

                    if (peopleVirtual.Email != null || peopleVirtual.WebSite != null || peopleVirtual.Telegram != null || peopleVirtual.Instagram != null || peopleVirtual.WhatsApp != null)
                    {
                        peopleVirtual.People = people;
                        _peopleVirtualService.AddPeopleVirtual(peopleVirtual);
                    }

                    if (address.Province != null || address.City != null || address.Area != null || address.Street != null || address.Alley != null || address.OtherAddress != null)
                    {
                        address.People = people;
                        _addressService.AddAddress(address);
                    }

                    if (tels.Count != 0)
                    {
                        if (checkRepeatedTels && CheckForRepeatedTels(tels))
                            throw new CustomeException(UI_Presentation.wwwroot.Resources.Mesages.DoesNotAlllowedToRegisterRepeatedTels);

                        for (int i = 0; i < tels.Count; i++)
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                string comment = tels[i].Comment == null ? string.Empty : tels[i].Comment.ToString();
                                _peoplePropertyService.AddPeopleProperty(new PeopleProperty
                                {
                                    People = people,
                                    TBASPeopleTypeField = j,
                                    Value = j == 1 ? tels[i].Code.ToString() : j == 2 ? tels[i].Number.ToString() : comment,
                                    Order = i,
                                });
                            }
                        }
                    }

                    if (mobiles.Count != 0)
                    {
                        if (checkRepeatedMobiles && CheckForRepeatedMobiles(mobiles))
                            throw new CustomeException(UI_Presentation.wwwroot.Resources.Mesages.DoesNotAlllowedToRegisterRepeatedMobiles);

                        for (int i = 0; i < mobiles.Count; i++)
                        {
                            for (int j = 4; j <= 5; j++)
                            {
                                string comment = mobiles[i].Comment == null ? string.Empty : mobiles[i].Comment.ToString();
                                _peoplePropertyService.AddPeopleProperty(new PeopleProperty
                                {
                                    People = people,
                                    TBASPeopleTypeField = j,
                                    Value = j == 4 ? mobiles[i].Mobile.ToString() : comment,
                                    Order = i,
                                });
                            }
                        }
                    }

                }

                message = UI_Presentation.wwwroot.Resources.Mesages.TheActionEndedWithSuccess;

                _peopleService.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                return Json(new { errorMessage = ex.Message == string.Empty || ex.Message == null ? UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation : ex.Message });
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
                People people = _peopleService.GetPeopleById(peopleId).FirstOrDefault();
                _peopleService.DeletePeople(people);
                message = UI_Presentation.wwwroot.Resources.Mesages.DeleteSuccessfullyApplied;
            }
            catch (Exception ex)
            {
                result = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation;
            }
            return Json(new
            {
                result = result,
                message = message,
            });
        }

        [NonAction]
        private bool CheckForRepeatedMobiles(List<TempMobiles> mobiles)
        {
            bool repeatedMobiles = false;
            var registeredMobiles = _peoplePropertyService.GetPeoplePrpoertyByMobileNumber((int)Enums.tbasPeopePropertyState.mobile).ToList();

            foreach (var item in mobiles)
            {
                for (int i = 0; i < registeredMobiles.Count; i++)
                {
                    if (item.Mobile == registeredMobiles[i].Value)
                    {
                        repeatedMobiles = true;
                        break;
                    }
                }
            }

            return repeatedMobiles;
        }

        [NonAction]
        private bool CheckForRepeatedTels(List<TempTels> tels)
        {
            bool repeatedTel = false;
            var registeredTels = _peoplePropertyService.GetPeoplePrpoertyByPhonenumber((int)Enums.tbasPeopePropertyState.number).ToList();

            foreach (var item in tels)
            {
                for (int i = 0; i < registeredTels.Count; i++)
                {
                    if (item.Number == registeredTels[i].Value)
                    {
                        repeatedTel = true;
                        break;
                    }
                }
            }

            return repeatedTel;
        }

        [NonAction]
        public ActionResult GetPeopleTelsAndMobiles(int peopleId)
        {
            string result = string.Empty;

            DataSet ds = GetDataFromSql.LoadStoreProcedureDS(peopleId);
            List<TempTels> telList = new List<TempTels>();
            List<TempMobiles> mobileList = new List<TempMobiles>();

            telList = GetDataFromSql.DataTableToList<TempTels>(ds.Tables[1]);
            mobileList = GetDataFromSql.DataTableToList<TempMobiles>(ds.Tables[0]);

            return Json(new
            {
                result = result,
                telList = telList,
                mobileList = mobileList,
            });
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
            string result = string.Empty;
            //GetPeopleByValue people = new GetPeopleByValue();
            string people = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                //using (var context = new CRM_CoreDB())
                //{
                //    var cmd = context.Database.GetDbConnection().CreateCommand();
                //    var param = cmd.CreateParameter();
                //    cmd.CommandText = "[dbo].[People_GetPropertyById]";
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    param.ParameterName = "@PeopleId";
                //    param.Value = peopleId;
                //    cmd.Parameters.Add(param);
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


    }
    #endregion

}
