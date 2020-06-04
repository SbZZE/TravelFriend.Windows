using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class ApiUtils
    {
        private static string BaseUrl => "http://47.106.139.187:8081/api";

        public static string Register => BaseUrl + "/user/register";
        public static string Login => BaseUrl + "/user/login";
        public static string Avatar => BaseUrl + "/user/avatar";
        public static string UserInfo => BaseUrl + "/user/userInfo";
        public static string CreateTeam => BaseUrl + "/team/create";
    }
}
