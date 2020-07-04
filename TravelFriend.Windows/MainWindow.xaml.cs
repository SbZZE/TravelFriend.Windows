﻿using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
using TravelFriend.Windows.Chat;
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Home;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;
using TravelFriend.Windows.Styles;
using TravelFriend.Windows.Team;
using TravelFriend.Windows.Transport;
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TeamChatAvatar> TeamsList = new ObservableCollection<TeamChatAvatar>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            TeamsList.CollectionChanged += TeamsList_CollectionChanged;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel();
            Home.IsChecked = true;
        }

        private void Unlogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //未登录，打开登录窗口
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void PersonalData_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PersonalDataPopup.IsOpen = PersonalDataPopup.IsOpen ? false : true;
        }

        private void Setting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SettingPopup.IsOpen = true;
        }

        private async void MenuAvatar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(AccountManager.Instance.Account))
                return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "图片(*.jpg;*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            if (dialog.ShowDialog() == true)
            {
                var response = HttpManager.Instance.UploadFile<HttpResponse>(new UploadRequest($"{ApiUtils.UserAvatar}?username={AccountManager.Instance.Account}", dialog.FileName, "avatar"));
                if (response.Ok)
                {
                    Toast.Show(RStrings.UpdateSuccess);
                    var user = GetUserByUserName(AccountManager.Instance.Account);
                    if (user != null)
                    {
                        user.Avatar = await ImageHelper.GetAvatarByteAsync(AccountManager.Instance.Account);
                        UserManager.UpdateUser(user);
                    }
                }
            }
        }

        public User GetUserByUserName(string userName) => UserManager.GetUserByUserName(userName);

        public MainWindowViewModel GetViewModel => DataContext is MainWindowViewModel viewModel ? viewModel : null;

        #region 窗口相关
        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Max_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Min_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
            else
            {
                DragMove();
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            GetViewModel.IsMax = WindowState == WindowState.Maximized ? new BitmapImage(new Uri("/Resources/Gray/NoMax.png", UriKind.Relative)) : new BitmapImage(new Uri("/Resources/Gray/Max.png", UriKind.Relative));
        }
        #endregion

        private void Home_Checked(object sender, RoutedEventArgs e)
        {
            TransportContainer.Visibility = Visibility.Collapsed;
            PageContainer.Content = new HomePage();
        }

        private void Team_Checked(object sender, RoutedEventArgs e)
        {
            TransportContainer.Visibility = Visibility.Collapsed;
            PageContainer.Content = new TeamPage();
        }

        private void Travel_Checked(object sender, RoutedEventArgs e)
        {
            TransportContainer.Visibility = Visibility.Collapsed;
        }

        private void Transport_Checked(object sender, RoutedEventArgs e)
        {
            TransportContainer.Visibility = Visibility.Visible;
        }

        #region Chat
        private void TeamsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TeamAvatarList.ItemsSource = TeamsList;
        }

        public async void ReloadTeams()
        {
            if (TeamsList != null && TeamsList.Count != 0)
            {
                TeamsList.Clear();
            }
            //直接请求获取团队吧，暂时先不做本地数据库的缓存了
            var response = await HttpManager.Instance.GetAsync<GetTeamsResponse>(new HttpRequest($"{ApiUtils.Teams}?username={AccountManager.Instance.Account}"));
            if (response.Ok)
            {
                foreach (var team in response.Teams)
                {
                    var teamChatAvatar = new TeamChatAvatar() { DataContext = team };
                    TeamsList.Add(teamChatAvatar);
                }
            }
        }
        #endregion
    }
}
