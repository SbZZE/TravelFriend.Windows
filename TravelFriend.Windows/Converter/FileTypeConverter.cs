using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Converter
{
    public class FileTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((FileType)value)
            {
                case FileType.IMAGE:
                    return new BitmapImage(new Uri("/Resources/Image.png", UriKind.Relative));
                case FileType.VIDEO:
                    return new BitmapImage(new Uri("/Resources/Video.png", UriKind.Relative));
                case FileType.UNKNOWN:
                    return new BitmapImage(new Uri("/Resources/Image.png", UriKind.Relative));
                default:
                    return new BitmapImage(new Uri("/Resources/Image.png", UriKind.Relative));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
