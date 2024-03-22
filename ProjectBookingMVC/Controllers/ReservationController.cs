using ProjectBookingMVC.Models;
using ProjectBookingMVC.Repository.RepRoom;
using ProjectBookingMVC.Repository.RepReservation;
using Microsoft.AspNetCore.Mvc;
using ProjectBookingMVC.DTO;
namespace ProjectBookingMVC.Controllers
{
    public class ReservationController : Controller
    {
        IRoomRepository roomRepository = null;
        IReservationRepository reservationRepository = null;
        public ReservationController()
        {
            roomRepository = new Repository.RepRoom.RoomRepository();
            reservationRepository = new ReservationRepository();
        }
        public IActionResult Cancel(int id)
        {
            reservationRepository.CancelReservation(id);
            return RedirectToAction("MyReservation", "Reservation");
        }
        public IActionResult MyReservation()
        {
            string username = HttpContext.Session.GetString("Logined");
            if (username != null)
            {
                List<Reservation> res = reservationRepository.GetMyReservation(username);
                if (res.Count == 0)
                {
                    ViewBag.reservations = null; return View();
                }
                List<ReservationDTO> result = new List<ReservationDTO>();
                foreach (var r in res)
                {
                    ReservationDTO dto = new ReservationDTO();
                    dto.IdUser = int.Parse(username);
                    dto.CheckIn = r.CheckIn;
                    dto.CheckOut = r.CheckOut;
                    dto.Room = roomRepository.GetRoomById(r.IdRoom);
                    dto.IdRoom = r.IdRoom;
                    dto.Id = r.Id;
                    dto.TotalPrice = r.TotalPrice;
                    result.Add(dto);
                }
                if (result != null)
                {
                    ViewBag.reservations = result;
                }
                return View();
            }
            else
            {
                return View("/Views/Authentication/Error.cshtml");
            }
        }
    }
}
