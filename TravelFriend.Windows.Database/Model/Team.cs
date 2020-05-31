using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class Team
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
        /// 团队名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 团队简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 是否是队长
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
