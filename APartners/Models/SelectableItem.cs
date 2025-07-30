using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Models
{
    public class SelectableItem<T> : INotifyPropertyChanged
    {
        public T Value { get; }
        public string DisplayText => Value.ToString();

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

        public SelectableItem(T value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
