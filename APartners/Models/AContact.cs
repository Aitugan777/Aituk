using AitukCore.Contracts;

namespace APartners.Models
{
    public class AContact : ViewModelBase
    {
        public int ContactTypeId { get => GetValue<int>(nameof(ContactTypeId)); set => SetValue(value, nameof(ContactTypeId)); }
        public string? Value { get => GetValue<string?>(nameof(Value)); set => SetValue(value, nameof(Value)); }

        public AContact(ContactContract contactContract)
        {
            ContactTypeId = contactContract.ContactTypeId;
            Value = contactContract.Value;
        }

        public AContact() { }

        public ContactContract ToContract()
        {
            return new ContactContract() { ContactTypeId = ContactTypeId, Value = Value };
        }
    }
}