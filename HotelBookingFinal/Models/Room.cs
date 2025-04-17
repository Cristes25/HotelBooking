// Updated with DB modifications 

namespace HotelBookingFinal.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        //Foreigns
        public int FloorID { get; set; }
        public int HotelId { get; set; }

        //ENUMSS
        public RoomCategory Category { get; set; }
        public RoomType? Type { get; set; }

        //Time Related
        public TimeSpan BookingTime { get; set; } = TimeSpan.FromHours(12);
        //Nav Properties
        public Floor? Floor { get; set; }
       public Hotel? Hotel { get; set; }
       public List<Asset> Assets { get; set; } = new List<Asset>();
       public List<Booking> Bookings { get; set; } = new List<Booking>();


        
    }
    public enum RoomCategory { Standard, Special }
    public enum RoomType { Suite, Deluxe }
}

