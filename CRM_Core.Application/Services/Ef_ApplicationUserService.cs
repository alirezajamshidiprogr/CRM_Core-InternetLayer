using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
   public class Ef_ApplicationUserService :RepositoryBase<ApplicationUser> , IApplicationUserService
    {
        public Ef_ApplicationUserService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return FindAll();
        }

        public IEnumerable<ApplicationUser> GetByIdentityUserID(string Id)
        {
            return FindByCondition(item => item.IdentityUserId.Equals(Id));
        }
    }
}
