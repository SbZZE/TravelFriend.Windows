using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http.Team
{
    public class CreateTeamRequest : HttpRequest
    {
        public CreateTeamRequest(string userName, string name, string introduction) : base(ApiUtils.CreateTeam)
        {
            UserName = userName;
            Name = name;
            Introduction = introduction;
        }

        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("introduction")]
        public string Introduction { get; set; }
    }
}
