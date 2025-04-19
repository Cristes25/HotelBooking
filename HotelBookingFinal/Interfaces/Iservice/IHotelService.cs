using HotelBookingFinal.Models;



namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IHotelService
    {
        Hotel GetHotelWithFloors(int hotelId);
    }
}
