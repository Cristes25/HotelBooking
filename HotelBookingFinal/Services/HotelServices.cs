using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;


namespace HotelBookingFinal.Services
{
    public class HotelServices
    {
        private readonly HotelRepository _hotelRepository;
        private readonly FloorRepository _floorRepository;

        public HotelServices()
        {
            _hotelRepository = new HotelRepository();
            _floorRepository = new FloorRepository();

        }
        public Hotel GetHotelWithFloors(int hotelId)
        { 
            var hotel = _hotelRepository.GetHotelById(hotelId);
            if (hotel != null)
            {
                hotel.Floors = _floorRepository.GetFloorsByHotelId(hotelId);
            }
            return hotel;

        }
    }
}
