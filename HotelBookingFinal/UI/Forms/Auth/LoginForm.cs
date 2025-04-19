using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Models;
using HotelBookingFinal.UI.Forms.Admin;
using HotelBookingFinal.UI.Forms.Customer;


namespace HotelBookingFinal.UI.Forms.Auth
{
    public partial class LoginForm : Form

    {
        private readonly IAdminService _adminServices;
        private readonly ICustomerService _customerService;
        private readonly ICustumerAuthService _customerAuthService;

        public LoginForm(IAdminService adminServices, ICustomerService customerService, ICustumerAuthService customerAuthService)
        {
            InitializeComponent();
            _adminServices = adminServices;
            _customerService = customerService;
            _customerAuthService = customerAuthService;
          
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Admin login
            string token = _adminServices.AuthenticateAdmin(txtUsername.Text, txtPassword.Text, out Models.Admin admin);
            if (admin != null)
            {
                var dashboard = new AdminDashboardForm(_adminServices, admin);
                dashboard.Show();
                this.Hide();
                return;
            }

            // Customer login
            var customer = _customerService.Login(txtUsername.Text, txtPassword.Text);
            if (customer != null)
            {
                var dashboard = new CustomerDashboardForm(_customerService, customer);
                dashboard.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("Invalid credentials", "Login Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var registerForm = new RegistrationForm(_customerAuthService);
            registerForm.ShowDialog();
        }
    }
}
