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
                foreach (var filePath in filePaths)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (App.Current.MainWindow is MainWindow mainWindow)
                    {
                        var uploadBlock = new UploadBlock(fileInfo.Name, ((double)fileInfo.Length / 1024 / 1024).ToString("0.00"), FileHelper.GetFileType(fileInfo.Extension), $"{TeamName}/{AlbumName}");
                        mainWindow.TransportContainer.UploadList.Items.Add(uploadBlock);
                        uploadBlock.UploadPrepare(TeamId, AlbumId, AlbumType, filePath);
                    }
                }
            }
        }
    }
}
