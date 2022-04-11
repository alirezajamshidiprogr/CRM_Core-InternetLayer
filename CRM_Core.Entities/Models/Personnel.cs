using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Models
{
    public class Personnel
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string PersonnelName { get; set; }
        [Required]
        [MaxLength(150)]
        public string PersonnelLastName { get; set; }
        [MaxLength(150)]
        public string PersonnelFatherName { get; set; }
        [MaxLength(10)]
        public string InsuranceNumber { get; set; }

        [MaxLength(13)]
        [Required]
        public string Mobile { get; set; }
        [MaxLength(13)]
        public string Tel { get; set; }
        [Required]
        [Description("نوع قرارداد ")]
        [ForeignKey("AgreementType")]
        public int? TBASAgreementTypeId { get; set; }
        public TBASAgreementType AgreementType { get; set; }

        [MaxLength(11)]
        public string CertificateCode { get; set; }
        [MaxLength(13)]
        public string HomeTel { get; set; }
        public string P_Birthday { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "Date")]
        public DateTime? M_Birthday { get; set; }
        [MaxLength(10)]
        public DateTime M_InsertDate { get; set; }
        public DateTime? M_EditDate { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        PersonnelSkils personnelSkills = new PersonnelSkils();
        public ICollection<PersonnelSkils> PersonnelSkils { get; set; }
    }

    public class PersonnelSkils
    {
        public int Id { get; set; }
        [ForeignKey("Personnel")]
        [Required]
        public int? PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        [ForeignKey("TBASServices")]
        [Required]
        public int? TBASServicesId { get; set; }
        public TBASServices TBASServices { get; set; }
        [Required]
        public int? Level { get; set; }

    }

    public class PersonnelWorkTime
    {
        public int Id { get; set; }
        [ForeignKey("Personnel")]
        [Required]
        public int? PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan FromTime { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan ToTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string P_WorkTimeDate { get; set; }
        [Required]
        public DateTime M_WorkTimeDate { get; set; }
        public DateTime M_InsertDate { get; set; }
        public DateTime M_EditDate { get; set; }

    }

    public class PersonnelVation
    {
        public int Id { get; set; }
        [ForeignKey("Personnel")]
        [Required]
        public int? PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan FromTime { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan ToTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string P_VacationDate { get; set; }
        [Required]
        public DateTime M_VacationDate { get; set; }
        [Required]
        [Description("صفر ساعنی و یک کامل ")]
        public int VacationType { get; set; }
        public DateTime M_InsertDate { get; set; }
        public DateTime M_EditDate { get; set; }
    }
    public class TBASAgreementType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
