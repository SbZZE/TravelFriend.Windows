using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http.Album;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Transport
{
    /// <summary>
    /// TransportPage.xaml 的交互逻辑
    /// </summary>
    public partial class TransportPage : UserControl
    {
        public TransportPage()
        {
            InitializeComponent();
        }

        public void LoadTransportList()
        {
            var uploaders = UploadManager.GetAllUploader(AccountManager.Instance.Account);
            foreach (var uploader in uploaders)
            {
                var fileSize = ((double)uploader.FileSize / 1024 / 1024).ToString("0.00");
                var uploadBlock = new UploadBlock(uploader.FileName, fileSize, (FileType)uploader.FileType, uploader.Target);
                uploadBlock._uploadBlockViewModel.Progress = (int)uploader.Progress;
                UploadList.Items.Add(uploadBlock);
                uploadBlock.UploadPrepare(uploader.TargetId, uploader.AlbumId, (AlbumType)uploader.AlbumType, uploader.FilePath);
            }
        }
    }
}
