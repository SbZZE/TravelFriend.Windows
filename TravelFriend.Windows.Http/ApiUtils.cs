using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class ApiUtils
    {
        private static string BaseUrl => "https://localhost:5001/api";

        public static string Register => BaseUrl + "/user/regieter";
        public static string Login => BaseUrl + "/user/login";
    }
}
