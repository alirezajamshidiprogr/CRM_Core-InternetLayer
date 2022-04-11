using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_Core.DomainLayer
{
    public class People
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string ManualCode { get; set; }
        [MaxLength(15)]
        public string SystemCode { get; set; }
        [MaxLength(150)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(11)]
        public string CertificateCode { get; set; }
        [MaxLength(200)]
        public string Job { get; set; }
        [MaxLength(11)]
        public string P_Birthday { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "Date")]
        public DateTime? M_Birthday { get; set; }
        [MaxLength(10)]
        public string P_MariedDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? M_MariedDate { get; set; }
        public DateTime M_InsertDate { get; set; }
        public DateTime? M_EditDate { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        public int? MarriedType{ get; set; }

        [ForeignKey("People")]
        public int? IntroduceId { get; set; }
        public People Introduce { get; set; }

        [Required]
        public int TBASCategoryId { get; set; }
        public  TBASCategory TBASCategory { get; set; }
        public Nullable<int> TBASPotentialId { get; set; }
        public TBASPotential TBASPotential { get; set; }
        public Nullable<int> TBASPrefixId { get; set; }
        public TBASPrefix TBASPrefix { get; set; }
        public Nullable<int> TBASGraduationId { get; set; }
        public TBASGraduation TBASGraduation { get; set; } 
        public int? TBASIntroductionTypeId { get; set; }
        public TBASIntroductionType TBASIntroductionType { get; set; }
        [NotMapped]
        public Address Address { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

    public class PeopleVirtual
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string WebSite { get; set; }
        [MaxLength(200)]
        public string Telegram { get; set; }
        [MaxLength(200)]  
        public string YouTube { get; set; }
        [MaxLength(200)]
        public string WhatsApp { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string Instagram { get; set; }
        public int RelativeId { get; set; }
        public int Type { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Province { get; set; }
        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Area { get; set; }
        [MaxLength(400)]
        public string Street { get; set; }
        [MaxLength(400)]
        public string Alley { get; set; }
        public string OtherAddress { get; set; }

        //[ForeignKey("PeopleId")]
        //[Required]
        //public People People { get; set; }
        public int RelativeId { get; set; }
        public int Type { get; set; }
        [NotMapped]
        public ICollection<People> Student { get; set; }

    }

    public class TBASGraduation
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }

    public class TBASCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }

    public class TBASPrefix
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }

    public class TBASPotential
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }

    public class PeopleProperty
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("TBASPeopleTypeFieldId")]
        public int TBASPeopleTypeField { get; set; }
        [Required]
        [MaxLength(500)]
        public string Value { get; set; }
        [Required]
        public int Order { get; set; }

        [ForeignKey("PeopleId")]
        [Required]
        public People People { get; set; }
    }

    public class TBASPeopleTypeField
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
    }

    public class TBASIntroductionType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name{ get; set; }

    }

}
