using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface IApplicationUserService
    {
        IEnumerable<ApplicationUser> GetByIdentityUserID(string Id);
        IEnumerable<ApplicationUser> GetAllUser();
    }
}
