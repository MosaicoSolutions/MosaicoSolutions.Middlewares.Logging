using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IO;
using MosaicoSolutions.Middlewares.Logging.Models;

namespace MosaicoSolutions.Middlewares.Logging
{
    // https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
    // https://gist.github.com/elanderson/c50b2107de8ee2ed856353dfed9168a2
    // https://stackoverflow.com/a/52328142/3563013
    // https://stackoverflow.com/a/43404745/3563013
    // https://gist.github.com/elanderson/c50b2107de8ee2ed856353dfed9168a2#gistcomment-2319007
    public class RequestResponseLoggingMiddleware
    {
        private const int ReadChunkBufferLength = 4096;
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly RequestResponseLoggingOptions? _options;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IOptions<RequestResponseLoggingOptions> options)
        {
            _next = next;
            _options = options.Value;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            var httpRequestLog = new HttpRequestResponseLog
            {
                RequestTime = DateTimeOffset.UtcNow,
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port,
                Path = request.Path.ToString(),
                QueryString = request.QueryString.ToString(),
                Url = request.GetDisplayUrl(),
                Method = request.Method,
                RequestHeaders = request.Headers,
                RequestBody = await GetRequestBody(request)
            };

            var originalBodyStream = context.Response.Body;

            using var newResponseBody = _recyclableMemoryStreamManager.GetStream();

            context.Response.Body = newResponseBody;

            await _next(context);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalBodyStream);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            httpRequestLog.ResponseBody = ReadStreamInChunks(newResponseBody);
            httpRequestLog.ResponseTime = DateTimeOffset.UtcNow;
            httpRequestLog.ResponseHeaders = context.Response.Headers;
            
            OnComplete(httpRequestLog);
        }

        public async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            
            using var requestStream = _recyclableMemoryStreamManager.GetStream();

            await request.Body.CopyToAsync(requestStream);
            request.Body.Seek(0, SeekOrigin.Begin);
            return ReadStreamInChunks(requestStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            string result;

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            
            var readChunk = new char[ReadChunkBufferLength];
            int readChunkLength;
            //do while: is useful for the last iteration in case readChunkLength < chunkLength
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, ReadChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            result = textWriter.ToString();

            return result;
        }

        private void OnComplete(HttpRequestResponseLog httpRequestLog)
        {
            if (_options is null || _options.HandlerType is null) return;

            var handlerTypeInstance = Activator.CreateInstance(_options.HandlerType);
            var handleMethod = _options.HandlerType
                                       .GetMethods()
                                       .Where(m => m.GetParameters().Any(p => p.ParameterType == typeof(HttpRequestResponseLog)))
                                       .FirstOrDefault();

            var task = (Task?)handleMethod?.Invoke(handlerTypeInstance, new [] { httpRequestLog });
            task?.Wait();
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
            => builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}