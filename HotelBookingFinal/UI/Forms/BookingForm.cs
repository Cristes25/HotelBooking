using MaterialSkin;
using MaterialSkin.Controls;

using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Services;
using HotelBookingFinal.Models;

namespace HotelBookingFinal.UI.Forms
{
    public partial class BookingForm : MaterialForm
    {
        private readonly IBookingService _bookingService;
        private readonly int _customerId;
        private readonly IRoomRepository _roomRepository;

        public BookingForm(int customerId, IBookingService bookingService, IRoomRepository roomRepository)
        {
            InitializeComponent();
            _customerId = customerId;
            _bookingService = bookingService;
            _roomRepository = roomRepository;

            // MaterialSkin setup
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            InitializeDates();
            LoadAvailableRooms();
        }

        private void InitializeDates()
        {
            dtpCheckIn.MinDate = DateTime.Today;
            dtpCheckOut.MinDate = DateTime.Today.AddDays(1);
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);
        }

        private void LoadAvailableRooms()
        {
            var rooms = _roomRepository.GetAvailableRooms(
                hotelId: 1, // Assuming customerId is the hotelId
                from: dtpCheckIn.Value,
                to: dtpCheckOut.Value
            );

            cmbRooms.DataSource = rooms;
            cmbRooms.DisplayMember = "RoomNumber";
            cmbRooms.ValueMember = "RoomId";
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (dtpCheckOut.Value <= dtpCheckIn.Value)
            {
                MessageBox.Show("Check-out date must be after check-in date",
                    "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = new BookingRequest
            {
                RoomID = (int)cmbRooms.SelectedValue,
                CheckInDate = dtpCheckIn.Value,
                CheckOutDate = dtpCheckOut.Value,
                SpecialRequests = txtSpecialRequests.Text
            };

            var result = _bookingService.CreateBooking(_customerId, request);

            MessageBox.Show(result.Message,
                result.Success ? "Success" : "Error",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void DtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            dtpCheckOut.MinDate = dtpCheckIn.Value.AddDays(1);
            LoadAvailableRooms();
        }

        private void DtpCheckOut_ValueChanged(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }
    }
}