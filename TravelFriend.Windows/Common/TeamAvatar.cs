using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.RabbitMQ;
using TravelFriend.Windows.RabbitMQ.Observe;

namespace TravelFriend.Windows.Common
{
    public class TeamAvatar : Image, IObserver
    {
        static TeamAvatar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TeamAvatar), new FrameworkPropertyMetadata(typeof(TeamAvatar)));
        }

        public TeamAvatar()
        {
            Unloaded += TeamAvatar_Unloaded;
            NotifyManager.TeamAvatarSubject.Add(this);//订阅头像变化
        }

        private void TeamAvatar_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static readonly DependencyProperty TeamIdProperty = DependencyProperty.Register(
          nameof(TeamId), typeof(string), typeof(TeamAvatar), new PropertyMetadata(default(string)));

        public string TeamId
        {
            get => (string)GetValue(TeamIdProperty);
            set => SetValue(TeamIdProperty, value);
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(TeamId))
                {
                    var team = TeamManager.GetTeamByTeamId(TeamId);
                    if (team != null)
                    {
                        this.Source = team.Avatar == null ? new BitmapImage(new Uri("/Resources/DefaultTeamAvatar.png", UriKind.Relative)) : ImageHelper.ByteArrayToBitmapImage(team.Avatar);
                    }
                }
            });
        }

        public async void UpdateWithHttp(int width, int height)
        {
            //请求获取
            using (MemoryStream ms = new MemoryStream())
            {
                var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.TeamAvatar}?teamid={TeamId}&isCompress=true&width={width}&height={height}"), ms);
                await Dispatcher.InvokeAsync(() =>
                {
                    var image = ImageHelper.GetImageByStreamAsync(ms);
                    this.Source = image == null ? new BitmapImage(new Uri("/Resources/DefaultTeamAvatar.png", UriKind.Relative)) : image;
                });
            }
        }
    }
}
