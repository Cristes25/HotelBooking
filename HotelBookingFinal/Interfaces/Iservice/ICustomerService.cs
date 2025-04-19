using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface ICustomerService
    {
        (bool Success, string Message) Register(Customer customer, string password);
        Customer? Login(string email, string password);
        List<Booking> GetCustomerBookings(int customerId);
        (bool Success, string Message) UpdateProfile(Customer customer);
        bool ChangePassword(int customerId, string newPassword);
    }
}
