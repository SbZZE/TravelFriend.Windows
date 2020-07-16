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
    public class UserAvatar : Image, IObserver
    {
        static UserAvatar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserAvatar), new FrameworkPropertyMetadata(typeof(UserAvatar)));
        }

        public UserAvatar()
        {
            NotifyManager.UserAvatarSubject.Add(this);//订阅头像变化
        }

        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register(
         nameof(UserName), typeof(string), typeof(UserAvatar), new PropertyMetadata(default(string)));

        public string UserName
        {
            get => (string)GetValue(UserNameProperty);
            set => SetValue(UserNameProperty, value);
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                var user = UserManager.GetUserByUserName(UserName);
                if (user != null && !string.IsNullOrEmpty(user.UserName))
                {
                    this.Source = user.Avatar == null ? new BitmapImage(new Uri("/Resources/DefaultBigAvatar.png", UriKind.Relative)) : ImageHelper.ByteArrayToBitmapImage(user.Avatar);
                }
            });
        }

        public async void UpdateWithHttp(int width, int height)
        {
            //获取头像
            using (MemoryStream ms = new MemoryStream())
            {
                await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.UserAvatar}?username={UserName}&isCompress=true&width={width}&height={height}"), ms);
                await Dispatcher.InvokeAsync(() =>
                {
                    var image = ImageHelper.GetImageByStreamAsync(ms);
                    this.Source = image == null ? new BitmapImage(new Uri("/Resources/DefaultBigAvatar.png", UriKind.Relative)) : image;
                });
            }
        }
    }
}
