using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CRM_Core.Application.ViewModels.People
{
    public class PeopleModel
    {
        //public IEnumerable<CRM_Core.DomainLayer.People> People { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ManualCode { get; set; }
        public string SystemCode { get; set; }
        public string CertificateCode { get; set; }
        public string P_Birthday { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public int? Age { get; set; }
        public int? MarriedType { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string TBASCategoryName { get; set; }
        public string TBASGraduationName { get; set; }
        public string TBASPotentialName { get; set; }
        public string TBASIntroduceName { get; set; }
    }

    public class peopleViewModel
    {
        public CRM_Core.DomainLayer.People People { get; set; }
        public IEnumerable<CRM_Core.DomainLayer.People> PeopleItems { get; set; }
        public IEnumerable<Address> AddressItems { get; set; }
        public IEnumerable<TBASPrefix> PrefixItems { get; set; }
        public IEnumerable<TBASCategory> CategoriyItem { get; set; }
        public IEnumerable<PeopleProperty> PeoplePropertyItems { get; set; }
        public IEnumerable<TBASPotential> PotentialItems { get; set; }
        public IEnumerable<TBASGraduation> GraduationItems { get; set; }
        public IEnumerable<TBASIntroductionType> TBASIntroductionTypeItems { get; set; }
        public Address Address { get; set; }
        public PeopleVirtual PeopleVirtual { get; set; }
        public TBASGraduation TBASGraduation { get; set; }
        public TBASPotential TBASPotential { get; set; }
        public TBASIntroductionType IntroductionType { get; set; }
        public TBASCategory TBASCategory { get; set; }
        public TBASPrefix TBASPrefix { get; set; }
        public TBASIntroductionType TBASIntroductionType { get; set; }
    }
}
