using System;
using MosaicoSolutions.Middlewares.Logging.Events;

namespace MosaicoSolutions.Middlewares.Logging.Options
{
    public class RequestResponseLoggingOptions
    {
        public EventHandler<RequestResponseLoggingEventArgs> OnComplete { get; set; }
    }
}