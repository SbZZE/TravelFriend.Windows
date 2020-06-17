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
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.Login;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// CreateTeam.xaml 的交互逻辑
    /// </summary>
    public partial class CreateTeam : UserControl
    {
        public CreateTeam()
        {
            InitializeComponent();
        }
        private  void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private async void Determine_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TeamName.Text))
            {
                Toast.Show(RStrings.InputTeamName);
                return;
            }
            if (string.IsNullOrEmpty(TeamProfile.Text))
            {
                Toast.Show(RStrings.InputTeamProfile);
                return;
            }

            var response = await HttpManager.Instance.PostAsync<HttpResponse>(new CreateTeamRequest(AccountManager.Instance.Account, TeamName.Text, TeamProfile.Text));
            if (response.Ok)
            {
                
                Visibility = Visibility.Collapsed;
                //var window = (MainWindow)App.Current.MainWindow;//CodeReview这里用if(App.Current.MainWindow is MainWindow window){  }，这样强转失败不会报错
                //，会先判断前面那个是不是 MainWindow类型，是的话命名为window，以后最好都这样写
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
