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

        public List<AColor> GetColors()
        {
            if (ClothPropertiesCache.AColors != null)
                return ClothPropertiesCache.AColors;

            var response = _httpClient.GetAsync("api/ClothProperties/colors").Result;
            response.EnsureSuccessStatusCode();

            var colorContracts = response.Content.ReadFromJsonAsync<List<ColorContract>>().Result;
            var colors = colorContracts.Select(c => new AColor { Id = c.Id, Name = c.Name }).ToList();

            ClothPropertiesCache.AColors = colors;
            return colors;
        }

        public List<ASize> GetSizes()
        {
            if (ClothPropertiesCache.ASizes != null)
                return ClothPropertiesCache.ASizes;

            var response = _httpClient.GetAsync("api/ClothProperties/sizes").Result;
            response.EnsureSuccessStatusCode();

            var sizeContracts = response.Content.ReadFromJsonAsync<List<SizeContract>>().Result;
            var sizes = sizeContracts.Select(s => new ASize { Id = s.Id, Name = s.Name }).ToList();

            ClothPropertiesCache.ASizes = sizes;
            return sizes;
        }

        public List<ACategory> GetCategories()
        {
            if (ClothPropertiesCache.ACategories != null)
                return ClothPropertiesCache.ACategories;

            var response = _httpClient.GetAsync("api/ClothProperties/categories").Result;
            response.EnsureSuccessStatusCode();

            var categoryContracts = response.Content.ReadFromJsonAsync<List<CategoryContract>>().Result;
            var categories = categoryContracts.Select(c => new ACategory { Id = c.Id, Name = c.Name }).ToList();

            ClothPropertiesCache.ACategories = categories;
            return categories;
        }

        public List<AGender> GetGenders()
        {
            if (ClothPropertiesCache.AGenders != null)
                return ClothPropertiesCache.AGenders;

            var response = _httpClient.GetAsync("api/ClothProperties/genders").Result;
            response.EnsureSuccessStatusCode();

            var genderContracts = response.Content.ReadFromJsonAsync<List<GenderContract>>().Result;
            var genders = genderContracts.Select(g => new AGender { Id = g.Id, Name = g.Name }).ToList();

            ClothPropertiesCache.AGenders = genders;
            return genders;
        }
    }


}
