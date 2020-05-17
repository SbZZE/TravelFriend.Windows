using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Http.UserInfo
{
    public class GetUserInfoResponse : HttpResponse
    {
        public User data { get; set; }
    }
}
