

using HotelBookingFinal.Services;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IEmailService
    {
        Task<EmailResult> SendBookingConfirmationAsync(string toEmail, string bookingCode);
        Task<EmailResult> SendCancellationConfirmationAsync(string toEmail, string bookingCode);
    }
}
