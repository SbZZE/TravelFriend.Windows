using Newtonsoft.Json;

namespace TravelFriend.Windows.Http
{
    public class LoginRequest : HttpRequest
    {
        public LoginRequest(string userName, string password) : base(ApiUtils.Login)
        {
            UserName = userName;
            Password = password;
        }

        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
