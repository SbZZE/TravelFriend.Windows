using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel();
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

        private async void MenuAvatar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(AccountManager.Instance.Account))
                return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "图片(*.jpg;*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            if (dialog.ShowDialog() == true)
            {
                var response = HttpManager.Instance.UploadFile<HttpResponse>(new UploadRequest($"{ApiUtils.Avatar}?username={AccountManager.Instance.Account}", dialog.FileName));
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
    }
}
