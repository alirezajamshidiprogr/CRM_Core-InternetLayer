using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Models
{
   public class Contact
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string FirstName { get; set; }
        [MaxLength(150)]
        public string LastName { get; set; }
        [MaxLength(13)]
        public string Tel { get; set; }
        [MaxLength(13)]
        public string Mobile { get; set; }
        [MaxLength(100)]
        public string Emails { get; set; }
        [MaxLength(100)]
        public string FatherName { get; set; }  
        [MaxLength(100)]
        public string Job { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime M_ContactInsertDate { get; set; }
        public DateTime? M_ContactEditDate { get; set; }
        [MaxLength(10)]
        public string P_BirthDay{ get; set; }
        public DateTime? M_BirthDay { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

    public class TBASPhoneTypes
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
    }

    public class TelPhones
    {
        public int Id { get; set; }
        [MaxLength(13)]
        public string Value { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public int RelativeId { get; set; }
        public int Type { get; set; }
        [ForeignKey("TBASPhoneTypes")]
        public int? TBASPhoneTypesId { get; set; }
        public TBASPhoneTypes TBASPhoneTypes { get; set; }
    }
}
