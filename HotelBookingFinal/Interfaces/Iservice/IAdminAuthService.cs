using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IAdminAuthService

    {

        public void RequestPasswordReset(string email)
        {
            throw new NotImplementedException();
        }

        public bool VerifyResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string token, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
