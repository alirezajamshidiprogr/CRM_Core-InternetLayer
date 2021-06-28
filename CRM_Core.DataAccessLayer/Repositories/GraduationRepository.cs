using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
    public class GraduationRepository :IGraduationRepository
    {
        public CRM_CoreDB _context;
        public GraduationRepository(CRM_CoreDB context)
        {
            _context = context;  
        }

        public IEnumerable<TBASGraduation> GetGraduations()
        {
           return _context.TBASGraduation;
        }
    }
}
