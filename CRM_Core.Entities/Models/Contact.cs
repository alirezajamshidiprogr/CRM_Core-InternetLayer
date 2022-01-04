using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Entities.Models
{
   public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Emails { get; set; }
        public string FatherName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string WorkTel { get; set; }

    }
}
