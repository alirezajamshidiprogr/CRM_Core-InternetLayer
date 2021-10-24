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
    public class Ef_AddressService : DataAccessLayer.Repositories.RepositoryBase<Address>, IAddressService
    {
        public Ef_AddressService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddAddress(Address address)
        {
            Create(address);
            //SaveChanges();
        }

        public IEnumerable<Address> GetAddress()
        {
            return FindAll();
        }

        public IEnumerable<Address> GetAddressById(int addressId)
        {
            return FindByCondition(item=>item.Id ==  addressId);
        }

        public IQueryable<Address> GetAddressByPeopleId(int peopleId)
        {
            return FindByCondition(item => item.People.Id == peopleId);
        }

        public void UpdateAddress(Address address)
        {
            Update(address);
            //SaveChanges();
        }
    }
}
