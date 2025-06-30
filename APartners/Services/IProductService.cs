using AitukCore.Models;
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
        Task<List<AProduct>> GetProducts();
        Task AddProduct(AProduct product);
        Task SaveProduct(AProduct product);
        Task DeleteProduct(int productId);
        Task<List<ImageSource>> GetProductPhotosAsync(int productId);
    }
}
