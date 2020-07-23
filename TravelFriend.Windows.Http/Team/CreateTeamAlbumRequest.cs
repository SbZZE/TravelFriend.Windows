﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class CreateTeamAlbumRequest : UploadRequest
    {
        /// <summary>
        /// 创建团队相册
        /// </summary>
        /// <param name="albumName">相册名称</param>
        /// <param name="cover">相册封面文件路径</param>
        public CreateTeamAlbumRequest(string cover, Dictionary<string, string> pairs) : base(ApiUtils.CreateAlbum, cover, "cover", pairs)
        {
        }
    }
}
