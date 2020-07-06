using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http.Album;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Transport
{
    /// <summary>
    /// UploadBlock.xaml 的交互逻辑
    /// </summary>
    public partial class UploadBlock : UserControl
    {
        private BreakPointManager _breakPointManager;
        private UploadBlockViewModel _uploadBlockViewModel;

        public UploadBlock(string fileName, string fileSize, FileType fileType, string target)
        {
            InitializeComponent();
            _uploadBlockViewModel = new UploadBlockViewModel()
            {
                FileName = fileName,
                FileSize = fileSize,
                FileType = fileType,
                Target = target
            };
            _breakPointManager = new BreakPointManager();
            _breakPointManager.OnUploadProgressChanged += BreakPointManager_OnUploadProgressChanged;
            _breakPointManager.OnUploadCompleted += BreakPointManager_OnUploadCompleted;
            _breakPointManager.OnUploadFailure += BreakPointManager_OnUploadFailure;
            DataContext = _uploadBlockViewModel;
            Unloaded += UploadBlock_Unloaded;
        }

        public void UploadPrepare(string targetId, string albumId, AlbumType albumType, string filePath)
        {
            _uploadBlockViewModel.TargetId = targetId;
            _uploadBlockViewModel.AlbumId = albumId;
            _uploadBlockViewModel.Identifier = FileHelper.GetIdentifier(filePath);
            _breakPointManager.UploadPrepare(targetId, albumId, albumType, _uploadBlockViewModel.FileType, filePath);
        }

        private void BreakPointManager_OnUploadProgressChanged(double progress, int time, double speed)
        {
            _uploadBlockViewModel.Progress = (int)progress;
            _uploadBlockViewModel.Time = time;
            if (speed >= 1000)
            {
                _uploadBlockViewModel.Speed = (speed / 1000).ToString("0.0") + "m/s";
            }
            else
            {
                _uploadBlockViewModel.Speed = speed.ToString() + "kb/s";
            }
        }

        private void BreakPointManager_OnUploadFailure()
        {

        }

        private void BreakPointManager_OnUploadCompleted()
        {
            _uploadBlockViewModel.Progress = 100;
            _uploadBlockViewModel.Timestamp = string.Empty;
            _uploadBlockViewModel.Speed = string.Empty;
        }

        private void UploadBlock_Unloaded(object sender, RoutedEventArgs e)
        {
            _breakPointManager.OnUploadProgressChanged -= BreakPointManager_OnUploadProgressChanged;
            _breakPointManager.OnUploadCompleted -= BreakPointManager_OnUploadCompleted;
            _breakPointManager.OnUploadFailure -= BreakPointManager_OnUploadFailure;
        }

        private void Pause_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (_uploadBlockViewModel.UploadStatus)
            {
                case UploadStatus.Uploading:
                    _breakPointManager.UploadPause();
                    _uploadBlockViewModel.UploadStatus = UploadStatus.Pause;
                    break;
                case UploadStatus.Pause:
                    var uploader = UploadManager.GetUploader(_uploadBlockViewModel.TargetId, _uploadBlockViewModel.AlbumId, _uploadBlockViewModel.Identifier);
                    _breakPointManager.UploadStart(uploader);
                    _uploadBlockViewModel.UploadStatus = UploadStatus.Uploading;
                    break;
                default:
                    break;
            }
        }
    }
}
