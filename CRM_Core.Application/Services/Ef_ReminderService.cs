using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM_Core.Application.Services
{
  public class Ef_ReminderService : DataAccessLayer.Repositories.RepositoryBase<Reminder>, IReminderService
    {
        public CRM_CoreDB _context;
        public Ef_ReminderService(CRM_CoreDB context) : base(context)
        {
            _context = context;
        }

        public void AddReminder(Reminder reminder)
        {
            Create(reminder);
        }

        public void DeleteReminder(Reminder reminder)
        {
            Delete(reminder);
        }

        public IEnumerable<ReminderViewModelSearch> GetCurrentReminderDay(string commandText)
        {
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter[] param = new DbParameter[1];
            DbParameter setParam;
            setParam = cmd.CreateParameter();
            setParam.ParameterName = "@RemiderStateNumber";
            param[0] = setParam;
            param[0].Value = 0;
            cmd.Parameters.AddRange(param);
            _context.Database.OpenConnection();
            var dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            return (MappingUtility.DataTableToList<ReminderViewModelSearch>(dt)).AsQueryable();
        }

        public Reminder GetLasReminder()
        {
            return FindAll().OrderBy(item=>item.Id).Last();
        }

        public IQueryable<Reminder> GetReminderById(int id)
        {
          return FindByCondition(item=>item.Id == id);
        }

        public IEnumerable<ReminderViewModelSearch> GetRemindersByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true)
        {
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            DbParameter[] param = new DbParameter[searchParameter == null ? 0 : searchParameter.Length];
            cmd.CommandText = commandText;
            cmd.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;

            if ((searchParameter != null && searchValues != null) && (searchParameter.Length == searchValues.Length && searchParameter.Length > 0))
            {
                DbParameter setParam;
                for (int i = 0; i < searchParameter.Length; i++)
                {
                    setParam = cmd.CreateParameter();
                    var getTypeSearchValue = searchValues[i] == null ? "String" : searchValues[i].GetType().Name;

                    setParam.ParameterName = searchParameter[i];
                    if (getTypeSearchValue == "String")
                        setParam.Value = searchValues[i] == null ? null : searchValues[i].ToString();
                    else if (getTypeSearchValue == "Int32")
                        setParam.Value = Convert.ToInt32(searchValues[i]);
                    else if (getTypeSearchValue == "Boolean")
                        setParam.Value = Convert.ToBoolean(searchValues[i]);
                    else if (getTypeSearchValue == "Double")
                        setParam.Value = Convert.ToBoolean(searchValues[i]);
                    else if (getTypeSearchValue == "DateTime")
                        setParam.Value = Convert.ToDateTime(searchValues[i]);

                    param[i] = setParam;
                    param[i].Value = searchValues[i];
                }
            }
            cmd.Parameters.AddRange(param);
            _context.Database.OpenConnection();
            var dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            return (MappingUtility.DataTableToList<ReminderViewModelSearch>(dt)).AsQueryable();

        }

        public void UpdateReminder(Reminder reminder)
        {
            Update(reminder);
        }
    }
}
