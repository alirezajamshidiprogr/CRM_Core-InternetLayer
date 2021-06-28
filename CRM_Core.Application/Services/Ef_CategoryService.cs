using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_CategoryService : RepositoryBase<TBASCategory> , ICategoryService
    {
        public Ef_CategoryService(CRM_CoreDB context) : base(context)
        {
                
        }

        public IEnumerable<TBASCategory> GetCategories()
        {
            return FindAll();
        }
    }
}
