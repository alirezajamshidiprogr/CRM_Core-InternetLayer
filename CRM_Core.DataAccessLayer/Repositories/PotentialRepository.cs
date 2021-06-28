using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
    public class PotentialRepository : IPotentialRepository
    {
        public CRM_CoreDB _context;
        public PotentialRepository(CRM_CoreDB context)
        {
            _context = context;
        }

        public IEnumerable<TBASPotential> GetPotentials()
        {
            return _context.TBASPotential;
        }
    }
}
