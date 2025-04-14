using Dapper;
using MySql.Data.MySqlClient;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert booking
                        var bookingId = conn.ExecuteScalar<int>(
                            @"INSERT INTO Bookings 
                            (CustomerID, RoomID, BookingCode, CheckInDate, CheckOutDate) 
                            VALUES (@CustomerID, @RoomID, @BookingCode, @CheckInDate, @CheckOutDate);
                            SELECT LAST_INSERT_ID();",
                            booking, transaction);

                        // 2. Insert food orders if any
                        if (booking.FoodOrders?.Any() == true)
                        {
                            conn.Execute(
                                "INSERT INTO FoodOrders (BookingID, OrderType) VALUES (@BookingID, @OrderType)",
                                booking.FoodOrders.Select(o => new
                                {
                                    BookingID = bookingId,
                                    o.OrderType
                                }),
                                transaction);
                        }

                        transaction.Commit();
                        return bookingId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return !conn.ExecuteScalar<bool>(
                    @"SELECT EXISTS(
                        SELECT 1 FROM Bookings 
                        WHERE RoomID = @RoomID 
                        AND (
                            (@CheckIn < CheckOutDate) 
                            AND (@CheckOut > CheckInDate)
                        ))",
                    new { RoomID = roomId, CheckIn = checkIn, CheckOut = checkOut });
            }
        }

        public Booking? GetBookingByCode(string code)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var booking = conn.QueryFirstOrDefault<Booking>(
                    "SELECT * FROM Bookings WHERE BookingCode = @Code",
                    new { Code = code });

                if (booking != null)
                {
                    // Load related data
                    booking.Customer = conn.QueryFirstOrDefault<Customer>(
                        "SELECT * FROM Customers WHERE CustomerID = @Id",
                        new { Id = booking.CustomerID });

                    booking.Room = conn.QueryFirstOrDefault<Room>(
                        "SELECT * FROM Rooms WHERE RoomID = @Id",
                        new { Id = booking.RoomID });

                    booking.FoodOrders = conn.Query<FoodOrder>(
                        "SELECT * FROM FoodOrders WHERE BookingID = @Id",
                        new { Id = booking.BookingID }).ToList();
                }

                return booking;
            }
        }
        // NEW METHOD: Get booking by integer ID
        public Booking? GetBookingById(int bookingId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var booking = conn.QueryFirstOrDefault<Booking>(
                    "SELECT * FROM Bookings WHERE BookingID = @Id",
                    new { Id = bookingId });

                if (booking != null)
                {
                    // Load related data (consistent with GetBookingByCode)
                    booking.Customer = conn.QueryFirstOrDefault<Customer>(
                        "SELECT * FROM Customers WHERE CustomerID = @CustomerId",
                        new { CustomerId = booking.CustomerID });

                    booking.Room = conn.QueryFirstOrDefault<Room>(
                        "SELECT * FROM Rooms WHERE RoomID = @RoomId",
                        new { RoomId = booking.RoomID });

                    booking.FoodOrders = conn.Query<FoodOrder>(
                        "SELECT * FROM FoodOrders WHERE BookingID = @BookingId",
                        new { BookingId = booking.BookingID }).ToList();
                }

                return booking;
            }
        }

        public bool CancelBooking(int bookingId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Execute(
                    "UPDATE Bookings SET IsCancelled = TRUE, CancellationDate = @Now WHERE BookingID = @Id",
                    new { Now = DateTime.Now, Id = bookingId }) > 0;
            }
        }

        public bool IsBookingCancellable(int bookingId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var booking = conn.QueryFirstOrDefault<Booking>(
                    "SELECT * FROM Bookings WHERE BookingID = @Id",
                    new { Id = bookingId });

                return booking != null &&
                       !booking.IsCancelled && // Handle nullable boolean
                       booking.CheckInDate > DateTime.Now.AddDays(1); // 24h cancellation policy
            }
        }
    }
}