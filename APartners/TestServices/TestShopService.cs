using APartners.Models;
using APartners.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace APartners.TestServices
{
    public class TestShopService : IShopService
    {
        public List<AShop> AllShops { get; set; }

        private long Identety { get; set; }

        public TestShopService()
        {
            string json = @"
            [
                { ""Id"": 1, ""Name"": ""TechStore"", ""Description"": ""Electronics and gadgets"", ""PersonId"": 100 },
                { ""Id"": 2, ""Name"": ""BookWorld"", ""Description"": ""Books and stationery"", ""PersonId"": 101 },
                { ""Id"": 3, ""Name"": ""FashionPoint"", ""Description"": ""Clothing and accessories"", ""PersonId"": 102 },
                { ""Id"": 4, ""Name"": ""HomeEssentials"", ""Description"": ""Furniture and kitchenware"", ""PersonId"": 103 },
                { ""Id"": 5, ""Name"": ""FoodCorner"", ""Description"": ""Groceries and snacks"", ""PersonId"": 104 }
            ]";
            Identety = 5;

            AllShops = JsonConvert.DeserializeObject<List<AShop>>(json)!;
        }

        public Task<List<AShop>> GetShops()
        {
            return Task.FromResult(AllShops);
        }
        
        public Task<AShop?> GetShop(long shopId)
        {
            var shop = AllShops.Where(x => x.Id == shopId).FirstOrDefault();
            return Task.FromResult(shop);
        }

        public Task AddShop(AShop shop)
        {
            shop.Id = ++Identety;
            AllShops.Add(shop);

            if (shop.Photos != null)
            {
                for (int i = 0; i < shop.Photos.Count; i++)
                {
                    string filePath = $"{shop.Id}_product_{i}.{FileHelper.GetImageFormat(shop.Photos[i].ConvertToBytes())}";
                    File.WriteAllBytes(filePath, shop.Photos[i].ConvertToBytes());
                }
            }

            return Task.CompletedTask;
        }

        public Task DeleteShop(long shopId)
        {
            return Task.CompletedTask;
        }

        public Task SaveShop(AShop shop)
        {
            var checkShop = AllShops.FirstOrDefault(x => x.Id == shop.Id);

            if (checkShop != null)
            {
                checkShop.Name = shop.Name;
                checkShop.Description = shop.Description;

                if (shop.Photos != null)
                {
                    for (int i = 0; i < shop.Photos.Count; i++)
                    {
                        string filePath = $"{shop.Id}_product_{i}.{FileHelper.GetImageFormat(shop.Photos[i].ConvertToBytes())}";
                        File.WriteAllBytes(filePath, shop.Photos[i].ConvertToBytes());
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
