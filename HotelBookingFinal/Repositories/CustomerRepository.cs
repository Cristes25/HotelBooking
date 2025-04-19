using Dapper;
using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Models;
using HotelBookingFinal.Utils;
using MySql.Data.MySqlClient;

namespace HotelBookingFinal.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }

        public bool CreateCustomer(Customer customer)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                @"INSERT INTO Customers 
                (FirstName, LastName, Email, Phone, PasswordHash) 
                VALUES (@FirstName, @LastName, @Email, @Phone, @PasswordHash)",
                customer
            ) > 0;
        }

        public Customer? GetCustomerByEmail(string email)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<Customer>(
                "SELECT * FROM Customers WHERE Email = @Email",
                new { Email = email }
            );
        }

        public Customer? GetCustomerById(int customerId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<Customer>(
                "SELECT * FROM Customers WHERE CustomerID = @CustomerID",
                new { CustomerID = customerId }
            );
        }

        public List<Customer> GetAllCustomers()
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Query<Customer>("SELECT * FROM Customers").ToList();
        }

        public bool UpdateCustomer(Customer customer)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                @"UPDATE Customers SET 
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Phone = @Phone
                WHERE CustomerID = @CustomerID",
                customer
            ) > 0;
        }
        public bool UpdatePassword(int customerId, string newPasswordHash)
        {
            using var conn = new MySqlConnection(_connectionString);
            try
            {
                int rowsAffected = conn.Execute(
                    "UPDATE Customers SET PasswordHash = @PasswordHash WHERE CustomerID = @CustomerID",
                    new { PasswordHash = newPasswordHash, CustomerID = customerId }
                );
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating password: {ex.Message}");
                return false;
            }
        }
        public bool DeleteCustomer(int customerId)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                "DELETE FROM Customers WHERE CustomerID = @CustomerID",
                new { CustomerID = customerId }
            ) > 0;
        }
        public bool SetPasswordResetToken(int customerId, string token, DateTime expiresAt)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.Execute(
                "UPDATE Customers SET PasswordResetToken = @Token, ResetTokenExpires = @Expires WHERE CustomerID = @ID",
                new { Token = token, Expires = expiresAt, ID = customerId }
            ) > 0;
        }

        public Customer? GetByResetToken(string token)
        {
            using var conn = new MySqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<Customer>(
                "SELECT * FROM Customers WHERE PasswordResetToken = @Token AND ResetTokenExpires > NOW()",
                new { Token = token }
            );
        }
    }
}