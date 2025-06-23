using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APartners.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public string? Login
        {
            get => GetValue<string>(nameof(Login));
            set => SetValue(value, nameof(Login));
        }

        public string? Password
        {
            get => GetValue<string>(nameof(Password));
            set => SetValue(value, nameof(Password));
        }
        public ICommand LoginCommand { get; }

        /// <summary>
        /// ctr
        /// </summary>
        public AuthViewModel() 
        {
            LoginCommand = new AsyncRelayCommand(async x => await LoginAsync());

        }

        /// <summary>
        /// Авторизация
        /// </summary>
        public async Task LoginAsync()
        {
            var authService = DIContainer.GetService<IAuthService>();

            if (authService != null)
            {
                if (Login != null && Password != null)
                {
                    if (await authService.Authorize(Login, Password))
                    {
                        var mainViewModel = DIContainer.GetService<MainViewModel>();
                        if (mainViewModel != null)
                        {
                            var view = new ShopsView();
                            view.DataContext = new ShopsViewModel();
                            mainViewModel.SelectedUserControl = view;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не правильный логин или пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните логин и пароль");
                }
            }
        }
    }
}
