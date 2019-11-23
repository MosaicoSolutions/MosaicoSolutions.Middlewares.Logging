using System.Threading.Tasks;
using MosaicoSolutions.Middlewares.Logging.Models;

namespace MosaicoSolutions.Middlewares.Logging.Services.Interfaces
{
    public interface IAsyncRequestResponseLogHandle
    {
         Task HandleAsync(HttpRequestResponseLog log);
    }
}