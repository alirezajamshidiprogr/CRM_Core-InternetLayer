using CRM_Core.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM_Core.Application.ViewModels.CustomViewModel
{
    public class ReservationViewModel
    {
        public int ReservationID { get; set; }
        public int PeopleId { get; set; }
        public string SystemCode { get; set; }
        public string PeopleManualCode { get; set; }
        public string PeopleSystemCode { get; set; }
        public string ReservationDate { get; set; }
        public string FullName { get; set; }
        public string ReservationStatusTitle { get; set; }
        [Description("با این فیلد مشخص می کنیم که نوبت دهی منقضی شده یا خیر ")]
        public string ReservationStatus { get; set; }

    }

    public class ReservationViewModelSearch
    {
        public string CustomerFirstName { get; set; }
        public string CustomerFamily { get; set; }
        public string FromReservationDate { get; set; }
        public string ToReservationDate { get; set; }
        public string SystemCode { get; set; }
        public string PeopleCode { get; set; }
        public string IsExpired { get; set; }
        public string HasCheque { get; set; }
        public int PageNumber { get; set; }
    }

    public class PeopleReservationHistoryInfo
    {
        public string CountOfBeCustomer { get; set; }
        public string CustomerType { get; set; }
        public string CustomerIncomeForSalon { get; set; }
    }

    public class PeopleServiceReservationViewModel : Reservation
    {
        public int ClerkId { get; set; }
    }
}
