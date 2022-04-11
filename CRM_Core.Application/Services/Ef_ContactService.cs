using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DomainLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ContactService : DataAccessLayer.Repositories.RepositoryBase<Contact>, IContactService
    {
        public Ef_ContactService(CRM_CoreDB context) : base(context)
        {

        }

        public void AddContact(Contact contact)
        {
            Create(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Delete(contact);
        }

        public IEnumerable<Contact> GetContact()
        {
            return FindAll();
        }

        public IQueryable<Contact> GetContactById(int id)
        {
            return FindByCondition(item => item.Id == id);
        }

        public IEnumerable<Contact> getLastContact()
        {
            return FindAll();
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }
    }
}
