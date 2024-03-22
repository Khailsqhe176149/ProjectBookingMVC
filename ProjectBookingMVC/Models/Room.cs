using System;
using System.Collections.Generic;

namespace ProjectBookingMVC.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int? Size { get; set; }

    public string? Capacity { get; set; }

    public string? Bed { get; set; }

    public string? Services { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }
}
