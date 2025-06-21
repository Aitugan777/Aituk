using AitukCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class TestShopService : IShopService
    {
        public List<AShop> AllShops { get; set; }

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

            AllShops = JsonConvert.DeserializeObject<List<AShop>>(json)!;
        }
        public List<AShop> GetShops()
        {
            return AllShops;
        }

        public void AddShop(AShop shop)
        {
            AllShops.Add(shop);
        }

        public void SaveShop(AShop shop)
        {
            var checkShop = AllShops.Where(x => x.Id == shop.Id).FirstOrDefault();

            if (checkShop != null)
            {
                checkShop.Name = shop.Name;
                checkShop.Description = shop.Description;
            }
        }

        public void DeleteShop(int id)
        {
            throw new NotImplementedException();
        }
    }
}
