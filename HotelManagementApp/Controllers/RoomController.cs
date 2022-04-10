using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagementApp.Models;
using HotelManagementApp.ViewModel;
using HotelManagementApp.Controllers;
using System.IO;

namespace HotelManagementApp.Controllers
{
    public class RoomController : Controller
    {
        private HotelDbEntities objHotelDbEntities;
        public RoomController()
        {
            objHotelDbEntities = new HotelDbEntities();
        }
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListofBookingStatus = (from obj in objHotelDbEntities.BookingStatus
               select new SelectListItem()
               {
                    Text = obj.BookingStatus1,
                    Value = obj.BookingStatusId.ToString()
               }).ToList();
            objRoomViewModel.ListofRoomType = (from obj in objHotelDbEntities.RoomTypes
               select new SelectListItem()
               {
                    Text = obj.RoomType,
                    Value = obj.RoomTypeId.ToString()
               }).ToList();

            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
         {
            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;
            if (objRoomViewModel.RoomId == 0)
            {
                 ImageUniqueName = Guid.NewGuid().ToString();
                 ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                //objHotelDbEntities
                Rooms objRooms = new Rooms()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    IsActive = true,
                    RoomImage = ActualImageName,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId
                };
                objHotelDbEntities.Rooms.Add(objRooms);
                message = "Added.";
            }
            else
            {
                Rooms objRooms = objHotelDbEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                if (objRoomViewModel.Image != null)
                {
                     ImageUniqueName = Guid.NewGuid().ToString();
                     ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                     objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                     objRooms.RoomImage = ActualImageName;
                }
                objRooms.RoomNumber = objRoomViewModel.RoomNumber;
                objRooms.RoomDescription = objRoomViewModel.RoomDescription;
                objRooms.RoomPrice = objRoomViewModel.RoomPrice;
                objRooms.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRooms.IsActive = true;               
                objRooms.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRooms.RoomTypeId = objRoomViewModel.RoomTypeId;
                message = "Updated.";
            }
            objHotelDbEntities.SaveChanges();
            return Json(data:new {message = "Room Successfully " + message, success = true},JsonRequestBehavior.AllowGet);
         }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                  (from objRooms in objHotelDbEntities.Rooms
                   join objBooking in objHotelDbEntities.BookingStatus on objRooms.BookingStatusId equals objBooking.BookingStatusId
                   join objRoomType in objHotelDbEntities.RoomTypes on objRooms.RoomTypeId equals objRoomType.RoomTypeId
                   where objRooms.IsActive == true
                   select new RoomDetailsViewModel()
                   {
                       RoomNumber = objRooms.RoomNumber,
                       RoomDescription = objRooms.RoomDescription,
                       RoomCapacity = objRooms.RoomCapacity,
                       RoomPrice= objRooms.RoomPrice,
                       BookingStatus1 = objBooking.BookingStatus1,
                       RoomType = objRoomType.RoomType,
                       RoomImage = objRooms.RoomImage,
                       RoomId = objRooms.RoomId
                   }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Rooms objRooms = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            objRooms.IsActive = false;
            objHotelDbEntities.SaveChanges();
            return Json(data:new { message = "Record successfully deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}