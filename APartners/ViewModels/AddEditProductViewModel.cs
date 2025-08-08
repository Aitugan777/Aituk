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
    public class AddEditProductViewModel : ViewModelBase
    {
        /// <summary>
        /// Признак добавления магазина
        /// </summary>
        private bool _isAddProduct;

        /// <summary>
        /// Команда для добавления/сохранения 
        /// </summary>
        public ICommand SaveCommand { get; }
        public ICommand UpdateImageCommand { get; }

        /// <summary>
        /// Текущий магазин
        /// </summary>
        public AProduct? Product
        {
            get => GetValue<AProduct>(nameof(Product));
            set => SetValue(value, nameof(Product));
        }
        
        public int AllProductCount
        {
            get => Product?.Shops?.Sum(x => x.ProductCount) ?? 0;
        }


        public ObservableCollection<AShop>? Shops
        {
            get => GetValue<ObservableCollection<AShop>>(nameof(Shops));
            set => SetValue(value, nameof(Shops));
        }

        
        public ObservableCollection<ACategory>? Categories
        {
            get => GetValue<ObservableCollection<ACategory>>(nameof(Categories));
            set => SetValue(value, nameof(Categories));
        }

        public ObservableCollection<AGender>? Genders
        {
            get => GetValue<ObservableCollection<AGender>>(nameof(Genders));
            set => SetValue(value, nameof(Genders));
        }

        public ObservableCollection<AColor>? Colors
        {
            get => GetValue<ObservableCollection<AColor>>(nameof(Colors));
            set => SetValue(value, nameof(Colors));
        }

        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand RemoveCommand { get; }

        public MultiSelectViewModel MultiSelectVM { get; set; } = new MultiSelectViewModel();

        public ObservableCollection<SelectableItem<ASize>>? Sizes
        {
            get => GetValue<ObservableCollection<SelectableItem<ASize>>>(nameof(Sizes));
            set => SetValue(value, nameof(Sizes));
        }


        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditProductViewModel(bool isAddProduct, AProduct product)
        {
            _isAddProduct = isAddProduct;
            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());
            UpdateImageCommand = new RelayCommand(x => UpdateImage());

            Product = product;

            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());
            MoveUpCommand = new CollectionCommand<ImageSource>(MoveUp, CanMoveUp);
            MoveDownCommand = new CollectionCommand<ImageSource>(MoveDown, CanMoveDown);
            RemoveCommand = new CollectionCommand<ImageSource>(Remove);
            InitializeAsync();
        }

        public string SelectedItemsDisplay
        {
            get
            {
                if (Sizes != null)
                    return string.Join(", ", Sizes.Where(i => i.IsSelected).Select(i => i.Value.Name));
                return "Выберите размеры...";
            }
        }

        public async Task InitializeAsync()
        {
            var clothService = DIContainer.GetService<IClothPropertiesService>();

            Categories = new ObservableCollection<ACategory>(await clothService.GetCategoriesAsync());
            Sizes = new ObservableCollection<SelectableItem<ASize>>((await clothService.GetSizesAsync()).Select(x => new SelectableItem<ASize>(x)).ToList());

            foreach (var size in Sizes)
            {
                size.IsSelected = Product?.Sizes?.Any(x => x.Id == size.Value.Id) ?? false;

                size.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(SelectableItem<string>.IsSelected))
                        OnPropertyChanged(nameof(SelectedItemsDisplay));
                    Product.Sizes = Sizes.Where(x =>  x.IsSelected).Select(x => x.Value).ToList();
                };
            }
            OnPropertyChanged(nameof(SelectedItemsDisplay));

            Genders = new ObservableCollection<AGender>(await clothService.GetGendersAsync());
            Colors = new ObservableCollection<AColor>(await clothService.GetColorsAsync());

            var shopService = DIContainer.GetService<IShopService>();
            Shops = new ObservableCollection<AShop>(await shopService.GetShops());

            foreach (var shop in Shops)
            {
                shop.ProductCount = 1;
            }

            if (Product?.Shops == null)
            {
                Product.Shops = Shops;
            }

            foreach (var shop in Shops)
            {
                shop.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(shop.ProductCount))
                    {
                        if (Product?.Shops != null)
                        {
                            var productShop = Product.Shops.FirstOrDefault(x => x.Id == shop.Id);
                            if (shop.ProductCount > 0)
                            {
                                if (productShop != null)
                                {
                                    productShop.ProductCount = shop.ProductCount;
                                }
                                else
                                {
                                    Product.Shops.Add(shop);
                                }
                                OnPropertyChanged(nameof(AllProductCount));
                            }
                            else
                            {
                                if (productShop != null)
                                {
                                    Product?.Shops.Remove(productShop);
                                }
                            }

                        }
                    }
                };
            }
            if (Product?.Shops != null)
            {
                // Создай snapshot коллекции Product.Shops
                var productShops = Product.Shops.ToList();

                foreach (var productShop in productShops)
                {
                    var shop = Shops.FirstOrDefault(x => x.Id == productShop.Id);
                    if (shop != null)
                    {
                        shop.ProductCount = productShop.ProductCount;
                    }
                }
            }


        }

        /// <summary>
        /// Сохранить/добавить товар
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await Task.Delay(1000);
            var productService = DIContainer.GetService<IProductService>();
            if (_isAddProduct)
            {
                productService.AddProduct(Product!);
            }
            else
            {
                productService.SaveProduct(Product!);
            }

            var mainViewModel = DIContainer.GetService<MainWindowViewModel>();

            var view = new MainView();
            view.DataContext = new MainViewModel();
            mainViewModel.SelectedUserControl = view;
        }


        #region управление фотографией

        private void UpdateImage()
        {
            var paths = FileHelper.OpenMultipleImageFileDialog();
            if (paths == null || paths.Length == 0) return;

            if (Product != null)
            {
                if (Product.Photos == null)
                    Product.Photos = new ObservableCollection<ImageSource>();

                foreach (var path in paths)
                {
                    byte[] imageBytes = File.ReadAllBytes(path);

                    Product.Photos.Add(imageBytes.ConvertToImageSource());
                }
            }
        }

        private void MoveUp(ImageSource image)
        {
            int index = Product.Photos.IndexOf(image);
            if (index > 0)
            {
                Product.Photos.Move(index, index - 1);
            }
        }

        private bool CanMoveUp(ImageSource image) => Product.Photos.IndexOf(image) > 0;

        private void MoveDown(ImageSource image)
        {
            int index = Product.Photos.IndexOf(image);
            if (index < Product.Photos.Count - 1)
            {
                Product.Photos.Move(index, index + 1);
            }
        }

        private bool CanMoveDown(ImageSource image) => Product.Photos.IndexOf(image) < Product.Photos.Count - 1;

        private void Remove(ImageSource image)
        {
            Product.Photos.Remove(image);
        }

        #endregion
    }
}