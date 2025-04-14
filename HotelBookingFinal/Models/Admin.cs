using HotelBookingFinal.Utils;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;


namespace HotelBookingFinal.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int HotelId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage ="Last name required")]
        [StringLength(50)]
        public required string LastName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public required string Username { get; set; }
        [Required]
        private string? _passwordHash;
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public void SetPassword(string password)
        {
            _passwordHash = PasswordHasher.HashPassword(password);
        }
        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyPassword(password, _passwordHash);
        }
        public string? GetPasswordHash()
        {
            return _passwordHash;
        }
    }
}
