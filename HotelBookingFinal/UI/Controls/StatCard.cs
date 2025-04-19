using MaterialSkin;
using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingFinal.UI.Controls
{
    public partial class StatCard : UserControl
    {
        public StatCard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public string CardTitle { get; set; } = "Title";
        public string Count { get; set; } = "0";
        public Image Icon { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var manager = MaterialSkinManager.Instance;
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(manager.BackgroundColor), ClientRectangle);

            using (var titleFont = new Font("Roboto", 10))
            using (var titleBrush = new SolidBrush(manager.TextHighEmphasisColor))
            {
                g.DrawString(CardTitle, titleFont, titleBrush, 10, 10);
            }

            using (var countFont = new Font("Roboto", 24, FontStyle.Bold))
            using (var countBrush = new SolidBrush(manager.ColorScheme.PrimaryColor))
            {
                g.DrawString(Count, countFont, countBrush, 10, 30);
            }

            if (Icon != null)
            {
                g.DrawImage(Icon, new Rectangle(Width - 50, Height - 50, 40, 40));
            }
        }
    }
}