using CRM_Core.DomainLayer;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Models
{
    public class Cheque
    {
        public int Id { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public CRM_Core.Entities.Reservation.Reservation Reservation { get; set; }
        
        [ForeignKey("TBASPayType")]
        public int TBASPayTypeId { get; set; }
        public TBASPayType TBASPayType { get; set; }

        [MaxLength(15)]
        public string SystemCode { get; set; }
        
        [Column(TypeName = "Date")]
        public DateTime M_RegisterDate { get; set; }
        [MaxLength(10)]
        public string P_RegisterDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? M_EditChequeDate { get; set; }

        [MaxLength(10)]
        public string P_EditChequeDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class ChequeDetails
    {
        public int Id { get; set; }

        [ForeignKey("Cheque")]
        public int ChequeId { get; set; }
        public Cheque Cheque { get; set; }
        public int Discount { get; set; }
        [Required]
        //[Column(TypeName = "decimal(18,2)")]
        public int TotalPriceCheque { get; set; }
        [Required]
        //[Column(TypeName = "decimal(18,2)")]
        public int FinalPriceCheque { get; set; }
    }
}
