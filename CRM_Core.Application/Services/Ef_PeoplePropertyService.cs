using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PeoplePropertyService : RepositoryBase<PeopleProperty>, IPeoplePropertyService
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

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyMobiles(People people, int mobile, int mobileComment)
        {
            return FindByCondition(item => item.People.Equals(people) && (item.TBASPeopleTypeField.Equals(mobile) || item.TBASPeopleTypeField.Equals(mobileComment)));
        }

        public IEnumerable<PeopleProperty> GetPeoplePrpoertyTels(People people, int code, int number, int telCommetn)
        {
            return FindByCondition(item => item.People.Equals(people) && (item.TBASPeopleTypeField.Equals(code) || item.TBASPeopleTypeField.Equals(number) || item.TBASPeopleTypeField.Equals(telCommetn)));
        }
    }
}
