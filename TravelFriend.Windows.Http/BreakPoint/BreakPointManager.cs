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
        private const int CHUNKSIZE = 1 * 1024 * 1024;
        //图片文件所以可能的扩展名
        static string[] Images = { ".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".jfif", ".png", ".gif", ".tif", ".tiff" };
        //视频文件所以可能的扩展名
        static string[] Videos = { ".mp4", ".avi", ".mkv" };

        public static void BreakPointUpload(string targetId, string albumId, AlbumType albumType, string filePath)
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
                    int totalChunks = totalSize / CHUNKSIZE;
                    while (true)
                    {
                        int r = fileStream.Read(buffer, 0, buffer.Length);
                        if (r == 0)
                        {
                            break;//读到没有就结束
                        }

                        var response = HttpManager.Instance.BreakPointUpload<HttpResponse>(
                                new UploadAlbumFileRequest(targetId, albumId, albumType, fileName, fileType, identifier, totalSize, totalChunks, chunkNumber, CHUNKSIZE, r), buffer
                            );

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
