using APartners.Models;
using APartners.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace APartners.TestServices
{
    public class TestProductService : IProductService
    {
        private List<AProduct> _products;
        private long _identity;

        public TestProductService()
        {
            _products = new List<AProduct>
            {
                new AProduct { Id = 1, Name = "Laptop", Description = "Gaming laptop", Cost = 1200.0m, },
                new AProduct { Id = 2, Name = "Book", Description = "Science fiction novel", Cost = 15.0m, }
            };
            _identity = _products.Max(p => (long)p.Id);
        }

        public Task AddProduct(AProduct product)
        {
            product.Id = ++_identity;
            _products.Add(product);

            if (product.Photos != null)
            {
                //SaveProductPhotos((long)product.Id, product.Photos);
            }

            return Task.CompletedTask;
        }

        public Task SaveProduct(AProduct product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Cost = product.Cost;

                if (product.Photos != null)
                {
                    //SaveProductPhotos(product.Id, product.Photos);
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteProduct(long productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _products.Remove(product);
                //DeleteProductPhotos(productId);
            }
            return Task.CompletedTask;
        }

        public Task<List<ImageSource>> GetProductPhotosAsync(long productId)
        {
            var photos = new List<ImageSource>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{productId}_product_*.jpg");

            foreach (var file in files)
            {
                photos.Add(FileHelper.LoadImage(File.ReadAllBytes(file)));
            }

            return Task.FromResult(photos);
        }



        private void DeleteProductPhotos(long productId)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{productId}_product_*.jpg");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }


        public Task<List<AProduct>> GetProductsAsync()
        {
            return Task.FromResult(_products);
        }

        public async Task<AProduct?> GetProductAsync(long productId)
        {
            var product = _products.FirstOrDefault(x => x.Id == productId);
            product.Shops = new System.Collections.ObjectModel.ObservableCollection<AShop>( await DIContainer.GetService<IShopService>().GetShops());
            return product;
        }

        public Task<List<AProduct>> GetProductsByShopsAsync(List<long> shopIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<AProduct>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }

}
