using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservation();
        IEnumerable<Reservation> GetReservationById(int reservationId);
        Reservation GetReservationByNumber(string number);

        IEnumerable<Reservation> GetReservationByParam(int customerId, DateTime reservationDate);
        Reservation GetLastReservationId();
        void UpdateReservation(Reservation reservation);
        void InsertReservation(Reservation reservation);
        void SaveChanges();
        bool CheckReservationHasAnyHistoryRecord(int reservationId);
        DataSet GetReservationByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true);
        PeopleReservationHistoryInfo GetPeopleHistoryReservation(int peopleId);
        DataSet GetReservationDetailsADO_ByID(string commandText,string[] searchParameter, object[] searchValues, bool isProcedure = true);

    }
}
