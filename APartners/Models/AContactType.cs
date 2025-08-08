using AitukCore.Contracts;

namespace APartners.Models
{
    public class AContactType : ViewModelBase
    {
        public int Id { get => GetValue<int>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }

        public AContactType(ContactTypeContract contactType)
        {
            Id = contactType.Id;
            Name = contactType.Name;
        }

        public AContactType() { }
    }
}