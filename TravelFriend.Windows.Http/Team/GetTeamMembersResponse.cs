using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Http
{
    public class GetTeamMembersResponse : HttpResponse
    {
        public List<TeamMember> Members { get; set; }
    }
}
