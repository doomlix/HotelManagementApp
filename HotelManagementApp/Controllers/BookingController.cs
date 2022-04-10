using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagementApp.Models;
using HotelManagementApp.ViewModel;

namespace HotelManagementApp.Controllers
{
    public class BookingController : Controller
    {
        private HotelDbEntities objHotelDbEntities;
        public BookingController()
        {
            objHotelDbEntities = new HotelDbEntities();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListofRooms = (from objRooms in objHotelDbEntities.Rooms
                                               where objRooms.BookingStatusId == 1
                                               select new SelectListItem()
                                               {
                                                   Text= objRooms.RoomNumber,
                                                   Value=objRooms.RoomId.ToString()
                                               }
                                               ).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingUntil = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }
        [HttpPost]
            public ActionResult Index(BookingViewModel objBookingViewModel)
            {
            int numberOfDays =Convert.ToInt32((objBookingViewModel.BookingUntil - objBookingViewModel.BookingFrom).TotalDays);
           Rooms objRooms= objHotelDbEntities.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);
            decimal RoomPrice = objRooms.RoomPrice;
            decimal TotalAmount = RoomPrice * numberOfDays;

            RoomBookings roomBookings = new RoomBookings()
            {
                BookingFrom = objBookingViewModel.BookingFrom,
                BookingUntil = objBookingViewModel.BookingUntil,
                AssignRoomId = objBookingViewModel.AssignRoomId,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                CustomerFirstName = objBookingViewModel.CustomerFirstName,
                CustomerLastName = objBookingViewModel.CustomerLastName,
                CustomerEmail=objBookingViewModel.CustomerEmail,
                CustomerPhoneNumber=objBookingViewModel.CustomerPhoneNumber,
                NumberOfMembers=objBookingViewModel.NumberOfMembers,
                TotalAmount= TotalAmount
            };
            objHotelDbEntities.RoomBookings.Add(roomBookings);
            objHotelDbEntities.SaveChanges();
            objRooms.BookingStatusId = 2;
            objHotelDbEntities.SaveChanges();
            return Json(data:new { message = "Hotel Booking had Successfuly been Created.", success = true }, JsonRequestBehavior.AllowGet);
            }
        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<RoomBookingViewModel> listOfBookingViewModels = new List<RoomBookingViewModel>();
            listOfBookingViewModels = (from objHotelBooking in objHotelDbEntities.RoomBookings
                                       join objRooms in objHotelDbEntities.Rooms on objHotelBooking.AssignRoomId equals objRooms.RoomId
                                       select new RoomBookingViewModel()
                                       {
                                        BookingFrom = objHotelBooking.BookingFrom,
                                        BookingUntil = objHotelBooking.BookingUntil,
                                        CustomerAddress = objHotelBooking.CustomerAddress,
                                        CustomerEmail = objHotelBooking.CustomerEmail,
                                        CustomerFirstName=objHotelBooking.CustomerFirstName,
                                        CustomerLastName=objHotelBooking.CustomerLastName,
                                        CustomerPhoneNumber=objHotelBooking.CustomerPhoneNumber,
                                        TotalAmount = objHotelBooking.TotalAmount,
                                        NumberOfMembers=objHotelBooking.NumberOfMembers,
                                        BookingId=objHotelBooking.BookingId,
                                        RoomNumber=objRooms.RoomNumber,
                                        RoomPrice=objRooms.RoomPrice,
                                        NumberOfDays=System.Data.Entity.DbFunctions.DiffDays(objHotelBooking.BookingFrom, objHotelBooking.BookingUntil).Value
                                       }).ToList();
            return PartialView("_BookingHistoryPartial", model: listOfBookingViewModels);
            

        }
    }
}