using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface IClerkServiceService
    {
        IEnumerable<ClerkServices> GetAllSalonServices();
        IEnumerable<ClerkServices> GetServicesByClerkId(int clerkId);
    }
}
