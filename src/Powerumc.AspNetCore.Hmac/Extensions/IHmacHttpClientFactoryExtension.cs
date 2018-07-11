using Microsoft.Extensions.DependencyInjection;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    public static class IHmacHttpClientFactoryExtension
    {
        public static IServiceCollection AddHmacHttpClientFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHmacHttpClientFactory, HmacHttpClientFactory>();

            return serviceCollection;
        }
    }
}