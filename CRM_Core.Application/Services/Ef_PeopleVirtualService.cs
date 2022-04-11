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
   public class Ef_PeopleVirtualService: DataAccessLayer.Repositories.RepositoryBase<PeopleVirtual>, IPeopleVirtualService
    {
        public Ef_PeopleVirtualService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddPeopleVirtual(PeopleVirtual peopleVirtual)
        {
            Create(peopleVirtual);
            //SaveChanges();
        }

        public IEnumerable<PeopleVirtual> GetPeopleVirtual()
        {
            return FindAll();
        }

        public IEnumerable<PeopleVirtual> GetPeopleVirtualById(int peopelVirtualId)
        {
            return FindByCondition(item => item.Id == peopelVirtualId);
        }

        public IQueryable<PeopleVirtual> GetPeopleVirtualByPeopleId(int contactId)
        {
            return FindByCondition(item => item.RelativeId == contactId && item.Type == 1 );
        }
        public IQueryable<PeopleVirtual> GetPeopleVirtualByContactId(int peopleId)
        {
            return FindByCondition(item => item.RelativeId == peopleId && item.Type == 1);
        }

        public void UpdatePeopleVirtual(PeopleVirtual peopleVirtual)
        {
            Update(peopleVirtual);
            //SaveChanges();
        }

        public void DeleteContact(PeopleVirtual contact)
        {
            Delete(contact);
        }
    }
}
