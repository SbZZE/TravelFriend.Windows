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
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;

namespace TravelFriend.Windows.Main
{
    /// <summary>
    /// UpdatePersonalData.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatePersonalData : UserControl
    {
        public UpdatePersonalDataViewModel ViewModel;

        public UpdatePersonalData()
        {
            InitializeComponent();
            Loaded += UpdatePersonalData_Loaded;
            ViewModel = new UpdatePersonalDataViewModel();
            DataContext = ViewModel;
        }

        private void UpdatePersonalData_Loaded(object sender, RoutedEventArgs e)
        {
            var user = UserManager.GetUserByUserName(AccountManager.Instance.Account);
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                ViewModel.NickName = user.NickName;
                ViewModel.Gender = user.Gender;
                ViewModel.Address = user.Address;
                ViewModel.Birthday = user.Birthday;
                ViewModel.Signature = user.Signature;
            }
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
            int gender = ViewModel.Gender == "0" ? 0 : 1;
            var response = await HttpManager.Instance.PostAsync<HttpResponse>(new UpdateUserInfoRequest(AccountManager.Instance.Account, ViewModel.NickName, ViewModel.Gender, ViewModel.Birthday, ViewModel.Address, ViewModel.Signature));
            if (response.Ok)
            {
                Visibility = Visibility.Collapsed;
                var window = (MainWindow)App.Current.MainWindow;
                window.Toast.Show(response.message);
            }
            else
            {
                Toast.Show(response.message);
            }
        }
    }
}
