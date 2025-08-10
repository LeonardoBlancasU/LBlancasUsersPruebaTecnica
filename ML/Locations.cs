using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Locations
    {
        public int IdLocation { get; set; }
        public string? address { get; set; }
        public string? place_id { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public List<object>? Locaciones { get; set; }
    }
}
