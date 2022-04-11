using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ChequService : DataAccessLayer.Repositories.RepositoryBase<Cheque>, IChequService
    {
        public Ef_ChequService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<Cheque> getAllCheque()
        {
            return FindAll();
        }

        public IEnumerable<Cheque> GetServiceCustomerByReservationId()
        {
            throw new NotImplementedException();
        }
    }
}
