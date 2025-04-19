using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IRoomRepository
    {
        List<Room> GetRoomsUnderMaintenance(int hotelId);
        List<Room> GetAvailableRooms(int hotelId, DateTime from, DateTime to);
        bool UpdateRoomAvailability(int roomId, bool isAvailable);
        List<Room> GetRoomsByFloor(int floorId);
        bool AddRoom(Room room);
        bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut);
        bool RoomExists(int roomId);
        Room GetRoomById(int roomId);

    }
}
