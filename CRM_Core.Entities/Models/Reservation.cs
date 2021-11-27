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
        public int PeopleId { get; set; }
        [Required]
        [MaxLength(10)]
        public string P_ReservationDate { get; set; }
        [Required]
        public DateTime M_ReservationDate { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan FromTime { get; set; }
        [Required]
        [Column(TypeName = "time(7)")]
        public TimeSpan ToTime { get; set; }
        [Required]
        public string SystemCode { get; set; }
        public double Price { get; set; }
        public TBASPayType TBASPayType { get; set; }
        public int TBASPayTypeId{ get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
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
        public bool Acitve { get; set; }
    }

    public class PeopleServices
    {
        public int Id { get; set; }

        [Required]
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }

        [Required]
        public ClerkServices ClerkServices { get; set; }
        public int ClerkServicesId{ get; set; }

        [Required]
        public bool isSalonCustomer { get; set; }
    }

}
