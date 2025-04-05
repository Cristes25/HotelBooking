using System.IO;
using Newtonsoft.Json.Linq;

namespace HotelBookingFinal.Utils
{
    public static class ConfigManager
    {
        private static readonly string configPath = "C:\\Users\\crist\\source\\repos\\Cristes25\\HotelBooking\\HotelBookingFinal\\Config.json";

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
    }
}
