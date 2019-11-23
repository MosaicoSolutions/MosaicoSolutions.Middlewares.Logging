using MosaicoSolutions.Middlewares.Logging.Models;

namespace MosaicoSolutions.Middlewares.Logging.Services.Interfaces
{
    public interface IRequestResponseLogHandle
    {
        void Handle(HttpRequestResponseLog log);
    }
}