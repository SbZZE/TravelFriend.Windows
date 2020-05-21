using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Common
{
    public class Avatar : Image
    {
        static Avatar()
        {

        }

        public bool IsReloadAvatar
        {
            get { return (bool)GetValue(IsReloadAvatarProperty); }
            set { SetValue(IsReloadAvatarProperty, value); }
        }

        public static readonly DependencyProperty IsReloadAvatarProperty =
            DependencyProperty.Register("IsReloadAvatar", typeof(bool), typeof(Avatar), new PropertyMetadata(false, OnIsReloadAvatarPropertyChanged));

        private async static void OnIsReloadAvatarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && (bool)e.NewValue)
            {
                var image = (Avatar)d;

                var user = UserManager.GetFirstUser();
                if (user != null && !string.IsNullOrEmpty(user.UserName))
                {
                    image.Source = ImageHelper.ByteArrayToBitmapImage(user.Avatar);
                }

                //var avatar = await ImageHelper.GetAvatarAsync(AccountManager.Instance.Account);
                //if (avatar != null)
                //{
                //    image.Source = avatar;
                //}
            }
        }
    }
}
