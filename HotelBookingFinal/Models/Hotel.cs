

namespace HotelBookingFinal.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public List<Floor> Floors { get; set; } = new List<Floor>();
        public List<Admin> Administrators { get; set; } = new List<Admin>();
    }
}
