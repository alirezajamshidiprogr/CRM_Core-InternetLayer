using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ReminderDayDetailsService : DataAccessLayer.Repositories.RepositoryBase<ReminderDayDetails>, IReminderDayDetailsService
    {
        public Ef_ReminderDayDetailsService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddReminderDayDetails(ReminderDayDetails reminderDayDetails)
        {
            Create(reminderDayDetails);
        }

        public void DeleteReminderDayDetails(ReminderDayDetails reminderDayDetails)
        {
           Delete(reminderDayDetails);
        }

        public IQueryable<ReminderDayDetails> GetReminderDayDetailsByReminderId(int reminderId)
        {
           return FindByCondition(item=>item.ReminderId == reminderId);
        }

        public void UpdateReminderDayDetails(ReminderDayDetails reminderDayDetails)
        {
            Update(reminderDayDetails);
        }
    }
}
