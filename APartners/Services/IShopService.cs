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
        /// <summary>
        /// Вернуть все магазины по токену пользователя
        /// </summary>
        /// <param name="token">токен</param>
        /// <returns>магазины</returns>
        List<AShop> GetShops();

        void SaveShop(AShop shop);
        void AddShop(AShop shop);
        void DeleteShop(int id);
    }
}