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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Album
{
    /// <summary>
    /// Thumbnail.xaml 的交互逻辑
    /// </summary>
    public partial class Thumbnail : UserControl
    {
        public string FileId;
        private ThumbnailViewModel ThumbnailViewModel;
        public Thumbnail(string fileId)
        {
            InitializeComponent();
            ThumbnailViewModel = new ThumbnailViewModel();
            DataContext = ThumbnailViewModel;
            FileId = fileId;
            Loaded += Thumbnail_Loaded;
        }

        private async void Thumbnail_Loaded(object sender, RoutedEventArgs e)
        {
            ThumbnailViewModel.LoadStatus = LoadStatus.Loading;
            //请求获取
            using (MemoryStream ms = new MemoryStream())
            {
                await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.GetThumbnail}?fileid={FileId}&width=260&height=260"), ms);
                if (ms != null && ms.Length > 0)
                {
                    ms.Position = 0;
                    this.Dispatcher.Invoke(() =>
                    {
                        ThumbnailImage.Source = ImageHelper.GetImageByStreamAsync(ms);
                        ThumbnailViewModel.LoadStatus = LoadStatus.LoadSuccess;
                    });
                }
                else
                {
                    ThumbnailViewModel.LoadStatus = LoadStatus.LoadFailure;
                }
            }
        }

        private void ThumbnailImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Shade.Visibility = Visibility.Visible;
        }

        private void Shade_MouseLeave(object sender, MouseEventArgs e)
        {
            Shade.Visibility = Visibility.Collapsed;
        }

        private void Image_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard story = (Storyboard)this.FindResource("LoadingStoryboard");
            if (ThumbnailViewModel.LoadingVisible == Visibility.Visible)
            {
                story.Begin();
            }
            else
            {
                story.Stop();
            }
        }
    }
}
