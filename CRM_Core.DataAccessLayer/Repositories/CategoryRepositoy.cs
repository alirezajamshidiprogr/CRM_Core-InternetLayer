using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
    public class CategoryRepositoy :ICategoryRepository
    {
        public CRM_CoreDB _context;
        public CategoryRepositoy(CRM_CoreDB context)
        {
            _context = context;
        }

        public IEnumerable<TBASCategory> GetCategories()
        {
            return _context.TBASCategory;
        }
    }
}
