using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KatalogFilm.ViewModel.Helper
{
    public static class ImageBrushConverter
    {
        public static ImageBrush PathToImageBrush(string imagePath)
        {
            ImageBrush result;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            result = new ImageBrush(bitmap);
            return result;
        }
    }
}
