using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeopleServiceService
    {
        IEnumerable<PeopleServices> getPeopleServiceByReservationId(int reservationId);
        void insertPeopleService(PeopleServices peopleServiceList);
        void removePeopleServiceByReservationId(PeopleServices peopleService);

        IEnumerable<PeopleServiceReservationViewModel> getPeopleServiceByClerkId(int clerkId);
    }
}
