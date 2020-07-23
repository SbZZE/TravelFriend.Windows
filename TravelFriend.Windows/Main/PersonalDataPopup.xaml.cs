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
    /// PersonalDataPopup.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalDataPopup : Popup
    {
        public PersonalDataPopup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.ShadePopup.Content = new UpdatePersonalData();
            mainWindow.ShadePopup.Visibility = Visibility.Visible;
        }
    }
}
