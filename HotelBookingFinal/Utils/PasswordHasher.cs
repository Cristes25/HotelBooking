using BCrypt.Net;

namespace HotelBookingFinal.Utils
{
    public static class PasswordHasher
    {
        private const int WorkFactor = 12;  //2^12 = 4096 iterations
                                            //hash (auto generated salt)
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty");
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor, HashType.SHA512);
        }
        //verify against hashed value
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);


            }
            catch (SaltParseException)
            {
                return false;//invalid hash

            }
        }

    }
}
