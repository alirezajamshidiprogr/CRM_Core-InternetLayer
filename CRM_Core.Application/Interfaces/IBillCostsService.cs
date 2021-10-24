using CRM_Core.Entities.Models.Salon;
using System.Collections.Generic;
using System.Linq;

namespace CRM_Core.Application.Interfaces
{
    public interface IBillCostsService
    {
        void AddBillCosts(BillCosts billCosts);
        List<BillCosts> GetAllBillCost();
        BillCosts GetBillCostById(int id);
        void UpdateBillCost(BillCosts billCosts);
        void DeleteBillCost(BillCosts billCost);
       
    }
}
