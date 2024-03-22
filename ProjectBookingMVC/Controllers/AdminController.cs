using ProjectBookingMVC.Repository.RepRoom;
using Microsoft.AspNetCore.Mvc;
using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Controllers
{
    public class AdminController : Controller
    {
        IRoomRepository roomRepository = null;
        public AdminController() => roomRepository = new RoomRepository();
        public ActionResult Index()
        {
            string roll = HttpContext.Session.GetString("Roll");
            if (roll == "ADMIN")
            {
                List<Room> rooms = roomRepository.GetAllRoom();
                return View(rooms);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

        }

        // GET: RoomController/Details/
        public ActionResult Details(int id)
        {
            Room room;
            if (id != null)
            {
                room = roomRepository.GetRoomById(id);
                return View(room);
            }
            else
            {
                return View();
            }

        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room, IFormFile Image)
        {
            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/room", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                room.Image = fileName;
            }


            if (room != null)
            {
                Console.WriteLine(room.Image);
                roomRepository.Create(room);
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/
        public ActionResult Edit(int id)
        {
            Room room;
            if (id != null)
            {
                room = roomRepository.GetRoomById(id);
                return View(room);
            }
            else
            {
                return View();
            }
        }

        // POST: RoomController/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            Console.WriteLine(room.Id);
            roomRepository.UpdateRoom(room);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/
        public ActionResult Delete(int id)
        {
            Room room;
            if (id != null)
            {
                room = roomRepository.GetRoomById(id);
                return View(room);
            }
            else
            {
                return View();
            }
        }

        // POST: RoomController/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Room room)
        {
            Console.WriteLine(room.Id);
            roomRepository.DeleteRoom(room.Id);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
