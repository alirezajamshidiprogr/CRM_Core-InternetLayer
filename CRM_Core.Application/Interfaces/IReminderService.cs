using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IReminderService
    {
        void AddReminder(Reminder reminder);
        void DeleteReminder(Reminder reminder);
        void UpdateReminder(Reminder reminder);
        IQueryable<Reminder> GetReminderById(int id);
        Reminder GetLasReminder();
        void SaveChanges();
        IEnumerable<ReminderViewModelSearch> GetRemindersByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);

        IEnumerable<ReminderViewModelSearch> GetCurrentReminderDay(string commandText);
    }
}
