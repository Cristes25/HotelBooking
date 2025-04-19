using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface ICustumerAuthService
    {
        Customer? Login(string email, string password);
        (bool Success, string Message) Register(Customer customer, string password);
        string GeneratePasswordResetToken(string email);
    }
}
