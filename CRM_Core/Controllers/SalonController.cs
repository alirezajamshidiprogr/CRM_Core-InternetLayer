using System;
using System.Collections.Generic;
using System.Linq;
using CRM_Core.Application.Interfaces;
using CRM_Core.Entities.Models.Salon;
using CRM_Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCRM.Layered.Model.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI_Presentation.Models;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Application.GridViewModels;
using System.Data;

namespace UI_Presentation.Controllers
{
    [Authorize]
    public class SalonController : Controller
    {
        #region CONSTANT
        private ISalonInfoService _salonInfoService;
        private ISalonCostsService _salonCostsService;
        private IBillCostsService _billCostsService;
        private ITransferCostsService _transferCostsService;
        private ITBASSalonCostsService _tbasSalonCostsService;

        public SalonController(ISalonInfoService salonInfoService, ISalonCostsService salonCostsService, IBillCostsService billCostsService, ITransferCostsService transferCostsService, ITBASSalonCostsService tbasSalonCostsService)
        {
            _salonInfoService = salonInfoService;
            _salonCostsService = salonCostsService;
            _billCostsService = billCostsService;
            _transferCostsService = transferCostsService;
            _tbasSalonCostsService = tbasSalonCostsService;
        }
        #endregion

        #region Action
        public IActionResult AddEditSalon()
        {
            ViewBag.Title = UI_Presentation.wwwroot.Resources.General.Title.SalonInfo;
            string errorMessage = string.Empty;

            SalonInfo getSalon = null;
            try
            {
                getSalon = _salonInfoService.GetSalon().LastOrDefault();
                ViewBag.isEditMode = getSalon != null ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }

            return PartialView("AddEditSalon", getSalon);
        }

        public IActionResult AddEditSalonCost(int? salonCostId)
        {
            string errorMessage = string.Empty;
            ViewBag.isEditMode = salonCostId > 0 ? true : false;
            SalonCostsViewModel salonCost = null;

            try
            {
                if (salonCostId > 0)
                {
                    salonCost = _salonCostsService.GetSalonCostsByADOSalonId("[dbo].[GetSalonCost]", (int)salonCostId);
                    ViewBag.costTypeId = salonCost.CostType;
                    ViewBag.SalonCostGroups = _tbasSalonCostsService.GetMainTBASSalonCosts().ToList().ConvertAll(item=> { return new SelectListItem() { Text = item.CostTitle.ToString(), Value = item.Id.ToString(), Selected = item.Id == salonCost.MainSalonCostId ? true : false }; });
                    ViewBag.SalonCostyType = _tbasSalonCostsService.GetAllSubSalonCosts((int)salonCost.MainSalonCostId).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.CostTitle.ToString(), Value = item.Id.ToString() , Selected = item.Id == salonCost.SubSalonCostId ? true : false }; }); ;

                    ViewBag.MainSalonCostId = salonCost.MainSalonCostId;
                    ViewBag.SubSalonCostId = salonCost.SubSalonCostId;
                }

                List<SelectListItem> CostGroups = new List<SelectListItem>();

                CostGroups.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                CostGroups.Insert(1, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.Salon.Title.CostWithPayment, Value = ((int)Enums.costs.CostWithPayment).ToString(),  Selected = salonCostId > 0 ? true : false }) ;
                CostGroups.Insert(2, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.Salon.Title.CostWithOutPayment, Value = ((int)Enums.costs.CostWithoutPayment).ToString() });

                List<SelectListItem> BillTypes = _tbasSalonCostsService.GetSubTBASSalonCosts((int)Enums.costs.Bills).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.CostTitle.ToString(), Value = item.Id.ToString() }; });
                List<SelectListItem> TransferType = _tbasSalonCostsService.GetSubTBASSalonCosts((int)Enums.costs.Transfer).ToList().ConvertAll(item => { return new SelectListItem() { Text = item.CostTitle.ToString(), Value = item.Id.ToString() }; });

                ViewBag.BillTypes = BillTypes;
                ViewBag.CostGroups = CostGroups;
                ViewBag.TransferType = TransferType;
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("AddEditSalonCosts", salonCost);
        }

        public ActionResult IndexSalonCosts()
        {
            string errorMessage = string.Empty;
            try
            {
                List<SelectListItem> CostGroups = new List<SelectListItem>();

                CostGroups.Insert(0, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.General.Title.SelectItem, Value = null });
                CostGroups.Insert(1, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.Salon.Title.CostWithPayment, Value = ((int)Enums.costs.CostWithPayment).ToString() });
                CostGroups.Insert(2, new SelectListItem() { Text = UI_Presentation.wwwroot.Resources.Salon.Title.CostWithOutPayment, Value = ((int)Enums.costs.CostWithoutPayment).ToString() });

                ViewBag.CostGroups = CostGroups;
                return PartialView("SalonCosts");
            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
        }



        #endregion Action

        #region Method

        public IActionResult FillSalonCostsTableData(SalonCostsViewModelSearch searchParams)
        {
            string errorMessage = string.Empty;
            DataGridViewModel<SalonCostsViewModelGrid> salonCostsList = new DataGridViewModel<SalonCostsViewModelGrid>();
            try
            {
                string[] searchParameter;
                object[] searchValues;
                searchParameter = new string[]
                {
                     "@TBASSalonCostId"
                    ,"@FromPrice"
                    ,"@ToPrice"
                    ,"@FromDate"
                    ,"@ToDate"
                    ,"@CostDescription"
                    ,"@Page"
                    ,"@PageSize"
                    ,"@Sort"
                };

                searchValues = new object[]
                {
                    searchParams.TBASSalonCostId
                   ,searchParams.FromPrice
                   ,searchParams.ToPrice
                   ,searchParams.FromDate
                   ,searchParams.ToDate
                   ,searchParams.CostDescription
                   ,searchParams.PageNumber
                   ,10
                   ,string.Empty
                };


                DataSet dsData = _salonCostsService.GetSalonCostsDataTables("[dbo].[SalonCosts_Search]", searchParameter, searchValues);
                ViewBag.totalAllRecords = (int)dsData.Tables[1].Rows[0][0];
                ViewBag.pageNumber = searchParams.PageNumber;
                salonCostsList.Records = MappingUtility.DataTableToList<SalonCostsViewModelGrid>(dsData.Tables[0]).AsQueryable();
                ViewBag.summery = MappingUtility.DataTableToList<SalonCostsViewModelGridSummery>(dsData.Tables[2]).AsQueryable();
                salonCostsList.TotalCount = salonCostsList.Records.Count();
                //salonCostsList.Records = _salonCostsService.GetSalonCostsByADO("[dbo].[SalonCosts_Search]", searchParameter, searchValues);

                ViewBag.isEditMode = true;
                ViewBag.isSelectedMode = true;
                ViewBag.isPrintMode = true;


            }
            catch (Exception ex)
            {
                errorMessage = string.Empty;
                Utility.RegisterErrorLog(ex, SessionProperty.UserName);
                return Json(new { errorMessage = UI_Presentation.wwwroot.Resources.Mesages.AnErrorHasAccuredInTheOperation });
            }
            return PartialView("FillSalonCostsTableData", salonCostsList);
        }


        [HttpPost]
        public ActionResult AddEditSalonMethod(bool isEdit, SalonInfo salonInfo)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            if (!ModelState.IsValid)
                throw new CustomeException("Model Is Not Valid", true, null);

            try
            {
                if (isEdit)
                {
                    SalonInfo oldSalon = _salonInfoService.GetSalon().LastOrDefault();
                    oldSalon.SalonName = salonInfo.SalonName;
                    oldSalon.Manager = salonInfo.Manager;
                    oldSalon.Tel = salonInfo.Tel;
                    oldSalon.Telegram = salonInfo.Telegram;
                    oldSalon.WhatsApp = salonInfo.WhatsApp;
                    oldSalon.Website = salonInfo.Website;
                    oldSalon.Instagram = salonInfo.Instagram;
                    oldSalon.Description = salonInfo.Description;
                    oldSalon.LisenceNumber = salonInfo.LisenceNumber;
                    oldSalon.Address = salonInfo.Address;
                    oldSalon.EditDate = DateTime.Now.ToPersianDate();
                    oldSalon.Mobile = salonInfo.Mobile;
                    _salonInfoService.UpdateSaloInfo(oldSalon);
                }
                else
                {
                    salonInfo.RegisterDate = DateTime.Now.ToPersianDate();
                    _salonInfoService.AddSalonInfo(salonInfo);
                }

                _salonInfoService.SaveChanges();
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
        public ActionResult AddEditSalonCostsMethod(bool isEdit, int costType, SalonCostsViewModel salonCosts)
        {
            string message = string.Empty;
            string errorMessage = string.Empty;
            if (!ModelState.IsValid)
                throw new CustomeException("Model Is Not Valid", true, null);

            try
            {
                if (isEdit)
                    EditSalonCostByType(salonCosts);
                else
                    AddSalonCostByType(costType, salonCosts);

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
        public ActionResult DeleteSalonCostsMethod(int salonCostId)
        {
            string message = string.Empty;
            string result = string.Empty;
            SalonCosts salonCost = _salonCostsService.GetSalonCostById(salonCostId);
            int costType = (int)salonCost.TBASSalonCostId;
            try
            {
                // NOTE :  BEFORE DELETE I SHOULD CHECK THE POEPLE HAS NOT ANY LOG IN SYSTEM SUCH AS RESERVATION AND ETC ... 
                switch (costType)
                {
                    case (int)Enums.costs.Bills:
                         BillCosts billCost = _billCostsService.GetBillCostById(Convert.ToInt32(salonCost.RelatinveId));
                        _billCostsService.DeleteBillCost(billCost);
                        break;
                    case (int)Enums.costs.Transfer:
                         TransferCosts transferCosts = _transferCostsService.GetTransferCostById(Convert.ToInt32(salonCost.RelatinveId));
                        _transferCostsService.DeleteTransferCost(transferCosts);
                        break;
                    case (int)Enums.costs.Foods:
                        break;
                    case (int)Enums.costs.Salary:
                        break;
                    default:
                        break;
                }
                _salonCostsService.DeleteSalonCost(salonCost);
                _salonInfoService.SaveChanges();
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

        public JsonResult GetCostTypeByCostGroup(int costGroupId)
        {
            string errorMessage = string.Empty;
            List<TempCostType> costType = new List<TempCostType>();
            try
            {
                costType.Add(new TempCostType { Id = 0, Name = UI_Presentation.wwwroot.Resources.General.Title.SelectItem });
                if (costGroupId == (int)Enums.costs.CostWithPayment)
                {
                    List<TBASSalonCosts> tbasSalonCosts = _tbasSalonCostsService.GetMainTBASSalonCosts().Where(item=>item.Id <4).ToList();
                    foreach (var item in tbasSalonCosts)
                        costType.Add(new TempCostType { Id = item.Id, Name = item.CostTitle });
                }
                else
                {
                    costType.Add(new TempCostType { Id = (int)Enums.costs.Depriciation, Name = UI_Presentation.wwwroot.Resources.Salon.Title.Depriciation });
                }
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
                costType = costType,
            });
        }

        public JsonResult GetCostTypeDetail(int costType)
        {
            string errorMessage = string.Empty;
            List<TempCostType> costDetailType = new List<TempCostType>();
            try
            {
                //costDetailType.Add(new TempCostType { Id = 0, Name = UI_Presentation.wwwroot.Resources.General.Title.SelectItem });
                foreach (var item in _tbasSalonCostsService.GetSubTBASSalonCosts(costType).ToList())
                    costDetailType.Add(new TempCostType { Id = item.Id, Name = item.CostTitle });
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
                costType = costDetailType.OrderByDescending(item=>item.Id),
            });
        }

        public void AddSalonCostByType(int CostType, SalonCostsViewModel model)
        {
            DateTime registerDate = DateTime.Now;

            SalonCosts salonCost = new SalonCosts();
            salonCost.CostName = model.CostName;
            salonCost.Price = model.Price;
            salonCost.M_RegisterDate = registerDate;
            salonCost.F_RegisterDate = registerDate.ToPersianDate();
            salonCost.F_CostDate = model.F_CostDate;
            salonCost.M_CostDate = model.F_CostDate.ToDateTime();
            salonCost.Description = model.Description;

            switch (CostType)
            {
                case (int)Enums.costs.OtherBill:
                    // Insert in General Costs
                    salonCost.RelatinveId = null;
                    salonCost.TBASSalonCostId = (int)Enums.costs.OtherBill;
                    _salonCostsService.AddGeneralSalonCosts(salonCost);
                    break;

                case (int)Enums.costs.Bills:
                    //Insert In Bill Costs 
                    BillCosts billCosts = new BillCosts();

                    billCosts.BillIdentity = model.BillIdentity;
                    billCosts.PayIdentity = model.PayIdentity;
                    billCosts.BillType = (int)model.BillType;

                    _billCostsService.AddBillCosts(billCosts);
                    _salonInfoService.SaveChanges();

                    salonCost.RelatinveId = _billCostsService.GetAllBillCost().LastOrDefault().Id;
                    salonCost.TBASSalonCostId = (int)Enums.costs.Bills;
                    _salonCostsService.AddGeneralSalonCosts(salonCost);
                    break;

                case (int)Enums.costs.Transfer:
                    //Insert In Transfer Costs 
                    TransferCosts transferCosts = new TransferCosts();

                    transferCosts.FromTarget = model.FromTarget;
                    transferCosts.ToDestination = model.ToDestination;
                    transferCosts.TransferType = (int)model.TransferType;

                    _transferCostsService.AddTransferCosts(transferCosts);
                    _salonInfoService.SaveChanges();


                    salonCost.RelatinveId = _transferCostsService.GetAllTransferCosts().LastOrDefault().Id;
                    salonCost.TBASSalonCostId = (int)Enums.costs.Transfer;
                    _salonCostsService.AddGeneralSalonCosts(salonCost);
                    break;
                case (int)Enums.costs.Foods:
                    //Insert In Foods Costs 
                    break;
                case (int)Enums.costs.Salary:
                    //Insert In Salary Costs 
                    break;
                default:
                    salonCost.RelatinveId = null;
                    salonCost.TBASSalonCostId = (int)Enums.costs.GeneralCosts;
                    salonCost.CostName = model.CostName;
                    _salonCostsService.AddGeneralSalonCosts(salonCost);
                    break;
            }

            _salonInfoService.SaveChanges();
        }
        public void EditSalonCostByType(SalonCostsViewModel model)
        {
            DateTime EditDate = DateTime.Now;

            SalonCosts oldSalonCost = _salonCostsService.GetSalonCostById(model.SalonCostId);

            oldSalonCost.CostName = model.CostName;
            oldSalonCost.Price = model.Price;
            oldSalonCost.M_EditDate = EditDate;
            oldSalonCost.F_EditDate = EditDate.ToPersianDate();
            oldSalonCost.F_CostDate = model.F_CostDate;
            oldSalonCost.M_CostDate = model.F_CostDate.ToMiladiDate();
            oldSalonCost.Description = model.Description;

            switch (oldSalonCost.TBASSalonCostId)
            {
                case (int)Enums.costs.OtherBill:
                    _salonCostsService.UpdateSalonCost(oldSalonCost);
                    break;

                case (int)Enums.costs.Bills:
                    BillCosts oldBillCost = _billCostsService.GetBillCostById(Convert.ToInt32(oldSalonCost.RelatinveId));

                    oldBillCost.BillIdentity = model.BillIdentity;
                    oldBillCost.PayIdentity = model.PayIdentity;
                    oldBillCost.BillType = (int)model.BillType;
                    _billCostsService.UpdateBillCost(oldBillCost);
                    break;

                case (int)Enums.costs.Transfer:

                    TransferCosts oldTransferCosts = _transferCostsService.GetTransferCostById(Convert.ToInt32(oldSalonCost.RelatinveId));

                    oldTransferCosts.FromTarget = model.FromTarget;
                    oldTransferCosts.ToDestination = model.ToDestination;
                    oldTransferCosts.TransferType = (int)model.TransferType;

                    _transferCostsService.UpdateTransferCost(oldTransferCosts);
                    break;

                case (int)Enums.costs.Foods:
                    //Insert In Foods Costs 
                    break;
                case (int)Enums.costs.Salary:
                    //Insert In Salary Costs 
                    break;
                default:
                    //AddEditSalonCostByType((int)Enums.costs.OtherBill);
                    break;
            }

            _salonInfoService.SaveChanges();
        }
        #endregion
    }
}
