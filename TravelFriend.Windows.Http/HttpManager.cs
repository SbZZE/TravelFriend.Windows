using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelFriend.Windows.Database;

namespace TravelFriend.Windows.Http
{
    public static class HttpManager
    {
        /// <summary>
        /// GET异步请求
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        public async static Task<T> GetAsync<T>(HttpRequest request) where T : HttpResponse, new()
        {
            try
            {
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(request.Url);
                http.Method = "GET";
                http.Headers.Add("token", AccountManager.Instance.UserToken);
                http.ContentType = "application/json";
                http.Timeout = 5 * 1000;

                using (WebResponse response = await http.GetResponseAsync())
                {
                    string json = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        json = await reader.ReadToEndAsync();
                    }
                    Console.WriteLine(json);
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("--------------");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("--------------");
                var error = new T
                {
                    code = 100,
                    message = e.Message
                };
                return error;
            }
        }

        /// <summary>
        /// Post异步请求
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="request">请求</param>
        public async static Task<T> PostAsync<T>(HttpRequest request) where T : HttpResponse, new()
        {
            try
            {
                string body = JsonConvert.SerializeObject(request);
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(request.Url);
                http.Method = "POST";
                http.Headers.Add("token", AccountManager.Instance.UserToken);
                http.ContentType = "application/json";
                http.Timeout = 5 * 1000;
                byte[] data = Encoding.UTF8.GetBytes(body);
                using var stream = await http.GetRequestStreamAsync();
                await stream.WriteAsync(data, 0, data.Length);

                using (WebResponse response = await http.GetResponseAsync())
                {
                    string json = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        json = await reader.ReadToEndAsync();
                    }
                    Console.WriteLine(json);
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("--------------");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("--------------");
                var error = new T
                {
                    code = 100,
                    message = e.Message
                };
                return error;
            }
        }
    }
}
