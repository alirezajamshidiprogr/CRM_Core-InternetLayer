using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Models.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_UserMenuService : RepositoryBase<UserMenu> , IUserMenuService
    {
        public Ef_UserMenuService(CRM_CoreDB context) :base(context)
        {

        }

        public void AddUserMenu(List<UserMenu> userMenu , string userIdentityId)
        {
            foreach (var userMenus in userMenu)
            {
                userMenus.IdentityUserId = userIdentityId;
                Create(userMenus);
            }
        }
        public void DeleteUserMenuByUserId(List<UserMenu> userMenu)
        {
            foreach (var item in userMenu)
            {
                    Delete(item);
            }
        }

        public IEnumerable<UserMenu> GetUserMenuByUserIdentity(string UserIdentityId)
        {
            return FindByCondition(item=>item.IdentityUserId == UserIdentityId);
        }
    }
}
