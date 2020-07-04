using System;
using System.Collections.Generic;
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
            _breakPointManager.UploadProgressChanged += BreakPointManager_UploadProgressChanged;
            DataContext = _uploadBlockViewModel;
            Loaded += UploadBlock_Loaded;
            Unloaded += UploadBlock_Unloaded;
        }

        private void UploadBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public async void StartUpload(string teamId, string albumId, AlbumType albumType, string filePath)
        {
            await _breakPointManager.UploadAsync(teamId, albumId, albumType, _uploadBlockViewModel.FileType, filePath);
        }

        private void BreakPointManager_UploadProgressChanged(double progress, int time, double speed)
        {
            _uploadBlockViewModel.Progress = (int)progress;
            _uploadBlockViewModel.Time = time;
            _uploadBlockViewModel.Speed = speed.ToString("0.00") + "m/s";
        }

        private void UploadBlock_Unloaded(object sender, RoutedEventArgs e)
        {
            _breakPointManager.UploadProgressChanged -= BreakPointManager_UploadProgressChanged;
        }
    }
}
