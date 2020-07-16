using Microsoft.Win32;
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
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.Album;
using TravelFriend.Windows.Http.BreakPoint;
using TravelFriend.Windows.Transport;

namespace TravelFriend.Windows.Album
{
    /// <summary>
    /// AlbumPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlbumPage : UserControl
    {
        private string TeamId;
        private string AlbumId;
        private string TeamName;
        private string AlbumName;
        private AlbumType AlbumType;

        public AlbumPage(string teamId, string albumId, string teamName, string albumName, AlbumType albumType)
        {
            InitializeComponent();
            TeamId = teamId;
            AlbumId = albumId;
            TeamName = teamName;
            AlbumName = albumName;
            AlbumType = albumType;
            Loaded += AlbumPage_Loaded;
        }

        private async void AlbumPage_Loaded(object sender, RoutedEventArgs e)
        {
            var response = await HttpManager.Instance.GetAsync<GetThumbnailListResponse>(new HttpRequest($"{ApiUtils.ThumbnailList}?albumid={AlbumId}"));
            if (response.Ok)
            {
                var thumbnails = response.Data;
                foreach (var thumbnail in thumbnails)
                {
                    ResetColumnCount(ActualWidth);
                    AlbumDetail.Children.Add(new Thumbnail(thumbnail.FileId));
                }
            }
        }

        private void Return_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = FileHelper.FileFileter
            };
            if (fileDialog.ShowDialog() == true)
            {
                var filePaths = fileDialog.FileNames;
                if (App.Current.MainWindow is MainWindow mainWindow)
                {
                    foreach (var filePath in filePaths)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        var uploadBlock = new UploadBlock(fileInfo.Name, ((double)fileInfo.Length / 1024 / 1024).ToString("0.00"), FileHelper.GetFileType(fileInfo.Extension), $"{TeamName}/{AlbumName}");
                        mainWindow.TransportContainer.UploadList.Items.Add(uploadBlock);
                        var uploader = uploadBlock.UploadPrepare(TeamId, AlbumId, AlbumType, filePath);
                        uploadBlock.UploadStart(uploader);
                    }
                    mainWindow.TransportContainer.EmptyTip.Visibility = mainWindow.TransportContainer.UploadList.Items.IsEmpty ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetColumnCount(e.NewSize.Width);
            AlbumDetail.Measure(AlbumDetail.RenderSize);
        }

        public void ResetColumnCount(double width)
        {
            if (AlbumDetail.Children.Count <= 0)
            {
                return;
            }
            int columnNum = (int)width / 270;
            //只在列数改变的情况下赋值，有效减少重绘次数
            if (columnNum != AlbumDetail.ColumnCount)
            {
                AlbumDetail.ColumnCount = columnNum;
            }
        }
    }
}
