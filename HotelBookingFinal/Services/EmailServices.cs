using HotelBookingFinal.Utils;
using System.Net;
using System.Net.Mail;
using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;

namespace HotelBookingFinal.Services
{
    public class EmailService: IEmailService
    {
        private readonly SmtpClient _client;
        private readonly string _fromEmail;

        public EmailService()
        {
            _client = new SmtpClient(ConfigManager.SmtpServer, ConfigManager.SmtpPort)
            {
                Credentials = new NetworkCredential(
                    ConfigManager.SmtpUsername,
                    ConfigManager.SmtpPassword),
                EnableSsl = ConfigManager.EnableSsl,
                Timeout = 10000 // 10-second timeout
            };

            _fromEmail = ConfigManager.FromEmail;
        }

        public async Task<EmailResult> SendBookingConfirmationAsync(string toEmail, string bookingCode)
        {
            var subject = "Your Booking Confirmation";
            var body = $"""
                <h1>Booking Confirmed!</h1>
                <p>Your booking code: <strong>{bookingCode}</strong></p>
                <p>Check-in details will be sent 24 hours before arrival.</p>
                <p>Need help? Reply to this email.</p>
                """;

            return await SendEmailAsync(toEmail, subject, body);
        }

        public async Task<EmailResult> SendCancellationConfirmationAsync(string toEmail, string bookingCode)
        {
            var subject = "Booking Cancellation Confirmation";
            var body = $"""
                <h1>Cancellation Complete</h1>
                <p>Booking <strong>{bookingCode}</strong> has been cancelled.</p>
                {(DateTime.Now.Hour < 22 ? "<p>Want to rebook? <a href=\"https://yourhotel.com\">Check availability</a></p>" : "")}
                """;

            return await SendEmailAsync(toEmail, subject, body);
        }

        private async Task<EmailResult> SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_fromEmail);
                    message.To.Add(toEmail);
                    message.Subject = subject;
                    message.Body = htmlBody;
                    message.IsBodyHtml = true;

                    await _client.SendMailAsync(message);
                    return new EmailResult(true, "Email sent successfully");
                }
            }
            catch (SmtpException ex) when (ex.StatusCode == SmtpStatusCode.MailboxBusy)
            {
                return new EmailResult(false, "Recipient mailbox busy, please try again later");
            }
            catch (Exception ex)
            {
                return new EmailResult(false, $"Failed to send email: {ex.Message}");
            }
        }
    }

    public class EmailResult
    {
        public bool Success { get; }
        public string Message { get; }

       public EmailResult (bool success, string message)
        {
            Success = success;
            Message = message;

        }
    }
}