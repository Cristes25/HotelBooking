using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;



namespace HotelBookingFinal.Services
{
    public class AdminServices
    {
        private readonly AdminRepository _adminRepository;

        public AdminServices()
        {
            _adminRepository = new AdminRepository();
        }


        //Create Admin
        public bool CreateAdmin(Admin admin, string password)
        {
            return _adminRepository.CreateAdmin(admin, password);
        }
        public Admin GetAdminById(int adminId)
        {
            return _adminRepository.GetAdminById(adminId);
        }
        public List<Admin> GetAllAdmins()
        {
            return _adminRepository.GetAllAdmins();
        }
        public bool UpdateAdmin(Admin admin)
        {
            return _adminRepository.UpdateAdmin(admin);
        }
        public bool DeleteAdmin(int adminId)
        {
            return _adminRepository.DeleteAdmin(adminId);
        }
        public string AuthenticateAdmin(string username, string password, out Admin admin)
        {
            return _adminRepository.AuthenticateAdmin(username, password, out admin);
        }

    }
}
