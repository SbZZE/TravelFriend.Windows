using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database.Data;
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

                        this.Source = team.Avatar == null ? new BitmapImage(new Uri("/Resources/DefaultBigAvatar.png", UriKind.Relative)) : ImageHelper.ByteArrayToBitmapImage(team.Avatar);
                    }
                }
            });
        }
    }
}
