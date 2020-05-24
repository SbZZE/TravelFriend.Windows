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
using System.Windows.Shapes;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.Login;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows.Login
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

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
            var response = await HttpManager.Instance.PostAsync<HttpResponse>(new RegisterRequest(UserName.Text, Password.Password, NickName.Text, string.Empty));
            if (response.Ok)
            {

            }
        }
    }
}
