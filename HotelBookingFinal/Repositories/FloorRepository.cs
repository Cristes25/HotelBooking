using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using System;
using Dapper;
using MySql.Data.MySqlClient;

namespace HotelBookingFinal.Repositories
{
    public class FloorRepository
    {
        private readonly string _connectionString;

        public FloorRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public List<Floor> GetFloorsByHotelId(int hotelId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var floors = conn.Query<Floor>(
                    @"SELECT * FROM Floors 
                      WHERE HotelID = @HotelId
                      ORDER BY FloorNumber",
                    new { HotelId = hotelId }).ToList();

                // Load rooms for each floor if needed
                foreach (var floor in floors)
                {
                    floor.Rooms = conn.Query<Room>(
                        @"SELECT * FROM Rooms 
                          WHERE FloorID = @FloorId
                          ORDER BY RoomNumber",
                        new { FloorId = floor.FloorID }).ToList();
                }

                return floors;
            }
        }

        public Floor GetFloorById(int floorId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var floor = conn.QueryFirstOrDefault<Floor>(
                    "SELECT * FROM Floors WHERE FloorID = @Id",
                    new { Id = floorId });

                if (floor != null)
                {
                    floor.Rooms = conn.Query<Room>(
                        "SELECT * FROM Rooms WHERE FloorID = @Id ORDER BY RoomNumber",
                        new { Id = floorId }).ToList();
                }

                return floor;
            }
        }

        public int CreateFloor(Floor floor)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.ExecuteScalar<int>(
                    @"INSERT INTO Floors 
                    (HotelID, FloorNumber, Description) 
                    VALUES (@HotelID, @FloorNumber, @Description);
                    SELECT LAST_INSERT_ID();",
                    floor);
            }
        }

        public bool UpdateFloor(Floor floor)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Execute(
                    @"UPDATE Floors SET 
                    FloorNumber = @FloorNumber,
                    Description = @Description
                    WHERE FloorID = @FloorID",
                    floor) > 0;
            }
        }

        public bool DeleteFloor(int floorId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                // First check if floor has rooms
                var hasRooms = conn.ExecuteScalar<bool>(
                    "SELECT COUNT(1) FROM Rooms WHERE FloorID = @Id",
                    new { Id = floorId });

                if (hasRooms)
                {
                    throw new InvalidOperationException("Cannot delete floor with existing rooms");
                }

                return conn.Execute(
                    "DELETE FROM Floors WHERE FloorID = @Id",
                    new { Id = floorId }) > 0;
            }
        }

        public bool FloorExists(int floorId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.ExecuteScalar<bool>(
                    "SELECT COUNT(1) FROM Floors WHERE FloorID = @Id",
                    new { Id = floorId });
            }
        }
    }
}
