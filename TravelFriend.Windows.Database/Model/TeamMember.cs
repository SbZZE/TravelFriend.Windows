using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class TeamMember
    {
        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey]
        public int? Id { get; set; }
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
        /// 成员是否为队长
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
