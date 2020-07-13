using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Http.Album
{
    public class GetThumbnailListResponse : HttpResponse
    {
        public List<Thumbnail> Data { get; set; }
    }
}
