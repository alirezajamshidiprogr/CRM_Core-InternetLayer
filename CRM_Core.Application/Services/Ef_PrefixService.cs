using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PrefixService : DataAccessLayer.Repositories.RepositoryBase<TBASPrefix>, IPrefixService
    {
        public Ef_PrefixService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<TBASPrefix> GetPrefixes()
        {
            return FindAll();
        }
    }
}
