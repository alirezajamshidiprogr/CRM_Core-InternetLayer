using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PotentialService : RepositoryBase<TBASPotential>, IPotentialService
    {
        public Ef_PotentialService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<TBASPotential> GetPotentials()
        {
            return FindAll();
        }
    }
}
