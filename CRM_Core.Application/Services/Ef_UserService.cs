using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_UserService : RepositoryBase<User>, IUserService
    {
        public Ef_UserService(CRM_CoreDB context) : base(context)
        {

        }

        public IQueryable<User> GetUserByUserNamePassword(string userName, string password)
        {
            return FindByCondition(item => item.UserName.Equals(userName) && item.Password.Equals(password) && item.IsActive == true);
        }

        public IEnumerable<User> GetUsers()
        {
            return FindAll();
        }
    }
}
