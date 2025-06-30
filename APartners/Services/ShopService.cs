using AitukCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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
                // Обновляем существующий токен
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task AddShop(AShop shop)
        {
            AddAuthHeader();
            var response = await _httpClient.PostAsJsonAsync("api/AShop", shop);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteShop(long shopId)
        {
            AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/AShop/{shopId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AShop>> GetShops()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("api/AShop");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AShop>>();
        }

        public async Task SaveShop(AShop shop)
        {
            AddAuthHeader();

            //var json = JsonSerializer.Serialize(shop);
            //Console.WriteLine("Отправляем на сервер SaveShop JSON:");
            //Console.WriteLine(json);

            var response = await _httpClient.PutAsJsonAsync($"api/AShop/{shop.Id}", shop);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {error}");
            }
        }

        public Task SavePhotoAsync(byte[] photoBytes, long shopId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageSource> GetShopPhotoAsync(long shopId)
        {
            throw new NotImplementedException();
        }
    }
}
