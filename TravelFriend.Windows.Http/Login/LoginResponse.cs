using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class LoginResponse : HttpResponse
    {
        public string token { get; set; }
    }
}
