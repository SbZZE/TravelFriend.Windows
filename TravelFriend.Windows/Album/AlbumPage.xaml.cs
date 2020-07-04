using Microsoft.Win32;
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
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Album
{
    /// <summary>
    /// AlbumPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlbumPage : UserControl
    {
        private readonly string teamId;
        private readonly string albumId;

        public AlbumPage(string teamId, string albumId)
        {
            InitializeComponent();
            this.teamId = teamId;
            this.albumId = albumId;
        }

        private void Return_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                var filePath = fileDialog.FileName;
                var breakPointManager = new BreakPointManager();
                breakPointManager.UploadProgressChanged += BreakPointManager_UploadProgressChanged;
                await breakPointManager.UploadAsync(teamId, albumId, Http.Album.AlbumType.TEAM, filePath);
            }
        }

        private void BreakPointManager_UploadProgressChanged(double progress)
        {
            Progress.Value = progress;
        }
    }
}
