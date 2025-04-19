using MaterialSkin.Controls;

namespace HotelBookingFinal.UI.Controls
{
    partial class RoomCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private MaterialLabel lblNumber;
        private MaterialLabel lblType;
        private MaterialLabel lblPrice;
        private PictureBox picRoom;
        private MaterialButton btnBook;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Component Designer generated code
        private void InitializeComponent()
        {
            lblNumber = new MaterialLabel();
            lblType = new MaterialLabel();
            lblPrice = new MaterialLabel();
            picRoom = new PictureBox();
            btnBook = new MaterialButton();
            ((System.ComponentModel.ISupportInitialize)picRoom).BeginInit();
            SuspendLayout();
            // 
            // lblNumber
            // 
            lblNumber.AutoSize = true;
            lblNumber.Depth = 0;
            lblNumber.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblNumber.Location = new Point(10, 10);
            lblNumber.MouseState = MaterialSkin.MouseState.HOVER;
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(74, 19);
            lblNumber.TabIndex = 0;
            lblNumber.Text = "Room 101";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Depth = 0;
            lblType.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblType.Location = new Point(10, 45);
            lblType.MouseState = MaterialSkin.MouseState.HOVER;
            lblType.Name = "lblType";
            lblType.Size = new Size(95, 19);
            lblType.TabIndex = 1;
            lblType.Text = "Deluxe Room";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Depth = 0;
            lblPrice.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblPrice.Location = new Point(10, 80);
            lblPrice.MouseState = MaterialSkin.MouseState.HOVER;
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(80, 19);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "$199/night";
            // 
            // picRoom
            // 
            picRoom.Location = new Point(150, 10);
            picRoom.Name = "picRoom";
            picRoom.Size = new Size(120, 100);
            picRoom.SizeMode = PictureBoxSizeMode.Zoom;
            picRoom.TabIndex = 3;
            picRoom.TabStop = false;
            // 
            // btnBook
            // 
            btnBook.AutoSize = false;
            btnBook.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnBook.Density = MaterialButton.MaterialButtonDensity.Default;
            btnBook.Depth = 0;
            btnBook.HighEmphasis = true;
            btnBook.Icon = null;
            btnBook.Location = new Point(10, 120);
            btnBook.Margin = new Padding(4, 6, 4, 6);
            btnBook.MouseState = MaterialSkin.MouseState.HOVER;
            btnBook.Name = "btnBook";
            btnBook.NoAccentTextColor = Color.Empty;
            btnBook.Size = new Size(260, 36);
            btnBook.TabIndex = 4;
            btnBook.Text = "BOOK NOW";
            btnBook.Type = MaterialButton.MaterialButtonType.Contained;
            btnBook.UseAccentColor = false;
            btnBook.UseVisualStyleBackColor = true;
            btnBook.Click += BtnBook_Click;
            // 
            // RoomCard
            // 
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnBook);
            Controls.Add(picRoom);
            Controls.Add(lblPrice);
            Controls.Add(lblType);
            Controls.Add(lblNumber);
            Name = "RoomCard";
            Size = new Size(823, 425);
            ((System.ComponentModel.ISupportInitialize)picRoom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

    }
}

