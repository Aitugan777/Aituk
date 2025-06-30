using APartners.TestServices;
using APartners.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public static class DIContainer
    {
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IDialogService, DialogService>();

            //Тестовый режим
            RegisterTestServices(services);

            //Боевой режим
            //RegisterReleaseServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Регистрация тестовых сервисов
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterTestServices(ServiceCollection services)
        {
            services.AddScoped<IAuthService, TestAuthService>();
            services.AddScoped<IShopService, TestShopService>();
            services.AddScoped<IProductService, TestProductService>();
        }

        /// <summary>
        /// Регистрация боевых сервисов
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterReleaseServices(ServiceCollection services)
        {
            var apiBaseAddress = new Uri("http://94.41.23.37:5070/");

            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = apiBaseAddress;
            });

            services.AddHttpClient<IShopService, ShopService>(client =>
            {
                client.BaseAddress = apiBaseAddress;
            });
        }

        public static T GetService<T>() => ServiceProvider.GetService<T>();
    }

}
