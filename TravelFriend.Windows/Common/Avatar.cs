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
using TravelFriend.Windows.RabbitMQ;
using TravelFriend.Windows.RabbitMQ.Observe;

namespace TravelFriend.Windows.Common
{
    public class Avatar : Image, IObserver
    {
        public Avatar()
        {
            NotifyManager.AvatarSubject.Add(this);//订阅头像变化
        }

        public void Update()
        {
            var user = UserManager.GetUserByUserName(AccountManager.Instance.Account);
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Source = ImageHelper.ByteArrayToBitmapImage(user.Avatar);
                });
            }
        }
    }
}
