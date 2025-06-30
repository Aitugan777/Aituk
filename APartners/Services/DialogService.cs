using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class DialogService : IDialogService
    {
        public string OpenImageFileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg",
                Multiselect = false
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}
