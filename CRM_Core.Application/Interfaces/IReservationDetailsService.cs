using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IReservationDetailsService
    {
        IEnumerable<ReservationDetails> GetReservationDetailsByReservationId(int reservationId);
        public void InsertReservationDetails(ReservationDetails reservationDetail);
        public void DeleteReservationDetails(List<ReservationDetails> reservationDetailList);
        public bool HasAnyRecordHistory(Reservation reservation);
     

    }
}
