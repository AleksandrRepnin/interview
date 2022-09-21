using ChannelEngine.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Core
{
    public interface IWebAPIService
    {
        ApiResponse<string> GetAsync(string action, string option = null);
        ApiResponse<string> PostAsync<T>(string action, T data);
    }

    public class ApiResponse<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        
        public Object Content { get; }
        public T Value { get; set; }
    }
}
