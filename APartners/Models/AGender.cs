using AitukCore.Contracts;

namespace APartners.Models
{
    public class AGender : ViewModelBase
    {
        public int Id { get => GetValue<int>(nameof(Id)); set => SetValue(value, nameof(Id)); }
        public string? Name { get => GetValue<string?>(nameof(Name)); set => SetValue(value, nameof(Name)); }

        public AGender(GenderContract genderContract)
        {
            Id = genderContract.Id;
            Name = genderContract.Name;
        }

        public AGender() { }
    }
}