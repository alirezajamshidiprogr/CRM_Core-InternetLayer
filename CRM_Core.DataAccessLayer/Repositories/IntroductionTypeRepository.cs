using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
   public class IntroductionTypeRepository : IIntroductionTypeRepository
    {
        public CRM_CoreDB _context;
        public IntroductionTypeRepository(CRM_CoreDB context)
        {
           _context = context; 
        }

        public IEnumerable<TBASIntroductionType> GetIntroductionTypes()
        {
            return _context.TBASIntroductionType;
        }
    }
}
