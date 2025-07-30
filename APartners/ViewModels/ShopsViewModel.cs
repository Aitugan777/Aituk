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
        private MainWindowViewModel _mainViewModel;

        private ObservableCollection<AShop> _shops;
        private ObservableCollection<AProduct> _products;
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

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ShopsViewModel() 
        {
            _mainViewModel = DIContainer.GetService<MainWindowViewModel>();
            Shops = new ObservableCollection<AShop>();
            AddCommand = new RelayCommand(x => AddShop());
            EditCommand = new AsyncRelayCommand(async x => await EditShop());
            DeleteCommand = new RelayCommand(x => DeleteShop());
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
        public async Task EditShop()
        {
            var shopService = DIContainer.GetService<IShopService>();
            var shop = await shopService.GetShop(SelectedShop.Id.Value);
            _mainViewModel.SelectedUserControl = new AddEditShopView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditShopViewModel(false, shop);
        }

        public void DeleteShop()
        {
        }

    }
}