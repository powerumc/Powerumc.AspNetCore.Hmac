using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Powerumc.AspNetCore.Hmac
{
    public class HmacHttpClientFactory : IHmacHttpClientFactory
    {
        public HttpClient Create(string secretKey, string data)
        {
            return new HttpClient(new HmacHttpDelegationHandler(secretKey, data));
        }

        public HttpClient Create(IConfiguration configuration, string data)
        {
            var secretKey = configuration["SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("SecretKey configuration is not set.");

            return Create(secretKey, data);
        }
    }
}