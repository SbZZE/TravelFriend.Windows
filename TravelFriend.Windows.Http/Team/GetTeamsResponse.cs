using System;
using System.Collections.Generic;
using System.Text;
using Team = TravelFriend.Windows.Database.Model.Team;

namespace TravelFriend.Windows.Http
{
    public class GetTeamsResponse : HttpResponse
    {
        public List<Team> Teams { get; set; }
    }
}
