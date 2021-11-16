using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class ReminderViewModel : Reminder
    {
        public bool Satuarday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Thursday { get; set; }
        public bool Wensday { get; set; }
        public bool Tuesday { get; set; }
        public bool Friday { get; set; }
    }
    public class DayWeek
    {
        public int Day { get; set; }

    }
    public class ReminderViewModelSearch 
    {
        public int Id { get; set; }
        public string ReminderTitle { get; set; }
        public string ReminderDate { get; set; }
        public string Time { get; set; }
        public string Personnel { get; set; }
        public string DayName { get; set; }
        public string DayNameEn { get; set; }
        public string RemiderState { get; set; }
        public int RemiderStateNumber { get; set; }
        public bool IsRepeatableReminder { get; set; }
    }

}
