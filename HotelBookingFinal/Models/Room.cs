

namespace HotelBookingFinal.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int FloorID { get; set; }
        public Floor? Floor { get; set; }
        public string RoomNumber { get; set; }
        public RoomCategory Category { get; set; }
        public RoomType? Type { get; set; }
        public TimeSpan BookingTime { get; set; } = TimeSpan.FromHours(12);
        public List<Asset> Assets { get; set; } = new List<Asset>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
    public enum RoomCategory { Standard, Special }
    public enum RoomType { Suite, Deluxe }
}

