using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Powerumc.AspNetCore.Hmac
{
    public class HmacHttpClientFactory : IHmacHttpClientFactory
    {
        private readonly HmacHttpClientFactoryOptions _options;

        public HmacHttpClientFactory(HmacHttpClientFactoryOptions options)
        {
            _options = options;
        }

        public HttpClient Create(string data)
        {
            return new HttpClient(new HmacHttpDelegationHandler(_options.SecretKey, data));
        }
    }
}