using CRM_Core.Entities.Models.Salon;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface ITransferCostsService
    {
        void AddTransferCosts(TransferCosts transferCost);
        List<TransferCosts> GetAllTransferCosts();
        TransferCosts GetTransferCostById(int id);
        void UpdateTransferCost(TransferCosts transferCost);
        void DeleteTransferCost(TransferCosts transferCost);
    }
}
