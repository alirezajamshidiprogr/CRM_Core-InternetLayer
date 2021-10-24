using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models.Salon;
using System.Collections.Generic;
using System.Linq;

namespace CRM_Core.Application.Services
{
   public class Ef_TransferCostsService : DataAccessLayer.Repositories.RepositoryBase<TransferCosts>, ITransferCostsService
    {
        public Ef_TransferCostsService(CRM_CoreDB context) : base(context)
        {

        }

        public void AddTransferCosts(TransferCosts transferCost)
        {
            Create(transferCost);
        }

        public void DeleteTransferCost(TransferCosts transferCost)
        {
            Delete(transferCost);
        }

        public List<TransferCosts> GetAllTransferCosts()
        {
            return FindAll().ToList();
        }

        public TransferCosts GetTransferCostById(int id)
        {
            return FindByCondition(item => item.Id == id).First();
        }

        public void UpdateTransferCost(TransferCosts transferCost)
        {
            Update(transferCost);
        }
    }
}
