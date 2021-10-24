using CRM_Core.Entities.Models.Salon;
using CRM_Core.Application.ViewModels.CustomViewModel;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace CRM_Core.Application.Interfaces
{
    public interface ISalonCostsService
    {
        void AddGeneralSalonCosts(SalonCosts salonCost);
        SalonCosts GetSalonCostById(int id);
        void UpdateSalonCost(SalonCosts salonCost);
        void DeleteSalonCost(SalonCosts salonCost);
        IEnumerable<SalonCostsViewModelGrid> GetSalonCostsByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);
        SalonCostsViewModel GetSalonCostsByADOSalonId(string commandText, int SalonCostId);

        DataSet GetSalonCostsDataTables(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);
    }
}
