using APartners.Models;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace APartners.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public UserControl? SelectedUserControl
        {
            get => GetValue<UserControl>(nameof(SelectedUserControl));
            set => SetValue(value, nameof(SelectedUserControl));
        }

        public UserControl AuthUserControl { get; }
        public UserControl ShopsUserControl { get; }

        public MainViewModel()
        {
            AuthUserControl = new AuthView();
            AuthUserControl.DataContext = new AuthViewModel();

            ShopsUserControl = new ShopsView();
            ShopsUserControl.DataContext = new ShopsViewModel();

            //Устанавливаем дефолтный UserControl при первом запуске приложения
            SelectedUserControl = AuthUserControl;
        }
    }
}
