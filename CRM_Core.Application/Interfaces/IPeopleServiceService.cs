using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeopleServiceService
    {
        IEnumerable<ReservationDetails> getPeopleServiceByReservationId(int reservationId);
        void insertPeopleService(ReservationDetails reservationDetailsList);
        void removePeopleServiceByReservationId(ReservationDetails reservationDetails);

        IEnumerable<PeopleServiceReservationViewModel> getPeopleServiceByClerkId(int clerkId);
    }
}
