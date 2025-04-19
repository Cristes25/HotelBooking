using HotelBookingFinal.Models;

namespace HotelBookingFinal.Interfaces.Irepos
{
    public interface ICustomerRepository
    {
        bool UpdatePassword(int customerId, string passwordHash);
        bool CreateCustomer(Customer customer);
        Customer? GetCustomerByEmail(string email);
        Customer? GetCustomerById(int customerId);
        List<Customer> GetAllCustomers();
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        bool SetPasswordResetToken(int customerId, string token, DateTime expiresAt);
        Customer? GetByResetToken(string token);
    }

}
