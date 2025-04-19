using HotelBookingFinal.Interfaces.Iservice;
using MaterialSkin;
using MaterialSkin.Controls;
namespace HotelBookingFinal.UI.Forms.Customer
  
{
    public partial class RegistrationForm : MaterialForm
    {
        private readonly ICustumerAuthService _customerAuthService;

        public RegistrationForm(ICustumerAuthService customerAuthService)
        {
            InitializeComponent();
            // MaterialSkin setup
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            _customerAuthService = customerAuthService;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var customer = new Models.Customer
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };

            var (success, message) = _customerAuthService.Register(customer, txtPassword.Text);

            MaterialMessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success) DialogResult = DialogResult.OK;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MaterialMessageBox.Show("Please fill all required fields", "Validation Error");
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MaterialMessageBox.Show("Passwords do not match", "Validation Error");
                return false;
            }

            return true;
        }
    }
}
