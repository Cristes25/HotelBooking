using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IHotelRepository
    {
        List<Hotel> GetAllHotels();
        Hotel GetHotelById(int id);
        bool AddHotel(Hotel hotel);
    }
}
