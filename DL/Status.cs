using System;
using System.Collections.Generic;

namespace DL;

public partial class Status
{
    public byte IdStatus { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
