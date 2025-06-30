using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace APartners.Services
{
    public class FileHelper
    {
        /// <summary>
        /// Получить путь к изображению
        /// </summary>
        /// <returns></returns>
        public static string OpenImageFileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg",
                Multiselect = false
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        /// <summary>
        /// Узнать формат изображения
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string? GetImageFormat(byte[] bytes)
        {
            if (bytes.Length < 8)
                return null;

            // JPEG: FF D8 FF
            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
                return "jpeg";

            // PNG: 89 50 4E 47 0D 0A 1A 0A
            if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E &&
                bytes[3] == 0x47 && bytes[4] == 0x0D && bytes[5] == 0x0A &&
                bytes[6] == 0x1A && bytes[7] == 0x0A)
                return "png";

            // GIF: GIF87a or GIF89a
            if (bytes[0] == 'G' && bytes[1] == 'I' && bytes[2] == 'F')
                return "gif";

            // BMP: 42 4D
            if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                return "bmp";

            // WebP: RIFF....WEBP
            if (bytes[0] == 'R' && bytes[1] == 'I' && bytes[2] == 'F' && bytes[8] == 'W' && bytes[9] == 'E' && bytes[10] == 'B' && bytes[11] == 'P')
                return "webp";

            return null; // неизвестный формат
        }

        public static ImageSource LoadImage(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
