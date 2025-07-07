using APartners.Models;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace APartners.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MenuItemModel> MenuItems { get; set; }
        public UserControl? SelectedUserControl
        {
            get => GetValue<UserControl>(nameof(SelectedUserControl));
            set => SetValue(value, nameof(SelectedUserControl));
        }
        
        public MenuItemModel? SelectedMenuItem
        {
            get => GetValue<MenuItemModel>(nameof(SelectedMenuItem));
            set 
            { 
                SetValue(value, nameof(SelectedMenuItem)); 
                OnMenuItemSelected(value!); 
            }
        }

        public ShopsView _shopsView { get; }
        public ProductsView _productsView { get; }
        public UserControl _settingsView { get; }

        public MainViewModel()
        {
            _shopsView = new ShopsView();
            _shopsView.DataContext = new ShopsViewModel();
            _productsView = new ProductsView();
            _productsView.DataContext = new ProductsViewModel();

            SelectedUserControl = _shopsView;

            MenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Title = "Настройки", IconPath = "/Resources/Icons/settings72.png" },
                new MenuItemModel { Title = "Магазины", IconPath = "/Resources/Icons/store50.png" },
                new MenuItemModel { Title = "Товары", IconPath = "/Resources/Icons/product60.png" },
            };
        }
        private async Task OnMenuItemSelected(MenuItemModel selected)
        {
            switch (selected?.Title)
            {
                case "Настройки":
                    SelectedUserControl = _settingsView;
                    break;
                case "Магазины":
                    SelectedUserControl = _shopsView;
                    break;
                case "Товары":
                    SelectedUserControl = _productsView;
                    if (_productsView.DataContext is ProductsViewModel viewModel)
                        await viewModel.InitializeAsync();
                    break;
            }
        }
        public class MenuItemModel
        {
            public string Title { get; set; }
            public string IconPath { get; set; }
        }
    }
}
