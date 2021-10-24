using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models.Salon;
using System;
using System.Collections.Generic;

namespace CRM_Core.Application.Services
{
    public class Ef_TBASSalonCostsService : DataAccessLayer.Repositories.RepositoryBase<TBASSalonCosts>, ITBASSalonCostsService
    {
        public Ef_TBASSalonCostsService(CRM_CoreDB context) :base(context)
        {

        }

        public IEnumerable<TBASSalonCosts> GetAllSubSalonCosts(int tbasSalonCostId)
        {
           return FindByCondition(item => item.TBASSalonCostId == tbasSalonCostId);
        }
        

        public IEnumerable<TBASSalonCosts> GetMainTBASSalonCosts()
        {
           return FindByCondition(item=>item.TBASSalonCostId == null );
        }

        public IEnumerable<TBASSalonCosts> GetSubTBASSalonCosts(int costTypeId)
        {
            return FindByCondition(item => item.TBASSalonCostId == costTypeId);
        }
    }
}
