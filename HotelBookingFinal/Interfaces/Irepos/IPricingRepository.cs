using HotelBookingFinal.Repositories;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IPricingRepository
    {
        decimal GetCurrentPrice(int hotelId, string roomCategory, string roomType);
        List<PricingRepository> GetActivePricing(int hotelId);
    }
}
