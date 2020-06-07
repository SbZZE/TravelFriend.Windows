using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Http
{
    public class GetTeamAlbumResponse : HttpResponse
    {
        public List<TeamAlbum> Albums { get; set; }
    }
}
