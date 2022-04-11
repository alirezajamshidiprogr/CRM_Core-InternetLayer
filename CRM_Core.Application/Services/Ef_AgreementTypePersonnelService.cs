using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_AgreementTypePersonnelService : DataAccessLayer.Repositories.RepositoryBase<TBASAgreementType>, ITBASAgreementTypePersonnel
    {
        public Ef_AgreementTypePersonnelService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<TBASAgreementType> GetAllAgreements()
        {
            return FindAll();
        }
    }
}