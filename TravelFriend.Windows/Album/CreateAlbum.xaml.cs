using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Album
{
    /// <summary>
    /// CreateAlbum.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAlbum : UserControl
    {
        private string TeamId;
        private string CoverPath;

        public CreateAlbum(string teamId)
        {
            InitializeComponent();
            TeamId = teamId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "图片(*.jpg;*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            if (dialog.ShowDialog() == true)
            {
                CoverPath = dialog.FileName;
                Cover.Source = new BitmapImage(new Uri(CoverPath, UriKind.RelativeOrAbsolute));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var requestPairs = new Dictionary<string, string>();
            requestPairs.Add("targetid", TeamId);
            requestPairs.Add("target", "1");
            requestPairs.Add("albumname", Name.Text);
            requestPairs.Add("introduction", Introduction.Text);
            var res = HttpManager.Instance.UploadFile<HttpResponse>(new CreateTeamAlbumRequest(CoverPath, requestPairs));
            if (res.Ok)
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void Close_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
