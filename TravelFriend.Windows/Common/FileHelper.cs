using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Common
{
    public class FileHelper
    {
        public static string FileFileter = "图片或视频|*.jpg;*.png;*.gif;*.jpeg;*.bmp;*.mkv;*.avi;*.mp4;*.tif;*.tiff";

        //图片文件所以可能的扩展名
        static string[] Images = { ".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".jfif", ".png", ".gif", ".tif", ".tiff" };
        //视频文件所以可能的扩展名
        static string[] Videos = { ".mp4", ".avi", ".mkv" };

        public static FileType GetFileType(string fileExtension)
        {
            if (Images.Contains(fileExtension))
                return FileType.IMAGE;
            if (Videos.Contains(fileExtension))
                return FileType.VIDEO;
            else
                return FileType.UNKNOWN;
        }

        public static string GetIdentifier(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            string lastModified = new FileInfo(filePath).LastWriteTime.ToString();
            return GenerateMD5($"{AccountManager.Instance.Account}{fileName}{filePath}{lastModified}");
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
