using Dapper;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;
using HotelBookingFinal.Interfaces.Irepos;

namespace HotelBookingFinal.Repositories
{
    public class FoodOrderRepository: IFoodOrderRepository
    {
        private readonly string _connectionString;

        public FoodOrderRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public bool CreateFoodOrder(FoodOrder order)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Execute(
                    "INSERT INTO FoodOrders (BookingID, OrderType) VALUES (@BookingID, @OrderType)",
                    order
                ) > 0;
            }
        }

        public List<FoodOrder> GetFoodOrdersByBooking(int bookingId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Query<FoodOrder>(
                    "SELECT * FROM FoodOrders WHERE BookingID = @BookingID",
                    new { BookingID = bookingId }
                ).ToList();
            }
        }
    }
}