using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface IReminderDayDetailsService
    {
        void AddReminderDayDetails(ReminderDayDetails reminderDayDetails);
        void DeleteReminderDayDetails(ReminderDayDetails reminderDayDetails);
        void UpdateReminderDayDetails(ReminderDayDetails reminderDayDetails);
        IQueryable<ReminderDayDetails> GetReminderDayDetailsByReminderId(int reminderId);
    }
}
