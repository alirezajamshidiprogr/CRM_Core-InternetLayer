using CRM_Core.Entities.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface ISalonInfoService
    {
        IEnumerable <SalonInfo> GetSalon();

        void UpdateSaloInfo(SalonInfo salonInfo);
        void AddSalonInfo(SalonInfo salonInfo);

        void SaveChanges();
    }
}
