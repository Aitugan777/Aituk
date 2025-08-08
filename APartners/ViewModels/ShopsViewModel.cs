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
        private IShopService _shopService;

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
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public AsyncRelayCommand EditCommand { get; set; }
        public AsyncRelayCommand DeleteCommand { get; set; }

        public ShopsViewModel() 
        {
            _mainViewModel = DIContainer.GetService<MainWindowViewModel>();

            _shopService = DIContainer.GetService<IShopService>();

            Shops = new ObservableCollection<AShop>();
            AddCommand = new RelayCommand(x => AddShop());
            EditCommand = new AsyncRelayCommand(async x => await EditShop(), x => SelectedShop != null);
            DeleteCommand = new AsyncRelayCommand(async x => await DeleteShop(), x => SelectedShop != null);
            InitialAsync();
        }

        private async Task InitialAsync()
        {
            var shops = await _shopService.GetShops();
            Shops = new ObservableCollection<AShop>(shops);
        }

        public void AddShop()
        {
            _mainViewModel.SelectedUserControl = new AddEditShopView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditShopViewModel(true, new AShop());
        }

        public async Task EditShop()
        {
            var shop = await _shopService.GetShop(SelectedShop.Id);
            _mainViewModel.SelectedUserControl = new AddEditShopView();
            _mainViewModel.SelectedUserControl.DataContext = new AddEditShopViewModel(false, shop);
        }

        public async Task DeleteShop()
        {
            await _shopService.DeleteShop(SelectedShop.Id);
            await InitialAsync();
        }
    }
}