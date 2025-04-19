using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingFinal.UI.Forms.Customer


{
    public partial class CustomerDashboardForm : MaterialForm
    {
        private readonly ICustomerService _customerService;
        private readonly Models.Customer _currentCustomer;
        private readonly IBookingService _bookingService;
        private readonly IRoomRepository _roomRepository;
        public CustomerDashboardForm(ICustomerService customerService, Models.Customer customer)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            _customerService = customerService;
            _currentCustomer = customer;

            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            lblWelcome.Text = $"Welcome, {_currentCustomer.FirstName}";
            var bookings = _customerService.GetCustomerBookings(_currentCustomer.CustomerID);
            bookingGrid.DataSource = bookings;
        }

        private void btnNewBooking_Click(object sender, EventArgs e)
        {
          

            var bookingForm = new BookingForm(_currentCustomer.CustomerID,_bookingService,_roomRepository);
            if (bookingForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomerData();
            }
        }
    }
}
