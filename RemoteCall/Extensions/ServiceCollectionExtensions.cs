using CommonLibraries.RemoteCall.Services;
using CommonLibraries.RemoteCall.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Hermes.RemoteCall.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRemoteCall(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IRemoteCallHelperService, RemoteCallHelperService>();
        }
    }
}
