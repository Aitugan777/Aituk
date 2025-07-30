using AitukCore.Contracts;
using APartners.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APartners.Models
{
    public class AProduct : ViewModelBase
    {
        public long? Id { get => GetValue<long?>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }
        public string? Description { get => GetValue<string?>(nameof(Description)); set => SetValue(value, nameof(Description)); }
        public decimal? Cost { get => GetValue<decimal?>(nameof(Cost)); set => SetValue(value, nameof(Cost)); }
        public ObservableCollection<AShop>? Shops { get => GetValue<ObservableCollection<AShop>>(nameof(Shops)); set => SetValue(value, nameof(Shops)); }
        public ObservableCollection<ASize>? Sizes { get => GetValue<ObservableCollection<ASize>?>(nameof(Sizes)); set => SetValue(value, nameof(Sizes)); }
        public ACategory? Category { get => GetValue<ACategory?>(nameof(Category)); set => SetValue(value, nameof(Category)); }
        public AColor? Color { get => GetValue<AColor?>(nameof(Color)); set => SetValue(value, nameof(Color)); }
        public AGender? Gender { get => GetValue<AGender?>(nameof(Gender)); set => SetValue(value, nameof(Gender)); }
        public string? Brand { get => GetValue<string?>(nameof(Brand)); set => SetValue(value, nameof(Brand)); }
        public string? Code { get => GetValue<string?>(nameof(Code)); set => SetValue(value, nameof(Code)); }
        public string? KeyWords { get => GetValue<string?>(nameof(KeyWords)); set => SetValue(value, nameof(KeyWords)); }
        public ObservableCollection<ImageSource>? Photos { get => GetValue<ObservableCollection<ImageSource>?>(nameof(Photos)); set => SetValue(value, nameof(Photos)); }
        public ImageSource? MainPhoto { get => GetValue<ImageSource?>(nameof(MainPhoto)); set => SetValue(value, nameof(MainPhoto)); }

        public int? Count { get => Shops?.Sum(x => x.ProductCount); }

        /// <summary>
        /// Создание полноценного товара для карточки
        /// </summary>
        /// <param name="productContract">Полный контракт товара</param>
        public AProduct(ProductContract productContract) 
        {
            Id = productContract.Id;
            Name = productContract.Name;
            Description = productContract.Description;
            Cost = productContract.Cost;

            Shops = new ObservableCollection<AShop>(productContract.Shops.Select(x =>
            {
                return new AShop(x);
            }));

            var clothPropertiesService = DIContainer.GetService<IClothPropertiesService>();

            Sizes = new ObservableCollection<ASize>(productContract.Sizes.Select(x =>
            {
                return clothPropertiesService.GetSizes().Where(ax => ax.Id == x).First();
            }).ToList());

            Category = clothPropertiesService.GetCategories().Where(x => x.Id == productContract.CategoryId).FirstOrDefault();

            Color = clothPropertiesService.GetColors().Where(x => x.Id == productContract.ColorId).FirstOrDefault();
            Gender = clothPropertiesService.GetGenders().Where(x => x.Id == productContract.GenderId).FirstOrDefault();
            Brand = productContract.Brand;
            Code = productContract.Code;
            KeyWords = productContract.KeyWords;

            if (productContract.Photos != null)
            {
                Photos = new ObservableCollection<ImageSource>(
                    productContract.Photos.Select(photoBytes =>
                    {
                        return photoBytes.ConvertToImageSource();
                    }));
            }
        }

        /// <summary>
        /// Создание товара для грида
        /// </summary>
        /// <param name="productCompactContract">Контракт с мин значениями</param>
        public AProduct(ProductCompactContract productCompactContract)
        {
            Id = productCompactContract.Id;
            Name = productCompactContract.Name;
            Description = productCompactContract.Description;
            Cost = productCompactContract.Cost;
            MainPhoto = productCompactContract.MainPhoto.ConvertToImageSource();
        }
        public ProductContract ToContract()
        {
            return new ProductContract
            {
                Id = this.Id ?? 0,
                Name = this.Name,
                Description = this.Description,
                Cost = this.Cost ?? 0,
                Brand = this.Brand,
                Code = this.Code,
                KeyWords = this.KeyWords,
                CategoryId = this.Category?.Id ?? 0,
                ColorId = this.Color?.Id ?? 0,
                GenderId = this.Gender?.Id ?? 0,
                Sizes = this.Sizes?.Select(s => s.Id).ToList() ?? new List<int>(),
                Shops = this.Shops?.Select(s => new ShopCompactContract { Id = s.Id ?? 0 }).ToList() ?? new List<ShopCompactContract>(),
                Photos = this.Photos?.Select(photo => photo.ConvertToBytes()).ToList() ?? new List<byte[]>()
            };
        }

        public AProduct() { }
    }
}
