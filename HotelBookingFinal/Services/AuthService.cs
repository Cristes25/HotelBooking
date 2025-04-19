using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Utils;

namespace HotelBookingFinal.Services
{
    public class AuthService: IAdminAuthService
    {
        private readonly IAdminRepository _adminRepo = new AdminRepository();

        public Admin? Login(string username, string password)
        {
            var admin = _adminRepo.GetAdminByUsername(username);

            if (admin == null || !admin.VerifyPassword(password))
                return null;

            return admin;
        }

        public bool ChangePassword(int adminId, string newPassword)
        {
            return _adminRepo.UpdatePassword(
                adminId,
                PasswordHasher.HashPassword(newPassword)
            );
        }

        
    }
}