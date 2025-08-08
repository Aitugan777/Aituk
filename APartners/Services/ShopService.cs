using AitukCore.Contracts;
using APartners.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace APartners.Services
{
    public class ShopService : IShopService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public ShopService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task AddShop(AShop shop)
        {
            var contract = shop.ToContract();
            var json = System.Text.Json.JsonSerializer.Serialize(contract, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("Отправляем JSON:");
            Console.WriteLine(json);
            var response = await _httpClient.PostAsJsonAsync("api/Shop", contract);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {errorText}");
            }
        }

        public async Task DeleteShop(long shopId)
        {
            var response = await _httpClient.DeleteAsync($"api/Shop/{shopId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AShop>?> GetShops()
        {
            var response = await _httpClient.GetAsync("api/Shop/compact");
            response.EnsureSuccessStatusCode();

            var compactContracts = await response.Content.ReadFromJsonAsync<List<ShopCompactContract>>();
            return compactContracts?.Select(c => new AShop(c)).ToList();
        }

        public async Task SaveShop(AShop shop)
        {
            var contract = shop.ToContract();

            var response = await _httpClient.PutAsJsonAsync($"api/Shop/{shop.Id}", contract);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {error}");
            }
        }

        public async Task<AShop> GetShop(long shopId)
        {
            var response = await _httpClient.GetAsync($"api/Shop/{shopId}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {error}");
            }

            var contract = await response.Content.ReadFromJsonAsync<ShopContract>();
            return new AShop(contract);
        }

        public async Task<PositionContract?> GetCoordinatesByAddressAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return null;

            var url = $"api/geo/coords?address={Uri.EscapeDataString(address)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка запроса к GeoController: {error}");
                return null;
            }

            var position = await response.Content.ReadFromJsonAsync<PositionContract>();
            return position;
        }

        public async Task<List<AContactType>?> GetContactTypesAsync()
        {
            var response = await _httpClient.GetAsync("api/Shop/contactTypes");
            response.EnsureSuccessStatusCode();

            var contactTypes = await response.Content.ReadFromJsonAsync<List<ContactTypeContract>>();
            return contactTypes?.Select(c => new AContactType(c)).ToList();
        }
    }
}
