using ProjectBookingMVC.Models;
using ProjectBookingMVC.Repository.RepRoom;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjectBookingMVC.Repository.RepReservation
{
    public class ReservationDAO
    {
        private static ReservationDAO instance;
        public static readonly object instanceLock = new object();
        public static ReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReservationDAO();
                    }
                    return instance;
                }
            }
        }
        internal Reservation CheckRoomReservation(Reservation reservation)
        {
            using var context = new BookingHotelContext();


            throw new NotImplementedException();
        }

        internal Reservation CreateReservation(Reservation reservation)
        {
            using var context = new BookingHotelContext();

            var overlappingBookings = context.Reservations
            .Where(rb => rb.IdRoom == reservation.IdRoom &&
                          ((rb.CheckIn >= reservation.CheckIn && rb.CheckIn < reservation.CheckOut) ||
                           (rb.CheckOut > reservation.CheckIn && rb.CheckOut <= reservation.CheckOut)))
            .ToList();


            if (overlappingBookings.Count > 0)
            {
                return null;
            }
            else
            {
                IRoomRepository roomRepository = new RoomRepository();

                TimeSpan timeSpan = reservation.CheckIn - reservation.CheckOut;
                int numberOfDays = (int)timeSpan.TotalDays;
                reservation.TotalPrice = numberOfDays * roomRepository.GetRoomById(reservation.IdRoom).Price;
                reservation.TotalPrice = reservation.TotalPrice > 0 ? reservation.TotalPrice : -reservation.TotalPrice;
                Console.WriteLine(reservation.TotalPrice);
                context.Reservations.Add(reservation);
                context.SaveChanges();
            }



            return reservation;
        }

        internal Reservation DeleteReservation(Reservation reservation)
        {
            using var context = new BookingHotelContext();
            throw new NotImplementedException();
        }

        internal List<Reservation> GetMyReservation(string id_username)
        {
            using var context = new BookingHotelContext();
            List<Reservation> reservations = context.Reservations.Where(re => re.IdUser == int.Parse(id_username)).ToList();
            return reservations;
        }

        internal void CancelReservation(int id)
        {
            using var context = new BookingHotelContext();
            List<Reservation> reservations = context.Reservations.Where(re => re.Id == id).ToList();
            //Reservation x = (from re in reservations
            //                 where re.Id == id
            //                 select re).FirstOrDefault();
            Reservation x = reservations.FirstOrDefault(re => re.Id == id);

            context.Reservations.Remove(x);

            context.SaveChanges();
        }
    }
}
