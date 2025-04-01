using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace HotelBookingFinal.Utils
{
    public static class PasswordHasher
    {
        //hash (auto generated salt)
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        //verify against hashed value
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
