using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;



namespace HotelBookingFinal.Services
{
    public class AdminServices
    {
        private readonly AdminRepository _adminRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly RoomRepository _roomRepository;

        public AdminServices()
        {
            _adminRepository = new AdminRepository();
            _bookingRepository = new BookingRepository();
            _roomRepository = new RoomRepository();
        }

        public AdminDashboard GetDashboard(int hotelId)
        {
            return new AdminDashboard
            {
                ActiveBookings = _bookingRepository.GetActiveBookings(hotelId).Count,
                AvailableRooms = _roomRepository.GetAvailableRooms(hotelId, DateTime.Today, DateTime.Today.AddDays(1)).Count,
                RevenueToday = _bookingRepository.GetDailyRevenue(hotelId, DateTime.Today),
                MaintenanceAssets = _roomRepository.GetRoomsUnderMaintenance(hotelId).Count

            };
        }
        public bool UpdateRoomStatus (int roomId, bool isAvailable)
        {
            return _roomRepository.UpdateRoomAvailability(roomId, isAvailable);
        }
        public List<Booking> GetBookings(int hotelId, DateTime from, DateTime to)
        {
            return _bookingRepository.GetHotelBookings(int hotelId,from,to);
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
public class AdminDashboard
{ 
    public int ActiveBookings { get; set; }
    public int AvailableRooms { get; set; }
    public decimal RevenueToday { get; set; }
    public int MaintenanceAssets { get; set; }
}