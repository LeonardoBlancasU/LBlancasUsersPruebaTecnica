using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllOrdersDTO
    {
        public int IdOrders { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public int IdStatus { get; set; }
        public string NombreStatus { get; set; }
        public string AddressPick { get; set; }
        public string AddressDrop { get; set; }
        public string PlaceIdPick { get; set; }
        public string PlaceIdDrop { get; set; }
        public int LatitudePick { get; set; }
        public int LatitudeDrop { get; set; }
        public int LongitudePick { get; set; }
        public int LongitudeDrop { get; set; }
    }
}
