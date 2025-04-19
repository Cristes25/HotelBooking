namespace HotelBookingFinal.UI.Forms.Customer
{
    partial class CustomerDashboardForm
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
            this.lblWelcome = new MaterialSkin.Controls.MaterialLabel();
            this.bookingGrid = new HotelBookingFinal.UI.Controls.CustomDataGridView();
            this.btnNewBooking = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.bookingGrid)).BeginInit();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Depth = 0;
            this.lblWelcome.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(30, 80);
            this.lblWelcome.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 48);
            this.lblWelcome.TabIndex = 0;

            // bookingGrid
            this.bookingGrid.Location = new System.Drawing.Point(30, 150);
            this.bookingGrid.Name = "bookingGrid";
            this.bookingGrid.Size = new System.Drawing.Size(740, 350);
            this.bookingGrid.TabIndex = 1;

            // btnNewBooking
            this.btnNewBooking.AutoSize = false;
            this.btnNewBooking.Depth = 0;
            this.btnNewBooking.HighEmphasis = true;
            this.btnNewBooking.Icon = null;
            this.btnNewBooking.Location = new System.Drawing.Point(30, 520);
            this.btnNewBooking.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnNewBooking.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.Size = new System.Drawing.Size(200, 40);
            this.btnNewBooking.TabIndex = 2;
            this.btnNewBooking.Text = "NEW BOOKING";
            this.btnNewBooking.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnNewBooking.UseVisualStyleBackColor = true;
            this.btnNewBooking.Click += new System.EventHandler(this.btnNewBooking_Click);

            // CustomerDashboard
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnNewBooking);
            this.Controls.Add(this.bookingGrid);
            this.Controls.Add(this.lblWelcome);
            this.Name = "CustomerDashboard";
            this.Text = "My Bookings";
            ((System.ComponentModel.ISupportInitialize)(this.bookingGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel lblWelcome;
        private Controls.CustomDataGridView bookingGrid;
        private MaterialSkin.Controls.MaterialButton btnNewBooking;

    }
}