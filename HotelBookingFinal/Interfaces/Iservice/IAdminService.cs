using HotelBookingFinal.Models;
using System;
using System.Collections.Generic;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IAdminService
    {
        AdminDashboardData GetDashboard(int hotelId);
        bool UpdateRoomStatus(int roomId, bool isAvailable);
        List<Booking> GetBookings(int hotelId, DateTime from, DateTime to);
        bool CreateAdmin(Admin admin, string password);
        Admin GetAdminById(int adminId);
        List<Admin> GetAllAdmins();
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(int adminId);
        string AuthenticateAdmin(string username, string password, out Admin admin);
    }

    public class AdminDashboardData
    {
        public int ActiveBookings { get; set; }
        public int AvailableRooms { get; set; }
        public decimal RevenueToday { get; set; }
        public int MaintenanceRooms { get; set; }
    }
}