using System;
using System.Threading.Tasks;
using MosaicoSolutions.Middlewares.Logging.Models;

namespace WebApi
{
    public class RequestResponseLoggingHandle
    {
        public Task HandleAsync(HttpRequestResponseLog log)
            => Task.Run(() => 
            {
                Console.WriteLine("Request Recieved!");
                Console.WriteLine($"{nameof(log.RequestTime)}[{log.RequestTime}]");
                Console.WriteLine($"RequestTime.LocalDateTime[{log.RequestTime.LocalDateTime}]");
                Console.WriteLine($"{nameof(log.Scheme)}[{log.Scheme}]");
                Console.WriteLine($"{nameof(log.Host)}[{log.Host}]");
                Console.WriteLine($"{nameof(log.Path)}[{log.Path}]");
                Console.WriteLine($"{nameof(log.QueryString)}[{log.QueryString}]");
                Console.WriteLine($"{nameof(log.ResponseTime)}[{log.ResponseTime}]");
                Console.WriteLine($"ResponseTime.LocalDateTime[{log.ResponseTime.LocalDateTime}]");
            });
    }
}