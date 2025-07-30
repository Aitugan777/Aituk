using AitukCore.Contracts;

namespace APartners.Models
{
    public class AColor : ViewModelBase
    {
        public int Id { get => GetValue<int>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }

        public AColor(ColorContract colorContract)
        {
            Id = colorContract.Id;
            Name = colorContract.Name;
        }

        public AColor() { }
    }
}