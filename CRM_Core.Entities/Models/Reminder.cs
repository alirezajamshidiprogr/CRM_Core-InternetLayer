using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM_Core.Entities.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        [Required]
        public string ReminderTitle { get; set; }
        public string F_ReminderDate { get; set; }
        public DateTime? M_ReminderDate { get; set; }
        public string F_EditDate { get; set; }
        public DateTime? M_EditDate { get; set; }
        [Required]
        public string Time { get; set; }
        public string Description { get; set; }
        public bool IsRepeatReminder { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("People")]
        public int? ToPersonelId { get; set; }
        public People ToPersonel { get; set; }
       
    }

    public class ReminderDayDetails
    {
        public int Id { get; set; }
        public int Day { get; set; }
        [ForeignKey("Reminder")]
        public int? ReminderId { get; set; }
        public Reminder Reminder { get; set; }
    }
}
