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
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string TargetId { get; set; }
        /// <summary>
        /// 相册
        /// </summary>
        public string AlbumId { get; set; }
        /// <summary>
        /// 相册类型
        /// </summary>
        public int AlbumType { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public int FileType { get; set; }
        /// <summary>
        /// 文件上传进度
        /// </summary>
        public double Progress { get; set; }
        /// <summary>
        /// 文件唯一标识
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 已上传的分片序号
        /// </summary>
        public int ChunkNumber { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
    }
}
