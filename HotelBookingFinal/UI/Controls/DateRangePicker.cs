using MaterialSkin;
using System;
using System.Windows.Forms;

namespace HotelBookingFinal.UI.Controls
{
    public partial class DateRangePicker : UserControl
    {
        public event EventHandler DatesChanged;

        public DateTime StartDate => dtpStart.Value;
        public DateTime EndDate => dtpEnd.Value;

        public DateRangePicker()
        {
            InitializeComponent();
            ApplyMaterialStyle();
        }

        private void ApplyMaterialStyle()
        {
            var skinManager = MaterialSkinManager.Instance;

            // Style DateTimePickers
            dtpStart.BackColor = skinManager.BackgroundColor;
            dtpStart.ForeColor = skinManager.TextHighEmphasisColor;
            dtpStart.Font = new Font("Roboto", 9.75F);

            dtpEnd.BackColor = skinManager.BackgroundColor;
            dtpEnd.ForeColor = skinManager.TextHighEmphasisColor;
            dtpEnd.Font = new Font("Roboto", 9.75F);

            // Style label
            lblTo.ForeColor = skinManager.TextHighEmphasisColor;
            lblTo.Font = new Font("Roboto", 9.75F);
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            DatesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}