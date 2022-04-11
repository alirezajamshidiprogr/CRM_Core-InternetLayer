using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPersonnelService
    {
        IEnumerable<Personnel> GetPersonnelById(int id);
        void AddPersonnel(Personnel personnel);

        void DeletePersonnel(Personnel personnel);
        void UpdatePersonnel(Personnel personnel);

        void SaveChanges();
    }
}
