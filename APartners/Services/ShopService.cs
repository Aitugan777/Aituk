using AitukCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class ShopService : IShopService
    {
        public List<AShop> GetShops()
        {
            return new List<AShop>()
            {
                new AShop() { Id = 1, Name = "Магаз 1", Description = "Описание 1", PersonId = 1 },
                new AShop() { Id = 2, Name = "Магаз 2", Description = "Описание 2", PersonId = 1 }
            };
        }
    }
}
