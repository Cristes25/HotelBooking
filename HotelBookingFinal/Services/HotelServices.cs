using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Interfaces.Irepos;
namespace HotelBookingFinal.Services
{
    public class HotelServices : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IFloorRepository _floorRepository;

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
