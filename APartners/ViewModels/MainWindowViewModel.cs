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
    public class MainWindowViewModel : ViewModelBase
    {
        public UserControl? SelectedUserControl
        {
            get => GetValue<UserControl>(nameof(SelectedUserControl));
            set => SetValue(value, nameof(SelectedUserControl));
        }

        public UserControl AuthUserControl { get; }

        public MainWindowViewModel()
        {
            AuthUserControl = new AuthView();
            AuthUserControl.DataContext = new AuthViewModel();

            //Устанавливаем дефолтный UserControl при первом запуске приложения
            SelectedUserControl = AuthUserControl;
        }
    }
}
