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
            var apiBaseAddress = new Uri("http://94.41.23.37:5070/");

            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();

            // Регистрируем сервисы только через AddHttpClient
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = apiBaseAddress;
            });

            services.AddHttpClient<IShopService, ShopService>(client =>
            {
                client.BaseAddress = apiBaseAddress;
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>() => ServiceProvider.GetService<T>();
    }

}
