using ProjectBookingMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectBookingMVC.Repository.RepRoom
{
    public class RoomDAO
    {
        private static RoomDAO instance;
        public static readonly object instanceLock = new object();
        public static RoomDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                    return instance;
                }
            }
        }


        public List<Room> GetAllRoom()
        {
            var rooms = new List<Room>();
            try
            {
                using var context = new BookingHotelContext();
                rooms = context.Rooms.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rooms;
        }
        public Room GetRoomById(int id)
        {
            try
            {
                var rooms = new List<Room>();
                using var context = new BookingHotelContext();
                rooms = context.Rooms.ToList();
                Room room = rooms.FirstOrDefault(r => r.Id == id);
                return room;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Create(Room room)
        {
            using (var context = new BookingHotelContext())
            {
                var newRoom = new Room
                {
                    Name = room.Name,
                    Description = room.Description,
                    Price = room.Price,
                    Bed = room.Bed,
                    Capacity = room.Capacity,
                    Services = room.Services,
                    Size = room.Size,
                    Image = room.Image,
                };
                context.Rooms.Add(newRoom);
                context.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            Room room = GetRoomById(id);
            try
            {
                if (room != null)
                {
                    using var context = new BookingHotelContext();
                    context.Rooms.Remove(room);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoom(Room room)
        {
            Room updateRoom = GetRoomById(room.Id);
            try
            {

                if (updateRoom != null)
                {
                    using var context = new BookingHotelContext();
                    context.Rooms.Update(room);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
