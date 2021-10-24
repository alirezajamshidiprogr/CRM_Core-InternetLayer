using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ReservationService : DataAccessLayer.Repositories.RepositoryBase<Reservation>,IReservationService
    {
        public CRM_CoreDB _context;
        public Ef_ReservationService(CRM_CoreDB context) :base(context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAllReservation()
        {
            return FindAll();
        }

        public Reservation GetLastReservationId()
        {
            return FindAll().LastOrDefault();
        }

        public IEnumerable<ReservationViewModel> GetReservationByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure)
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
            return (MappingUtility333.DataTableToList<ReservationViewModel>(dt)).AsQueryable();
        }

        public void insertReservation(Reservation reservation)
        {
            Create(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            Update(reservation);
        }
    }
}
