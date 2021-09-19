using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IPeopleServiceService
    {
        IEnumerable<PeopleServices> getPeopleServiceByReservationId(int reservationId);
        void insertPeopleService(PeopleServices peopleServiceList);
        void removePeopleServiceByReservationId(PeopleServices peopleService);
    }
}
