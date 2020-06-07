using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class UploadRequest : HttpRequest
    {
        public UploadRequest(string url, string filePath, string fileKey) : base(url)
        {
            FileKey = fileKey;
            FilePath = filePath;
        }

        [JsonIgnore]
        public string FileKey { get; set; }
        [JsonIgnore]
        public string FilePath { get; set; }
    }
}
