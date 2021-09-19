using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservation();
        Reservation GetLastReservationId();
        void UpdateReservation(Reservation reservation);
        void insertReservation(Reservation reservation);
        void SaveChanges();
        IEnumerable<ReservationViewModel> GetReservationByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);

    }
}
