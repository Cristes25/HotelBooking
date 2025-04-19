using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingFinal.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public int RoomID { get; set; }
        public Room? Room { get; set; }
        public string BookingCode { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();
        public bool IsCancelled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? SpecialRequests { get; set; }
    }
}
