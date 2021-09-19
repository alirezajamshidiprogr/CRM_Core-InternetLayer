using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_PeopleServiceService :RepositoryBase<PeopleServices> , IPeopleServiceService
    {
        public Ef_PeopleServiceService(CRM_CoreDB context):base(context)
        {

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
