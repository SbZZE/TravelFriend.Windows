using Newtonsoft.Json;

namespace TravelFriend.Windows.Http
{
    public class LoginRequest : HttpRequest
    {
        public LoginRequest(string userName, string password) : base($"{ApiUtils.Login}?username={userName}&password={password}")
        {

        }
    }
}
