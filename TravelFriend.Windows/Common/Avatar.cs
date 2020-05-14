using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TravelFriend.Windows.Common
{
    public class Avatar : Image
    {
        public Avatar()
        {
            
        }

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(Avatar), new PropertyMetadata(new PropertyChangedCallback(OnUrlPropertyChanged)));

        private static void OnUrlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
