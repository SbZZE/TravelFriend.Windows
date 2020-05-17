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
                try
                {
                    var image = (Avatar)d;
                    var userName = (string)e.NewValue;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.Avatar}?username={userName}"), ms);
                        ms.Position = 0;
                        BitmapImage result = new BitmapImage();
                        result.BeginInit();
                        result.CacheOption = BitmapCacheOption.OnLoad;
                        result.StreamSource = ms;
                        result.EndInit();
                        result.Freeze();
                        image.Source = result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
