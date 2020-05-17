using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Common
{
    public class Avatar : Image
    {
        static Avatar()
        {

        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(Avatar), new PropertyMetadata("", OnUserNamePropertyChanged));

        private async static void OnUserNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var image = (Avatar)d;
                var avatar = await ImageHelper.GetAvatarAsync((string)e.NewValue);
                if (avatar != null)
                {
                    image.Source = avatar;
                }
            }
        }
    }
}
