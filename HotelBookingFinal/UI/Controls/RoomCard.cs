using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace HotelBookingFinal.UI.Controls
{
    public partial class RoomCard : UserControl
    {
        public event EventHandler BookNowClicked;

        public RoomCard()
        {
            InitializeComponent();
        }

        public string RoomNumber
        {
            get => lblNumber.Text;
            set => lblNumber.Text = value;
        }

        public string RoomType
        {
            get => lblType.Text;
            set => lblType.Text = value;
        }

        public string Price
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }

        public Image RoomImage
        {
            get => picRoom.Image;
            set => picRoom.Image = value;
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            BookNowClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}