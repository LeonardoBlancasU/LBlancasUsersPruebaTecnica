using System;
using System.Collections.Generic;

namespace DL;

public partial class Rol
{
    public byte IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
