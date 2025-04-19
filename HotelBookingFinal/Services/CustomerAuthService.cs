
using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;

namespace HotelBookingFinal.Services
{
    public class CustomerAuthService: ICustumerAuthService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerAuthService (ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public Customer? Login(string email, string password)
        {
            var customer = _customerRepo.GetCustomerByEmail(email);
            return (bool)(customer.VerifyPassword(password)) == true ? customer : null;
        }

        public (bool Success, string Message) Register(Customer customer, string password)
        {
            if (_customerRepo.GetCustomerByEmail(customer.Email) != null)
                return (false, "Email already registered");

            customer.SetPassword(password);
            return _customerRepo.CreateCustomer(customer)
                ? (true, "Registration successful")
                : (false, "Registration failed");
        }
        public string GeneratePasswordResetToken(string email)
        {
            var customer = _customerRepo.GetCustomerByEmail(email);
            if (customer == null) return null;

            var token = Guid.NewGuid().ToString("N");
            customer.PasswordResetToken = token;
            _customerRepo.UpdateCustomer(customer);
            return token;
        }
    }
}