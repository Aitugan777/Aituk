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

        public ObservableCollection<AProduct> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private AShop _selectedShop;
        private AProduct _selectedProduct; 
        public AShop SelectedShop
        {
            get => _selectedShop;
            set
            {
                _selectedShop = value;
                OnPropertyChanged(nameof(SelectedShop));
                LoadShopPhoto();
                LoadProducts();
            }
        }

        public AProduct SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
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
        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand UpdateShopsCommand { get; set; }

        public ShopsViewModel() 
        {
            _mainViewModel = DIContainer.GetService<MainWindowViewModel>();
            Shops = new ObservableCollection<AShop>();
            Products = new ObservableCollection<AProduct>();
            AddShopCommand = new RelayCommand(x => AddShop());
            EditShopCommand = new RelayCommand(x => EditShop());
            AddProductCommand = new RelayCommand(x => AddProduct());
            EditProductCommand = new RelayCommand(x => EditProduct());
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

        public void AddProduct()
        {
            _mainViewModel.SelectedUserControl = new AddEditProductView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditProductViewModel(true, new AProduct() { ShopId = SelectedShop.Id});
        }

        public void EditProduct()
        {
            _mainViewModel.SelectedUserControl = new AddEditProductView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditProductViewModel(false, SelectedProduct);
        }

        private async Task LoadShopPhoto()
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


        private async Task LoadProducts()
        {
            if (SelectedShop != null)
            {
                var productService = DIContainer.GetService<IProductService>();
                var products = await productService.GetProductsAsync();
                Products = new ObservableCollection<AProduct>(products);
            }
        }

    }
}