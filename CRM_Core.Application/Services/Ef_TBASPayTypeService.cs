using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_TBASPayTypeService : DataAccessLayer.Repositories.RepositoryBase<TBASPayType> , ITBASPayTypeService
    {
        public Ef_TBASPayTypeService(CRM_CoreDB context) :base(context)
        {

        }

        public IEnumerable<TBASPayType> getAllPayTypes()
        {
            return FindAll();
        }
    }
}
