using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI_Presentation.Models
{
    public class PeopleViewModeldelete
    {
        public People People { get; set; }
        public IEnumerable<People> PeopleItems { get; set; }
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
        public  TBASPotential TBASPotential { get; set; }
        public TBASIntroductionType IntroductionType { get; set; }
        public TBASCategory TBASCategory { get; set; }
        public TBASPrefix TBASPrefix { get; set; }
        public TBASIntroductionType TBASIntroductionType { get; set; }
    }
}
