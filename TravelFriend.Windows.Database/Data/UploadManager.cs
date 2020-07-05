using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class UploadManager
    {
        /// <summary>
        /// 添加上传项
        /// </summary>
        public static void AddUploader(Upload upload)
        {
            SqliteHelper.Instance.Add<Upload>(upload);
        }

        /// <summary>
        /// 更新上传项
        /// </summary>
        public static int UpdateUploader(Upload upload)
        {
            return SqliteHelper.Instance.Update<Upload>(upload);
        }

        /// <summary>
        /// 获取某用户所有正在上传项
        /// </summary>
        public static List<Upload> GetAllUploader(string userName)
        {
            return SqliteHelper.Instance.Query<Upload>($"Select * from Upload where UserName='{userName}'");
        }

        /// <summary>
        /// 根据文件唯一标识获取上传信息
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static Upload GetUploader(string targetId, string albumId, string identifier)
        {
            return SqliteHelper.Instance.Query<Upload>($"Select * from Upload where Identifier='{identifier}' and TargetId='{targetId}' and AlbumId='{albumId}'").FirstOrDefault();
        }

        /// <summary>
        /// 删除上传对象
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public static int DeleteUploader(Upload upload)
        {
            return SqliteHelper.Instance.Delete(upload);
        }
    }
}
