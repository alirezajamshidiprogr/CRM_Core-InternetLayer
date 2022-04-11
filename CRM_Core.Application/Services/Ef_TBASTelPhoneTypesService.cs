using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_TBASTelPhoneTypesService : DataAccessLayer.Repositories.RepositoryBase<TBASPhoneTypes>, ITBASTelPhoneTypesService
    {
        public Ef_TBASTelPhoneTypesService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<TBASPhoneTypes> GetTelPhoneTypes()
        {
            return FindAll();
        }
    }
}
