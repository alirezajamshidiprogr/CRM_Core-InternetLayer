using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface ITelPhonesService
    {
        void AddTelPhones(List<TelPhoneType> telPhones,int relativeId,int type);
        void DeleteTelPhones(List<TelPhones> telPhones);
        IEnumerable<TelPhones> GetTelPhonesByType(int relativeId , int type);

        bool IsRepeatedTelPhones(List<TelPhoneType> telPhones); 
    }
}

