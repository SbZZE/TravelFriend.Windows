using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class ApiUtils
    {
        private static string BaseUrl => "http://123.56.6.43:8081/api";

        public static string Register => BaseUrl + "/user/register";
        public static string Login => BaseUrl + "/user/login";
        public static string UserAvatar => BaseUrl + "/user/avatar";
        public static string UserInfo => BaseUrl + "/user/userInfo";
        public static string CreateTeam => BaseUrl + "/team/create";
        public static string Teams => BaseUrl + "/team/teams";
        public static string TeamAvatar => BaseUrl + "/team/avatar";
        public static string UpdateTeam => BaseUrl + "/team/teaminfo";
        public static string TeamMember => BaseUrl + "/team/member";
        /// <summary>
        /// 踢出团队成员
        /// </summary>
        public static string DeleteMember => BaseUrl + "/team/member";
        public static string CreateAlbum => BaseUrl + "/album/create";
        public static string AlbumList => BaseUrl + "/album/list";
        public static string AlbumCover => BaseUrl + "/album/cover";
        public static string GetThumbnail => BaseUrl + "/album/file/thumb";
        public static string ThumbnailList => BaseUrl + "/album/file/list";
        public static string BreakPointUpload => BaseUrl + "/breakpoint/upload";
    }
}
