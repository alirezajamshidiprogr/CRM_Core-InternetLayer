using CRM_Core.DomainLayer;
using CRM_Core.Entities.Models;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CRM_Core.Application.ViewModels.Personnel
{
    public class PersonnelViewModel 
    {
        public int? Id { get; set; }
        public string PersonnelName { get; set; }
        public string PersonnelLastName { get; set; }
        public string PersonnelFatherName { get; set; }
        public string InsuranceNumber { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public int? TBASAgreementTypeId { get; set; }
        public string CertificateCode { get; set; }
        public string HomeTel { get; set; }
        public string P_Birthday { get; set; }
        public string Description { get; set; }
        public List<PersonnelSkilsViewModel> personnelSkilsViewModel;
    }


    public class PersonnelViewModelSearch
    {
     

    }

    public class PersonnelSkilsViewModel 
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Level { get; set; }
    }

}
