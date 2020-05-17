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
        /// 异步获取头像
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async static Task<BitmapImage> GetAvatarAsync(string userName)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.Avatar}?username={userName}"), ms);
                    ms.Position = 0;
                    BitmapImage result = new BitmapImage();
                    result.BeginInit();
                    result.CacheOption = BitmapCacheOption.OnLoad;
                    result.StreamSource = ms;
                    result.EndInit();
                    result.Freeze();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
                var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.Avatar}?username={userName}"), ms);
                ms.Position = 0;
                BitmapImage result = new BitmapImage();
                try
                {
                    result.BeginInit();
                    result.CacheOption = BitmapCacheOption.OnLoad;
                    result.StreamSource = ms;
                    result.EndInit();
                    result.Freeze();
                }
                catch { }
                return ImageHelper.BitmapImageToByteArray(result);
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
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }

        /// <summary>
        /// 图片资源转byte数组
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;
            try
            {
                Stream sMarket = bmp.StreamSource;
                if (sMarket != null && sMarket.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。 
                    sMarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
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
}
