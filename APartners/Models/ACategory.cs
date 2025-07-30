using AitukCore.Contracts;

namespace APartners.Models
{
    public class ACategory : ViewModelBase
    {
        public int Id { get => GetValue<int>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }

        public ACategory(CategoryContract categoryContract)
        {
            Id = categoryContract.Id;
            Name = categoryContract.Name;
        }

        public ACategory() { }
    }
}