using AitukCore.Contracts;

namespace APartners.Models
{
    public class ASize : ViewModelBase
    {
        public int Id { get => GetValue<int>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }

        public ASize(SizeContract sizeContract)
        {
            Id = sizeContract.Id;
            Name = sizeContract.Name;
        }
        public ASize() { }
    }
}