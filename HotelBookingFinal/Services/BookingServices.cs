using System;
using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;


namespace HotelBookingFinal.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly RoomRepository _roomRepo;
        private readonly EmailService _emailService;

        public BookingService()
        {
            _bookingRepo = new BookingRepository();
            _roomRepo = new RoomRepository();
            _emailService = new EmailService();
        }

        public BookingResult CreateBooking(Booking booking)
        {
            try
            {
                // 1. Validate dates
                if (booking.CheckInDate >= booking.CheckOutDate)
                    return new BookingResult(false, "Check-out date must be after check-in date");

                // 2. Check room availability
                if (!_roomRepo.IsRoomAvailable(booking.RoomID, booking.CheckInDate, booking.CheckOutDate))
                    return new BookingResult(false, "Room not available for selected dates");

                // 3. Generate unique booking code
                booking.BookingCode = GenerateBookingCode();

                // 4. Save to database
                var bookingId = _bookingRepo.CreateBooking(booking);

                return bookingId > 0
                    ? new BookingResult(true, $"Booking confirmed! Your code: {booking.BookingCode}")
                    : new BookingResult(false, "Failed to create booking");
            }
            catch (Exception ex)
            {
                return new BookingResult(false, $"Error: {ex.Message}");
            }
        }

        private string GenerateBookingCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        public BookingResult CancelBooking(int bookingId, string email)
        {
            try
            {
                // 1. Validate cancellation
                if (!_bookingRepo.IsBookingCancellable(bookingId))
                    return new BookingResult(false, "Booking cannot be cancelled");

                // 2. Process cancellation
                var success = _bookingRepo.CancelBooking(bookingId);
                if (!success)
                    return new BookingResult(false, "Cancellation failed");

                // 3. Send confirmation email
                var booking = _bookingRepo.GetBookingByCode(bookingId);
                _emailService.SendCancellationConfirmationAsync(email, booking.BookingCode);

                return new BookingResult(true, "Booking cancelled successfully");
            }
            catch (Exception ex)
            {
                return new BookingResult(false, $"Error: {ex.Message}");
            }
        }

        // Update CreateBooking to send email
        public BookingResult CreateBooking(Booking booking, string customerEmail)
        {
            var bookingResult = CreateBooking(booking);
            if (bookingResult.Success)
            {
                _emailService.SendBookingConfirmationAsync(customerEmail, booking.BookingCode);
            }
            return bookingResult;
        }
    }

    public class BookingResult
    {
        public bool Success { get; }
        public string Message { get; }

        public BookingResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}