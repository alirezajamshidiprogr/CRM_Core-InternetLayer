using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeoplePropertyService
    {
        IEnumerable<PeopleProperty> GetPeoplePrpoertyTels(People people, int code , int number , int telCommetn);
        IEnumerable<PeopleProperty> GetPeoplePrpoertyMobiles(People people ,int mobile , int mobileComment);
        IEnumerable<PeopleProperty> GetPeoplePrpoertyByPhonenumber(int number);
        IEnumerable<PeopleProperty> GetPeoplePrpoertyByMobileNumber(int mobile);
        void DeletePeopleProperty(List<PeopleProperty> peopleProperty);
        void AddPeopleProperty(PeopleProperty peopleProperty);
    }
}
