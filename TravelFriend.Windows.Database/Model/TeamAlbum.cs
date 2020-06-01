using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class TeamAlbum
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
        /// 相册ID
        /// </summary>
        public string AlbumId { get; set; }
        /// <summary>
        /// 相册名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 相册内容数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public byte[] Cover { get; set; }
    }
}
