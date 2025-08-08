using APartners.TestServices;
using APartners.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<TokenStore>();
            services.AddTransient<JwtHandler>();

            RegisterReleaseServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Регистрация боевых сервисов
        /// </summary>
        private static void RegisterReleaseServices(IServiceCollection services)
        {
            var apiBaseAddress = new Uri("http://94.41.23.37:5070/");

            var sharedHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(60)
            };

            RegisterHttpService<IAuthService, AuthService>(services, apiBaseAddress, sharedHandler, withJwt: false);
            RegisterHttpService<IShopService, ShopService>(services, apiBaseAddress, sharedHandler, withJwt: true);
            RegisterHttpService<IProductService, ProductService>(services, apiBaseAddress, sharedHandler, withJwt: true);
            RegisterHttpService<IClothPropertiesService, ClothPropertiesService>(services, apiBaseAddress, sharedHandler, withJwt: true);
        }

        private static void RegisterHttpService<TInterface, TImplementation>(
            IServiceCollection services,
            Uri baseAddress,
            SocketsHttpHandler sharedHandler,
            bool withJwt
        )
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var clientBuilder = services.AddHttpClient<TInterface, TImplementation>(client =>
            {
                client.BaseAddress = baseAddress;
            })
            .ConfigurePrimaryHttpMessageHandler(() => sharedHandler);

            if (withJwt)
            {
                clientBuilder.AddHttpMessageHandler<JwtHandler>();
            }
        }

        public static T GetService<T>() => ServiceProvider.GetRequiredService<T>();
    }

}
