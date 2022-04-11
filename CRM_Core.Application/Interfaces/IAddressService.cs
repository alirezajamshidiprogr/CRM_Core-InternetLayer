using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IAddressService
    {
        IEnumerable<Address> GetAddress();
        IEnumerable<Address> GetAddressById(int addressId);
        IQueryable<Address> GetAddressByPeopleId(int peopleId);
        IQueryable<Address> GetAddressByContactId(int contactId);
        void UpdateAddress(Address address);
        void AddAddress(Address address); 

    }
}
