using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_TelPhoneService : DataAccessLayer.Repositories.RepositoryBase<TelPhones>, ITelPhonesService
    {
        public Ef_TelPhoneService(CRM_CoreDB context) : base(context)
        {
        }

        public void AddPeople(TelPhones telPhones)
        {
            Create(telPhones);
        }

        public void AddTelPhones(List<TelPhoneType> telPhones, int relativeId, int type)
        {
            foreach (var item in telPhones)
            {
                TelPhones telPhone = new TelPhones();
                telPhone.Description = item.Description;
                telPhone.RelativeId = relativeId;
                telPhone.TBASPhoneTypesId = item.TBASTelTypeId;
                telPhone.Value = item.TelValue;
                telPhone.Type = type;
                Create(telPhone);
            }
        }

        public void DeleteTelPhones(List<TelPhones> telPhones)
        {
            foreach (var item in telPhones)
                Delete(item);
        }

        public IEnumerable<TelPhones> GetTelPhonesByType(int relativeId, int type)
        {
            return FindByCondition(item => item.RelativeId == relativeId && item.Type == type);
        }

        public bool IsRepeatedTelPhones(List<TelPhoneType> telPhones)
        {
            bool isRepeated = false;
            IQueryable<TelPhones> listTelPhones = FindAll();
            foreach (var item in telPhones)
            {
                foreach (var item2 in listTelPhones)
                {
                    if (item.TelValue == item2.Value)
                    {
                        isRepeated = true;
                        break;
                    }
                }
            }

            return isRepeated;
        }
    }
}
