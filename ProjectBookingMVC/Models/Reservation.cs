using System;
using System.Collections.Generic;

namespace ProjectBookingMVC.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }

    public int? TotalPrice { get; set; }

    public int IdRoom { get; set; }

    public int? IdUser { get; set; }
}
