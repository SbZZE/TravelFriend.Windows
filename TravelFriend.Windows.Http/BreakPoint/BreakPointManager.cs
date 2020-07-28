using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http.Album;

namespace TravelFriend.Windows.Http.BreakPoint
{
    public class BreakPointManager
    {
        #region 事件
        /// <summary>
        /// 上传开始事件(参数:文件路径)
        /// </summary>
        public event Action OnUploadStart;
        /// <summary>
        /// 上传完成事件
        /// </summary>
        public event Action OnUploadCompleted;
        /// <summary>
        /// 上传进度事件(参数:已上传百分比,剩余时间，速度)
        /// </summary>
        public event Action<double, int, double> OnUploadProgressChanged;
        /// <summary>
        /// 上传失败事件
        /// </summary>
        public event Action OnUploadFailure;
        #endregion
        private const int CHUNKSIZE = 1 * 1024 * 1024;
        private UploadStatus UploadStatus = UploadStatus.Pause;

        /// <summary>
        /// 准备上传
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetId"></param>
        /// <param name="albumId"></param>
        public Upload UploadPrepare(string targetId, string albumId, AlbumType albumType, FileType fileType, string target, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            string lastModified = new FileInfo(filePath).LastWriteTime.ToString();
            var identifier = GenerateMD5($"{AccountManager.Instance.Account}{fileName}{filePath}{lastModified}");
            Upload uploader;
            var isUploading = IsUploading(targetId, albumId, identifier);
            //校验是否在正在上传列表
            if (isUploading.Item1)
            {
                uploader = isUploading.Item2;
            }
            else
            {
                UploadManager.AddUploader(new Upload()
                {
                    UserName = AccountManager.Instance.Account,
                    FileName = fileName,
                    FileSize = (int)fileInfo.Length,
                    FileType = (int)fileType,
                    TargetId = targetId,
                    AlbumId = albumId,
                    AlbumType = (int)albumType,
                    Progress = 0,
                    Identifier = identifier,
                    ChunkNumber = 0,
                    FilePath = filePath,
                    Target = target
                });
                uploader = UploadManager.GetUploader(targetId, albumId, identifier);
            }
            return uploader;
        }

        /// <summary>
        /// 开始上传
        /// </summary>
        /// <param name="upload"></param>
        public async void UploadStart(Upload upload)
        {
            UploadStatus = UploadStatus.Uploading;
            if (upload != null && upload.FileType != (int)FileType.UNKNOWN)
            {
                using (FileStream fileStream = new FileStream(upload.FilePath, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[CHUNKSIZE];
                    int chunkNumber = upload.ChunkNumber;
                    int totalSize = upload.FileSize;
                    int totalChunks = (int)Math.Ceiling((double)totalSize / CHUNKSIZE);
                    int bytesRead = 0;
                    int offset = chunkNumber * CHUNKSIZE;
                    int uploadedLength = offset;
                    fileStream.Seek((long)offset, SeekOrigin.Begin);

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        //如果暂停了
                        if (UploadStatus == UploadStatus.Pause)
                        {
                            break;
                        }
                        //开始时间
                        var watch = new Stopwatch();
                        watch.Start();
                        //byte转换
                        var finalBuffer = new byte[bytesRead];
                        Buffer.BlockCopy(buffer, 0, finalBuffer, 0, bytesRead);
                        //上传一个分片
                        var response = await HttpManager.Instance.BreakPointUploadAsync<BreakPointUploadResponse>(
                                new UploadAlbumFileRequest(upload.TargetId, upload.AlbumId, (AlbumType)upload.AlbumType, upload.FileName, (FileType)upload.FileType, upload.Identifier, totalSize, totalChunks, chunkNumber, CHUNKSIZE, bytesRead), finalBuffer
                            );
                        //当前分片上传失败(只要不是完成和分片成功都视为失败)
                        if (!response.Ok && response.code != 201)
                        {
                            OnUploadFailure?.Invoke();
                            break;
                        }
                        //上传完成
                        if (response.Ok)
                        {
                            OnUploadCompleted?.Invoke();
                            UploadManager.DeleteUploader(upload);
                            break;
                        }
                        var progress = Convert.ToDouble((uploadedLength / (Double)totalSize) * 100);
                        //当前分片上传成功
                        upload.Progress = progress;
                        upload.ChunkNumber = chunkNumber;
                        var a = UploadManager.UpdateUploader(upload);
                        chunkNumber++;//文件块序号+1
                        //结束时间
                        watch.Stop();
                        uploadedLength += bytesRead;
                        //速度计算byte/s
                        var speed = CHUNKSIZE * 1000L / watch.ElapsedMilliseconds;
                        //计算剩余时间
                        var time = (totalSize - uploadedLength) / speed;
                        //上传进度通知
                        OnUploadProgressChanged?.Invoke(progress, (int)time, speed / 1024);
                    }
                }
            }
        }

        public void UploadPause()
        {
            UploadStatus = UploadStatus.Pause;
        }

        private (bool, Upload) IsUploading(string targetId, string albumId, string identifier)
        {
            var uploader = UploadManager.GetUploader(targetId, albumId, identifier);
            if (uploader != null)
            {
                return (true, uploader);
            }
            else
            {
                return (false, null);
            }
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public string GenerateMD5(string txt)
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
