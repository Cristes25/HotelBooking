using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface IFoodOrderRepository
    {
        bool CreateFoodOrder(FoodOrder order);
        List<FoodOrder> GetFoodOrdersByBooking(int bookingId);

    }
}
