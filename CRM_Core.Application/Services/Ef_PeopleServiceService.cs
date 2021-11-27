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
    public class Ef_PeopleServiceService : DataAccessLayer.Repositories.RepositoryBase<PeopleServices> , IPeopleServiceService
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

        public IEnumerable<PeopleServices> getPeopleServiceByReservationId(int reservationId)
        {
            return FindByCondition(item => item.ReservationId == reservationId);
        }

        public void insertPeopleService(PeopleServices peopleService)
        {
              Create(peopleService);
        }

        public void removePeopleServiceByReservationId(PeopleServices peopleService)
        {
             Delete(peopleService);
        }

        public void removePeopleServiceByReservationId(List<PeopleServices> peopleServiceList)
        {
            throw new NotImplementedException();
        }
    }
}
