using System;
using System.Collections.Generic;

namespace DL;

public partial class Order
{
    public int IdOrder { get; set; }

    public int IdUser { get; set; }

    public int IdTruck { get; set; }

    public byte IdStatus { get; set; }

    public int IdPickUp { get; set; }

    public int IdDropOff { get; set; }

    public virtual Location IdDropOffNavigation { get; set; } = null!;

    public virtual Location IdPickUpNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual Truck IdTruckNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
