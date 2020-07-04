using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class Upload
    {
        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey]
        public int? Id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
    }
}
