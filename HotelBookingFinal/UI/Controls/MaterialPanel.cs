using MaterialSkin;
using MaterialSkin.Controls;

namespace HotelBookingFinal.UI.Controls
{
    public class MaterialPanel : Panel
    {
        public MaterialPanel()
        {
            this.BackColor = MaterialSkinManager.Instance.BackgroundColor;
            this.BorderStyle = BorderStyle.None;
        }
      
    }
}
