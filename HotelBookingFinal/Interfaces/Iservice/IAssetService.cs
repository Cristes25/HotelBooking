using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IAssetService
    {
        List<Asset> GetRoomAssets(int roomId);
        bool UpdateAssetStatus(int assetId, string newStatus);
        int AddAssetToRoom(Asset asset);
        bool IsRoomOperational(int roomId);
    }
}
