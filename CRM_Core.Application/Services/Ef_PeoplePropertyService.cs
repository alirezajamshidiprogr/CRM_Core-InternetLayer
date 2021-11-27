using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PeoplePropertyService : DataAccessLayer.Repositories.RepositoryBase<PeopleProperty>, IPeoplePropertyService
    {
        public Ef_PeoplePropertyService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddPeopleProperty(PeopleProperty peopleProperty)
        {
            Create(peopleProperty);
            //SaveChanges();
        }

        public void DeletePeopleProperty(List<PeopleProperty> peopleProperty)
        {
            for (int i = 0; i < peopleProperty.Count; i++)
            {
                Delete(peopleProperty[i]);
            }
            //SaveChanges();
        }

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyByMobileNumber(int mobile)
        {
            return FindByCondition(item => item.TBASPeopleTypeField == mobile);
        }

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyByPhonenumber(int number)
        {
            return FindByCondition(item => item.TBASPeopleTypeField == number);
        }

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyMobiles(People people, int mobile)
        {
            return FindByCondition(item => item.People.Equals(people) && item.TBASPeopleTypeField.Equals(mobile));
        }

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyTels(People people, int number)
        {
            return FindByCondition(item => item.People.Equals(people) &&  item.TBASPeopleTypeField.Equals(number));
        }
    }
}
