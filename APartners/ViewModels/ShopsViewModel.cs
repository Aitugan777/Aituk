using AitukCore.Models;
using APartners.Models;
using APartners.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace APartners.ViewModels
{
    public class ShopsViewModel : ViewModelBase
    {
        public ObservableCollection<AShop> Shops { get; set; }

        public AShop? SelectedShop
        {
            get => GetValue<AShop>(nameof(SelectedShop));
            set => SetValue(value, nameof(SelectedShop));
        }

        public ShopsViewModel() 
        {
            Shops = new ObservableCollection<AShop>();
            var shopService = DIContainer.GetService<IShopService>();

            if (shopService != null )
            {
                Shops = new ObservableCollection<AShop>(shopService.GetShops());
            }
        }
    }
}
