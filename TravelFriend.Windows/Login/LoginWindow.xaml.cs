using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TravelFriend.Windows.Chat;
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;
using TravelFriend.Windows.Login;
using TravelFriend.Windows.RabbitMQ;
using TravelFriend.Windows.Styles;
using TravelFriend.Windows.Transport;

namespace TravelFriend.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginViewModel LoginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            LoginViewModel = DataContext as LoginViewModel;
            Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Password.Password = LoginViewModel.IsRememberPassword ? LoginViewModel.Password : string.Empty;
        }

        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Min_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Avatar_MouseEnter(object sender, MouseEventArgs e)
        {
            AvatarScale.ScaleX = 1.1;
            AvatarScale.ScaleY = 1.1;
        }

        private void Avatar_MouseLeave(object sender, MouseEventArgs e)
        {
            AvatarScale.ScaleX = 1;
            AvatarScale.ScaleY = 1;
        }

        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Password = string.IsNullOrEmpty(Password.Password) ? LoginViewModel.Password : Password.Password;
            var response = await HttpManager.Instance.GetAsync<LoginResponse>(new LoginRequest(LoginViewModel.UserName, LoginViewModel.Password));
            switch (response.code)
            {
                case 200:
                    //登录成功
                    LoginSuccess(response.token);
                    break;
                case 201:
                    break;
                case 202:
                    break;
                default:
                    Console.WriteLine(response.message);
                    break;
            }
        }

        private async void LoginSuccess(string token)
        {
            AccountManager.Instance.UserToken = token;
            AccountManager.Instance.Account = LoginViewModel.UserName;
            Close();
            //主界面更新
            if (App.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Unlogin.Visibility = Visibility.Collapsed;
                mainWindow.PersonalData.Visibility = Visibility.Visible;
                mainWindow.Team.Visibility = Visibility.Visible;
                mainWindow.Transport.Visibility = Visibility.Visible;
                mainWindow.TransportContainer.LoadTransportList();
                mainWindow.WindowState = WindowState.Normal;
                RabbitMQ.RabbitMQManager.Connection();

                //获取个人资料
                var response = await HttpManager.Instance.GetAsync<GetUserInfoResponse>(new HttpRequest($"{ApiUtils.UserInfo}?username={LoginViewModel.UserName}"));
                if (response.Ok)
                {
                    var user = response.data;
                    user.NickName = string.IsNullOrEmpty(user.NickName) ? user.UserName : user.NickName;
                    AccountManager.Instance.NickName = string.IsNullOrEmpty(user.NickName) ? user.UserName : user.NickName;
                    user.Avatar = await ImageHelper.GetAvatarByteAsync(LoginViewModel.UserName);
                    user.IsRememberPassword = LoginViewModel.IsRememberPassword;
                    user.Password = LoginViewModel.IsRememberPassword ? LoginViewModel.Password : string.Empty;
                    //把最近登录的账号信息存到本地数据库
                    UserManager.SetUserToLast(user);
                    NotifyManager.UpdateUserAvatar(user.UserName);
                    NotifyManager.UpdateUserInfo(user.UserName);
                }
                mainWindow.ReloadTeams();
                ChatManager.Instance.ConnectChat();
            }
        }

        private void RegisterAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Register.Visibility = Visibility.Visible;
        }

        private void SelectAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<UserNameModel> users = new List<UserNameModel>();
            UserManager.GetAllUserName().ForEach(x => users.Add(new UserNameModel { UserName = x }));
            AccountList.ItemsSource = users;
            AccountPopup.IsOpen = true;
        }

        private void AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountList.SelectedItem is UserNameModel userNameModel)
            {
                var user = UserManager.GetUserByUserName(userNameModel.UserName);
                LoginViewModel.UserName = user.UserName;
                LoginViewModel.Password = user.Password;
                Password.Password = user.Password;
                LoginViewModel.IsRememberPassword = user.IsRememberPassword;
                AccountPopup.IsOpen = false;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement).DataContext is UserNameModel userNameModel)
            {
                UserManager.DeleteUserByUserName(userNameModel.UserName);
                if (userNameModel.UserName == LoginViewModel.UserName)
                {
                    LoginViewModel.UserName = string.Empty;
                    LoginViewModel.Password = string.Empty;
                    Password.Password = string.Empty;
                    LoginViewModel.IsRememberPassword = false;
                }
                AccountPopup.IsOpen = false;
            }
        }
    }
}
