using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IChequService
    {
        IEnumerable<Cheque> GetServiceCustomerByReservationId();
        IEnumerable<Cheque> getAllCheque();
    }
}
