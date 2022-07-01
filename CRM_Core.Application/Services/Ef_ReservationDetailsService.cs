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
        private IChequService _chequeService;

        public Ef_ReservationDetailsService(CRM_CoreDB context, IChequService chequeService) : base(context)
        {
            _chequeService = chequeService;
        }

        public void DeleteReservationDetails(List<ReservationDetails> reservationDetailList)
        {
            foreach (var reservationDetail in reservationDetailList)
                Delete(reservationDetail);
        }

        public IEnumerable<ReservationDetails> GetReservationDetailsByReservationId(int reservatinoId)
        {
            return FindByCondition(item=>item.ReservationId == reservatinoId);
        }

        public bool HasAnyRecordHistory(Reservation reservation)
        {
            bool hasAnyRecordHistory = false;
            return hasAnyRecordHistory;
        }

        public void InsertReservationDetails(ReservationDetails reservationDetail)
        {
            Create(reservationDetail);
        }
    }
}
