using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Orders
    {
        public int IdOrder { get; set; }

        //public ML.User User { get; set; }
        //public ML.Truck Truck { get; set; }
        //public ML.Location PIckUp { get; set; }
        //public ML.Location DropOff { get; set; }
        public ML.Status Status { get; set; }
        public List<object> Ordenes { get; set; }
    }
}
