using CRM_Core.Application.Interfaces;
using CRM_Core.DomainLayer;
using CRM_Core.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CRM_Core.DataAccessLayer;
using CRM_Core.Application.ViewModels.People;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using CRM_Core.Infrastructure;
using System.Data.Common;

namespace CRM_Core.Application.Services
{
   public class Ef_PeopleService : DataAccessLayer.Repositories.RepositoryBase<People>, IPeopleService
    {
        public CRM_CoreDB _context;
        public Ef_PeopleService(CRM_CoreDB context): base(context)
        {
            _context = context;
        }

        public void AddPeople(People people)
        {
             Create(people);
        }

        public void DeletePeople(People people)
        {
            Delete(people);
        }

        public IEnumerable<People> GetPeople()
        {
            return FindAll();
        }

        //public IEnumerable<PeopleViewModel> GetPeopleByADO()
        //{
        //    return GetModelByADO();
        //}

        public IEnumerable<PeopleModel> GetPeopleByADO(string commandText , string[] searchParameter , object[] searchValues, bool isProcedure )
        {
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            DbParameter[] param = new DbParameter[searchParameter == null ? 0 : searchParameter.Length] ;
            cmd.CommandText = commandText;
            cmd.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;

            if ((searchParameter != null  && searchValues != null) &&  (searchParameter.Length == searchValues.Length && searchParameter.Length > 0 ))
            {
                DbParameter setParam ;
                for (int i = 0; i < searchParameter.Length; i++)
                {
                    setParam = cmd.CreateParameter();
                    var getTypeSearchValue = searchValues[i]== null ? "String" : searchValues[i].GetType().Name;

                    setParam.ParameterName = searchParameter[i];
                    if (getTypeSearchValue == "String")
                        setParam.Value = searchValues[i] == null ? null : searchValues[i].ToString() ;
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
            return (MappingUtility333.DataTableToList<PeopleModel>(dt)).AsQueryable();
        }

        public IEnumerable<PeopleModel> GetPeopleByAdoById(int peopleId)
        {
            DataTable dt = new DataTable();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[People_GetPropertyById]";
            DbParameter param = cmd.CreateParameter();

            param.ParameterName = "@PeopleId";
            param.Value = peopleId;

            cmd.Parameters.Add(param);
            cmd.CommandType = CommandType.StoredProcedure;
            _context.Database.OpenConnection();
            var dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            return (MappingUtility333.DataTableToList<PeopleModel>(dt)).AsQueryable();

        }

        public IEnumerable<People> GetPeopleByCategoryId(int categoryId)
        {
            return FindByCondition(item => item.TBASCategoryId == categoryId);
        }

        public IQueryable<People> GetPeopleById(int id)
        {
            return FindByCondition(item => item.Id == id);
        }

        public IQueryable<People> GetPeopleByManualCode(string manualCode)
        {
            return FindByCondition(item => item.ManualCode == manualCode);
        }

        public void UpdatePeople(People people)
        {
            Update(people);
        }


        //public async Task Add(AttributGrp attributGrp)
        //{
        //    Create(attributGrp);
        //    await SaveAsync();
        //}

        //public async Task Remove(AttributGrp attributGrp)
        //{
        //    Delete(attributGrp);
        //    await SaveAsync();
        //}

        //public async Task Edit(AttributGrp attributGrp)
        //{
        //    Update(attributGrp);
        //    await SaveAsync();
        //}

        //public async Task<IEnumerable<AttributGrp>> GetAll() =>
        //     await FindAll().ToListAsync();

        //public async Task<DataGridViewModel<AttributGrp>> GetBySearch(int page, int pageSize, string searchString)
        //{
        //    var DataGridView = new DataGridViewModel<AttributGrp>
        //    {
        //        Records = await FindByCondition(x => x.Name.Contains(searchString)).Include(x => x.ProductGroup)
        //        .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),

        //        TotalCount = await FindByCondition(x => x.Name.Contains(searchString)).Include(x => x.ProductGroup)
        //        .Skip((page - 1) * pageSize).Take(pageSize).CountAsync(),
        //    };

        //    return DataGridView;
        //}

        //public async Task<AttributGrp> GetById(int? id) =>
        //    await FindByCondition(x => x.AttributGrpId.Equals(id)).Include(x => x.ProductGroup)
        //    .DefaultIfEmpty(new AttributGrp()).SingleAsync();

        //public async Task<bool> AttributeGrpExistence(int? id) =>
        //    await FindByCondition(x => x.AttributGrpId.Equals(id)).AnyAsync();

        //public async Task<IList<AttributGrp>> GetByProductGroupId(int? productGroupId) =>
        //   await FindByCondition(x => x.ProductGroupId == productGroupId)
        //    .Include(x => x.AttributItem)
        //       .ThenInclude(x => x.Product_Attribut).ToListAsync();
    }
}
