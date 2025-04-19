using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace HotelBookingFinal.UI.Forms
{
    partial class BookingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

#region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.cmbRooms = new MaterialSkin.Controls.MaterialComboBox();
            this.txtSpecialRequests = new MaterialSkin.Controls.MaterialTextBox();
            this.btnBook = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();

            // dtpCheckIn
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(30, 100);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckIn.TabIndex = 0;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.DtpCheckIn_ValueChanged);

            // dtpCheckOut
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(30, 160);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckOut.TabIndex = 1;
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.DtpCheckOut_ValueChanged);

            // cmbRooms
            this.cmbRooms.AutoResize = false;
            this.cmbRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRooms.Depth = 0;
            this.cmbRooms.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRooms.DropDownHeight = 174;
            this.cmbRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRooms.DropDownWidth = 121;
            this.cmbRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.cmbRooms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbRooms.FormattingEnabled = true;
            this.cmbRooms.Hint = "Select Room";
            this.cmbRooms.IntegralHeight = false;
            this.cmbRooms.ItemHeight = 43;
            this.cmbRooms.Location = new System.Drawing.Point(30, 220);
            this.cmbRooms.MaxDropDownItems = 4;
            this.cmbRooms.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbRooms.Name = "cmbRooms";
            this.cmbRooms.Size = new System.Drawing.Size(200, 49);
            this.cmbRooms.StartIndex = 0;
            this.cmbRooms.TabIndex = 2;

            // txtSpecialRequests
            this.txtSpecialRequests.AnimateReadOnly = false;
            this.txtSpecialRequests.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpecialRequests.Depth = 0;
            this.txtSpecialRequests.Font = new System.Drawing.Font("Roboto", 16F);
            this.txtSpecialRequests.Hint = "Special Requests (Optional)";
            this.txtSpecialRequests.LeadingIcon = null;
            this.txtSpecialRequests.Location = new System.Drawing.Point(30, 300);
            this.txtSpecialRequests.MaxLength = 500;
            this.txtSpecialRequests.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSpecialRequests.Multiline = true;
            this.txtSpecialRequests.Name = "txtSpecialRequests";
            this.txtSpecialRequests.Size = new System.Drawing.Size(300, 100);
            this.txtSpecialRequests.TabIndex = 3;
            this.txtSpecialRequests.Text = "";

            // btnBook
            this.btnBook.AutoSize = false;
            this.btnBook.Depth = 0;
            this.btnBook.HighEmphasis = true;
            this.btnBook.Icon = null;
            this.btnBook.Location = new System.Drawing.Point(30, 420);
            this.btnBook.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBook.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(300, 50);
            this.btnBook.TabIndex = 4;
            this.btnBook.Text = "BOOK NOW";
            this.btnBook.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.BtnBook_Click);

            // materialLabel1
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F);
            this.materialLabel1.Location = new System.Drawing.Point(30, 70);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(89, 29);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "Check In";

            // materialLabel2
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F);
            this.materialLabel2.Location = new System.Drawing.Point(30, 130);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(108, 29);
            this.materialLabel2.TabIndex = 6;
            this.materialLabel2.Text = "Check Out";

            // materialLabel3
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F);
            this.materialLabel3.Location = new System.Drawing.Point(30, 190);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(72, 29);
            this.materialLabel3.TabIndex = 7;
            this.materialLabel3.Text = "Room";

            // materialLabel4
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold);
            this.materialLabel4.Location = new System.Drawing.Point(30, 20);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(149, 48);
            this.materialLabel4.TabIndex = 8;
            this.materialLabel4.Text = "BOOKING";

            // BookingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.txtSpecialRequests);
            this.Controls.Add(this.cmbRooms);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.dtpCheckIn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Booking";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private MaterialSkin.Controls.MaterialComboBox cmbRooms;
        private MaterialSkin.Controls.MaterialTextBox txtSpecialRequests;
        private MaterialSkin.Controls.MaterialButton btnBook;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;

    }
}