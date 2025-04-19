using Org.BouncyCastle.Asn1.Crmf;
using MaterialSkin;
using MaterialSkin.Controls;

namespace HotelBookingFinal.UI.Forms.Admin
{
    partial class AdminDashboardForm
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
            this.adminGrid = new HotelBookingFinal.UI.Controls.CustomDataGridView();
            this.btnManageAdmins = new MaterialSkin.Controls.MaterialButton();
            this.cardActiveBookings = new HotelBookingFinal.UI.Controls.StatCard();
            this.cardAvailableRooms = new HotelBookingFinal.UI.Controls.StatCard();
            this.cardRevenueToday = new HotelBookingFinal.UI.Controls.StatCard();
            this.cardMaintenance = new HotelBookingFinal.UI.Controls.StatCard();
            ((System.ComponentModel.ISupportInitialize)(this.adminGrid)).BeginInit();
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

            // adminGrid
            this.adminGrid.Location = new System.Drawing.Point(30, 280);
            this.adminGrid.Name = "adminGrid";
            this.adminGrid.Size = new System.Drawing.Size(740, 300);
            this.adminGrid.TabIndex = 1;

            // btnManageAdmins
            this.btnManageAdmins.AutoSize = false;
            this.btnManageAdmins.Depth = 0;
            this.btnManageAdmins.HighEmphasis = true;
            this.btnManageAdmins.Icon = null;
            this.btnManageAdmins.Location = new System.Drawing.Point(30, 220);
            this.btnManageAdmins.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnManageAdmins.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnManageAdmins.Name = "btnManageAdmins";
            this.btnManageAdmins.Size = new System.Drawing.Size(180, 40);
            this.btnManageAdmins.TabIndex = 2;
            this.btnManageAdmins.Text = "MANAGE ADMINS";
            this.btnManageAdmins.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnManageAdmins.UseVisualStyleBackColor = true;
            this.btnManageAdmins.Click += new System.EventHandler(this.btnManageAdmins_Click);

            // cardActiveBookings
            this.cardActiveBookings.BackColor = System.Drawing.Color.White;
            this.cardActiveBookings.CardTitle = "Active Bookings";
            this.cardActiveBookings.Count = "0";
            this.cardActiveBookings.Icon = null;
            this.cardActiveBookings.Location = new System.Drawing.Point(30, 150);
            this.cardActiveBookings.Name = "cardActiveBookings";
            this.cardActiveBookings.Size = new System.Drawing.Size(180, 100);
            this.cardActiveBookings.TabIndex = 3;

            // cardAvailableRooms
            this.cardAvailableRooms.BackColor = System.Drawing.Color.White;
            this.cardAvailableRooms.CardTitle = "Available Rooms";
            this.cardAvailableRooms.Count = "0";
            this.cardAvailableRooms.Icon = null;
            this.cardAvailableRooms.Location = new System.Drawing.Point(220, 150);
            this.cardAvailableRooms.Name = "cardAvailableRooms";
            this.cardAvailableRooms.Size = new System.Drawing.Size(180, 100);
            this.cardAvailableRooms.TabIndex = 4;

            // cardRevenueToday
            this.cardRevenueToday.BackColor = System.Drawing.Color.White;
            this.cardRevenueToday.CardTitle = "Revenue Today";
            this.cardRevenueToday.Count = "$0";
            this.cardRevenueToday.Icon = null;
            this.cardRevenueToday.Location = new System.Drawing.Point(410, 150);
            this.cardRevenueToday.Name = "cardRevenueToday";
            this.cardRevenueToday.Size = new System.Drawing.Size(180, 100);
            this.cardRevenueToday.TabIndex = 5;

            // cardMaintenance
            this.cardMaintenance.BackColor = System.Drawing.Color.White;
            this.cardMaintenance.CardTitle = "Maintenance";
            this.cardMaintenance.Count = "0";
            this.cardMaintenance.Icon = null;
            this.cardMaintenance.Location = new System.Drawing.Point(600, 150);
            this.cardMaintenance.Name = "cardMaintenance";
            this.cardMaintenance.Size = new System.Drawing.Size(180, 100);
            this.cardMaintenance.TabIndex = 6;

            // AdminDashboardForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.cardMaintenance);
            this.Controls.Add(this.cardRevenueToday);
            this.Controls.Add(this.cardAvailableRooms);
            this.Controls.Add(this.cardActiveBookings);
            this.Controls.Add(this.btnManageAdmins);
            this.Controls.Add(this.adminGrid);
            this.Controls.Add(this.lblWelcome);
            this.Name = "AdminDashboardForm";
            this.Text = "Admin Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.adminGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

#endregion

        private MaterialSkin.Controls.MaterialLabel lblWelcome;
        private Controls.CustomDataGridView adminGrid;
        private MaterialSkin.Controls.MaterialButton btnManageAdmins;
        private Controls.StatCard cardActiveBookings;
        private Controls.StatCard cardAvailableRooms;
        private Controls.StatCard cardRevenueToday;
        private Controls.StatCard cardMaintenance;
    }

}
