using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http;
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// TeamPage.xaml 的交互逻辑
    /// </summary>
    public partial class TeamPage : UserControl
    {
        ObservableCollection<TeamModel> CreatedTeam = new ObservableCollection<TeamModel>();
        ObservableCollection<TeamModel> JoinedTeam = new ObservableCollection<TeamModel>();
        public TeamPage()
        {
            InitializeComponent();
            CreatedTeam.CollectionChanged += CreatedTeam_CollectionChanged;
            JoinedTeam.CollectionChanged += JoinedTeam_CollectionChanged;
            Loaded += TeamPage_Loaded;
            Unloaded += TeamPage_Unloaded;
        }

        private void TeamPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTeams();
        }

        private async void LoadTeams()
        {
            //直接请求获取团队吧，暂时先不做本地数据库的缓存了
            var response = await HttpManager.Instance.GetAsync<GetTeamsResponse>(new HttpRequest($"{ApiUtils.Teams}?username={AccountManager.Instance.Account}"));
            if (response.Ok)
            {
                foreach (var team in response.Teams)
                {
                    if (team.IsLeader)
                    {
                        CreatedTeam.Add(team);
                    }
                    else
                    {
                        JoinedTeam.Add(team);
                    }
                }
            }
        }

        private void JoinedTeam_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            JoinBlank.Visibility = JoinedTeam.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            foreach (var team in e.NewItems)
            {
                var card = new TeamCard() { DataContext = team };
                card.MouseLeftButtonUp += Card_MouseLeftButtonUp;
                JoinedTeamList.Children.Add(card);
            }
        }

        private void CreatedTeam_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                CreateBlank.Visibility = CreatedTeam.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                foreach (var team in e.NewItems)
                {
                    var card = new TeamCard() { DataContext = team };
                    card.MouseLeftButtonUp += Card_MouseLeftButtonUp;
                    CreatedTeamList.Children.Add(card);
                }
            });
        }

        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as FrameworkElement).DataContext is TeamModel team)
            {
                var detail = new TeamDetail(team.TeamId, team.TeamName);
                DetailContainer.Content = detail;
            }
        }

        private void TeamPage_Unloaded(object sender, RoutedEventArgs e)
        {
            //辣鸡回收一波
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// 展示CreateTeam控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTeam_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.CreateTeam.Visibility = Visibility.Visible;
            }
        }
    }
}
