using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows.Main
{
    /// <summary>
    /// SettingPopup.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPopup : Popup
    {
        public SettingPopup()
        {
            InitializeComponent();
        }

        private void ChangeAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
