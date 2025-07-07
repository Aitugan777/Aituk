using AitukCore.Models;
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
                new AProduct { Id = 1, Name = "Laptop", Description = "Gaming laptop", Cost = 1200.0m, Count = 10, ShopId = 1, CategoryId = 1 },
                new AProduct { Id = 2, Name = "Book", Description = "Science fiction novel", Cost = 15.0m, Count = 200, ShopId = 2, CategoryId = 2 }
            };
            _identity = _products.Max(p => p.Id);
        }

        public Task AddProduct(AProduct product)
        {
            product.Id = ++_identity;
            _products.Add(product);

            if (product.Photos != null)
            {
                SaveProductPhotos(product.Id, product.Photos);
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
                existing.Count = product.Count;
                existing.ShopId = product.ShopId;
                existing.CategoryId = product.CategoryId;

                if (product.Photos != null)
                {
                    SaveProductPhotos(product.Id, product.Photos);
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


        private void SaveProductPhotos(long productId, List<APhoto> photos)
        {
            //DeleteProductPhotos(productId); // удалить старые фото

            for (int i = 0; i < photos.Count; i++)
            {
                string filePath = $"{productId}_product_{i}.{FileHelper.GetImageFormat(photos[i].Content)}";
                File.WriteAllBytes(filePath, photos[i].Content);
            }
        }

        private void DeleteProductPhotos(long productId)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{productId}_product_*.jpg");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        public Task<List<AProduct>> GetProductsAsync(AProductFilter filter)
        {
            var products = _products.Where(x => x.ShopId == filter.ShopId).ToList();
            return Task.FromResult(products);
        }

        public Task<List<AProduct>> GetProductsAsync()
        {
            return Task.FromResult(_products);
        }
    }

}
