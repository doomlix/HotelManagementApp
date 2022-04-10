using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelManagementApp.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string CustomerFirstName { get; set; }
        [Display(Name = "Customer Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string CustomerLastName { get; set; }
        [Display(Name = "Customer Phone Number")]
        [Required(ErrorMessage = "Phone Number is required.")]

        public string CustomerPhoneNumber { get; set; }
       
        [Display(Name = "Customer Email")]
        [Required(ErrorMessage = "Email is required.")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Booking From")]
        [Required(ErrorMessage = "Booking From date is required.")]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}",ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime BookingFrom { get; set; }
        [Display(Name = "Booking Until")]
        [Required(ErrorMessage = "Booking From date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingUntil { get; set; }
        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Room is required.")]
        public int AssignRoomId { get; set; }
        [Display(Name = "No. of Members")]
        [Required(ErrorMessage = "No. of Member is required.")]
        public int NumberOfMembers { get; set; }

        public IEnumerable<SelectListItem>ListofRooms { get; set; }



    }
}