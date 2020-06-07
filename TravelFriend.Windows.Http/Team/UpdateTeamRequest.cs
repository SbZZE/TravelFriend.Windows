using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class UpdateTeamRequest : HttpRequest
    {
        public UpdateTeamRequest(string teamId, string teamName, string introduction) : base(ApiUtils.UpdateTeam)
        {
            TeamId = teamId;
            TeamName = teamName;
            Introduction = introduction;
        }

        [JsonProperty("teamid")]
        public string TeamId { get; set; }
        [JsonProperty("teamname")]
        public string TeamName { get; set; }
        [JsonProperty("introduction")]
        public string Introduction { get; set; }
    }
}
