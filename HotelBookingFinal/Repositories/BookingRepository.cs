using Dapper;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;

namespace HotelBookingFinal.Repositories
{
    public class BookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public int CreateBooking(Booking booking)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.ExecuteScalar<int>(
                @"INSERT INTO Bookings 
                (CustomerID, RoomID, BookingCode, CheckInDate, CheckOutDate, TotalPrice, IsCancelled) 
                VALUES (@CustomerID, @RoomID, @BookingCode, @CheckInDate, @CheckOutDate, @TotalPrice, @IsCancelled);
                SELECT LAST_INSERT_ID();",
                booking
            );
        }
        public Booking? GetBookingById(int bookingId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var result = conn.Query<Booking, Room, Customer, Booking>(
                @"SELECT b.*, 
        r.*,
        c.*
        FROM Bookings b
        JOIN Rooms r ON b.RoomID = r.RoomID
        JOIN Customers c ON b.CustomerID = c.CustomerID
        WHERE b.BookingID = @BookingId",
                (booking, room, customer) =>
                {
                    booking.Room = room;
                    booking.Customer = customer;
                    return booking;
                },
                new { BookingId = bookingId },
                splitOn: "RoomID,CustomerID"
            ).FirstOrDefault();

            return result;
        }

        public Booking? GetBookingByCode(string bookingCode)
        {
            using var conn = new MySqlConnection(_connectionString);

            var result = conn.Query<Booking, Room, Customer, Booking>(
                @"SELECT b.*, 
        r.*,
        c.*
        FROM Bookings b
        JOIN Rooms r ON b.RoomID = r.RoomID
        JOIN Customers c ON b.CustomerID = c.CustomerID
        WHERE b.BookingCode = @BookingCode",
                (booking, room, customer) =>
                {
                    booking.Room = room;
                    booking.Customer = customer;
                    return booking;
                },
                new { BookingCode = bookingCode },
                splitOn: "RoomID,CustomerID"
            ).FirstOrDefault();

            return result;
        }



        public List<Booking> GetCustomerBookings(int customerId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Booking>(
                @"SELECT b.*, r.RoomNumber, r.RoomCategory 
                FROM Bookings b
                JOIN Rooms r ON b.RoomID = r.RoomID
                WHERE b.CustomerID = @CustomerId
                ORDER BY b.CheckInDate DESC",
                new { CustomerId = customerId }
            ).ToList();
        }

        public List<Booking> GetActiveBookings(int hotelId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Booking>(
                @"SELECT b.* FROM Bookings b
                JOIN Rooms r ON b.RoomID = r.RoomID
                JOIN Floors f ON r.FloorID = f.FloorID
                WHERE f.HotelID = @HotelId
                AND b.CheckOutDate > NOW()
                AND b.IsCancelled = FALSE
                ORDER BY b.CheckInDate",
                new { HotelId = hotelId }
            ).ToList();
        }

        public List<Booking> GetHotelBookings(int hotelId, DateTime fromDate, DateTime toDate)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Booking>(
                @"SELECT b.*, r.RoomNumber, c.FirstName, c.LastName 
                FROM Bookings b
                JOIN Rooms r ON b.RoomID = r.RoomID
                JOIN Floors f ON r.FloorID = f.FloorID
                JOIN Customers c ON b.CustomerID = c.CustomerID
                WHERE f.HotelID = @HotelId
                AND b.CheckInDate BETWEEN @FromDate AND @ToDate
                ORDER BY b.CheckInDate",
                new { HotelId = hotelId, FromDate = fromDate, ToDate = toDate }
            ).ToList();
        }

        public decimal GetDailyRevenue(int hotelId, DateTime date)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.ExecuteScalar<decimal>(
                @"SELECT COALESCE(SUM(b.TotalPrice), 0) 
                FROM Bookings b
                JOIN Rooms r ON b.RoomID = r.RoomID
                JOIN Floors f ON r.FloorID = f.FloorID
                WHERE f.HotelID = @HotelId
                AND DATE(b.CheckInDate) = @Date
                AND b.IsCancelled = FALSE",
                new { HotelId = hotelId, Date = date.Date }
            );
        }

        public bool IsBookingCancellable(int bookingId)
        {
            using var conn = new MySqlConnection(_connectionString);
            var booking = conn.QueryFirstOrDefault<Booking>(
                "SELECT CheckInDate, IsCancelled FROM Bookings WHERE BookingID = @BookingId",
                new { BookingId = bookingId }
            );

            return booking != null &&
                   !booking.IsCancelled &&
                   booking.CheckInDate > DateTime.Now.AddDays(1);
        }

        public bool CancelBooking(int bookingId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                @"UPDATE Bookings 
                SET IsCancelled = TRUE, 
                    CancellationDate = @Now 
                WHERE BookingID = @BookingId",
                new { Now = DateTime.Now, BookingId = bookingId }
            ) > 0;
        }

        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            using var conn = new MySqlConnection(_connectionString);
            return !conn.ExecuteScalar<bool>(
                @"SELECT EXISTS(
                    SELECT 1 FROM Bookings 
                    WHERE RoomID = @RoomId 
                    AND IsCancelled = FALSE
                    AND (
                        (@CheckIn < CheckOutDate) 
                        AND (@CheckOut > CheckInDate)
                    ))",
                new { RoomId = roomId, CheckIn = checkIn, CheckOut = checkOut }
            );
        }

        public List<FoodOrder> GetFoodOrders(int bookingId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<FoodOrder>(
                "SELECT * FROM FoodOrders WHERE BookingID = @BookingId",
                new { BookingId = bookingId }
            ).ToList();
        }
    }
}
