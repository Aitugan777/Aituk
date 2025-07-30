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

        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
                OnPropertyChanged(nameof(IsImageSelected));
            }
        }
        public bool IsImageSelected => SelectedImage != null;

        public ICommand DeleteImageCommand { get; }
        public ObservableCollection<ImageSource> ProductImages { get; set; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand RemoveCommand { get; }

        public MultiSelectViewModel MultiSelectVM { get; set; } = new MultiSelectViewModel();

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isAddShop">признак добавления магазина</param>
        /// <param name="shop">магазин</param>
        public AddEditProductViewModel(bool isAddProduct, AProduct product)
        {
            ProductImages = new ObservableCollection<ImageSource>();
            _isAddProduct = isAddProduct;
            SaveCommand = new AsyncRelayCommand(async x => await SaveAsync());

            Product = product;

            MoveUpCommand = new CollectionCommand<ImageSource>(MoveUp, CanMoveUp);
            MoveDownCommand = new CollectionCommand<ImageSource>(MoveDown, CanMoveDown);
            RemoveCommand = new CollectionCommand<ImageSource>(Remove);

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


        private void MoveUp(ImageSource image)
        {
            int index = ProductImages.IndexOf(image);
            if (index > 0)
            {
                ProductImages.Move(index, index - 1);
            }
        }

        private bool CanMoveUp(ImageSource image) => ProductImages.IndexOf(image) > 0;

        private void MoveDown(ImageSource image)
        {
            int index = ProductImages.IndexOf(image);
            if (index < ProductImages.Count - 1)
            {
                ProductImages.Move(index, index + 1);
            }
        }

        private bool CanMoveDown(ImageSource image) => ProductImages.IndexOf(image) < ProductImages.Count - 1;

        private void Remove(ImageSource image)
        {
            ProductImages.Remove(image);
        }

        #endregion
    }
}