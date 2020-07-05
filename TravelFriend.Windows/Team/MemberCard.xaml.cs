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

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// MemberCard.xaml 的交互逻辑
    /// </summary>
    public partial class MemberCard : UserControl
    {
        public MemberCard()
        {
            InitializeComponent();
            Loaded += MemberCard_Loaded;
        }

        private void MemberCard_Loaded(object sender, RoutedEventArgs e)
        {
            Avatar.UpdateWithHttp(50, 50);//请求获取头像
        }
    }
}
