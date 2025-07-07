using APartners.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace APartners.Converters
{
    public class ImageEqualityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var currentImage = values[0] as ImageSource;
            var userControl = values[1] as FrameworkElement;
            var vm = userControl?.DataContext as AddEditProductViewModel;

            if (currentImage == null || vm == null)
                return Brushes.Transparent;

            return Equals(currentImage, vm.SelectedImage) ? Brushes.Red : Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
