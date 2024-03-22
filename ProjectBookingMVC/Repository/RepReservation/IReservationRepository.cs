using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepReservation
{
    public interface IReservationRepository
    {
        void CancelReservation(int id);
        public Reservation CheckRoomReservation(Reservation reservation);
        public Reservation CreateReservation(Reservation reservation);
        public Reservation DeleteReservation(Reservation reservation);
        List<Reservation> GetMyReservation(string username);
    }
}
