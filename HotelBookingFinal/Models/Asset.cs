
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingFinal.Models
{
    public class Asset
    {
        [Key]
        public int AssetID { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomID { get; set; }

        [Required]
        [Column(TypeName = "enum('Bed','Fan','AC','TV','Shower')")]
        public string AssetType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "enum('Working','Under Maintenance','Damaged')")]
        public string AssetStatus { get; set; } = "Working";

        // Navigation property
        public virtual Room? Room { get; set; }

        // Constructor for initialization
        public Asset()
        {
            // Set default values
            AssetStatus = "Working";
        }

        // Helper method to check if asset is operational
        public bool IsOperational()
        {
            return AssetStatus == "Working";
        }

        // Method to update status with validation
        public void UpdateStatus(string newStatus)
        {
            var validStatuses = new[] { "Working", "Under Maintenance", "Damaged" };
            if (!validStatuses.Contains(newStatus))
            {
                throw new ArgumentException("Invalid asset status");
            }
            AssetStatus = newStatus;
        }
    }
}