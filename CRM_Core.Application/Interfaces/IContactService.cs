using CRM_Core.DomainLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IContactService
    {
        public IEnumerable<Contact> GetContact();
        IQueryable<Contact> GetContactById(int id);
        void AddContact(Contact contact);

        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
        public IEnumerable<Contact> getLastContact();

        void SaveChanges();
    }
}
