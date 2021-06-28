using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_GraduaionService : RepositoryBase<TBASGraduation>, IGraduationService
    {
        public Ef_GraduaionService(CRM_CoreDB context) : base(context)
        {

        }
        public IEnumerable<TBASGraduation> GetGraduations()
        {
            return FindAll();
        }
    }
}
