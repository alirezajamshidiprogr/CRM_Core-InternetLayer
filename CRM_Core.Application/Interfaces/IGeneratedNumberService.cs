using CRM_Core.Entities.Models.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface IGeneratedNumberService
    {
        string NewGenerateNumber(string id ,int state);
        void InsertNumberInActivity(ActivityNumber activityNumber); 
        void DeleteNumberFromActivety(string activityNumber , int stateId); 
    }
}
