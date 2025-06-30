using AitukCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace APartners.Services
{
    public interface IShopService
    {
        Task<List<AShop>> GetShops();
        Task AddShop(AShop shop);
        Task DeleteShop(long shopId);
        Task SaveShop(AShop shop);
        Task<ImageSource> GetShopPhotoAsync(long shopId);
    }
}