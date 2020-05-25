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
            Opened += PersonalDataPopup_Opened;
        }

        private void PersonalDataPopup_Opened(object sender, EventArgs e)
        {
            //var user = UserManager.GetUserByUserName(AccountManager.Instance.Account);
            //if (user != null && !string.IsNullOrEmpty(user.UserName))
            //{
            //    DataContext = user;
            //}
        }
    }
}
