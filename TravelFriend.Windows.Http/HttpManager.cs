using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TravelFriend.Windows.Database;

namespace TravelFriend.Windows.Http
{
    public class HttpManager
    {
        private static readonly Lazy<HttpManager> _instance = new Lazy<HttpManager>(() => new HttpManager());
        public static HttpManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private HttpManager() { }

        /// <summary>
        /// GET异步请求
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        public async Task<T> GetAsync<T>(HttpRequest request) where T : HttpResponse, new()
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
        public async Task<T> PostAsync<T>(HttpRequest request) where T : HttpResponse, new()
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

        /// <summary>
        /// 异步下载
        /// </summary>
        /// <param name="request">下载请求</param>
        /// <param name="outStream">下载到内存流</param>
        /// <returns></returns>
        public async Task<HttpResponse> DownloadAsync(HttpRequest request, MemoryStream outStream)
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
                    using (Stream reader = response.GetResponseStream())
                    {
                        byte[] buffer = new byte[4096];
                        int size;
                        while ((size = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await outStream.WriteAsync(buffer, 0, size);
                        }
                    }
                    Console.WriteLine("--------------");
                    Console.WriteLine("response from " + request.Url);
                    Console.WriteLine("--------------");
                    var result = new HttpResponse() { code = 0 };
                    return result;
                }
            }
            catch (Exception e)
            {
                var error = new HttpResponse()
                {
                    code = 100,
                    message = e.Message
                };
                return error;
            }
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="request">上传请求</param>
        /// <returns></returns>
        public T UploadFile<T>(UploadRequest uploadRequest) where T : HttpResponse, new()
        {
            var client = new RestClient(uploadRequest.Url);
            client.Timeout = -1;
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("token", AccountManager.Instance.UserToken);
            restRequest.AddFile("avatar", uploadRequest.FilePath);
            IRestResponse response = client.Execute(restRequest);
            try
            {
                T result = JsonConvert.DeserializeObject<T>(response.Content);
                return result;
            }
            catch (Exception)
            {
                return new T
                {
                    code = (int)response.StatusCode,
                    message = response.ErrorMessage
                };
            }
            //try
            //{
            //    Console.WriteLine("--------------");
            //    Console.WriteLine("request - " + request.Url);
            //    string body = JsonConvert.SerializeObject(request);
            //    Console.WriteLine("request body - " + body);
            //    Console.WriteLine("--------------");
            //    HttpWebRequest http = (HttpWebRequest)WebRequest.Create(request.Url);//15ms
            //    MultipartFormDataContent multipart = new MultipartFormDataContent();
            //    multipart.Add(new StreamContent(request.FileStream), "image");
            //    http.Timeout = 600 * 1000;
            //    http.Headers.Add("token", AccountManager.Instance.UserToken);
            //    http.Method = "POST";
            //    http.ContentType = multipart.Headers.ContentType.ToString();
            //    http.ContentLength = multipart.Headers.ContentLength.Value;

            //    var stream = await http.GetRequestStreamAsync();
            //    await multipart.CopyToAsync(stream);

            //    using (WebResponse response = await http.GetResponseAsync())
            //    {
            //        string json = string.Empty;
            //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //        {
            //            json = await reader.ReadToEndAsync();
            //        }
            //        Console.WriteLine("--------------");
            //        Console.WriteLine("response from " + request.Url);
            //        Console.WriteLine(json);
            //        Console.WriteLine("--------------");
            //        T result = JsonConvert.DeserializeObject<T>(json);
            //        return result;
            //    }
            //}
            //catch (Exception e)
            //{
            //    var error = new T
            //    {
            //        code = 100,
            //        message = e.Message
            //    };
            //    return error;
            //}
        }
    }
}
