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
using TravelFriend.Windows.Database;
using TravelFriend.Windows.RabbitMQ.Observe;
using TeamModel = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Chat
{
    /// <summary>
    /// ChatPopup.xaml 的交互逻辑
    /// </summary>
    public partial class ChatPopup : Popup, IObserver
    {
        public ChatPopup()
        {
            InitializeComponent();
            Opened += ChatPopup_Opened;
            ChatManager.Instance.MessageSubject.Add(this);//订阅头像变化
        }

        private void ChatPopup_Opened(object sender, EventArgs e)
        {
            TeamAvatar.UpdateWithHttp();
        }

        private void Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsOpen = false;
        }

        public void Update()
        {
            var message = ChatManager.Instance.Message;
            Message.Items.Add(message);
            Message.ScrollIntoView(message);
        }

        private void Send_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(Input.Text) && (sender as FrameworkElement).DataContext is TeamModel teamModel)
            {
                ChatManager.Instance.SendMessage(teamModel.TeamId, AccountManager.Instance.NickName, DateTime.Now.ToString("hh:MM"), Input.Text);
                Input.Text = string.Empty;
                Input.Focus();
            }
        }

        private void UserAvatar_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TravelFriend.Windows.Common.UserAvatar avatar)
            {
                avatar.UpdateWithHttp();
            }
        }
    }
}
