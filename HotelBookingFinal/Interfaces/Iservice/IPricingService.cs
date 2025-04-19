

namespace HotelBookingFinal.Interfaces.Iservice
{
    public interface IPricingService
    {
        decimal CalculateBookingCost(int roomId, DateTime checkIn, DateTime checkOut, bool hasFoodOrder);
    }
}
