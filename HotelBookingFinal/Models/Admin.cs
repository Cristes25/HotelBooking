using HotelBookingFinal.Utils;


namespace HotelBookingFinal.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        private string _passwordHash;
        public string Email { get; set; }


        public void SetPassword(string password)
        {
            _passwordHash = PasswordHasher.HashPassword(password);
        }
        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyPassword(password, _passwordHash);
        }
        public string GetPasswordHash()
        {
            return _passwordHash;
        }

    }
}
