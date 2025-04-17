using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using System;

namespace HotelBookingFinal.Services
{
    public class PricingService
    {
        private readonly PricingRepository _pricingRepo = new PricingRepository();
        private readonly AssetRepository _assetRepo = new AssetRepository();

        public decimal CalculateBookingCost(int roomId, DateTime checkIn, DateTime checkOut, bool hasFoodOrder)
        {
            var room = new RoomRepository().GetRoomById(roomId);
            if (room == null) throw new ArgumentException("Room not found");
            int hotelId = room.Floor?.HotelID ?? throw new Exception("Could not determine hotel");
            int nights = (checkOut - checkIn).Days;
            decimal basePrice = _pricingRepo.GetCurrentPrice(
                room.RoomID,
                room.Category.ToString(),
                room.Type?.ToString()
            );

            // Additional charges
            decimal assetPremium = CalculateAssetPremium(roomId);
            decimal foodCharge = hasFoodOrder ? 15.00m : 0;

            return (basePrice + assetPremium) * nights + foodCharge;
        }

        private decimal CalculateAssetPremium(int roomId)
        {
            var assets = _assetRepo.GetAssetsByRoom(roomId);
            decimal premium = 0;

            foreach (var asset in assets)
            {
                premium += asset.AssetType switch
                {
                    "AC" => 10.00m,
                    "TV" => 5.00m,
                    _ => 0
                };
            }

            return premium;
        }
    }
}