using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
            DataContext = new MainWindowViewModel();
        }

        public MainWindowViewModel GetViewModel => DataContext is MainWindowViewModel viewModel ? viewModel : null;

        private void Unlogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //未登录，打开登录窗口
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void PersonalData_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

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
    }
}
