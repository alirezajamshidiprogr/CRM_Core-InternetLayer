using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeopleVirtualService
    {
        IEnumerable<PeopleVirtual> GetPeopleVirtual();
        IEnumerable<PeopleVirtual> GetPeopleVirtualById(int peopelVirtualId);
        IQueryable<PeopleVirtual> GetPeopleVirtualByPeopleId(int peopleId);
        void UpdatePeopleVirtual(PeopleVirtual peopleVirtual);
        void AddPeopleVirtual(PeopleVirtual peopleVirtual);
    }
}
