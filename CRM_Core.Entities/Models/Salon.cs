using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CRM_Core.Entities.Models.Salon
{
    public class SalonInfo
    {
        public int Id { get; set; }
        public string SalonName { get; set; }
        public string Manager { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Telegram{ get; set; }
        public string WhatsApp{ get; set; }
        public string Instagram{ get; set; }
        public string Website{ get; set; }
        public string LisenceNumber{ get; set; }
        public string Description{ get; set; }
        public string Address{ get; set; }
        public string RegisterDate { get; set; }
        public string EditDate { get; set; }

        //[BindProperty]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }

    public class SalonCosts
    {
        public int Id { get; set; }
        public string CostName{ get; set; }
        [Required]
        public double Price { get; set; }
        public string F_RegisterDate { get; set; }
        public string F_EditDate { get; set; }
        public DateTime M_RegisterDate { get; set; }
        public DateTime? M_EditDate { get; set; }
        [Required]
        public string F_CostDate { get; set; }
        public DateTime? M_CostDate { get; set; }
        public string Description { get; set; }
        public int? RelatinveId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("TBASSalonCosts")]
        public int? TBASSalonCostId { get; set; }
        public TBASSalonCosts TBASSalonCost { get; set; }
    }

    public class BillCosts
    {
        public int Id { get; set; }
        [Required]
        public string BillIdentity { get; set; }
        [Required]
        public string PayIdentity { get; set; }
        public int BillType { get; set; }  // گاز ، آب ، برق 

    }

    public class TransferCosts
    {
        public int Id { get; set; }
        [Required]
        public string FromTarget  { get; set; }
        [Required]
        public string ToDestination  { get; set; }
        public int TransferType  { get; set; } // snap , taxi , ...
    }

    public class TBASSalonCosts
    {
        public int Id { get; set; }
        public string CostName { get; set; }
        public string CostTitle { get; set; }
        [ForeignKey("TBASSalonCosts")]
        public int? TBASSalonCostId { get; set; }
        public TBASSalonCosts TBASSalonCost { get; set; }
    }
}
