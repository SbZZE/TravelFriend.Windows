using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class Message
    {
        /// <summary>
        /// 团队ID
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// 成员用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 成员昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否为我发送的
        /// </summary>
        public bool IsSendByMe { get; set; }
    }
}
