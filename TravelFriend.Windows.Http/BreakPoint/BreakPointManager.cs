using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Http.Album;

namespace TravelFriend.Windows.Http.BreakPoint
{
    public class BreakPointManager
    {
        public BreakPointManager()
        {

        }

        #region 事件
        /// <summary>
        /// 上传开始事件(参数:文件路径)
        /// </summary>
        public event Action UploadStart;
        /// <summary>
        /// 上传完成事件(参数:文件路径,返回参数)
        /// </summary>
        public event Action<String, String> UploadFileCompleted;
        /// <summary>
        /// 上传进度事件(参数:文件路径,文件总大小，上传速度,已上传的字节数,已耗费时间,已上传百分比)
        /// </summary>
        public event Action<String, Int64, Int64, Int64, Double, Double> UploadProgressChanged;
        /// <summary>
        /// 上传失败事件(参数:文件路径,错误原因)
        /// </summary>
        public event Action<String, String> UploadFailure;
        /// <summary>
        /// 上传被取消事件(参数:被取消的文件路径)
        /// </summary>
        public event Action<String> UploadCancel;
        /// <summary>
        /// 已删除未上传完成的文件事件(参数:资源文件名称,返回结果)
        /// </summary>
        public event Action<String, String> DeleteFileCompleted;
        #endregion

        private const int CHUNKSIZE = 5 * 1024 * 1024;
        //图片文件所以可能的扩展名
        static string[] Images = { ".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".jfif", ".png", ".gif", ".tif", ".tiff" };
        //视频文件所以可能的扩展名
        static string[] Videos = { ".mp4", ".avi", ".mkv" };

        public async void UploadAsync(string targetId, string albumId, AlbumType albumType, string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string lastModified = new FileInfo(filePath).LastWriteTime.ToString();
            FileType fileType = FileType.UNKNOWN;
            if (Images.Contains(fileExtension))
                fileType = FileType.IMAGE;
            if (Videos.Contains(fileExtension))
                fileType = FileType.VIDEO;

            if (fileType != FileType.UNKNOWN)
            {
                var identifier = GenerateMD5($"{AccountManager.Instance.Account}{fileName}{filePath}{lastModified}");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[CHUNKSIZE];
                    int chunkNumber = 0;
                    int totalSize = (int)fileStream.Length;
                    int totalChunks = (int)Math.Ceiling((double)totalSize / CHUNKSIZE);
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        Console.WriteLine(bytesRead);
                        var response = await HttpManager.Instance.BreakPointUploadAsync<BreakPointUploadResponse>(
                                new UploadAlbumFileRequest(targetId, albumId, albumType, fileName, fileType, identifier, totalSize, totalChunks, chunkNumber, CHUNKSIZE, bytesRead), buffer
                            );

                        if (response.data != null)
                        {

                        }

                        chunkNumber++;//文件块序号+1
                    }
                }
            }
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
