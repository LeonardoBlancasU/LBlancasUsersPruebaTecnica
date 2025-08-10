using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ML
{
    public class Orders
    {
        public int IdOrder { get; set; }

        [ValidateNever]
        public ML.Users? User { get; set; }
        [ValidateNever]
        public ML.Trucks? Truck { get; set; }
        public ML.Locations? LocationPick { get; set; }
        public ML.Locations? LocationDrop { get; set; }
        public ML.Status? Status { get; set; }
        public List<object>? Ordenes { get; set; }
    }
}
