using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IAdminRepository
    {
        bool CreateAdmin(Admin admin, string password);
        Admin GetAdminById(int adminId);
        Admin? GetAdminByUsername(string username);
        List<Admin> GetAllAdmins();
        bool UpdateAdmin(Admin admin);
        bool UpdatePassword(int adminId, string newPassword);
        bool DeleteAdmin(int adminId);
        string AuthenticateAdmin(string username, string password, out Admin admin);
    }
}
