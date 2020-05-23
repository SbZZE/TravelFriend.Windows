using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.RabbitMQ
{
    public class MessageModel
    {
        public string Id { get; set; }
        public MessageType Type { get; set; }
        public string Account { get; set; }
        public MessageClient Client { get; set; }
        public string Content { get; set; }
    }
}
