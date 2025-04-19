using HotelBookingFinal.Interfaces.Iservice;
using MaterialSkin;
using MaterialSkin.Controls;

namespace HotelBookingFinal.UI.Forms.Admin
{
    public partial class AdminDashboardForm : MaterialForm
    {
        private readonly IAdminService _adminServices;
        private readonly Models.Admin _currentAdmin;
        public AdminDashboardForm(IAdminService adminServices,Models.Admin admin)   
        {
            InitializeComponent();
            // MaterialSkin setup
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            _adminServices = adminServices;
            _currentAdmin = admin;
           
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            lblWelcome.Text = $"Welcome, {_currentAdmin.FirstName}";
            var stats = _adminServices.GetDashboard(_currentAdmin.HotelId);

            cardActiveBookings.Count = stats.ActiveBookings.ToString();
            cardAvailableRooms.Count = stats.AvailableRooms.ToString();
            cardRevenueToday.Count = stats.RevenueToday.ToString("C");
            cardMaintenance.Count = stats.MaintenanceRooms.ToString();
        }

        private void btnManageAdmins_Click(object sender, EventArgs e)
        {
            var admins = _adminServices.GetAllAdmins();
            adminGrid.DataSource = admins;
        }

  
    }
}
   