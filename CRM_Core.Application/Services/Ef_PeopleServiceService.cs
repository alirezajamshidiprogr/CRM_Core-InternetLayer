using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PeopleServiceService : DataAccessLayer.Repositories.RepositoryBase<ReservationDetails> , IPeopleServiceService
    {
        IReservationService _reservationService;
        public Ef_PeopleServiceService(CRM_CoreDB context, IReservationService reservationService) :base(context)
        {
            _reservationService = reservationService;
        }

        public IEnumerable<PeopleServiceReservationViewModel> getPeopleServiceByClerkId(int clerkId)
        {
            var peopleService = FindByCondition(item => item.ClerkServicesId == clerkId);
            var reservation = _reservationService.GetAllReservation() ;
            return null;
        }

        public IEnumerable<ReservationDetails> getPeopleServiceByReservationId(int reservationId)
        {
            return FindByCondition(item => item.ReservationId == reservationId);
        }

        public void insertPeopleService(ReservationDetails reservationDetails)
        {
              Create(reservationDetails);
        }

        public void removePeopleServiceByReservationId(ReservationDetails reservationDetails)
        {
             Delete(reservationDetails);
        }

        public void removePeopleServiceByReservationId(List<ReservationDetails> reservationDetailsList)
        {
            throw new NotImplementedException();
        }
    }
}
