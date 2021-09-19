using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Models.General
{
   public class TBASMenu
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public int Order { get; set; }
        public string Event { get; set; }
        public bool  Active { get; set; }
        public bool Visible { get; set; }
        public bool  IsButton { get; set; }
        public string IconClass { get; set; }
    }

    public class UserMenu
    {
        public int Id { get; set; }
        public int TBASMenuId { get; set; }
        public string IdentityUserId{ get; set; }
        [NotMapped]
        public string FStartDate { get; set; }
        [NotMapped]
        public string FEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Description("با این استیت مشخص میکنیم منورا نشان بده یا مخفی کنه ، نشان دادن با 0 و مخفی کردن با 1")]
        public int MenueState { get; set; }
    }
}
