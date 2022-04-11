using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
   public class Ef_PersonnelSkilsService : DataAccessLayer.Repositories.RepositoryBase<PersonnelSkils>, IPersonnelSkillsService
    {
        public Ef_PersonnelSkilsService(CRM_CoreDB context) : base(context)
        {

        }

        public void AddPersonnelSkills(PersonnelSkils personneSkills)
        {
            throw new NotImplementedException();
        }

        public void AddPersonnelSkils(PersonnelSkils personneSkills)
        {
            Create(personneSkills);
        }

        public void DeletePersonnelSkills(PersonnelSkils personneSkills)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonnelSkils(PersonnelSkils personneSkills)
        {
            Delete(personneSkills);
        }

        public IEnumerable<PersonnelSkils> GetPersonnelSkillsByPersonnelId(int personnelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonnelSkils> GetPersonnelSkilsByPersonnelId(int personnelId)
        {
            return FindByCondition(item => item.PersonnelId == personnelId);
        }

        public void UpdatePersonnelSkills(PersonnelSkils personneSkills)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonnelSkils(PersonnelSkils personneSkills)
        {
            Update(personneSkills);
        }
    }
}
