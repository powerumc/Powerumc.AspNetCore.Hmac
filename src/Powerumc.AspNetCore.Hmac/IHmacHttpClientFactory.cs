using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Powerumc.AspNetCore.Hmac
{
    public interface IHmacHttpClientFactory
    {
        HttpClient Create(string secretKey, string data);
        HttpClient Create(IConfiguration configuration, string data);
    }
}