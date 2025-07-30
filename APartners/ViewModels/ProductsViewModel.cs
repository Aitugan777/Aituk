using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand EditCommand { get; set; }

        public ProductsViewModel()
        {
            EditCommand = new AsyncRelayCommand(async _ => await EditProduct());
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
            var productsList = await productService.GetProductsAsync();
            Products = new ObservableCollection<AProduct>(productsList);
        }
    }
}
