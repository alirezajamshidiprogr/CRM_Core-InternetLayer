using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class ChequCustomerServiceViewModel
    {
        public string ServiceName { get; set; }
        public string ClerkName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SalonPortion { get; set; }
        public decimal PersonnelPortion { get; set; }
    }
}
