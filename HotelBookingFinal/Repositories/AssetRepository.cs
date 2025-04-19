using Dapper;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;
using HotelBookingFinal.Interfaces.Irepos;
namespace HotelBookingFinal.Repositories
{
    public class AssetRepository: IAssetRepository
    {
        private readonly string _connectionString;

        public AssetRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        // Get single asset by ID
        public Asset GetAssetById(int assetId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.QueryFirstOrDefault<Asset>(
                    "SELECT * FROM Assets WHERE AssetID = @AssetID",
                    new { AssetID = assetId }
                );
            }
        }

        // Get all assets in a room
        public List<Asset> GetAssetsByRoom(int roomId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Query<Asset>(
                    "SELECT * FROM Assets WHERE RoomID = @RoomID ORDER BY AssetType",
                    new { RoomID = roomId }
                ).ToList();
            }
        }

        // Update asset status
        public bool UpdateAssetStatus(int assetId, string newStatus)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Execute(
                    "UPDATE Assets SET AssetStatus = @Status WHERE AssetID = @AssetID",
                    new { Status = newStatus, AssetID = assetId }
                ) > 0;
            }
        }

        // Check if all assets in a room are operational
        public bool AreAllAssetsOperational(int roomId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return !conn.ExecuteScalar<bool>(
                    @"SELECT EXISTS(
                        SELECT 1 FROM Assets 
                        WHERE RoomID = @RoomID 
                        AND AssetStatus != 'Working'
                    )",
                    new { RoomID = roomId }
                );
            }
        }

        // Add new asset to a room
        public int AddAsset(Asset asset)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.ExecuteScalar<int>(
                    @"INSERT INTO Assets 
                    (RoomID, AssetType, AssetStatus, Quantity)
                    VALUES (@RoomID, @AssetType, @AssetStatus, @Quantity);
                    SELECT LAST_INSERT_ID();",
                    asset
                );
            }
        }

        // Remove asset
        public bool DeleteAsset(int assetId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Execute(
                    "DELETE FROM Assets WHERE AssetID = @AssetID",
                    new { AssetID = assetId }
                ) > 0;
            }
        }
    }
}