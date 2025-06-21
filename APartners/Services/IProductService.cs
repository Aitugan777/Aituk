using AitukCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public interface IProductService
    {
        List<AProduct> GetProducts();

        void AddProduct(AProduct product);
        void SaveProduct(AProduct product);
        void DeleteProduct(int id);
    }
}
