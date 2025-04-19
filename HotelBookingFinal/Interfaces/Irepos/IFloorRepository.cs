using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IFloorRepository
    {
        List<Floor> GetFloorsByHotelId(int hotelId);
        Floor GetFloorById(int floorId);
        int CreateFloor(Floor floor);
        bool UpdateFloor(Floor floor);
        bool DeleteFloor(int floorId);
        bool FloorExists(int floorId);
    }
}
