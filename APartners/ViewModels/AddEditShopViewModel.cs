using AitukCore.Models;
using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public AShop? Shop
        {
            get => GetValue<AShop>(nameof(Shop));
            set => SetValue(value, nameof(Shop));
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

            Shop = shop;
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

            var mainViewModel = DIContainer.GetService<MainViewModel>();
            mainViewModel.SelectedUserControl = new ShopsView();
            mainViewModel.SelectedUserControl.DataContext = new ShopsViewModel();
        }
    }
}