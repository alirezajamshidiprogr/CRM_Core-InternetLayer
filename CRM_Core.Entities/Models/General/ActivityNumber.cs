using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM_Core.Entities.Models.General
{
    public class ActivityNumber
    {
        public int Id { get; set; }
        [Required]
        public string RelatedNumber { get; set; }
        [Required]
        public int TBASStateId { get; set; }
        [Required]
        public TBASState TBASState { get; set; }
    }

    public class TBASState
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
