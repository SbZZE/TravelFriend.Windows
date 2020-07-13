using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Database.Model
{
    public class Thumbnail
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 团队ID
        /// </summary>
        public FileType Type { get; set; }
    }
}
