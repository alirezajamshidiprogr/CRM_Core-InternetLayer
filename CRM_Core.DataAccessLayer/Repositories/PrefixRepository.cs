using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
    public class PrefixRepository:IPrefixRepository
    {
        public CRM_CoreDB _context;
        public PrefixRepository(CRM_CoreDB context)
        {
            _context = context; 
        }

        public IEnumerable<TBASPrefix> GetPrefixes()
        {
            return _context.TBASPrefix;
        }
    }
}
