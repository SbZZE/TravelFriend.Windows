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
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;

namespace TravelFriend.Windows.Team
{
    /// <summary>
    /// AlbumCard.xaml 的交互逻辑
    /// </summary>
    public partial class AlbumCard : UserControl
    {
        public AlbumCard()
        {
            InitializeComponent();
            Loaded += AlbumCard_Loaded;
        }

        private async void AlbumCard_Loaded(object sender, RoutedEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext;
            if (data is TeamAlbum album)
            {
                //请求获取
                using (MemoryStream ms = new MemoryStream())
                {
                    var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.AlbumCover}?albumid={album.AlbumId}"), ms);
                    if (ms != null && ms.Length > 0)
                    {
                        ms.Position = 0;
                        this.Dispatcher.Invoke(() =>
                        {
                            Cover.Source = ImageHelper.GetImageByStreamAsync(ms);
                        });
                    }
                }
            }
        }
    }
}
