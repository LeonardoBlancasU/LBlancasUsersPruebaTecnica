using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllOrdersDTO
    {
        public int IdOrder { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Plates { get; set; }
        public byte IdStatus { get; set; }
        public string NombreStatus { get; set; }
        public string AddressPick { get; set; }
        public string AddressDrop { get; set; }
        public string PlaceIdPick { get; set; }
        public string PlaceIdDrop { get; set; }
        public double LatitudePick { get; set; }
        public double LatitudeDrop { get; set; }
        public double LongitudePick { get; set; }
        public double LongitudeDrop { get; set; }
    }
}
