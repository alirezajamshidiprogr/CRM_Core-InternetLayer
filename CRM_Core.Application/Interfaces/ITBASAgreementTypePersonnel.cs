using CRM_Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.Interfaces
{
    public interface ITBASAgreementTypePersonnel
    {
        IEnumerable<TBASAgreementType> GetAllAgreements();
    }
}
