using AitukCore.Contracts;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using APartners.Services;

namespace APartners.Models
{
    public class AShop : ViewModelBase
    {
        public long Id { get => GetValue<long>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }
        public string? Description { get => GetValue<string?>(nameof(Description)); set => SetValue(value, nameof(Description)); }
        public string? Address { get => GetValue<string?>(nameof(Address)); set => SetValue(value, nameof(Address)); }
        public double? Latitude { get => GetValue<double?>(nameof(Latitude)); set => SetValue(value, nameof(Latitude)); }
        public double? Longitude { get => GetValue<double?>(nameof(Longitude)); set => SetValue(value, nameof(Longitude)); }
        public ObservableCollection<ImageSource>? Photos { get => GetValue<ObservableCollection<ImageSource>?>(nameof(Photos)); set => SetValue(value, nameof(Photos)); }
        public int? ProductCount { get => GetValue<int?>(nameof(ProductCount)); set => SetValue(value, nameof(ProductCount)); }
        public AWorkSheldure? WorkSheldure { get => GetValue<AWorkSheldure?>(nameof(WorkSheldure)); set => SetValue(value, nameof(WorkSheldure)); }

        public ObservableCollection<AContact>? Contacts { get => GetValue<ObservableCollection<AContact>?>(nameof(Contacts)); set => SetValue(value, nameof(Contacts)); }

        /// <summary>
        /// Создание полноценного магазина по полному контракту
        /// </summary>
        /// <param name="shopContract">Контракт магазина</param>
        public AShop(ShopContract shopContract)
        {
            Id = shopContract.Id;
            Name = shopContract.Name;
            Description = shopContract.Description;
            Address = shopContract.Address;
            Latitude = shopContract.Latitude;
            Longitude = shopContract.Longitude;

            if (shopContract.Photos != null)
            {
                Photos = new ObservableCollection<ImageSource>(
                    shopContract.Photos.Select(photoBytes =>
                    {
                        return photoBytes.ConvertToImageSource();
                    }));
            }

            WorkSheldure = new AWorkSheldure(shopContract.WorkSheldure);

            Contacts = new ObservableCollection<AContact>(shopContract.Contacts.Select(x => new AContact(x)));
        }

        /// <summary>
        /// Создание магазина по компактному контракту
        /// </summary>
        /// <param name="shopCompactContract">Компактный контракт магазина</param>
        public AShop(ShopCompactContract shopCompactContract)
        {
            Id = shopCompactContract.Id;
            Name = shopCompactContract.Name;
            Description = shopCompactContract.Description;
            Address = shopCompactContract.Address;
            ProductCount = shopCompactContract.ProductCount;
        }

        public ShopContract ToContract()
        {
            return new ShopContract
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Address = this.Address,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Photos = this.Photos?.Select(image =>
                {
                    return image.ConvertToBytes(); // Предполагается, что у тебя есть такой метод-расширение
                }).ToList(),
                WorkSheldure = this.WorkSheldure?.ToContract(), // Предполагается, что AWorkSheldure имеет метод ToContract()
                Contacts = this.Contacts?.Select(c => c.ToContract()).ToList() // Предполагается, что AContact имеет метод ToContract()
            };
        }

        public AShop() { }
    }

}