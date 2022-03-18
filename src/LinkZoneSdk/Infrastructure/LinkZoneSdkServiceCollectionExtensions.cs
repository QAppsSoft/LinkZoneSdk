using Microsoft.Extensions.DependencyInjection;

namespace LinkZoneSdk.Infrastructure
{
    public static class LinkZoneSdkServiceCollectionExtensions
    {
        public static IServiceCollection AddLinkZoneSdk(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<ISdk, Sdk>();

            return services;
        }
    }
}
