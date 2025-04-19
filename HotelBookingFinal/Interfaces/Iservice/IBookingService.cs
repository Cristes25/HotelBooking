using HotelBookingFinal.Models;
using HotelBookingFinal.Services;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IBookingService
    {
        BookingResult CreateBooking(int customerId, BookingRequest request);
        BookingResult CancelBooking(int bookingId, int customerId);
        List<Booking> GetCustomerBookings(int customerId);
    }
}
