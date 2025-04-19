using MaterialSkin;
using System.Windows.Forms;

namespace HotelBookingFinal.UI.Controls
{
    public class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            // Material Skin Styling
            var manager = MaterialSkinManager.Instance;

            this.BorderStyle = BorderStyle.None;
            this.BackgroundColor = manager.BackgroundColor;
            this.GridColor = manager.DividersColor;

            // Header Styling
            this.ColumnHeadersDefaultCellStyle.BackColor = manager.ColorScheme.PrimaryColor;
            this.ColumnHeadersDefaultCellStyle.ForeColor = manager.ColorScheme.TextColor;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.ColumnHeadersHeight = 40;

            // Cell Styling
            this.DefaultCellStyle.BackColor = manager.BackgroundColor;
            this.DefaultCellStyle.ForeColor = manager.TextHighEmphasisColor;
            this.DefaultCellStyle.SelectionBackColor = manager.ColorScheme.LightPrimaryColor;
            this.DefaultCellStyle.SelectionForeColor = manager.TextHighEmphasisColor;

            // Row Styling
            this.RowHeadersVisible = false;
            this.EnableHeadersVisualStyles = false;
            this.RowTemplate.Height = 36;
            this.AlternatingRowsDefaultCellStyle.BackColor = manager.BackgroundAlternativeColor;
        }
    }
}