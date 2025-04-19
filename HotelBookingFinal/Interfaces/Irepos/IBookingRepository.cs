using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IBookingRepository
    {
        int CreateBooking(Booking booking);
        Booking? GetBookingById(int bookingId);
        Booking? GetBookingByCode(string bookingCode);
        List<Booking> GetCustomerBookings(int customerId);
        List<Booking> GetActiveBookings(int hotelId);
        List<Booking> GetHotelBookings(int hotelId, DateTime fromDate, DateTime toDate);
        decimal GetDailyRevenue(int hotelId, DateTime date);
        bool IsBookingCancellable(int bookingId);
        bool CancelBooking(int bookingId);
        bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut);
        List<FoodOrder> GetFoodOrders(int bookingId);
    }
}

