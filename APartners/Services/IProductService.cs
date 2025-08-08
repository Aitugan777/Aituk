using APartners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace APartners.Services
{
    public interface IProductService
    {
        Task<List<AProduct>> GetProductsByShopsAsync(List<long> shopIds);
        Task<List<AProduct>> GetAllProductsAsync();
        Task<AProduct> GetProductAsync(long productId);
        Task AddProduct(AProduct product);
        Task SaveProduct(AProduct product);
        Task DeleteProduct(long productId);
    }
}
