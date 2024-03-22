using ProjectBookingMVC.Repository.RepReservation;
using ProjectBookingMVC.Repository.RepRoom;
using Microsoft.AspNetCore.Mvc;
using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Controllers
{
    public class RoomController : Controller
    {
        IRoomRepository roomRepository = null;
        IReservationRepository reservationRepository = null;
        public RoomController()
        {
            roomRepository = new RoomRepository();
            reservationRepository = new ReservationRepository();
        }


        public IActionResult Room(string sort)
        {
            Console.WriteLine(sort);

            string userName = HttpContext.Session.GetString("Logined");

            if (userName != null)
            {
                List<Room> rooms = roomRepository.GetAllRoom();
                if (sort == "des")
                {
                    rooms = rooms.OrderByDescending(product => product.Price).ToList();
                }
                else
                {
                    rooms = rooms.OrderBy(product => product.Price).ToList();
                }
                var recordsForPage = rooms.ToList();

                ViewBag.Rooms = recordsForPage;


                return View();
            }
            else
            {
                return View("/Views/Authentication/Error.cshtml");
            }

        }
        public IActionResult RoomDetail(int id)
        {
            Console.WriteLine(id);
            ViewBag.room = roomRepository.GetRoomById(id);
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReservation(Reservation reservation)
        {
            Console.WriteLine(reservation.CheckIn);
            Console.WriteLine(reservation.CheckOut);
            Console.WriteLine(reservation.IdRoom);
            reservation.IdUser = int.Parse(HttpContext.Session.GetString("Logined"));
            Reservation checkRoom = reservationRepository.CreateReservation(reservation);
            ViewBag.Msg = null;
            if (reservation.CheckIn < DateTime.Now || reservation.CheckOut < DateTime.Now)
            {
                ViewBag.Msg = "Day booking is outdate";
                ViewBag.room = roomRepository.GetRoomById(reservation.IdRoom);
                return View("/Views/Room/RoomDetail.cshtml");
            }
            else
            {
                if (checkRoom != null)
                {
                    ViewBag.Msg = null;
                    return RedirectToAction("MyReservation", "Reservation");
                }
                else
                {
                    ViewBag.Msg = "This Room is used by others";
                    ViewBag.room = roomRepository.GetRoomById(reservation.IdRoom);
                    return View("/Views/Room/RoomDetail.cshtml");
                }
            }


        }
    }
}
