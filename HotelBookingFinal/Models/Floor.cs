using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingFinal.Models
{
    public class Floor
    {
        public int FloorID { get; set; }
        public int HotelID { get; set; }
        [ForeignKey("HotelID")]
        public Hotel? Hotel { get; set; }
        public int FloorNumber { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();

    }
}
