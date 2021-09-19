using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public int PeopleId { get; set; }
        public string SystemCode { get; set; }
        public string FullName { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ReservationDate { get; set; }
        public int ReservatonCount { get; set; }
        public string TBASServicesValues { get; set; }
        public string ClerkProperty { get; set; }
        public double Price { get; set; }
        public string PayType { get; set; }
    }

    public class ReservationViewModelSearch
    {
        public string PeopleName { get; set; }
        public string PeopleLastName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public int? TBASServiceId { get; set; }
        public string Date { get; set; }
        public int? PayTypeId { get; set; }
        public string SystemCode { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
    }

}
