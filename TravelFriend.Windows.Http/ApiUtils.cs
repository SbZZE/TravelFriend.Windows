﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class ApiUtils
    {
        private static string baseUrl = "47.106.139.187";
        public static string BaseUrl
        {
            get
            {
                return $"http://{baseUrl}:8081/api";
            }
            set
            {
                baseUrl = string.IsNullOrEmpty(value) ? "47.106.139.187" : value;
            }
        }

        public static string Register => BaseUrl + "/user/register";
        public static string Login => BaseUrl + "/user/login";
        public static string UserAvatar => BaseUrl + "/user/avatar";
        public static string UserInfo => BaseUrl + "/user/userInfo";
        public static string CreateTeam => BaseUrl + "/team/create";
        public static string Teams => BaseUrl + "/team/teams";
        public static string TeamAvatar => BaseUrl + "/team/avatar";
        public static string UpdateTeam => BaseUrl + "/team/teaminfo";
        public static string TeamMember => BaseUrl + "/team/member";
        public static string CreateAlbum => BaseUrl + "/album/create";
        public static string AlbumList => BaseUrl + "/album/list";
        public static string AlbumCover => BaseUrl + "/album/cover";
        public static string BreakPointUpload => BaseUrl + "/breakpoint/upload";
    }
}
