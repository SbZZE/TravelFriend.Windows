using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.RabbitMQ.Observe;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows.Chat
{
    public class ChatManager
    {
        private static readonly Lazy<ChatManager> _instance = new Lazy<ChatManager>(() => new ChatManager());
        public static ChatManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private ChatManager() { }

        public Subject MessageSubject = new MessageSubject();
        public HubConnection connection;
        public Message Message { get; set; }

        public async void ConnectChat()
        {
            connection = new HubConnectionBuilder()
               //.WithUrl("http://localhost:5000/ChatHub")
               .WithUrl("http://47.106.139.187:5005/ChatHub")
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string, string>("Login", (userName, nickName, teamId) =>
            {
                Message = new Message()
                {
                    TeamId = teamId,
                    UserName = userName,
                    NickName = nickName,
                    Content = RStrings.Logined,
                    IsSendByMe = false,
                    Type = MessageType.Tip
                };
                MessageSubject.Notify();
            });

            connection.On<string, string>("Logout", (userName, teamId) =>
            {
                Console.WriteLine(teamId);
                Console.WriteLine(userName);
            });

            connection.On<string, string, string, string, string>("Received", (teamId, userName, nickName, sendTime, content) =>
            {
                bool isSendByMe = false;
                //是自己发的
                if (userName == AccountManager.Instance.Account)
                {
                    isSendByMe = true;
                }
                Message = new Message()
                {
                    TeamId = teamId,
                    UserName = userName,
                    NickName = nickName,
                    SendTime = sendTime,
                    Content = content,
                    IsSendByMe = isSendByMe,
                    Type = MessageType.Message
                };
                MessageSubject.Notify();
            });

            try
            {
                await connection.StartAsync();
                await connection.SendAsync("UserLogin", AccountManager.Instance.Account, AccountManager.Instance.NickName);
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="content">发送的内容</param>
        public async void SendMessage(string teamId, string nickName, string sendTime, string content)
        {
            await connection.SendAsync("SendMessage", teamId, nickName, sendTime, content);
        }

        public async void Logout()
        {
            if (connection != null)
            {
                await connection.SendAsync("UserLogout");
            }
        }
    }
}
