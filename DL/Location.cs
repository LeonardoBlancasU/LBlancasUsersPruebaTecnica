using System;
using System.Collections.Generic;

namespace DL;

public partial class Location
{
    public int IdLocation { get; set; }

    public string? Address { get; set; }

    public string? PlaceId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public virtual ICollection<Order> OrderIdDropOffNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderIdPickUpNavigations { get; set; } = new List<Order>();
}
