using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Converter
{
    public class UploadStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((UploadStatus)value)
            {
                case UploadStatus.Uploading:
                    return new BitmapImage(new Uri("/Resources/Gray/Pause.png", UriKind.Relative));
                case UploadStatus.Pause:
                    return new BitmapImage(new Uri("/Resources/Gray/Play.png", UriKind.Relative));
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
