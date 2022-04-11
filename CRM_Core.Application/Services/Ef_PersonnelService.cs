using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PersonnelService : DataAccessLayer.Repositories.RepositoryBase<Personnel>, IPersonnelService
    {
        public Ef_PersonnelService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddPersonnel(Personnel personnel)
        {
            Create(personnel);
        }

        public void DeletePersonnel(Personnel personnel)
        {
            Delete(personnel);
        }

        public IEnumerable<Personnel> GetPersonnelById(int id)
        {
            return FindByCondition(item => item.Id == id);
        }

        public void UpdatePersonnel(Personnel personnel)
        {
            Update(personnel);
        }
    }
}
