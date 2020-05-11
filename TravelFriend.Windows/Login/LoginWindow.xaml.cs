using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Http;

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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var response = await HttpManager.PostAsync<LoginResponse>(new LoginRequest(LoginViewModel.UserName, LoginViewModel.Password));
            switch (response.code)
            {
                case 200:
                    //登录成功
                    AccountManager.Instance.Account = LoginViewModel.UserName;
                    AccountManager.Instance.UserToken = response.token;
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
    }
}
