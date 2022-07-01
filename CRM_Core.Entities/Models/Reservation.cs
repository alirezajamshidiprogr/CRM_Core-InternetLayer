using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Reservation
{
   public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public People People { get; set; }
        [Column("CustomerId")]
        public int PeopleId { get; set; }
        [Required]
        [MaxLength(15)]
        public string SystemCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string P_ReservationDate { get; set; }
        [Required]
        public DateTime M_ReservationDate { get; set; }
        [Required]
        public DateTime M_ReservationInsertDate { get; set; }
        public DateTime? M_ReservationEditDate { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public virtual ICollection<ReservationDetails> ReservationDetails { get; set; }
    }

    public class ReservationDetails
    {
        public int Id { get; set; }

        [Required]
        public ClerkServices ClerkServices { get; set; }
        public int? ClerkServicesId { get; set; }

        [Required]
        public bool isSalonCustomer { get; set; }

        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan FromTime { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan ToTime { get; set; }

        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }

    public class TBASPayType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }

    public class TBASServices
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class ClerkServices
    {
        public int Id { get; set; }

        [Required]
        public TBASServices TBASServices { get; set; }
        public int TBASServicesId { get; set; }

        [Required]
        public People People { get; set; }
        public int PeopleId { get; set; }
        [Required]
        public int Salary { get; set; }
        public int PersonnelPortionPercentage { get; set; }

        [Required]
        public bool Acitve { get; set; }
    }


}
