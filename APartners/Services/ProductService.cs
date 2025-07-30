using AitukCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APartners.Models;
using System.Net.Http.Json;

namespace APartners.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public ProductService(HttpClient httpClient, IAuthService authService)
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

        // Возвращаем полный список продуктов с компактным контрактом (если есть),
        // но API отдаёт компактный контракт только через POST by-shops,
        // тут предполагаю GetProducts без фильтра - полный список с компактными контрактами.
        public async Task<List<AProduct>> GetProductsAsync()
        {
            AddAuthHeader();

            // Если есть отдельный эндпоинт для всех продуктов компактных:
            var response = await _httpClient.GetAsync("api/Product");
            response.EnsureSuccessStatusCode();

            var compactContracts = await response.Content.ReadFromJsonAsync<List<ProductCompactContract>>();
            return compactContracts.Select(c => new AProduct(c)).ToList();
        }

        // Получение полного продукта по id
        public async Task<AProduct> GetProductAsync(long productId)
        {
            AddAuthHeader();

            var response = await _httpClient.GetAsync($"api/Product/{productId}");
            if (!response.IsSuccessStatusCode) return null;

            var contract = await response.Content.ReadFromJsonAsync<ProductContract>();
            if (contract == null) return null;

            return new AProduct(contract);
        }

        // Добавление нового продукта, отправляем контракт
        public async Task AddProduct(AProduct product)
        {
            AddAuthHeader();

            // Предполагаем, что есть метод для конвертации AProduct -> ProductContract (нужно реализовать)
            var contract = product.ToContract();

            var response = await _httpClient.PostAsJsonAsync("api/Product", contract);
            response.EnsureSuccessStatusCode();
        }

        // Сохранение изменений продукта
        public async Task SaveProduct(AProduct product)
        {
            AddAuthHeader();

            var contract = product.ToContract();

            var response = await _httpClient.PutAsJsonAsync($"api/Product/{product.Id}", contract);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка {response.StatusCode}: {error}");
            }
        }

        public async Task DeleteProduct(long productId)
        {
            AddAuthHeader();

            var response = await _httpClient.DeleteAsync($"api/Product/{productId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
