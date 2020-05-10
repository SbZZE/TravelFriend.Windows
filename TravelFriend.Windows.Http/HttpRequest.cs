using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Http
{
    public class HttpRequest
    {
        public HttpRequest(string url)
        {
            Url = url;
        }

        [JsonIgnore]
        public string Url { get; }
    }
}
