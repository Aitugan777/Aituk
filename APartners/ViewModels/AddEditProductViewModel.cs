using AitukCore.Models;
using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APartners.ViewModels
{
    /// <summary>
    /// Добавление/редактирование магазина
    /// </summary>
    public class AddEditProductViewModel : ViewModelBase
    {
        /// <summary>
        /// Признак добавления магазина
        /// </summary>
        private bool _isAddProduct { get; set; }

        /// <summary>
        /// Команда для добавления/сохранения 
        /// </summary>
        public ICommand SaveCommand { get; }
        public ICommand UpdateImageCommand { get; }

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public AProduct? Product
        {
            get => GetValue<AProduct>(nameof(Product));
            set => SetValue(value, nameof(Product));
        }

        private ImageSource _productImage;
        public ImageSource ProductImage
        {
            get => _productImage;
            set
            {
                _productImage = value;
                OnPropertyChanged(nameof(ProductImage));
            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditProductViewModel(bool isAddProduct, AProduct product)
        {
            _isAddProduct = isAddProduct;
            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());

            UpdateImageCommand = new RelayCommand(async x => await UpdateImageAsync());
            if (!isAddProduct)
                LoadImageAsync(product.Id);

            Product = product;
        }

        private async Task UpdateImageAsync()
        {

            var path = FileHelper.OpenImageFileDialog();
            if (string.IsNullOrEmpty(path)) return;

            byte[] imageBytes = File.ReadAllBytes(path);
            if (Product != null)
            {
                if (Product.Photos == null)
                {
                    Product.Photos = new APhoto()
                    {
                        Content = imageBytes,
                        ParentId = Product.Id,
                        PhotoFor = EPhotoFor.Product
                    };
                }
                else
                    Product.Photos.Content = imageBytes;
                ProductImage = LoadImage(imageBytes);
            }
        }

        private async Task LoadImageAsync(long productId)
        {
            var productService = DIContainer.GetService<IProductService>();
            var photo = await productService.GetProductPhotosAsync(productId);
            if (photo != null)
            {
                ProductImage = photo;
            }
        }

        private ImageSource LoadImage(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Сохранить/добавить товар
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await Task.Delay(1000);
            var productService = DIContainer.GetService<IProductService>();
            if (_isAddProduct)
            {
                productService.AddProduct(Product!);
            }
            else
            {
                productService.SaveProduct(Product!);
            }

            var mainViewModel = DIContainer.GetService<MainViewModel>();
            mainViewModel.SelectedUserControl = new ShopsView();
            mainViewModel.SelectedUserControl.DataContext = new ShopsViewModel();
        }
    }
}