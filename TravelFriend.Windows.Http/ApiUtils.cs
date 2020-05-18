using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class ApiUtils
    {
        private static string BaseUrl => "http://47.106.139.187:8081/api";

        public static string Register => BaseUrl + "/user/regieter";
        public static string Login => BaseUrl + "/user/login";
        public static string Avatar => BaseUrl + "/user/avatar";
        public static string UserInfo => BaseUrl + "/user/userInfo";
    }
}
