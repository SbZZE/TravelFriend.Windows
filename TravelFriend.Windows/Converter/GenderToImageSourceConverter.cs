using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TravelFriend.Windows.Converter
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class GenderToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (System.Convert.ToInt32((string)value) == 0)
            {
                return new BitmapImage(new Uri("/Resources/famale.png", UriKind.Relative));
            }
            else if (System.Convert.ToInt32((string)value) == 1)
            {
                return new BitmapImage(new Uri("/Resources/male.png", UriKind.Relative));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
