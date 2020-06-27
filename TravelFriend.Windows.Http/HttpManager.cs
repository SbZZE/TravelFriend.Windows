using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Http.Album;
using TravelFriend.Windows.Http.BreakPoint;

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
            restRequest.AddJsonBody(JsonConvert.SerializeObject(uploadRequest));
            restRequest.AddFile(uploadRequest.FileKey, uploadRequest.FilePath);
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
        }

        /// <summary>
        /// 断点上传文件
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="request">上传请求</param>
        /// <returns></returns>
        public async Task<T> BreakPointUpload<T>(UploadAlbumFileRequest request, byte[] fileChunk) where T : HttpResponse, new()
        {
            try
            {
                string body = JsonConvert.SerializeObject(request);
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(request.Url);
                http.Method = "POST";
                http.Headers.Add("token", AccountManager.Instance.UserToken);
                // 组装文件上传表单
                MultipartFormDataContent multipart = new MultipartFormDataContent
                {
                    { new StringContent(body) },
                    // 文件
                    { new ByteArrayContent(fileChunk), "filechunk"}
                };
                //http.Timeout = 5 * 1000;
                //byte[] data = Encoding.UTF8.GetBytes(body);
                //这里必须用表单的ContentType，附带boundary
                http.ContentType = multipart.Headers.ContentType.ToString();
                http.ContentLength = multipart.Headers.ContentLength.Value;
                var stream = await http.GetRequestStreamAsync();
                await multipart.CopyToAsync(stream);
                stream.Close();
                //await stream.WriteAsync(data, 0, data.Length);

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

            //var client = new RestClient(request.Url);
            //client.Timeout = -1;
            //var restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("token", AccountManager.Instance.UserToken);
            //restRequest.AddParameter("targetid", request.TargetId);
            //restRequest.AddParameter("albumid", request.AlbumId);
            //restRequest.AddParameter("albumtype", 1);
            //restRequest.AddParameter("filename", request.FileName);
            //restRequest.AddParameter("filetype", 1);
            //restRequest.AddParameter("identifier", request.Identifier);
            //restRequest.AddParameter("totalsize", request.TotalSize);
            //restRequest.AddParameter("totalchunks", request.TotalChunks);
            //restRequest.AddParameter("chunknumber", request.ChunkNumber);
            //restRequest.AddParameter("chunksize", request.ChunkSize);
            //restRequest.AddParameter("currentchunksize", request.CurrentChunkSize);
            ////restRequest.AddJsonBody(JsonConvert.SerializeObject(request));
            ////restRequest.AddFile("filechunk", fileChunk, request.FileName);
            //IRestResponse response = client.Execute(restRequest);
            //try
            //{
            //    T result = JsonConvert.DeserializeObject<T>(response.Content);
            //    return result;
            //}
            //catch (Exception)
            //{
            //    return new T
            //    {
            //        code = (int)response.StatusCode,
            //        message = response.ErrorMessage
            //    };
            //}

        }
    }
}
