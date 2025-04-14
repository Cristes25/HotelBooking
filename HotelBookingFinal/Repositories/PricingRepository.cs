using Dapper;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;
using System;

namespace HotelBookingFinal.Repositories
{
    public class PricingRepository
    {
        private readonly string _connectionString;

        public PricingRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public decimal GetCurrentPrice(int hotelId, string roomCategory, string roomType)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    return conn.ExecuteScalar<decimal>(
                        @"SELECT BasePrice FROM Pricing 
                        WHERE HotelID = @HotelID 
                        AND RoomCategory = @RoomCategory
                        AND (RoomType = @RoomType OR RoomType IS NULL)
                        AND StartDate <= NOW() 
                        AND EndDate >= NOW()
                        ORDER BY StartDate DESC 
                        LIMIT 1",
                        new
                        {
                            HotelID = hotelId,
                            RoomCategory = roomCategory,
                            RoomType = roomType
                        }
                    );
                }
                catch
                {
                    // Return default price if no pricing found
                    return roomCategory == "Standard" ? 99.99m : 199.99m;
                }
            }
        }

        // Optional: Add method to get all active pricing
        public List<PricingRepository> GetActivePricing(int hotelId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Query<PricingRepository>(
                    @"SELECT * FROM Pricing 
                    WHERE HotelID = @HotelID
                    AND StartDate <= NOW() 
                    AND EndDate >= NOW()",
                    new { HotelID = hotelId }
                ).ToList();
            }
        }
    }
}