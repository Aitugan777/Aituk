using AitukCore.Contracts;
using APartners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class ClothPropertiesService : IClothPropertiesService
    {
        private readonly HttpClient _httpClient;

        public ClothPropertiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AColor>> GetColorsAsync()
        {
            if (ClothPropertiesCache.AColors != null)
                return ClothPropertiesCache.AColors;

            var response = await _httpClient.GetAsync("api/ClothProperties/colors");
            response.EnsureSuccessStatusCode();

            var colorContracts = await response.Content.ReadFromJsonAsync<List<ColorContract>>();
            var colors = colorContracts.Select(c => new AColor { Id = c.Id, Name = c.Name }).ToList();

            ClothPropertiesCache.AColors = colors;
            return colors;
        }

        public async Task<List<ASize>> GetSizesAsync()
        {
            if (ClothPropertiesCache.ASizes != null)
                return ClothPropertiesCache.ASizes;

            var response = await _httpClient.GetAsync("api/ClothProperties/sizes");
            response.EnsureSuccessStatusCode();

            var sizeContracts = await response.Content.ReadFromJsonAsync<List<SizeContract>>();
            var sizes = sizeContracts.Select(s => new ASize { Id = s.Id, Name = s.Name }).ToList();

            ClothPropertiesCache.ASizes = sizes;
            return sizes;
        }

        public async Task<List<ACategory>> GetCategoriesAsync()
        {
            if (ClothPropertiesCache.ACategories != null)
                return ClothPropertiesCache.ACategories;

            var response = await _httpClient.GetAsync("api/ClothProperties/categories");
            response.EnsureSuccessStatusCode();

            var categoryContracts = await response.Content.ReadFromJsonAsync<List<CategoryContract>>();
            var categories = categoryContracts.Select(c => new ACategory { Id = c.Id, Name = c.Name }).ToList();

            ClothPropertiesCache.ACategories = categories;
            return categories;
        }

        public async Task<List<AGender>> GetGendersAsync()
        {
            if (ClothPropertiesCache.AGenders != null)
                return ClothPropertiesCache.AGenders;

            var response = await _httpClient.GetAsync("api/ClothProperties/genders");
            response.EnsureSuccessStatusCode();

            var genderContracts = await response.Content.ReadFromJsonAsync<List<GenderContract>>();
            var genders = genderContracts.Select(g => new AGender { Id = g.Id, Name = g.Name }).ToList();

            ClothPropertiesCache.AGenders = genders;
            return genders;
        }
    }
}
