using CRM_Core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Entities.Interfaces
{
   public interface IGraduationRepository
    {
        IEnumerable<TBASGraduation> GetGraduations();

    }
}
