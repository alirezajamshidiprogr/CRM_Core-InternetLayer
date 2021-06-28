using CRM_Core.DomainLayer;
using CRM_Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public CRM_CoreDB _context;
        public UserRepository(CRM_CoreDB context)
        {
            _context = context;
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.User;
        }
    }
}
