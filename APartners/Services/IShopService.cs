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
        Task<List<AShop>> GetShops();
        Task AddShop(AShop shop);
        Task DeleteShop(long shopId);
        Task SaveShop(AShop shop);
        Task<byte[]?> GetShopPhotoAsync(long shopId);
    }
}