using CRM_Core.Entities.Models.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IUserMenuService
    {
        public IEnumerable<UserMenu> GetUserMenuByUserIdentity(string UserIdentityId);
        public void AddUserMenu(List<UserMenu> userMenu , string userIdentityId);
        public void DeleteUserMenuByUserId(List<UserMenu> userMenu);
        public void SaveChanges();

    }
}
