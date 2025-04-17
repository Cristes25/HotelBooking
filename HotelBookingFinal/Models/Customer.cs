using HotelBookingFinal.Utils;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingFinal.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(15)]
        public string Phone { get; set; }

        public string? _passwordHash;

        [Required]
        public string PasswordHash
        {
            get => _passwordHash ?? string.Empty;
            private set => _passwordHash = value;
        }

        public string FullName => $"{FirstName} {LastName}";

        public void SetPassword(string password)
        {
            _passwordHash = PasswordHasher.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyPassword(password, _passwordHash);
        }
        public bool IsEmailVerified { get; private set; }
        public string? EmailVerificationToken { get; private set; }

        public void GenerateVerificationToken()
        {
            EmailVerificationToken = Guid.NewGuid().ToString("N");
        }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}