using Dapper;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;

namespace HotelBookingFinal.Repositories
{
    public class HotelRepository
    {
        private readonly string _connectionString;

        public HotelRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public List<Hotel> GetAllHotels()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Hotel>("SELECT * FROM Hotels").ToList();
            }
        }

        public Hotel GetHotelById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Hotel>(
                    "SELECT * FROM Hotels WHERE HotelID = @Id",
                    new { Id = id }
                );
            }
        }

        public bool AddHotel(Hotel hotel)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var result = conn.Execute(
                    "INSERT INTO Hotels (HotelName, Location) VALUES (@HotelName, @Location)",
                    hotel
                );
                return result > 0;
            }
        }
    }



}

