using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace MosaicoSolutions.Middlewares.Logging.Models
{
    public class HttpRequestResponseLog
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public IHeaderDictionary RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public DateTimeOffset RequestTime { get; set; }
        public IHeaderDictionary ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
        public DateTimeOffset ResponseTime { get; set; }
    }
}