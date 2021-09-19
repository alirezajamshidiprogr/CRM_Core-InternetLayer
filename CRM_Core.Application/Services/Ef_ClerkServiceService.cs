using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ClerkServiceService : RepositoryBase<ClerkServices>, IClerkServiceService
    {
        public Ef_ClerkServiceService(CRM_CoreDB context) :base(context)
        {

        }
        public IEnumerable<ClerkServices> GetAllSalonServices()
        {
            return FindAll();
        }

        public IEnumerable<ClerkServices> GetServicesByClerkId(int clerkId)
        {
            return FindByCondition(item => item.PeopleId == clerkId && item.Acitve == true );
        }
    }
}
