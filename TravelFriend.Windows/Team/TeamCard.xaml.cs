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
using TravelFriend.Windows.RabbitMQ;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// TeamCard.xaml 的交互逻辑
    /// </summary>
    public partial class TeamCard : UserControl
    {
        public TeamCard()
        {
            InitializeComponent();
            Loaded += TeamCard_Loaded;
        }

        private void TeamCard_Loaded(object sender, RoutedEventArgs e)
        {
            Avatar.UpdateWithHttp();
        }
    }
}
