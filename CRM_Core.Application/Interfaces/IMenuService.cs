using CRM_Core.Entities.Models.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
   public interface IMenuService
    {
        IEnumerable<TBASMenu> GetApplicationMenu();

    }
}
