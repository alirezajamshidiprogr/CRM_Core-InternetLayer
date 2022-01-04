using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI_Presentation.Models
{
    public class TempReservationDetails
    {
        public int CustomerId { get; set; }
        public string P_ReservationDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string Description { get; set; }
        public int ClerkServicesId { get; set; }
        public bool isSalonCustomer { get; set; }
    }
}
