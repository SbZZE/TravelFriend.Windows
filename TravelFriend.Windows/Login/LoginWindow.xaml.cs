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
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;
using TravelFriend.Windows.RabbitMQ;

namespace TravelFriend.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel LoginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            LoginViewModel = DataContext as LoginViewModel;
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

        private void LoginSuccess(string token)
        {
            AccountManager.Instance.UserToken = token;
            AccountManager.Instance.Account = LoginViewModel.UserName;
            Close();
            //主界面更新
            if (App.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Unlogin.Visibility = Visibility.Collapsed;
                mainWindow.PersonalData.Visibility = Visibility.Visible;
                mainWindow.WindowState = WindowState.Normal;
                RabbitMQ.RabbitMQManager.Connection();
                Task.Run(async () =>
                {
                    //获取个人资料
                    var response = await HttpManager.Instance.GetAsync<GetUserInfoResponse>(new HttpRequest($"{ApiUtils.UserInfo}?username={LoginViewModel.UserName}"));
                    if (response.Ok)
                    {
                        var user = response.data;
                        Dispatcher.Invoke(() =>
                        {
                            mainWindow.GetViewModel.NickName = user.NickName;
                            mainWindow.GetViewModel.Address = user.Address;
                            mainWindow.GetViewModel.Gender = user.Gender;
                        });
                        user.Avatar = await ImageHelper.GetAvatarByteAsync(LoginViewModel.UserName);
                        user.Password = LoginViewModel.Password;
                        //把最近登录的账号信息存到本地数据库
                        UserManager.UpdateUser(user);
                        NotifyManager.UpdateAvatar(user.UserName);
                    }
                });
            }
        }
    }
}
