using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace APartners.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private ObservableCollection<AProduct> _products;

        public ObservableCollection<AProduct> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public AProduct? SelectedProduct
        {
            get => GetValue<AProduct>(nameof(SelectedProduct));
            set => SetValue(value, nameof(SelectedProduct));
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ProductsViewModel()
        {
            AddCommand = new AsyncRelayCommand(async _ => await AddProduct());
            EditCommand = new AsyncRelayCommand(async _ => await EditProduct());
        }

        private async Task AddProduct()
        {
            AProduct newProduct = new AProduct();
            var mainWindowViewModel = DIContainer.GetService<MainWindowViewModel>();
            var editProductView = new AddEditProductView();
            editProductView.DataContext = new AddEditProductViewModel(true, newProduct);
            mainWindowViewModel.SelectedUserControl = editProductView;
        }
        
        private async Task EditProduct()
        {
            if (SelectedProduct != null)
            {
                var productService = DIContainer.GetService<IProductService>();
                var product = await productService.GetProductAsync((long)SelectedProduct.Id!);
                var mainWindowViewModel = DIContainer.GetService<MainWindowViewModel>();
                var editProductView = new AddEditProductView();
                editProductView.DataContext = new AddEditProductViewModel(false, product);
                mainWindowViewModel.SelectedUserControl = editProductView;
            }
        }

        public async Task InitializeAsync()
        {
            var productService = DIContainer.GetService<IProductService>();
            var productsList = await productService.GetAllProductsAsync();
            Products = new ObservableCollection<AProduct>(productsList);
        }
    }
}
