using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APartners.Services
{
    public static class ConverterHelper
    {
        public static ImageSource ConvertToImageSource(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentException("Byte array is empty", nameof(bytes));

            using var mem = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad; // Загружаем сразу, чтобы можно было закрыть поток
            image.StreamSource = mem;
            image.EndInit();
            image.Freeze(); // Делаем потокобезопасным и немодифицируемым
            return image;
        }

        public static byte[] ConvertToBytes(this ImageSource imageSource)
        {
            if (imageSource is not BitmapSource bitmapSource)
                throw new ArgumentException("Only BitmapSource is supported", nameof(imageSource));

            // Создаем чистую копию, без метаданных
            BitmapSource source = bitmapSource switch
            {
                WriteableBitmap writeable => writeable,
                _ => new WriteableBitmap(bitmapSource)
            };

            // Используем PNG (можно заменить на JPEG, GIF, и т.д.)
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));

            using var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }


    }
}
