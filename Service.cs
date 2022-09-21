using ChannelEngine.Core.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChannelEngine.Core
{
    public class WebAPIService : IWebAPIService
    {
        HttpClient _httpClient = new HttpClient();

        string apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        public ApiResponse<string> GetAsync(string action, string parameters=null)
        {
            
            UriBuilder baseAddress = new UriBuilder($"https://api-dev.channelengine.net/api/v2/{action}");
            baseAddress.Query = parameters + "&apikey=" + apiKey;
            
            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(media);

            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                var request = _httpClient.GetAsync(baseAddress.Uri).Result;

                using (StreamReader sr = new StreamReader(request.Content.ReadAsStreamAsync().Result))
                {
                    var data = sr.ReadToEnd();

                    response.Code = HttpStatusCode.OK;
                    response.Message = "Ok";
                    response.Value = data;

                }

                return response;

            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = ex.Message.ToString();

                return response;

            }

        }

        public ApiResponse<string> PostAsync<T>(string action, T data)
        {
            UriBuilder baseAddress = new UriBuilder($"https://api-dev.channelengine.net/api/v2/{action}");
            baseAddress.Query = "apikey=" + apiKey;

            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(media);

            ApiResponse<string> response = new ApiResponse<string>();

            try
            {

                var myContent = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var request = _httpClient.PostAsync(baseAddress.Uri, byteContent).Result;

                using (StreamReader sr = new StreamReader(request.Content.ReadAsStreamAsync().Result))
                {
                    var responseData = sr.ReadToEnd();

                    response.Code = HttpStatusCode.OK;
                    response.Message = "Ok";
                    response.Value = responseData;

                }

                return response;

            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = ex.Message.ToString();

                return response;

            }

        }
    }
}
