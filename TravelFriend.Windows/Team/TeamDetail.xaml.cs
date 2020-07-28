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
using TravelFriend.Windows.Album;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// TeamDetail.xaml 的交互逻辑
    /// </summary>
    public partial class TeamDetail : UserControl
    {
        private string TeamId;
        private string TeamName;
        private bool IsLeader;
        public TeamDetail(string teamId, string teamName, bool isLeader)
        {
            InitializeComponent();
            TeamId = teamId;
            TeamName = teamName;
            IsLeader = isLeader;
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
                        MemberList.Items.Add(new MemberCard(IsLeader) { DataContext = member });
                    }
                }

                //加载团队相册
                var albumResponse = await HttpManager.Instance.GetAsync<GetTeamAlbumResponse>(new HttpRequest($"{ApiUtils.AlbumList}?targetid={TeamId}&target=1"));
                if (albumResponse.Ok && albumResponse.Data != null)
                {
                    foreach (var album in albumResponse.Data)
                    {
                        var albumCard = new AlbumCard() { DataContext = album };
                        albumCard.MouseLeftButtonUp += AlbumCard_MouseLeftButtonUp;
                        AlbumList.Children.Add(albumCard);
                    }
                }
            }
        }

        private void AlbumCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as FrameworkElement).DataContext is TeamAlbum teamAlbum)
            {
                var album = new AlbumPage(TeamId, teamAlbum.AlbumId, TeamName, teamAlbum.AlbumName, Http.Album.AlbumType.TEAM);
                AlbumDetailContainer.Content = album;
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

        private void UpdateTeamInfo_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ShadePopup.Content = new UpdateTeamData(TeamId);
                mainWindow.ShadePopup.Visibility = Visibility.Visible;
            }
        }

        private void CreateAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ShadePopup.Content = new CreateAlbum(TeamId);
                mainWindow.ShadePopup.Visibility = Visibility.Visible;
            }
        }
    }
}
