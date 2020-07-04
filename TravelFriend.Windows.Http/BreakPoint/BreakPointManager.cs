using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        /// 上传进度事件(参数:已上传百分比,剩余时间，速度)
        /// </summary>
        public event Action<double, int, double> UploadProgressChanged;
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

        private const int CHUNKSIZE = 1 * 1024 * 1024;

        public async Task UploadAsync(string targetId, string albumId, AlbumType albumType, FileType fileType, string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string lastModified = new FileInfo(filePath).LastWriteTime.ToString();

            if (fileType != FileType.UNKNOWN)
            {
                var identifier = GenerateMD5($"{AccountManager.Instance.Account}{fileName}{filePath}{lastModified}");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[CHUNKSIZE];
                    double uploadedTime = 0;
                    int uploadedLength = 0;
                    int chunkNumber = 0;
                    int totalSize = (int)fileStream.Length;
                    int totalChunks = (int)Math.Ceiling((double)totalSize / CHUNKSIZE);
                    int bytesRead = 0;
                    //开始上传事件通知
                    //UploadStart();
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        //开始时间
                        var startTime = DateTime.Now.Millisecond;
                        //byte转换
                        var finalBuffer = new byte[bytesRead];
                        Buffer.BlockCopy(buffer, 0, finalBuffer, 0, bytesRead);
                        //上传一个分片
                        var response = await HttpManager.Instance.BreakPointUploadAsync<BreakPointUploadResponse>(
                                new UploadAlbumFileRequest(targetId, albumId, albumType, fileName, fileType, identifier, totalSize, totalChunks, chunkNumber, CHUNKSIZE, bytesRead), finalBuffer
                            );
                        //当前分片上传失败
                        if (!response.Ok)
                        {
                            break;
                        }
                        //结束时间
                        var endTime = DateTime.Now.Millisecond;
                        //速度计算m/s
                        var speed = (double)1 / ((double)Math.Abs(endTime - startTime) / 1000);
                        uploadedTime += ((double)Math.Abs(endTime - startTime) / 1000);
                        //计算剩余时间
                        var time = ((double)totalSize / 1024 / 1024) / speed - uploadedTime;

                        //当前分片上传成功，上传进度通知
                        uploadedLength += bytesRead;
                        UploadProgressChanged(Convert.ToDouble((uploadedLength / (Double)totalSize) * 100), (int)time, speed);
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
