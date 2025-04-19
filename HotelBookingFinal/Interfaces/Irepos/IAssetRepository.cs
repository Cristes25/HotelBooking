using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IAssetRepository
    {
        Asset GetAssetById(int assetId);
        List<Asset> GetAssetsByRoom(int roomId);
        bool UpdateAssetStatus(int assetId, string newStatus);
        bool AreAllAssetsOperational(int roomId);
        int AddAsset(Asset asset);
        bool DeleteAsset(int assetId);
    }
}
