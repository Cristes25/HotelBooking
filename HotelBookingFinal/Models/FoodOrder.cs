using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingFinal.Models
{
    public class FoodOrder
    {
        public int OrderID { get; set; }
        public int BookingID { get; set; }
        public Booking Booking { get; set; }
        public FoodOrderType OrderType { get; set; }
    }
    public enum FoodOrderType { Unlimited, Limited }
}
