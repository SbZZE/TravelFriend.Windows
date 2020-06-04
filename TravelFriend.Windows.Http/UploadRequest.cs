using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class UploadRequest : HttpRequest
    {
        public UploadRequest(string url, string filePath) : base(url)
        {
            FilePath = filePath;
        }

        [JsonIgnore]
        public string FilePath { get; set; }
    }
}
