namespace ProjectBookingMVC.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int IdRoom { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? IdUser { get; set; }
        public Models.Room? Room { get; set; }
    }
}