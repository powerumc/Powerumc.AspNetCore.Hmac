using System;
using Microsoft.Extensions.DependencyInjection;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    public static class IHmacHttpClientFactoryExtension
    {
        public static IServiceCollection AddHmacHttpClientFactory(this IServiceCollection serviceCollection,
            Action<HmacHttpClientFactoryOptions> optionsBuilder)
        {
            serviceCollection.AddSingleton<IHmacHttpClientFactory>(_ =>
            {
                var options = new HmacHttpClientFactoryOptions();
                optionsBuilder(options);
                return new HmacHttpClientFactory(options);
            });

            return serviceCollection;
        }
    }
}