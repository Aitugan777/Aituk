using AitukCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class TestProductService : IProductService
    {
        private List<AProduct> products = new List<AProduct>();
        public void AddProduct(AProduct product)
        {
            products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = products.Where(x => x.Id == id).FirstOrDefault();
            if (product != null)
            {
                products.Remove(product);
            }
        }

        public List<AProduct> GetProducts()
        {
            return products;
        }

        public void SaveProduct(AProduct product)
        {
            int id = product.Id;
            AProduct? oldproduct = products.Where(x => x.Id == id).FirstOrDefault();
            if (oldproduct != null)
            {
                oldproduct.Name = product.Name;
                oldproduct.Description = product.Description;
            }
        }
    }
}
