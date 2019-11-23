using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

namespace MosaicoSolutions.Middlewares.Logging.Models
{
    public class HttpRequestResponseLog
    {
        public DateTimeOffset RequestTime { get; internal set; }
        public QueryString QueryString { get; internal set; }
        public  IQueryCollection? Query { get; internal set; }
        public  string? Protocol { get; internal set; }
        public  PathString PathBase { get; internal set; }
        public  PathString Path { get; internal set; }
        public  string? Method { get; internal set; }
        public  bool IsHttps { get; internal set; }
        public IReadOnlyDictionary<string, object>? RouteValues { get; internal set; }
        public  HostString Host { get; internal set; }
        public  bool HasFormContentType { get; internal set; }
        public  IFormCollection? Form { get; internal set; }
        public  IRequestCookieCollection? RequestCookies { get; internal set; }
        public  string? RequestContentType { get; internal set; }
        public  long? RequestContentLength { get; internal set; }
        public  IReadOnlyDictionary<string, StringValues>? RequestHeaders { get; internal set; }
        public string? RequestBody { get; internal set; }
        public  string? Scheme { get; internal set; }
        public DateTimeOffset ResponseTime { get; internal set; }
        public IReadOnlyDictionary<string, StringValues>? ResponseHeaders { get; internal set; }
        public IResponseCookies? ResponseCookies { get; internal set; }
        public string? ResponseContentType { get; internal set; }
        public long? ResponseContentLength { get; internal set; }
        public string? ResponseBody { get; internal set; }
        public int StatusCode { get; internal set; }
    }
}