using HotelBookingFinal.Interfaces.Irepos;
using HotelBookingFinal.Interfaces.Iservice;
using HotelBookingFinal.Repositories;
using HotelBookingFinal.Services;
using HotelBookingFinal.UI.Forms;
using HotelBookingFinal.UI.Forms.Admin;
using HotelBookingFinal.UI.Forms.Auth;
using HotelBookingFinal.UI.Forms.Customer;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace HotelBookingFinal
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register Repositories (concrete implementations)
            services.AddTransient<CustomerRepository>();
            services.AddTransient<AdminRepository>();
            services.AddTransient<BookingRepository>();
            services.AddTransient<RoomRepository>();
            services.AddTransient<FoodOrderRepository>();

            // Register Interfaces
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IFoodOrderRepository, FoodOrderRepository>();

            // Register Services
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustumerAuthService, CustomerAuthService>();
            services.AddTransient<IAdminService, AdminServices>();
            services.AddTransient<IBookingService, BookingService>();

            // Register Forms
            services.AddTransient<LoginForm>();
            services.AddTransient<RegistrationForm>();
            services.AddTransient<CustomerDashboardForm>();
            services.AddTransient<BookingForm>();
            services.AddTransient<AdminDashboardForm>();
        }
    }
}