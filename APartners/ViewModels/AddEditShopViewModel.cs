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
    public class AddEditShopViewModel : ViewModelBase
    {
        /// <summary>
        /// Признак добавления магазина
        /// </summary>
        private bool _isAddShop { get; set; }

        /// <summary>
        /// Команда для добавления/сохранения 
        /// </summary>
        public ICommand SaveCommand { get; }
        public ICommand UpdateImageCommand { get; }

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public AShop? Shop
        {
            get => GetValue<AShop>(nameof(Shop));
            set => SetValue(value, nameof(Shop));
        }

        private ImageSource _shopImage;
        public ImageSource ShopImage
        {
            get => _shopImage;
            set
            {
                _shopImage = value;
                OnPropertyChanged(nameof(ShopImage));
            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditShopViewModel(bool isAddShop, AShop shop) 
        {
            _isAddShop = isAddShop;
            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());

            UpdateImageCommand = new RelayCommand(async x => await UpdateImageAsync());
            if (!isAddShop)
                LoadImageAsync(shop.Id);

            Shop = shop;
        }

        private async Task UpdateImageAsync()
        {

            var path = FileHelper.OpenImageFileDialog();
            if (string.IsNullOrEmpty(path)) return;

            byte[] imageBytes = File.ReadAllBytes(path);
            if (Shop != null)
            {
                if (Shop.Photo == null)
                {
                    Shop.Photo = new APhoto()
                    {
                        Content = imageBytes,
                        ParentId = Shop.Id,
                        PhotoFor = EPhotoFor.Shop
                    };
                }
                else
                    Shop.Photo.Content = imageBytes;
                ShopImage = LoadImage(imageBytes);
            }
        }

        private async Task LoadImageAsync(long shopId)
        {
            var shopService = DIContainer.GetService<IShopService>();
            var photo = await shopService.GetShopPhotoAsync(shopId);
            if (photo != null)
            {
                ShopImage = photo;
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
        /// Сохранить - редактировать магазин
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await Task.Delay(1000);
            var shopService = DIContainer.GetService<IShopService>();
            if (_isAddShop)
            {
                shopService.AddShop(Shop!);
            }
            else
            {
                shopService.SaveShop(Shop!);
            }

            var mainViewModel = DIContainer.GetService<MainWindowViewModel>();
            mainViewModel.SelectedUserControl = new ShopsView();
            mainViewModel.SelectedUserControl.DataContext = new ShopsViewModel();
        }
    }
}