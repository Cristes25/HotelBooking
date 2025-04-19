using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;



namespace HotelBookingFinal.Services
{
    public class AssetService: IAssetService
    {
        private readonly IAssetRepository _assetRepo = new AssetRepository();
        private readonly IRoomRepository _roomRepo = new RoomRepository();

        // Get all assets in a room with validation
        public List<Asset> GetRoomAssets(int roomId)
        {
            if (!_roomRepo.RoomExists(roomId))
                throw new ArgumentException("Room not found");

            return _assetRepo.GetAssetsByRoom(roomId);
        }

        // Update asset status with validation
        public bool UpdateAssetStatus(int assetId, string newStatus)
        {
            var validStatuses = new[] { "Working", "Under Maintenance", "Damaged" };
            if (!validStatuses.Contains(newStatus))
                throw new ArgumentException("Invalid status");

            return _assetRepo.UpdateAssetStatus(assetId, newStatus);
        }

        // Add new asset to a room
        public int AddAssetToRoom(Asset asset)
        {
            if (!_roomRepo.RoomExists(asset.RoomID))
                throw new ArgumentException("Room not found");

            if (asset.AssetType == "Bed" && GetBedCount(asset.RoomID) >= 3)
                throw new InvalidOperationException("Maximum 3 beds per room");

            return _assetRepo.AddAsset(asset);
        }

        // Helper: Count beds in a room
        private int GetBedCount(int roomId)
        {
            return _assetRepo.GetAssetsByRoom(roomId)
                .Count(a => a.AssetType == "Bed");
        }

        // Check if room is bookable (all assets working)
        public bool IsRoomOperational(int roomId)
        {
            return _assetRepo.AreAllAssetsOperational(roomId);
        }
    }
}