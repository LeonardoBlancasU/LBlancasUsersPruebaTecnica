using System;
using System.Collections.Generic;

namespace DL;

public partial class Truck
{
    public int IdTruck { get; set; }

    public string Year { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Plates { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
