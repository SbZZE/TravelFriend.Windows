using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// TeamPage.xaml 的交互逻辑
    /// </summary>
    public partial class TeamPage : UserControl
    {
        ObservableCollection<TeamModel> CreatedTeam = new ObservableCollection<Database.Model.Team>();
        ObservableCollection<TeamModel> JoinedTeam = new ObservableCollection<Database.Model.Team>();
        public TeamPage()
        {
            InitializeComponent();
            Loaded += TeamPage_Loaded;
            Unloaded += TeamPage_Unloaded;
        }

        private void TeamPage_Loaded(object sender, RoutedEventArgs e)
        {
            CreatedTeam = TeamManager.GetCreatedTeam();
            JoinedTeam = TeamManager.GetJoinedTeam();
            CreateBlank.Visibility = CreatedTeam.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            JoinBlank.Visibility = JoinedTeam.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            LoadTeamCard();
        }

        private void LoadTeamCard()
        {
            foreach (var team in CreatedTeam)
            {
                var card = new TeamCard() { DataContext = team };
                card.MouseLeftButtonUp += Card_MouseLeftButtonUp;
                CreatedTeamList.Children.Add(card);
            }
        }

        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as FrameworkElement).DataContext is TeamModel team)
            {
                var detail = new TeamDetail(team.TeamId);
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
    }
}
