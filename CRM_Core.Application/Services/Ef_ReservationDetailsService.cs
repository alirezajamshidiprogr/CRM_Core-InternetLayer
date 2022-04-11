using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ReservationDetailsService : DataAccessLayer.Repositories.RepositoryBase<ReservationDetails>, IReservationDetailsService
    {
        public Ef_ReservationDetailsService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<ReservationDetails> GetReservationDetailsByReservationId(int reservatinoId)
        {
            return FindByCondition(item=>item.ReservationId == reservatinoId);
        }
    }
}
