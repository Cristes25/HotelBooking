using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Models;
using System;
using System.Collections.Generic;

namespace HotelBookingFinal.Services
{
    public class AdminServices : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public AdminServices(
            IAdminRepository adminRepository,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository)
        {
            _adminRepository = adminRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public AdminDashboardData GetDashboard(int hotelId)
        {
            return new AdminDashboardData
            {
                ActiveBookings = _bookingRepository.GetActiveBookings(hotelId).Count,
                AvailableRooms = _roomRepository.GetAvailableRooms(hotelId, DateTime.Today, DateTime.Today.AddDays(1)).Count,
                RevenueToday = _bookingRepository.GetDailyRevenue(hotelId, DateTime.Today),
                MaintenanceRooms = _roomRepository.GetRoomsUnderMaintenance(hotelId).Count
            };
        }

        public bool UpdateRoomStatus(int roomId, bool isAvailable)
        {
            return _roomRepository.UpdateRoomAvailability(roomId, isAvailable);
        }

        public List<Booking> GetBookings(int hotelId, DateTime from, DateTime to)
        {
            return _bookingRepository.GetHotelBookings(hotelId, from, to);
        }

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