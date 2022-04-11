using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class ContactViewModel 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string FatherName { get; set; }
        public string Job { get; set; }
        public string P_BirthDay { get; set; }
        public string Description { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string Alley { get; set; }
        public string OtherAddress { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string WhatsApp { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string YouTube { get; set; }
        public string WebSite { get; set; }
    }

    public class TelPhoneType
    {
        public int TBASTelTypeId { get; set; }
        public string TelValue { get; set; }
        public string Description { get; set; }
    }

    public class ContactViewModelSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
    }
}
