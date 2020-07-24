using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static BitmapImage GetImageByStreamAsync(Stream ms)
        {
            BitmapImage bmp = null;
            try
            {
                if (ms != null && ms.Length > 0)
                {
                    ms.Position = 0;
                    bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CreateOptions = BitmapCreateOptions.None;
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    bmp.Freeze();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                bmp = null;
            }
            return bmp;
        }

        /// <summary>
        /// 异步获取头像数组
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async static Task<byte[]> GetAvatarByteAsync(string userName)
        {
            //获取头像
            using (MemoryStream ms = new MemoryStream())
            {
                var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.UserAvatar}?username={userName}&isCompress=true&width=80&height=80"), ms);
                byte[] byteArray = null;
                try
                {
                    if (ms != null && ms.Length > 0)
                    {
                        //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。 
                        ms.Position = 0;
                        using (BinaryReader br = new BinaryReader(ms))
                        {
                            byteArray = br.ReadBytes((int)ms.Length);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return byteArray;
            }
        }

        /// <summary>
        /// byte数组转图片资源
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
                bmp.Freeze();
            }
            catch (Exception e)
            {
                bmp = null;
            }
            return bmp;
        }
    }
}
