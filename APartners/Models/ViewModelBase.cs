using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Models
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object?> _propertyValues = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected T? GetValue<T>([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (_propertyValues.TryGetValue(propertyName, out var value))
                return (T?)value;

            return default;
        }

        protected bool SetValue<T>(T value, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (EqualityComparer<T>.Default.Equals(GetValue<T>(propertyName), value))
                return false;

            _propertyValues[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
