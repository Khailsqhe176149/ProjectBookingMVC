using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepRoom
{
    public class RoomRepository : IRoomRepository
    {
        public void Create(Room room) => RoomDAO.Instance.Create(room);


        public void DeleteRoom(int id) => RoomDAO.Instance.DeleteRoom(id);

        public List<Room> GetAllRoom() => RoomDAO.Instance.GetAllRoom();

        public Room GetRoomById(int id) => RoomDAO.Instance.GetRoomById(id);

        public void UpdateRoom(Room room) => RoomDAO.Instance.UpdateRoom(room);
    }
}
