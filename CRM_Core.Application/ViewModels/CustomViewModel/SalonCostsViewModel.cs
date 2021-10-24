using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class SalonCostsViewModel
    {
        public int SalonCostId { get; set; }
        public int CostType { get; set; }
        public string CostName { get; set; }
        public double Price { get; set; }
        public string F_RegisterDate { get; set; }
        public string F_EditDate { get; set; }
        public DateTime M_RegisterDate { get; set; }
        public DateTime? M_EditDate { get; set; }
        public string F_CostDate { get; set; }
        public DateTime? M_CostDate { get; set; }
        public string Description { get; set; }
        public int? BillCostsId { get; set; }
        public int? TransferCostsId { get; set; }
        public string BillIdentity { get; set; }
        public string PayIdentity { get; set; }
        public int? BillType { get; set; } 
        public string FromTarget { get; set; }
        public string ToDestination { get; set; }
        public int? TransferType { get; set; }
        public int? MainSalonCostId { get; set; }
        public int? SubSalonCostId { get; set; }
    }

    public class SalonCostsViewModelSearch
    {
        public int? TBASSalonCostId { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CostDescription { get; set; }
        public int PageNumber { get; set; }
    }
    public class SalonCostsViewModelGrid
    {
        public int SalonCostId { get; set; }
        public string CostTitle { get; set; }
        public string Price { get; set; }
        public string CostName { get; set; }
        public string F_CostDate { get; set; }
        public string Description { get; set; }
    }

    public class SalonCostsViewModelGridSummery
    {
        public string totalPriceGeneralCost { get; set; }
        public string totalSellAndDistibuteCost { get; set; }
        public string totalAccontantCosts { get; set; }
    }

}
