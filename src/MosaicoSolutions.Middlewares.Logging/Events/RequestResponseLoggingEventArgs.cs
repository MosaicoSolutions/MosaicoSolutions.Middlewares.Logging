using System;
using MosaicoSolutions.Middlewares.Logging.Models;

namespace MosaicoSolutions.Middlewares.Logging.Events
{
    public class RequestResponseLoggingEventArgs : EventArgs
    {
        public HttpRequestResponseLog RequestResponseLog { get; set; }
    }
}