using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class UploadRequest
    {
        public UploadRequest(string url, string filePath)
        {
            Url = url;
            FilePath = filePath;
        }

        [JsonIgnore]
        public string Url { get; set; }
        [JsonIgnore]
        public string FilePath { get; set; }
    }
}
