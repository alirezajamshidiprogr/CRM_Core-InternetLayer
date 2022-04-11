using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Models.General;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace CRM_Core.Application.Services
{
    public class Ef_GeneralService : DataAccessLayer.Repositories.RepositoryBase<ActivityNumber>, IGeneratedNumberService
    {
        public CRM_CoreDB _context;
        public Ef_GeneralService(CRM_CoreDB context) : base(context)
        {
            _context = context;
        }

        public void DeleteNumberFromActivety(string activityNumber, int stateId)
        {
            ActivityNumber getActivityNumber = FindByCondition(item => item.RelatedNumber == activityNumber && item.TBASStateId == stateId).LastOrDefaultAsync().Result;
            Delete(getActivityNumber);
        }

        public void InsertNumberInActivity(ActivityNumber activityNumber)
        {
            Create(activityNumber);
        }

        public string NewGenerateNumber(string id, int state)
        {
            string generatedNumber = string.Empty;
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[GenerateNumber]";
            DbParameter[] param = new DbParameter[2];
            DbParameter idParameter = cmd.CreateParameter();
            DbParameter stateParameter = cmd.CreateParameter();

            idParameter.ParameterName = "@UserIdentityID";
            idParameter.Value = id;
            param[0] = idParameter;

            stateParameter.ParameterName = "@State";
            stateParameter.Value = state;
            param[1] = stateParameter;

            cmd.Parameters.AddRange(param);
            cmd.CommandType = CommandType.StoredProcedure;
            _context.Database.OpenConnection();
            var dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            generatedNumber =  dt.Rows[0][0].ToString() ;
            return generatedNumber;
        }
    }
}
