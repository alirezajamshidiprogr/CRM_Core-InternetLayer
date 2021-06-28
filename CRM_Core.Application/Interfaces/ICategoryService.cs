using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface ICategoryService
    {
        IEnumerable<TBASCategory> GetCategories();
    }
}
