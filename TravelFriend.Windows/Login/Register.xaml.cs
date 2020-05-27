using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.Login;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows.Login
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        public LoginWindow GetLoginWindow => App.Current.Windows.Cast<Window>().FirstOrDefault(x => x is LoginWindow) as LoginWindow;

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Text))
            {
                Toast.Show(RStrings.InputAccount);
                return;
            }
            if (string.IsNullOrEmpty(NickName.Text))
            {
                Toast.Show(RStrings.InputNickName);
                return;
            }
            if (string.IsNullOrEmpty(Password.Password))
            {
                Toast.Show(RStrings.InputPassword);
                return;
            }
            if (Password.Password != ConfirmPassword.Password)
            {
                Toast.Show(RStrings.PasswordNotSame);
                return;
            }

            //注册逻辑
            var response = await HttpManager.Instance.PostAsync<HttpResponse>(new RegisterRequest(UserName.Text, Password.Password, NickName.Text));
            if (response.Ok)
            {
                switch (response.code)
                {
                    case 200:
                        GetLoginWindow.Register.Visibility = Visibility.Collapsed;
                        GetLoginWindow.Toast.Show(RStrings.RegisterSuccess);
                        break;
                    case 201:
                        Toast.Show(RStrings.UserExist);
                        break;
                    case 202:
                        Toast.Show(RStrings.EmailPhoneError);
                        break;
                    case 203:
                        Toast.Show(RStrings.RegisterFail);
                        break;
                }
            }
        }

        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetLoginWindow.Close();
        }

        private void Login_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetLoginWindow.Register.Visibility = Visibility.Collapsed;
        }
    }
}
