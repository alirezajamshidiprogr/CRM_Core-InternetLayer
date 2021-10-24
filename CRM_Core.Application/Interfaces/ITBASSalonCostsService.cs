using CRM_Core.Entities.Models.Salon;
using System.Collections.Generic;

namespace CRM_Core.Application.Interfaces
{
    public interface ITBASSalonCostsService
    {
        IEnumerable<TBASSalonCosts> GetMainTBASSalonCosts();
        IEnumerable<TBASSalonCosts> GetSubTBASSalonCosts(int costTypeId);
        IEnumerable<TBASSalonCosts> GetAllSubSalonCosts(int tbasSalonCostId);

    }
}
