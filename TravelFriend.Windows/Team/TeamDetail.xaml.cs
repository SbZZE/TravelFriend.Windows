using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// TeamDetail.xaml 的交互逻辑
    /// </summary>
    public partial class TeamDetail : UserControl
    {
        private string TeamId;
        public TeamDetail(string teamId)
        {
            InitializeComponent();
            TeamId = teamId;
            Loaded += TeamDetail_Loaded;
        }

        private async void TeamDetail_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TeamId))
            {
                //加载团队成员
                var memberResponse = await HttpManager.Instance.GetAsync<GetTeamMembersResponse>(new HttpRequest($"{ApiUtils.TeamMember}?teamid={TeamId}"));
                if (memberResponse.Ok)
                {
                    foreach (var member in memberResponse.Members)
                    {
                        MemberList.Items.Add(new MemberCard() { DataContext = member });
                    }
                }

                //加载团队相册
                var albumResponse = await HttpManager.Instance.GetAsync<GetTeamAlbumResponse>(new HttpRequest($"{ApiUtils.TeamAlbum}?teamid={TeamId}"));
                if (albumResponse.Ok)
                {
                    foreach (var album in albumResponse.Albums)
                    {
                        AlbumList.Children.Add(new AlbumCard() { DataContext = album });
                    }
                }
            }
        }

        private void Return_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void Left_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Scroll.ScrollToHorizontalOffset(Scroll.HorizontalOffset - 220);
        }

        private void Right_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Scroll.ScrollToHorizontalOffset(Scroll.HorizontalOffset + 220);
        }
    }
}
