using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Services;
using HotelBookingFinal.UI.Forms.Admin;
using HotelBookingFinal.UI.Forms.Auth;
using HotelBookingFinal.UI.Forms.Customer;
using HotelBookingFinal.UI.Forms;
using HotelBookingFinal.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace HotelBookingFinal
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register repositories with their interfaces
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IFoodOrderRepository, FoodOrderRepository>();

            // Register services
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustumerAuthService, CustomerAuthService>();
            services.AddTransient<IAdminService, AdminServices>();
            services.AddTransient<IBookingService, BookingService>();

            // Register forms
            services.AddTransient<LoginForm>();
            services.AddTransient<RegistrationForm>();
            services.AddTransient<CustomerDashboardForm>();
            services.AddTransient<BookingForm>();
            services.AddTransient<AdminDashboardForm>();

            // Add connection string
            var connectionString = ConfigManager.GetConnectionString();
            services.AddSingleton(connectionString);
        }
    }
}