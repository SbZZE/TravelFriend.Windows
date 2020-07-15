using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TravelFriend.Windows.Album
{
    public class ThumbnailViewModel : BaseViewModel
    {
        private Visibility _noThumbnailVisible = Visibility.Collapsed;
        public Visibility NoThumbnailVisible
        {
            get
            {
                return _noThumbnailVisible;
            }
            set
            {
                _noThumbnailVisible = value;
                Change(nameof(NoThumbnailVisible));
            }
        }

        private Visibility _thumbnailVisible = Visibility.Collapsed;
        public Visibility ThumbnailVisible
        {
            get
            {
                return _thumbnailVisible;
            }
            set
            {
                _thumbnailVisible = value;
                Change(nameof(ThumbnailVisible));
            }
        }

        private Visibility _loadingVisible = Visibility.Collapsed;
        public Visibility LoadingVisible
        {
            get
            {
                return _loadingVisible;
            }
            set
            {
                _loadingVisible = value;
                Change(nameof(LoadingVisible));
            }
        }


        public LoadStatus LoadStatus
        {
            set
            {
                NoThumbnailVisible = value == LoadStatus.LoadFailure ? Visibility.Visible : Visibility.Collapsed;
                ThumbnailVisible = value == LoadStatus.LoadSuccess ? Visibility.Visible : Visibility.Collapsed;
                LoadingVisible = value == LoadStatus.Loading ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }

    public enum LoadStatus
    {
        Loading,
        LoadSuccess,
        LoadFailure
    }
}
