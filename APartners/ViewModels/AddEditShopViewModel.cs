using AitukCore.Contracts;
using APartners.Commands;
using APartners.Models;
using APartners.Services;
using APartners.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APartners.ViewModels
{
    /// <summary>
    /// Добавление/редактирование магазина
    /// </summary>
    public class AddEditShopViewModel : ViewModelBase
    {
        /// <summary>
        /// Признак добавления магазина
        /// </summary>
        private bool _isAddShop { get; set; }
        private IShopService _shopService { get; set; }

        /// <summary>
        /// Команда для добавления/сохранения 
        /// </summary>
        public ICommand SaveCommand { get; }
        public ICommand UpdateImageCommand { get; }

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand RemoveCommand { get; }
        public AsyncRelayCommand SearchCoordinatesCommand { get; }

        public ICommand AddContactCommand => new RelayCommand(x =>
        {
            if (Shop.Contacts == null)
                Shop.Contacts = new ObservableCollection<AContact>();
            Shop.Contacts.Add(new AContact());
        });

        public ICommand RemoveContactCommand => new CollectionCommand<AContact>(contact =>
        {
            if (Shop.Contacts == null)
                Shop.Contacts = new ObservableCollection<AContact>();
            if (Shop.Contacts.Contains(contact))
                Shop.Contacts.Remove(contact);
        });

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public AShop? Shop
        {
            get => GetValue<AShop>(nameof(Shop));
            set => SetValue(value, nameof(Shop));
        }
        
        /// <summary>
        /// Текущий магазин
        /// </summary>
        public ObservableCollection<AContactType>? ContactTypes
        {
            get => GetValue<ObservableCollection<AContactType>>(nameof(ContactTypes));
            set => SetValue(value, nameof(ContactTypes));
        }

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public ImageSource? SelectedImage
        {
            get => GetValue<ImageSource>(nameof(SelectedImage));
            set => SetValue(value, nameof(SelectedImage));
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditShopViewModel(bool isAddShop, AShop shop) 
        {
            if (shop.WorkSheldure == null)
            {
                shop.WorkSheldure = new AWorkSheldure
                {
                    Monday = new AWorkDay(DayOfWeek.Monday, true, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                    Tuesday = new AWorkDay(DayOfWeek.Tuesday, true, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                    Wednesday = new AWorkDay(DayOfWeek.Wednesday, true, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                    Thursday = new AWorkDay(DayOfWeek.Thursday, true, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                    Friday = new AWorkDay(DayOfWeek.Friday, true, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                    Saturday = new AWorkDay(DayOfWeek.Saturday, false),
                    Sunday = new AWorkDay(DayOfWeek.Sunday, false)
                };
            }
            _shopService = DIContainer.GetService<IShopService>();

            _isAddShop = isAddShop;
            Shop = shop;

            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());
            UpdateImageCommand = new RelayCommand(x => UpdateImage());

            MoveUpCommand = new CollectionCommand<ImageSource>(MoveUp, CanMoveUp);
            MoveDownCommand = new CollectionCommand<ImageSource>(MoveDown, CanMoveDown);
            RemoveCommand = new CollectionCommand<ImageSource>(Remove);

            SearchCoordinatesCommand = new AsyncRelayCommand(async x => await SearchCoordinatesAsync(), y => IsActiveButtonSearchCoordinates());

            Shop.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Shop.Address))
                {
                    SearchCoordinatesCommand.RaiseCanExecuteChanged();
                }
            };

            InitialAsync();
        }

        private bool IsActiveButtonSearchCoordinates()
        {
            return Shop != null && !string.IsNullOrEmpty(Shop.Address);
        }

        public async Task InitialAsync()
        {
            var listContactTypes = await _shopService.GetContactTypesAsync();
            if (listContactTypes != null)
                ContactTypes = new ObservableCollection<AContactType>(listContactTypes);
        }

        private void UpdateImage()
        {
            var paths = FileHelper.OpenMultipleImageFileDialog();
            if (paths == null || paths.Length == 0) return;

            if (Shop != null)
            {
                if (Shop.Photos == null)
                    Shop.Photos = new ObservableCollection<ImageSource>();

                foreach (var path in paths)
                {
                    byte[] imageBytes = File.ReadAllBytes(path);

                    Shop.Photos.Add(imageBytes.ConvertToImageSource());
                }
            }
        }

        /// <summary>
        /// Сохранить - редактировать магазин
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            using (new WaitIndicator("Сохранение магазина..."))
            {
                var shopService = DIContainer.GetService<IShopService>();
                if (_isAddShop)
                {
                    await shopService.AddShop(Shop!);
                }
                else
                {
                    await shopService.SaveShop(Shop!);
                }

            }

            var mainViewModel = DIContainer.GetService<MainWindowViewModel>();

            var view = new MainView();
            view.DataContext = new MainViewModel();
            mainViewModel.SelectedUserControl = view;
        }

        public async Task SearchCoordinatesAsync()
        {
            using (new WaitIndicator("Поиск адреса..."))
            {
                var shopService = DIContainer.GetService<IShopService>();
                if (Shop ==  null || string.IsNullOrEmpty(Shop.Address)) return;
                var coordinateContract = await shopService.GetCoordinatesByAddressAsync(Shop.Address);
                if (coordinateContract == null) return;
                Shop.Latitude = coordinateContract.Latitude;
                Shop.Longitude = coordinateContract.Longitude;
                Shop.Address = coordinateContract.FullAddress;
            }

        }

        #region управление фотографией


        private void MoveUp(ImageSource image)
        {
            int index = Shop.Photos.IndexOf(image);
            if (index > 0)
            {
                Shop.Photos.Move(index, index - 1);
            }
        }

        private bool CanMoveUp(ImageSource image) => Shop.Photos.IndexOf(image) > 0;

        private void MoveDown(ImageSource image)
        {
            int index = Shop.Photos.IndexOf(image);
            if (index < Shop.Photos.Count - 1)
            {
                Shop.Photos.Move(index, index + 1);
            }
        }

        private bool CanMoveDown(ImageSource image) => Shop.Photos.IndexOf(image) < Shop.Photos.Count - 1;

        private void Remove(ImageSource image)
        {
            Shop.Photos.Remove(image);
        }

        #endregion
    }
}