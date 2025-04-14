using System.IO;
using Newtonsoft.Json.Linq;

namespace HotelBookingFinal.Utils
{
    public static class ConfigManager
    {
        private static readonly string configPath = "C:\\Users\\crist\\source\\repos\\Cristes25\\HotelBooking\\HotelBookingFinal\\Config.json";
        private static readonly JObject config;
        static ConfigManager()
        {
            try
            {
                string json = File.ReadAllText(configPath);
                config = JObject.Parse(json);
            }
            catch
            {
                throw new System.Exception("Error loading configuration file.");
            }
        }

        public static string GetConnectionString()
            
        {
            try
            {
                string json = File.ReadAllText(configPath);
                JObject config = JObject.Parse(json);

                string server = config["Database"]["Server"].ToString();
                string database = config["Database"]["Database"].ToString();
                string user = config["Database"]["User"].ToString();
                string password = config["Database"]["Password"].ToString();

                return $"server={server}; database={database}; user={user}; password={password};";
            }
            catch
            {
                throw new System.Exception("Error loading database configuration.");
            }
        }
        // New email configuration methods
        public static string SmtpServer => GetEmailValue("SmtpServer");
        public static int SmtpPort => int.Parse(GetEmailValue("SmtpPort"));
        public static string FromEmail => GetEmailValue("FromEmail");
        public static string SmtpUsername => GetEmailValue("Username");
        public static string SmtpPassword => GetEmailValue("Password");
        public static bool EnableSsl => bool.Parse(GetEmailValue("EnableSsl") ?? "false");
        public static int SmtpTimeout => int.Parse(GetEmailValue("Timeout") ?? "10000"); // Default to 10 seconds
        private static string GetEmailValue(string key)
        {
            try
            {
                return config["Email"]?[key]?.ToString() ??
                    throw new Exception($"Missing email configuration: {key}");
            }
            catch
            {
                throw new Exception($"Error reading email configuration: {key}");
            }
        }
        // Generic configuration access
        public static string GetValue(string section, string key)
        {
            try
            {
                return config[section]?[key]?.ToString() ??
                    throw new Exception($"Missing configuration: {section}:{key}");
            }
            catch
            {
                throw new Exception($"Error reading configuration: {section}:{key}");
            }
        }

    }
}
