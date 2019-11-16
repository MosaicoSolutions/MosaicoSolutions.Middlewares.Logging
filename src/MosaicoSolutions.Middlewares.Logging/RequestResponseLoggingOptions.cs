using System;

namespace MosaicoSolutions.Middlewares.Logging
{
    public class RequestResponseLoggingOptions
    {
        internal readonly string[] AcceptHandleMethodNames = new [] { "Handle", "HandleAsync" };
        public Type HandlerType { get; set; }
    }
}