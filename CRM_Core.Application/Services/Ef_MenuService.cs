using CRM_Core.Application.Interfaces;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Models.General;
using System.Collections.Generic;

namespace CRM_Core.Application.Services
{
    public class Ef_MenuService: RepositoryBase<TBASMenu>, IMenuService
    {
        public Ef_MenuService(CRM_CoreDB context) : base(context)
        {

        }

        public IEnumerable<TBASMenu> GetApplicationMenu()
        {
            return FindAll();
        }
    }
}
