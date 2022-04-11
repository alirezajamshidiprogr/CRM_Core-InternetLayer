using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPersonnelSkillsService
    {
        void AddPersonnelSkills(PersonnelSkils personneSkills);
        void DeletePersonnelSkills(PersonnelSkils personneSkills);
        void UpdatePersonnelSkills(PersonnelSkils personneSkills);
        IEnumerable<PersonnelSkils> GetPersonnelSkillsByPersonnelId(int personnelId);
    }
}
