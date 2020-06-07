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
            NotifyManager.TeamAvatarSubject.Add(this);//订阅头像变化
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

        public async void UpdateWithHttp()
        {
            //请求获取
            using (MemoryStream ms = new MemoryStream())
            {
                var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.TeamAvatar}?teamid={TeamId}&isCompress=true"), ms);
                if (ms != null && ms.Length > 0)
                {
                    ms.Position = 0;
                    using (BinaryReader br = new BinaryReader(ms))
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.Source = ImageHelper.ByteArrayToBitmapImage(br.ReadBytes((int)ms.Length));
                        });
                    }
                }
            }
        }
    }
}
