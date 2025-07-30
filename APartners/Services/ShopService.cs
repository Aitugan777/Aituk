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

        private void AddAuthHeader()
        {
            var token = _authService.GetToken();
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task AddShop(AShop shop)
        {
            AddAuthHeader();
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
            AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/Shop/{shopId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AShop>> GetShops()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("api/Shop/compact");
            response.EnsureSuccessStatusCode();

            var compactContracts = await response.Content.ReadFromJsonAsync<List<ShopCompactContract>>();
            return compactContracts.Select(c => new AShop(c)).ToList();
        }


        public async Task SaveShop(AShop shop)
        {
            AddAuthHeader();
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
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/Shop/{shopId}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {error}");
            }

            var contract = await response.Content.ReadFromJsonAsync<ShopContract>();
            return new AShop(contract);
        }
    }

}
