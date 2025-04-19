using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;
using Dapper;
using HotelBookingFinal.Interfaces.Irepos;
namespace HotelBookingFinal.Repositories
{
    public class RoomRepository: IRoomRepository
    {


        private readonly string _connectionString;

        public RoomRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }
        public List<Room> GetRoomsUnderMaintenance(int hotelId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Room>(
                @"SELECT r.* FROM Rooms r
                JOIN Floors f ON r.FloorID = f.FloorID
                WHERE f.HotelID = @HotelId
                AND r.IsUnderMaintenance = TRUE
                AND (r.MaintenanceEndDate IS NULL OR r.MaintenanceEndDate > NOW())",
                new { HotelId = hotelId }
            ).ToList();
        }

        public bool SetRoomMaintenanceStatus(int roomId, bool isUnderMaintenance,
                                          DateTime? startDate = null,
                                          DateTime? endDate = null,
                                          string notes = null)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                @"UPDATE Rooms 
                SET IsUnderMaintenance = @IsUnderMaintenance,
                    MaintenanceStartDate = @StartDate,
                    MaintenanceEndDate = @EndDate,
                    MaintenanceNotes = @Notes
                WHERE RoomID = @RoomID",
                new
                {
                    RoomID = roomId,
                    IsUnderMaintenance = isUnderMaintenance,
                    StartDate = startDate,
                    EndDate = endDate,
                    Notes = notes
                }
            ) > 0;
        }
        public List<Room> GetAvailableRooms(int hotelId, DateTime from, DateTime to)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Room>(
                @"SELECT r.* FROM Rooms r
                JOIN Floors f ON r.FloorID = f.FloorID
                WHERE f.HotelID = @HotelId
                AND r.RoomID NOT IN (
                    SELECT RoomID FROM Bookings
                    WHERE CheckInDate < @To
                    AND CheckOutDate > @From
                    AND IsCancelled = FALSE
                )",
                new { HotelId = hotelId, From = from, To = to }
            ).ToList();
        }
        public bool UpdateRoomAvailability(int roomId, bool isAvailable)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                @"UPDATE Rooms SET IsAvailable = @IsAvailable
                WHERE RoomID = @RoomID",
                new { RoomID = roomId, IsAvailable = isAvailable }
            ) > 0;
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

        public bool RoomExists(int roomId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.ExecuteScalar<bool>(
                    "SELECT COUNT(1) FROM Rooms WHERE RoomID = @RoomID",
                    new { RoomID = roomId }
                );
            }
        }
        public Room GetRoomById(int roomId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var roomDict = new Dictionary<int, Room>();

            var result = conn.Query<Room, Floor, Hotel, Room>(
                @"SELECT r.*, f.*, h.* 
                 FROM Rooms r
                 JOIN Floors f ON r.FloorID = f.FloorID AND r.HotelID = f.HotelID
                 JOIN Hotels h ON f.HotelID = h.HotelID
                 WHERE r.RoomID = @RoomId",
                (room, floor, hotel) =>
                {
                    if (!roomDict.TryGetValue(room.RoomID, out var roomEntry))
                    {
                        roomEntry = room;
                        roomDict.Add(room.RoomID, roomEntry);
                    }

                    roomEntry.Floor = floor;
                    roomEntry.Floor.Hotel = hotel;
                    return roomEntry;
                },
                new { RoomId = roomId },
                splitOn: "FloorID,HotelID"
            ).FirstOrDefault();

            return result;
        }
      
       
        }
   
    }

    


