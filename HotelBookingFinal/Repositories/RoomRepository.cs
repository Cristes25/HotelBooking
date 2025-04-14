using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;
using Dapper;

namespace HotelBookingFinal.Repositories
{
    public class RoomRepository{


            private readonly string _connectionString;

    public RoomRepository()
    {
        _connectionString = ConfigManager.GetConnectionString();
    }

    public List<Room> GetRoomsByFloor(int floorId)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            return conn.Query<Room>(
                "SELECT * FROM Rooms WHERE FloorID = @FloorId",
                new { FloorId = floorId }
            ).ToList();
        }
    }

    public bool AddRoom(Room room)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            var result = conn.Execute(
                @"INSERT INTO Rooms (FloorID, RoomNumber, RoomCategory, RoomType, BookingTime)
                VALUES (@FloorID, @RoomNumber, @Category, @Type, @BookingTime)",
                new
                {
                    room.FloorID,
                    room.RoomNumber,
                    Category = room.Category.ToString(),
                    Type = room.Type?.ToString(),
                    room.BookingTime
                }
            );
            return result > 0;
        }
    }
        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            using var conn = new MySqlConnection(_connectionString);
            return !conn.ExecuteScalar<bool>(
                @"SELECT EXISTS(
                    SELECT 1 FROM Bookings 
                    WHERE RoomID = @RoomID 
                    AND CheckInDate < @CheckOut 
                    AND CheckOutDate > @CheckIn
                )",
                new { RoomID = roomId, CheckIn = checkIn, CheckOut = checkOut });
        }
    }
}
