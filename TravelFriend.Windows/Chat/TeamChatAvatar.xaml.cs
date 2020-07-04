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
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Chat
{
    /// <summary>
    /// TeamChatAvatar.xaml 的交互逻辑
    /// </summary>
    public partial class TeamChatAvatar : UserControl
    {
        public TeamChatAvatar()
        {
            InitializeComponent();
            Loaded += TeamChatAvatar_Loaded;
        }

        private void TeamChatAvatar_Loaded(object sender, RoutedEventArgs e)
        {
            Avatar.UpdateWithHttp();
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //主界面更新
            if (App.Current.MainWindow is MainWindow mainWindow)
            {
                //mainWindow.ChatPopup.IsOpen = false;
                //mainWindow.ChatPopup.Message.Items.Clear();
                //mainWindow.ChatPopup.PlacementTarget = this;
                //mainWindow.ChatPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
                //mainWindow.ChatPopup.VerticalOffset = 50;
                //mainWindow.ChatPopup.HorizontalOffset = -10;
                //mainWindow.ChatPopup.DataContext = (sender as FrameworkElement).DataContext;
                //mainWindow.ChatPopup.IsOpen = true;
            }
        }
    }
}
