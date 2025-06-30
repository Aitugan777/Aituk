using AitukCore.Models;
using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace APartners.ViewModels
{
    public class ShopsViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        private ObservableCollection<AShop> _shops;
        public ObservableCollection<AShop> Shops
        {
            get => _shops;
            set
            {
                _shops = value;
                OnPropertyChanged(nameof(Shops));
            }
        }

        private AShop _selectedShop;
        public AShop SelectedShop
        {
            get => _selectedShop;
            set
            {
                _selectedShop = value;
                OnPropertyChanged(nameof(SelectedShop));
                LoadShopPhoto();
            }
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

        public ICommand AddShopCommand { get; set; }
        public ICommand EditShopCommand { get; set; }
        public ICommand UpdateShopsCommand { get; set; }

        public ShopsViewModel() 
        {
            _mainViewModel = DIContainer.GetService<MainViewModel>();
            Shops = new ObservableCollection<AShop>();
            AddShopCommand = new RelayCommand(x => AddShop());
            EditShopCommand = new RelayCommand(x => EditShop());
            UpdateShopsCommand = new AsyncRelayCommand(async x => await Initial());
            Initial();
        }

        private async Task Initial()
        {
            var shopService = DIContainer.GetService<IShopService>();
            var shops = await shopService.GetShops();
            Shops = new ObservableCollection<AShop>(shops);
        }

        public void AddShop()
        {
            _mainViewModel.SelectedUserControl = new AddEditShopView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditShopViewModel(true, new AShop());
        }
        public void EditShop()
        {
            _mainViewModel.SelectedUserControl = new AddEditShopView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditShopViewModel(false, SelectedShop);
        }

        private async void LoadShopPhoto()
        {
            if (SelectedShop != null)
            {
                var shopService = DIContainer.GetService<IShopService>();
                var content = await shopService.GetShopPhotoAsync(SelectedShop.Id);
                if (content != null)
                    ShopImage = content;
                else
                    ShopImage = null;
            }
        }

    }
}
