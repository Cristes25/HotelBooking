using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingFinal.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly RoomRepository _roomRepo;
        private readonly EmailService _emailService;
        private readonly AssetService _assetService;
        private readonly FoodOrderRepository _foodOrderRepo;
        private readonly PricingService _pricingService;
        private readonly CustomerRepository _customerRepo;

        public BookingService()
        {
            _bookingRepo = new BookingRepository();
            _roomRepo = new RoomRepository();
            _emailService = new EmailService();
            _assetService = new AssetService();
            _foodOrderRepo = new FoodOrderRepository();
            _pricingService = new PricingService();
            _customerRepo = new CustomerRepository();
        }

        public BookingResult CreateBooking(int customerId, BookingRequest request)
        {
            try
            {
                // Validate customer exists
                var customer = _customerRepo.GetCustomerById(customerId);
                if (customer == null)
                    return new BookingResult(false, "Customer not found");

                // Validate dates
                if (request.CheckInDate < DateTime.Today)
                    return new BookingResult(false, "Check-in date cannot be in the past");

                if (request.CheckInDate >= request.CheckOutDate)
                    return new BookingResult(false, "Check-out must be after check-in");

                // Validate room
                var room = _roomRepo.GetRoomById(request.RoomId);
                if (room == null)
                    return new BookingResult(false, "Room not found");

                if (!_roomRepo.IsRoomAvailable(request.RoomId, request.CheckInDate, request.CheckOutDate))
                    return new BookingResult(false, "Room not available for selected dates");

                // Check assets
                if (!_assetService.IsRoomOperational(request.RoomId))
                    return new BookingResult(false, "Room has maintenance issues");

                // Calculate price
                decimal totalPrice = _pricingService.CalculateBookingCost(
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate,
                    request.FoodOrders?.Any() == true
                );

                // Create booking
                var booking = new Booking
                {
                    CustomerID = customerId,
                    RoomID = request.RoomId,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    BookingCode = GenerateBookingCode(),
                    TotalPrice = totalPrice
                };

                // Process in transaction
                using (var transaction = new System.Transactions.TransactionScope())
                {
                    var bookingId = _bookingRepo.CreateBooking(booking);

                    if (request.FoodOrders?.Any() == true)
                    {
                        foreach (var order in request.FoodOrders)
                        {
                            order.BookingID = bookingId;
                            _foodOrderRepo.CreateFoodOrder(order);
                        }
                    }

                    transaction.Complete();
                }

                // Send confirmation
                _emailService.SendBookingConfirmationAsync(customer.Email, booking.BookingCode);

                return new BookingResult(true, "Booking created successfully", booking.BookingCode);
            }
            catch (Exception ex)
            {
                return new BookingResult(false, $"Error: {ex.Message}");
            }
        }

        public BookingResult CancelBooking(int bookingId, int customerId)
        {
            try
            {
                var booking = _bookingRepo.GetBookingById(bookingId);
                if (booking == null)
                    return new BookingResult(false, "Booking not found");

                if (booking.CustomerID != customerId)
                    return new BookingResult(false, "Not authorized to cancel this booking");

                if (!_bookingRepo.IsBookingCancellable(bookingId))
                    return new BookingResult(false, "Cancellation not allowed");

                var customer = _customerRepo.GetCustomerById(customerId);
                if (customer == null)
                    return new BookingResult(false, "Customer not found");

                bool success = _bookingRepo.CancelBooking(bookingId);
                if (!success)
                    return new BookingResult(false, "Cancellation failed");

                _emailService.SendCancellationConfirmationAsync(customer.Email, booking.BookingCode);

                return new BookingResult(true, "Booking cancelled successfully");
            }
            catch (Exception ex)
            {
                return new BookingResult(false, $"Error: {ex.Message}");
            }
        }

        public List<Booking> GetCustomerBookings(int customerId)
        {
            return _bookingRepo.GetCustomerBookings(customerId);
        }

        private string GenerateBookingCode()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }
    }

    public class BookingResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string? BookingCode { get; }

        public BookingResult(bool success, string message, string? bookingCode = null)
        {
            Success = success;
            Message = message;
            BookingCode = bookingCode;
        }
    }

    public class BookingRequest
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<FoodOrder>? FoodOrders { get; set; }
    }
}