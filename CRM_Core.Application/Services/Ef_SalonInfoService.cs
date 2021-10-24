using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM_Core.Application.Services
{
    public class Ef_SalonInfoService : RepositoryBase<SalonInfo>, Interfaces.ISalonInfoService
    {
        public Ef_SalonInfoService(CRM_CoreDB context) : base(context)
        {

        }

        public void AddSalonInfo(SalonInfo salonInfo)
        {
            Create(salonInfo);
        }

        public IEnumerable<SalonInfo> GetSalon()
        {
            return FindAll();
        }

        public void UpdateSaloInfo(SalonInfo salonInfo)
        {
            Update(salonInfo);
        }
    }

}
