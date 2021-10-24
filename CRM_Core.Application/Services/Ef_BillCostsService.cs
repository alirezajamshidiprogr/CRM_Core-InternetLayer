using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models.Salon;
using System.Collections.Generic;
using System.Linq;

namespace CRM_Core.Application.Services
{
    public class Ef_BillCostsService : DataAccessLayer.Repositories.RepositoryBase<BillCosts>, IBillCostsService
    {
        public Ef_BillCostsService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddBillCosts(BillCosts billCosts)
        {
            Create(billCosts);
        }

        public void DeleteBillCost(BillCosts billCost)
        {
            Delete(billCost); 
        }

        public List<BillCosts> GetAllBillCost()
        {
            return FindAll().ToList();
        }

        public BillCosts GetBillCostById(int id)
        {
            return FindByCondition(item => item.Id == id).First();
        }

        public void UpdateBillCost(BillCosts billCosts)
        {
            Update(billCosts);
        }
    }
}
