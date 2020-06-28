using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    /// UpdateTeamData.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateTeamData : UserControl
    {
        public UpdateTeamData()
        {
            InitializeComponent();
        }

        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext;
            if (data is TeamModel teamModel)
            {
                var response = await HttpManager.Instance.PostAsync<HttpResponse>(new UpdateTeamRequest(teamModel.TeamId, TeamName.Text, TeamProfile.Text));
                if (response.Ok)
                {
                    Visibility = Visibility.Collapsed;
                    if (App.Current.MainWindow is MainWindow window)
                    {
                        //创建成功，清空文本框
                        window.Toast.Show(response.message);
                        TeamName.Text = "";
                        TeamProfile.Text = "";
                    }
                }
                else
                {
                    Toast.Show(response.message);
                }
            }
        }
    }
}
