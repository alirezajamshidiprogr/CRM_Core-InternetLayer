using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models.Salon;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace CRM_Core.Application.Services
{
    public class Ef_SalonCostsService : DataAccessLayer.Repositories.RepositoryBase<SalonCosts>, ISalonCostsService
    {
        public CRM_CoreDB _context;
        private string _GetContext;
        public Ef_SalonCostsService(CRM_CoreDB context) : base(context)
        {
            _context = context;
            _GetContext = _connection;
        }

        public void AddGeneralSalonCosts(SalonCosts salonCost)
        {
            Create(salonCost);
        }

        public void DeleteSalonCost(SalonCosts salonCost)
        {
            Delete(salonCost);
        }

        public SalonCosts GetSalonCostById(int id)
        {
            return FindByCondition(item => item.Id == id).FirstOrDefault();
        }

        public IEnumerable<SalonCostsViewModelGrid> GetSalonCostsByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true)
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
            return (MappingUtility.DataTableToList<SalonCostsViewModelGrid>(dt)).AsQueryable();
        }
        public SalonCostsViewModel GetSalonCostsByADOSalonId(string commandText, int SalonCostId)
        {
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            var param = cmd.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = SalonCostId;
            cmd.Parameters.Add(param);
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure ;
            _context.Database.OpenConnection();
            var dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            return (MappingUtility.DataTableToList<SalonCostsViewModel>(dt)).First();
        }

        public DataSet GetSalonCostsDataTables(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true)
        {

            SqlConnection conn = new SqlConnection(_GetContext);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;
            //if ((searchParameter != null && searchValues != null) && (searchParameter.Length == searchValues.Length && searchParameter.Length > 0))
            //{
                for (int i = 0; i < searchParameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue(searchParameter[i], searchValues[i]);
                }
            //}
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            ///conn.Open();
            da.Fill(ds);
            ///conn.Close();
            ///
            return ds;
            //listSalonCostsViewModelGrid = (MappingUtility.DataTableToList<SalonCostsViewModelGrid>(ds.Tables[0]).AsQueryable());
        }

        public void UpdateSalonCost(SalonCosts salonCost)
        {
            Update(salonCost);
        }
    }
}
