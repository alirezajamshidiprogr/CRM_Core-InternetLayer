using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_TBASServicesService: DataAccessLayer.Repositories.RepositoryBase<TBASServices> , ITBASServiceService
    {
        public Ef_TBASServicesService(CRM_CoreDB context):base(context)
        {

        }

        public IEnumerable<TBASServices> GetAllServices()
        {
            return FindAll();
        }
    }
}
