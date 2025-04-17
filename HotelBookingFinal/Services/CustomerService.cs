using HotelBookingFinal.Models;
using HotelBookingFinal.Repositories;
using System;

namespace HotelBookingFinal.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepo;
        private readonly BookingRepository _bookingRepo;

        public CustomerService()
        {
            _customerRepo = new CustomerRepository();
            _bookingRepo = new BookingRepository();
        }

        public (bool Success, string Message) Register(Customer customer, string password)
        {
            try
            {
                if (_customerRepo.GetCustomerByEmail(customer.Email) != null)
                    return (false, "Email already registered");

                customer.SetPassword(password);
                return _customerRepo.CreateCustomer(customer)
                    ? (true, "Registration successful")
                    : (false, "Registration failed");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public Customer? Login(string email, string password)
        {
            var customer = _customerRepo.GetCustomerByEmail(email);
            return customer?.VerifyPassword(password) == true ? customer : null;
        }

        public List<Booking> GetCustomerBookings(int customerId)
        {
            return _bookingRepo.GetCustomerBookings(customerId);
        }

        public (bool Success, string Message) UpdateProfile(Customer customer)
        {
            try
            {
                var existing = _customerRepo.GetCustomerByEmail(customer.Email);
                if (existing != null && existing.CustomerID != customer.CustomerID)
                    return (false, "Email already in use");

                return _customerRepo.UpdateCustomer(customer)
                    ? (true, "Profile updated successfully")
                    : (false, "Profile update failed");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public bool ChangePassword(int customerId, string newPassword)
        {
            var customer = _customerRepo.GetCustomerById(customerId);
            if (customer == null) return false;

            customer.SetPassword(newPassword);
            return _customerRepo.UpdatePassword(customerId, customer.PasswordHash);
        }
    }
}