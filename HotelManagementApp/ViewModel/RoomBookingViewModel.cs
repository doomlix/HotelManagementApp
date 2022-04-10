using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelManagementApp.ViewModel
{
    public class RoomBookingViewModel
    {

        public int BookingId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime BookingFrom { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime BookingUntil { get; set; }
        public string RoomNumber { get; set; }
        public int NumberOfMembers { get; set; }
        public decimal TotalAmount { get; set; }

        public int NumberOfDays { get; set; }


    }
}