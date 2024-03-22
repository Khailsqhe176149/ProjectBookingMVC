

using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepReservation
{
    public class ReservationRepository : IReservationRepository
    {
        public void CancelReservation(int id)
         => ReservationDAO.Instance.CancelReservation(id);

        public Reservation CheckRoomReservation(Reservation reservation) => ReservationDAO.Instance.CheckRoomReservation(reservation);

        public Reservation CreateReservation(Reservation reservation) => ReservationDAO.Instance.CreateReservation(reservation);

        public Reservation DeleteReservation(Reservation reservation) => ReservationDAO.Instance.DeleteReservation(reservation);

        public List<Reservation> GetMyReservation(string username) => ReservationDAO.Instance.GetMyReservation(username);

    }
}
