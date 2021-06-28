using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        IQueryable<User> GetUserByUserNamePassword(string userName , string password);

    }
}
