using MySql.Data.MySqlClient;
using HotelBookingFinal.Utils;
using HotelBookingFinal.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingFinal.Repositories
{
    public class AdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository()
        {
            _connectionString = ConfigManager.GetConnectionString();
        }
        //C
        public bool CreateAdmin(Admin admin, string password)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM hotels WHERE HotelID = @HotelID";
                using (var checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@HotelID", admin.HotelId);
                    var count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count == 0)
                    {
                        throw new Exception("Invalid HotelId.");
                    }
                }

                string query = "INSERT INTO Administrators (HotelID, FirstName, LastName, Username, PasswordHash, Email) VALUES (@HotelID, @FirstName, @LastName, @Username, @PasswordHash, @Email)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HotelID", admin.HotelId);
                    cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                    cmd.Parameters.AddWithValue("@Username", admin.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHasher.HashPassword(password));
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //R
        public Admin GetAdminById(int adminId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Administrators WHERE AdminId=@AdminId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AdminId", adminId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Admin
                            {
                                AdminId = Convert.ToInt32(reader["AdminId"]),
                                HotelId = Convert.ToInt32(reader["HotelID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
        //R
        public List<Admin> GetAllAdmins()
        {
            List<Admin> admins = new List<Admin>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Administrators";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admins.Add(new Admin
                            {
                                AdminId = Convert.ToInt32(reader["AdminId"]),
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return admins;
        }
        //U
        public bool UpdateAdmin(Admin admin)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"UPDATE Administrators 
                                    SET HotelId = @HotelId, FirstName= @FirstName, LastName=@LastName,
                                     Username=@Username, Email=@Email
                                          WHERE AdminId= @AdminId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AdminId", admin.AdminId);
                    cmd.Parameters.AddWithValue("@HotelId", admin.HotelId);
                    cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                    cmd.Parameters.AddWithValue("@Username", admin.Username);
                    cmd.Parameters.AddWithValue("@Email", admin.Email);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool UpdatePassword(int adminId, string newPassword)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Administrators SET Password = @Password WHERE AdminId = @AdminId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", PasswordHasher.HashPassword(newPassword));
                    cmd.Parameters.AddWithValue("@AdminId", adminId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }


        }

        //D
        public bool DeleteAdmin(int AdminId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Administrators WHERE AdminId=@AdminId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AdminId", AdminId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Authentication and JWT token generation
        public string AuthenticateAdmin(string username, string password, out Admin admin)
        {
            admin = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Administrators WHERE Username=@Username";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPassword = reader["PasswordHash"].ToString();

                            if (PasswordHasher.VerifyPassword(password, hashedPassword))
                            {
                                admin = new Admin
                                {
                                    AdminId = Convert.ToInt32(reader["AdminId"]),
                                    HotelId = Convert.ToInt32(reader["HotelId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString()
                                };
                                //Generate JWT token
                                return GenerateJwtToken(admin);
                            }


                        }
                    }
                }
            }
            return null; //Authentication failed
        }

        private string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supersecretkey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public void TestRepository()
        {
            var repo = new AdminRepository();

            // Test Create
            var newAdmin = new Admin()
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@example.com",
                HotelId = 1
            };
            bool createSuccess = repo.CreateAdmin(newAdmin, "TempPassword123");
            Console.WriteLine($"Create admin: {createSuccess}");

            // Test Authentication
            string token = repo.AuthenticateAdmin("johndoe", "SecurePassword123", out Admin authenticatedAdmin);
            Console.WriteLine($"Auth result: {authenticatedAdmin != null}");
            Console.WriteLine($"JWT Token: {token?.Substring(0, 20)}...");

        }
    }
}