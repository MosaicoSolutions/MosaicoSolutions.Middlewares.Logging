using Microsoft.Extensions.DependencyInjection;
using MosaicoSolutions.Middlewares.Logging.Services.Interfaces;

namespace MosaicoSolutions.Middlewares.Logging.Services
{
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestResponseLogHandleScoped<T>(this IServiceCollection services) where T: class, IRequestResponseLogHandle
        {
            services.AddScoped<IRequestResponseLogHandle, T>();
            return services;
        }

        public static IServiceCollection AddAsyncRequestResponseLogHandleScoped<T>(this IServiceCollection services) where T: class, IAsyncRequestResponseLogHandle
        {
            services.AddScoped<IAsyncRequestResponseLogHandle, T>();
            return services;
        }
    }
}