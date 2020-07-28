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
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// MemberCard.xaml 的交互逻辑
    /// </summary>
    public partial class MemberCard : UserControl
    {
        private bool IsLeader;
        public MemberCard(bool isLeader)
        {
            InitializeComponent();
            IsLeader = isLeader;
            Loaded += MemberCard_Loaded;
        }

        private void MemberCard_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsLeader && DataContext is TeamMember member && !member.IsLeader)
            {
                //我创建的团队且该member不是我
                Delete.Visibility = Visibility.Visible;
            }
            Avatar.UpdateWithHttp(50, 50);//请求获取头像
        }

        private async void Delete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is TeamMember member && Window.GetWindow(this) is MainWindow mainWindow)
            {
                var response = await HttpManager.Instance.DeleteAsync<HttpResponse>(new HttpRequest($"{ApiUtils.DeleteMember}?teamid={member.TeamId}&username={member.UserName}"));
                mainWindow.Toast.Show(response.message);
            }
        }
    }
}
