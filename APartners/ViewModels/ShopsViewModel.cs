using AitukCore.Models;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace APartners.ViewModels
{
    public class ShopsViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ObservableCollection<AShop> Shops { get; set; }

        public ICommand AddShopCommand { get; set; }
        public ICommand EditShopCommand { get; set; }

        public AShop? SelectedShop
        {
            get => GetValue<AShop>(nameof(SelectedShop));
            set => SetValue(value, nameof(SelectedShop));
        }

        public ShopsViewModel() 
        {
            _mainViewModel = DIContainer.GetService<MainViewModel>();
            Shops = new ObservableCollection<AShop>();
            var shopService = DIContainer.GetService<IShopService>();
            AddShopCommand = new RelayCommand(x => AddShop());
            EditShopCommand = new RelayCommand(x => EditShop());

            if (shopService != null )
            {
                Shops = new ObservableCollection<AShop>(shopService.GetShops());
            }
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
    }
}
