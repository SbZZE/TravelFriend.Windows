using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http.UserInfo
{
    public class UpdateUserInfoRequest : HttpRequest
    {
        public UpdateUserInfoRequest(string userName, string nickName, string gender, string birthday, string address, string signature) : base(ApiUtils.UserInfo)
        {
            UserName = userName;
            NickName = nickName;
            Gender = gender;
            Birthday = birthday;
            Address = address;
            Signature = signature;
        }

        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
