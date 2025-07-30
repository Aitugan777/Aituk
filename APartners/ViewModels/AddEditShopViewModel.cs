using AitukCore.Contracts;
using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public ImageSource? SelectedImage
        {
            get => GetValue<ImageSource>(nameof(SelectedImage));
            set => SetValue(value, nameof(SelectedImage));
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditShopViewModel(bool isAddShop, AShop shop) 
        {
            _isAddShop = isAddShop;
            Shop = shop;

            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());
            UpdateImageCommand = new RelayCommand(x => UpdateImage());
        }

        private void UpdateImage()
        {
            var paths = FileHelper.OpenMultipleImageFileDialog();
            if (paths == null || paths.Length == 0) return;

            if (Shop != null)
            {
                if (Shop.Photos == null)
                    Shop.Photos = new ObservableCollection<ImageSource>();

                foreach (var path in paths)
                {
                    byte[] imageBytes = File.ReadAllBytes(path);

                    Shop.Photos.Add(imageBytes.ConvertToImageSource());
                }
            }
        }

        /// <summary>
        /// Сохранить - редактировать магазин
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            using (new WaitIndicator("Сохранение магазина..."))
            {
                var shopService = DIContainer.GetService<IShopService>();
                if (_isAddShop)
                {
                    await shopService.AddShop(Shop!);
                }
                else
                {
                    await shopService.SaveShop(Shop!);
                }

            }

            var mainViewModel = DIContainer.GetService<MainWindowViewModel>();

            var view = new MainView();
            view.DataContext = new MainViewModel();
            mainViewModel.SelectedUserControl = view;
        }
    }
}