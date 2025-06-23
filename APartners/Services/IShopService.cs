using AitukCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public interface IShopService
    {
        Task AddShop(AShop shop);
        Task DeleteShop(int id);
        Task<List<AShop>> GetShops();
        Task SaveShop(AShop shop);
    }

}