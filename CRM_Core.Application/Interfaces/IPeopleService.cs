using CRM_Core.Application.ViewModels.People;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeopleService
    {
        IEnumerable<People> GetPeople();
        IQueryable<People> GetPeopleById(int id);

        IEnumerable<People> GetPeopleByCategoryId(int categoryId);
        
        IQueryable<People> GetPeopleByManualCode(string manualCode);
        IEnumerable<PeopleModel> GetPeopleByAdoById(int peopleId);
        IEnumerable<PeopleModel> GetPeopleByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);
        void AddPeople(People people);

        void DeletePeople(People people);
        void UpdatePeople(People people);

        void SaveChanges();

        //Task<DataGridViewModel<AttributGrp>> GetBySearch(int page, int pageSize, string searchString);
        //Task<AttributGrp> GetById(int? id);
        //Task Add(AttributGrp attributGrp);
        //Task Edit(AttributGrp attributGrp);
        //Task Remove(AttributGrp attributGrp);
        //Task<IEnumerable<AttributGrp>> GetAll();
        //Task<IList<AttributGrp>> GetByProductGroupId(int? productGroupId);
        //Task<bool> AttributeGrpExistence(int? id);

    }
}
