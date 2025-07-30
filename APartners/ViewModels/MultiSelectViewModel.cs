using APartners.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.ViewModels
{
    public class MultiSelectViewModel : ViewModelBase
    {
        public ObservableCollection<SelectableItem<string>> Items { get; }

        public MultiSelectViewModel()
        {
            Items = new ObservableCollection<SelectableItem<string>>
            {
                new SelectableItem<string>("44 (XS)"),
                new SelectableItem<string>("46 (S)"),
                new SelectableItem<string>("48 (M)"),
                new SelectableItem<string>("50 (M/L)"),
                new SelectableItem<string>("52 (L)"),
                new SelectableItem<string>("54 (XL)"),
                new SelectableItem<string>("56 (XXL)"),
                new SelectableItem<string>("58 (XXXL)"),
                new SelectableItem<string>("60 (4XL)"),
            };


            foreach (var item in Items)
            {
                item.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(SelectableItem<string>.IsSelected))
                        OnPropertyChanged(nameof(SelectedItemsDisplay));
                };
            }
        }

        public string SelectedItemsDisplay =>
            string.Join(", ", Items.Where(i => i.IsSelected).Select(i => i.DisplayText));
    }
}
