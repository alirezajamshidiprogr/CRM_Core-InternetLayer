using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IReservationDetailsService
    {
        IEnumerable<ReservationDetails> GetReservationDetailsByReservationId(int reservationId);
    }
}
