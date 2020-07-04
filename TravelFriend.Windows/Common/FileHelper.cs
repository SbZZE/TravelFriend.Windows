using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
